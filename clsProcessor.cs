using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using MacGen;
using System;
using System.Windows.Forms;
using BTP;
using System.Drawing;

/// <summary> 
/// Processes the cnc file and loads the graphics records. 
/// </summary> 
/// <remarks> 
/// Copyright © MacGen Programming 2006 
/// Jason Titcomb 
/// www.CncEdit.com 
/// </remarks> 
class clsProcessor
{
    private int[] mColors16 = { -16777216, -8388608, -16744448, -8355840, -16777088, -8388480, -16744320, -4144960, -8355712, -65536,
    -16711936, -256, -16776961, -12525360, -65281, -1 };

    private Regex mRegSubs;
    private Regex mRegWords;

    // private string mCodefile;    //the input file 
    private Motion mPlane;
    private float mDrillClear;
    private System.Drawing.Color mCurrentColor;
    private System.Collections.Specialized.StringCollection mSubFiles = new System.Collections.Specialized.StringCollection();

    private int CurPos;
    private float mInitialZBeforeDrill;
    private string mEndmain;    //M30 
    private string mSubcall;    //M98 
    private string mSubRepeats;    //L 
                                   //  private int mSubFileNumber;
    private string mCommentMatch;
    private float mSang;
    private float mRad;
    private float mEang;
    private float mYcentr;
    private float mXcentr;
    private float mZcentr;
    private float mJ;
    private float mI;
    private float mK;
    private float mYpos;
    private float mXpos;
    private float mZpos;
    private float mPrevY;
    private float mPrevX;
    private float mPrevZ;
    private float mPrevABC;
    private float mABC;
    private int mRotDir;
    private bool mRotating;
    private float mRpoint;
    private Motion mDrillReturnMode;    //G98,G99 
    private float mArcRad;
    private float mFeed;
    private float mSpeed;
    private float mTool = 1;
    private float mPrevTool;
    private Motion mMode;
    private bool mAbsolute;
    private string mCodeText;
    private int mTotalLines;
    private int mTotalBites;
    private clsMotion mMotion = new clsMotion();
    private Address mCurAddress;
    private bool mNewProfile;
    private clsMotionRecord mGrfxRec;
    private System.Collections.Generic.List<clsMotionRecord> mGfxRecs;
    public clsMachine mCurMachine;
    private const float ONE_RADIAN = (float)(Math.PI * 2);
    private int jumpString;
    private float OldZ;

    public enum letters
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        ANY
    }
    private bool[] mBlockAddresses = new bool[27];
    public event OnAddBlockEventHandler OnAddBlock;
    public delegate void OnAddBlockEventHandler(int value, int max);
    //  public event OnToolChangedEventHandler OnToolChanged;
    public delegate void OnToolChangedEventHandler(float tool);

    #region "Singleton"
    private static clsProcessor mInstance;
    //PRIVATE constructor can only be called from this class 
    private clsProcessor()
    {
    }
    /// <summary> 
    /// Static method for creating the single instance of the Constructor 
    /// </summary> 
    public static clsProcessor Instance()
    {
        // initialize if not already done 
        if (mInstance == null)
        {
            mInstance = new clsProcessor();
        }
        // return the initialized instance of the Singleton Class 
        return mInstance;
    }
    //Instance 
    #endregion

    private class clsProg
    {
        public bool Main;
        public string Label;
        public int Value;
        public int Index;
        public string Contents;
        public int TimesCalled = 0;
    }

    private System.Collections.Generic.List<clsProg> mNcProgs = new System.Collections.Generic.List<clsProg>();

    private void Arc_Center()
    {
        float side_opposite = 0;
        float meanX = 0;
        float meanY = 0;
        float centerVector = 0;
        float quarterArc = 0;

        switch (mPlane)
        {
            case Motion.XY_PLN:
                //This is for an arc or helix that uses an R insdead of I,J,K, 

                //Radius move with R 
                if (mBlockAddresses[(int)letters.R] & (mMode > Motion.LINE))
                {
                    quarterArc = (float)(Math.PI / 2);
                    //|------- Calculate arc center position -------| 

                    //A "-R" is used to specify big arc 
                    if (mArcRad < 0)
                    {
                        quarterArc = -quarterArc; //Total angle > 180 deg 
                    }
                    //Total angle is always <180 if "+R" 

                    //this is a full arc 
                    if (mPrevX == mXpos & mPrevY == mYpos)
                    {
                        quarterArc = 0;
                    }

                    mRad = System.Math.Abs(mArcRad); //calculate side opposite 'hypotenuse 
                    side_opposite = (float)System.Math.Abs((Math.Pow(mRad, 2)) - (Math.Pow((GcodeViewer.VectorLength(mPrevX, mPrevY, 0, mXpos, mYpos, 0) / 2), 2)));
                    side_opposite = (float)System.Math.Sqrt(side_opposite);
                    //find mid point of start and end of arc start and end points 
                    meanX = (mPrevX + mXpos) / 2;
                    meanY = (mPrevY + mYpos) / 2;

                    if (mMode == Motion.CCARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mXpos - mPrevX, mYpos - mPrevY, false) - quarterArc;
                        if (centerVector < 0)
                        {
                            centerVector = ONE_RADIAN + centerVector;
                        }
                    }


                    if (mMode == Motion.CWARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mXpos - mPrevX, mYpos - mPrevY, false) + quarterArc;
                        if (centerVector > ONE_RADIAN)
                        {
                            centerVector = centerVector - ONE_RADIAN;
                        }
                    }


                    mXcentr = (float)(meanX - (side_opposite * System.Math.Cos(centerVector)));
                    mYcentr = (float)(meanY - (side_opposite * System.Math.Sin(centerVector)));

                    //Calculate start and end angle 
                    mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
                }
                else
                {

                    if (mCurMachine.AbsArcCenter)
                    {
                        mI = mI - mPrevX;
                        mJ = mJ - mPrevY;
                    }

                    mRad = (float)System.Math.Sqrt(Math.Pow(mI, 2) + Math.Pow(mJ, 2));
                    //calculate rad 
                    mXcentr = mPrevX + mI;
                    //Arc origins 
                    mYcentr = mPrevY + mJ;
                    mZcentr = mPrevZ + mK;
                    mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
                }


                break;
            //InStr(codeline, "R") 

            case Motion.XZ_PLN:
                //This is for an arc or helix that uses an R insdead of I,J,K, 

                //Radius move with R 
                if (mBlockAddresses[(int)letters.R] & mMode > Motion.LINE)
                {
                    quarterArc = (float)(Math.PI / 2);
                    //|------- Calculate arc center position -------| 

                    //A "-R" is used to specify big arc 
                    if (mArcRad < 0)
                    {
                        quarterArc = -quarterArc;
                        //Total angle > 180 deg 
                    }
                    //Total angle is always <180 if "+R" 

                    //this is a full arc 
                    if (mPrevX == mXpos & mPrevZ == mZpos)
                    {
                        quarterArc = 0;
                    }

                    mRad = System.Math.Abs(mArcRad);
                    //calculate side opposite 'hypotenuse 
                    side_opposite = (float)System.Math.Abs((Math.Pow(mRad, 2)) - (Math.Pow((GcodeViewer.VectorLength(mPrevX, mPrevZ, 0, mXpos, mZpos, 0) / 2), 2)));
                    side_opposite = (float)System.Math.Sqrt(side_opposite);
                    //find mid point of start and end of arc start and end points 
                    meanX = (mPrevX + mXpos) / 2;
                    meanY = (mPrevZ + mZpos) / 2;

                    if (mMode == Motion.CCARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mXpos - mPrevX, mZpos - mPrevZ, false) - quarterArc;
                        if (centerVector < 0)
                        {
                            centerVector = ONE_RADIAN + centerVector;
                        }
                    }


                    if (mMode == Motion.CWARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mXpos - mPrevX, mZpos - mPrevZ, false) + quarterArc;
                        if (centerVector > ONE_RADIAN)
                        {
                            centerVector = centerVector - ONE_RADIAN;
                        }
                    }


                    mXcentr = (float)(meanX - (side_opposite * System.Math.Cos(centerVector)));
                    mZcentr = (float)(meanY - (side_opposite * System.Math.Sin(centerVector)));

                    //Calculate start and end angle 
                    mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevZ - mZcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mZpos - mZcentr, false);
                }
                else
                {

                    if (mCurMachine.AbsArcCenter)
                    {
                        mI = mI - mPrevX;
                        mK = mK - mPrevZ;
                    }

                    mRad = (float)System.Math.Sqrt(Math.Pow(mI, 2) + Math.Pow(mK, 2));
                    //calculate rad 
                    mXcentr = mPrevX + mI;
                    mYcentr = mPrevY + mJ;
                    //Arc origins 
                    mZcentr = mPrevZ + mK;

                    mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevZ - mZcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mZpos - mZcentr, false);
                }


                break;
            //InStr(codeline, "R") 

            case Motion.YZ_PLN:
                //This is for an arc or helix that uses an R insdead of I,J,K, 

                //Radius move with R 
                if (mBlockAddresses[(int)letters.R] & mMode > Motion.LINE)
                {
                    quarterArc = (float)(Math.PI / 2);
                    //|------- Calculate arc center position -------| 

                    //A "-R" is used to specify big arc 
                    if (mArcRad < 0)
                    {
                        quarterArc = -quarterArc;
                        //Total angle > 180 deg 
                    }
                    //Total angle is always <180 if "+R" 

                    //this is a full arc 
                    if (mPrevY == mYpos & mPrevZ == mZpos)
                    {
                        quarterArc = 0;
                    }

                    mRad = System.Math.Abs(mArcRad);
                    //calculate side opposite 'hypotenuse 
                    side_opposite = (float)System.Math.Abs((Math.Pow(mRad, 2)) - (Math.Pow((GcodeViewer.VectorLength(mPrevY, mPrevZ, 0, mYpos, mZpos, 0) / 2), 2)));
                    side_opposite = (float)System.Math.Sqrt(side_opposite);
                    //find mid point of start and end of arc start and end points 
                    meanX = (mPrevY + mYpos) / 2;
                    meanY = (mPrevZ + mZpos) / 2;

                    if (mMode == Motion.CCARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mYpos - mPrevY, mZpos - mPrevZ, false) - quarterArc;
                        if (centerVector < 0)
                        {
                            centerVector = ONE_RADIAN + centerVector;
                        }
                    }


                    if (mMode == Motion.CWARC)
                    {
                        centerVector = GcodeViewer.AngleFromPoint(mYpos - mPrevY, mZpos - mPrevZ, false) + quarterArc;
                        if (centerVector > ONE_RADIAN)
                        {
                            centerVector = centerVector - ONE_RADIAN;
                        }
                    }


                    mYcentr = (float)(meanX - (side_opposite * System.Math.Cos(centerVector)));
                    mZcentr = (float)(meanY - (side_opposite * System.Math.Sin(centerVector)));

                    //Calculate start and end angle 
                    mSang = GcodeViewer.AngleFromPoint(mPrevY - mYcentr, mPrevZ - mZcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mYpos - mYcentr, mZpos - mZcentr, false);
                }
                else
                {

                    if (mCurMachine.AbsArcCenter)
                    {
                        mJ = mJ - mPrevY;
                        mK = mK - mPrevZ;
                    }

                    mRad = (float)System.Math.Sqrt(Math.Pow(mJ, 2) + Math.Pow(mK, 2));
                    //calculate rad 
                    mXcentr = mPrevX + mI;
                    mYcentr = mPrevY + mJ;
                    //Arc origins 
                    mZcentr = mPrevZ + mK;

                    mSang = GcodeViewer.AngleFromPoint(mPrevY - mYcentr, mPrevZ - mZcentr, false);
                    mEang = GcodeViewer.AngleFromPoint(mYpos - mYcentr, mZpos - mZcentr, false);
                }


                break;
                //InStr(codeline, "R") 

        }

    }

    private void AddMotionRecord()
    {
        mGrfxRec = new clsMotionRecord();
        {

            mGrfxRec.BeginProfile = mNewProfile;
            mGrfxRec.WrkPlane = (int)mPlane;
            mGrfxRec.Tool = mTool;
            mGrfxRec.DrawClr = mCurrentColor;
            mGrfxRec.Yold = -mPrevY; // + (float)(ConnectionData.GlobalY); // mCurMachine.ViewShift[1];
            mGrfxRec.Ycentr = -mYcentr;// + (float)(ConnectionData.GlobalY);//mCurMachine.ViewShift[1];
            mGrfxRec.Ypos = -mYpos;// + (float)(ConnectionData.GlobalY); // mCurMachine.ViewShift[1];

            mGrfxRec.Codestring = mCodeText;

            //Console.WriteLine("Code Text = {0}", mCodeText);
            mGrfxRec.MotionType = mMode;
            //Console.WriteLine("Mode Motion = {0}", mMode);
            mGrfxRec.Rpoint = mRpoint + mCurMachine.ViewShift[2];
            mGrfxRec.DrillClear = -mDrillClear;
            mGrfxRec.Xold = -mPrevX;// + (float)(ConnectionData.GlobalX); // mCurMachine.ViewShift[0];
            mGrfxRec.Xpos = -mXpos;// + (float)(ConnectionData.GlobalX); //mCurMachine.ViewShift[0];
                                   //Console.WriteLine("Xpos = {0}", mGrfxRec.Xpos);
            mGrfxRec.Rad = -mRad;
            mGrfxRec.Xcentr = -mXcentr;// + (float)(ConnectionData.GlobalX); // mCurMachine.ViewShift[0];

            mGrfxRec.Zold = mPrevZ + mCurMachine.ViewShift[2];
            mGrfxRec.Zpos = mZpos + mCurMachine.ViewShift[2];
            mGrfxRec.Zcentr = mZcentr + mCurMachine.ViewShift[2];

            mGrfxRec.Rotate = mRotating;
            mGrfxRec.NewRotaryPos = mABC;
            mGrfxRec.OldRotaryPos = mPrevABC;
            mGrfxRec.RotaryDir = mRotDir;
            // * mcurmachine.nRotaryDir 

            mGrfxRec.Speed = mSpeed;
            mGrfxRec.Feed = mFeed;
            mGrfxRec.Sang = mSang;
            mGrfxRec.Eang = mEang;
            if (jumpString != 1)
            {
                mGfxRecs.Add(mGrfxRec);
            }
            jumpString = 0;
        }
    }


    void AddCircleRecord(double CircleDiameter, double XcenterPos, double YcenterPos, MacGen.Motion MotionType)
    {
        mGrfxRec = new clsMotionRecord();
        {
            mGrfxRec.BeginProfile = true;
            mGrfxRec.WrkPlane = (int)Motion.XY_PLN;
            mGrfxRec.Tool = 0;
            mGrfxRec.DrawClr = Color.Black;//Color16(13);
            mGrfxRec.Yold = (float)(YcenterPos);
            mGrfxRec.Ycentr = (float)(YcenterPos);
            mGrfxRec.Ypos = (float)(YcenterPos);
            mGrfxRec.Codestring = "";
            mGrfxRec.MotionType = MotionType; // Motion.CWARC;
            mGrfxRec.Rpoint = 0;
            mGrfxRec.DrillClear = 0;
            mGrfxRec.Xold = (float)(XcenterPos);
            mGrfxRec.Xpos = (float)(XcenterPos + CircleDiameter);
            mGrfxRec.Xcentr = (float)(XcenterPos + CircleDiameter / 2);
            //Console.WriteLine("Xpos = {0}", mGrfxRec.Xpos);
            mGrfxRec.Rad = (float)(CircleDiameter / 2);//(float)System.Math.Sqrt(Math.Pow(3, 2) + Math.Pow(0, 2)); ;

            mGrfxRec.Zold = 0;
            mGrfxRec.Zpos = 0;
            mGrfxRec.Zcentr = 0;

            mGrfxRec.Rotate = false;
            mGrfxRec.NewRotaryPos = 0;
            mGrfxRec.OldRotaryPos = 0;
            mGrfxRec.RotaryDir = 1;
            // * mcurmachine.nRotaryDir 
            mGrfxRec.Speed = 10;
            mGrfxRec.Feed = 10;
            mGrfxRec.Sang = GcodeViewer.AngleFromPoint((float)(-CircleDiameter / 2), 0, false);
            mGrfxRec.Eang = GcodeViewer.AngleFromPoint((float)(CircleDiameter / 2), 0, false);
            mGfxRecs.Add(mGrfxRec);
            //mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
            // mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
        }
    }

    /*   void AddCircleRecord1(double CircleDiameter, double XcenterPos, double YcenterPos)
       {
           mGrfxRec = new clsMotionRecord();
           {
               mGrfxRec.BeginProfile = true;
               mGrfxRec.WrkPlane = (int)Motion.XY_PLN;
               mGrfxRec.Tool = 0;
               mGrfxRec.DrawClr = Color.Black;//Color16(13);
               //mGrfxRec.Yold = (float)(ConnectionData.DrawDishY);
               mGrfxRec.Yold = (float)YcenterPos;
               //mGrfxRec.Ycentr = (float)(ConnectionData.DrawDishY);
               mGrfxRec.Ycentr = (float)YcenterPos;
               //mGrfxRec.Ypos = (float)(ConnectionData.DrawDishY);
               mGrfxRec.Ypos = (float)YcenterPos;
               mGrfxRec.Codestring = "";
               mGrfxRec.MotionType = Motion.CCARC;
               mGrfxRec.Rpoint = 0;
               mGrfxRec.DrillClear = 0;
               //mGrfxRec.Xold = (float)(ConnectionData.DrawDishX);
               mGrfxRec.Xold = (float)XcenterPos;
               //mGrfxRec.Xpos = (float)(ConnectionData.DrawDishX + ConnectionData.PetriDishDiam);
               //mGrfxRec.Xpos = (float)(ConnectionData.DrawDishX + CircleDiameter);
               mGrfxRec.Xpos = (float)(XcenterPos + CircleDiameter);
               //mGrfxRec.Xcentr = (float)(ConnectionData.DrawDishX + ConnectionData.PetriDishDiam / 2);
               //mGrfxRec.Xcentr = (float)(ConnectionData.DrawDishX + CircleDiameter / 2);
               mGrfxRec.Xcentr = (float)(XcenterPos + CircleDiameter / 2);
               //Console.WriteLine("Xpos = {0}", mGrfxRec.Xpos);

               mGrfxRec.Rad = (float)(CircleDiameter / 2);//(float)System.Math.Sqrt(Math.Pow(3, 2) + Math.Pow(0, 2)); ;

               mGrfxRec.Zold = 0;
               mGrfxRec.Zpos = 0;
               mGrfxRec.Zcentr = 0;

               mGrfxRec.Rotate = false;
               mGrfxRec.NewRotaryPos = 0;
               mGrfxRec.OldRotaryPos = 0;
               mGrfxRec.RotaryDir = 1;
               // * mcurmachine.nRotaryDir 
               mGrfxRec.Speed = 10;
               mGrfxRec.Feed = 10;
               //mGrfxRec.Sang = GcodeViewer.AngleFromPoint((float)(-ConnectionData.PetriDishDiam / 2), 0, false);
               mGrfxRec.Sang = GcodeViewer.AngleFromPoint((float)(- CircleDiameter / 2), 0, false);
               //mGrfxRec.Eang = GcodeViewer.AngleFromPoint((float)(ConnectionData.PetriDishDiam / 2), 0, false);
               mGrfxRec.Eang = GcodeViewer.AngleFromPoint((float)(CircleDiameter / 2), 0, false);
               mGfxRecs.Add(mGrfxRec);
               //mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
               // mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
           }
       }
       */
    void AddLineRecord(double Xstart, double Ystart, double Xstop, double Ystop)
    {
        mGrfxRec = new clsMotionRecord();
        {
            mGrfxRec.BeginProfile = true;
            mGrfxRec.WrkPlane = (int)Motion.XY_PLN;
            mGrfxRec.Tool = 0;
            mGrfxRec.DrawClr = Color.Black;//Color16(13);
            mGrfxRec.Yold = (float)Ystart;// (ConnectionData.DrawWellY);
            mGrfxRec.Ycentr = (float)((Ystop - Ystart) / 2);// (float)(ConnectionData.DrawWellY);
            mGrfxRec.Ypos = (float)Ystop; //(ConnectionData.DrawWellY);
            mGrfxRec.Codestring = "";
            mGrfxRec.MotionType = Motion.LINE;
            mGrfxRec.Rpoint = 0;
            mGrfxRec.DrillClear = 0;
            mGrfxRec.Xold = (float)Xstart;
            mGrfxRec.Xpos = (float)Xstop;//(ConnectionData.DrawWellX + ConnectionData.PetriDishDiam);
            mGrfxRec.Xcentr = (float)((Xstop - Xstart) / 2);// (float)(ConnectionData.DrawWellX + ConnectionData.PetriDishDiam / 2);
            //Console.WriteLine("Xpos = {0}", mGrfxRec.Xpos);
            mGrfxRec.Rad = (float)0;// (ConnectionData.PetriDishDiam / 2);//(float)System.Math.Sqrt(Math.Pow(3, 2) + Math.Pow(0, 2)); ;

            mGrfxRec.Zold = 0;
            mGrfxRec.Zpos = 0;
            mGrfxRec.Zcentr = 0;

            mGrfxRec.Rotate = false;
            mGrfxRec.NewRotaryPos = 0;
            mGrfxRec.OldRotaryPos = 0;
            mGrfxRec.RotaryDir = 1;
            // * mcurmachine.nRotaryDir 
            mGrfxRec.Speed = 10;
            mGrfxRec.Feed = 10;
            mGrfxRec.Sang = GcodeViewer.AngleFromPoint((float)Xstart - (float)((Xstop - Xstart) / 2), (float)Ystart - (float)((Ystop - Ystart) / 2), false);
            mGrfxRec.Eang = GcodeViewer.AngleFromPoint((float)Xstop - (float)((Xstop - Xstart) / 2), (float)Ystop - (float)((Ystop - Ystart) / 2), false);
            mGfxRecs.Add(mGrfxRec);
            //mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
            // mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
        }
    }

    void AddZeroRecord(double StartX, double StartY)
    {
        mGrfxRec = new clsMotionRecord();
        {
            mGrfxRec.BeginProfile = true;
            mGrfxRec.WrkPlane = (int)Motion.XY_PLN;
            mGrfxRec.Tool = 0;
            mGrfxRec.DrawClr = Color.Transparent;
            mGrfxRec.Yold = (float)StartX; // (ConnectionData.DrawDishY);
            mGrfxRec.Ycentr = 0;
            mGrfxRec.Ypos = 0;
            mGrfxRec.Codestring = "";
            mGrfxRec.MotionType = Motion.RAPID;
            mGrfxRec.Rpoint = 0;
            mGrfxRec.DrillClear = 0;
            mGrfxRec.Xold = (float)StartY;// (ConnectionData.DrawDishX - ConnectionData.PetriDishDiam / 2);
            mGrfxRec.Xpos = 0;
            mGrfxRec.Xcentr = 0;
            //Console.WriteLine("Xpos = {0}", mGrfxRec.Xpos);
            mGrfxRec.Rad = 0;// (float)(ConnectionData.PetriDishDiam / 2);//(float)System.Math.Sqrt(Math.Pow(3, 2) + Math.Pow(0, 2)); ;

            mGrfxRec.Zold = 0;
            mGrfxRec.Zpos = 0;
            mGrfxRec.Zcentr = 0;

            mGrfxRec.Rotate = false;
            mGrfxRec.NewRotaryPos = 0;
            mGrfxRec.OldRotaryPos = 0;
            mGrfxRec.RotaryDir = 1;
            // * mcurmachine.nRotaryDir 
            mGrfxRec.Speed = 10;
            mGrfxRec.Feed = 10;
            mGrfxRec.Sang = 0;// GcodeViewer.AngleFromPoint(0, (float)(ConnectionData.PetriDishDiam / 2), false);
            mGrfxRec.Eang = 0; // GcodeViewer.AngleFromPoint(0, (float)(-ConnectionData.PetriDishDiam / 2), false);
            mGfxRecs.Add(mGrfxRec);
            //mSang = GcodeViewer.AngleFromPoint(mPrevX - mXcentr, mPrevY - mYcentr, false);
            // mEang = GcodeViewer.AngleFromPoint(mXpos - mXcentr, mYpos - mYcentr, false);
        }
    }

    private float FormatAxis(string sVal, int precision)
    {
        //decimal place 
        //if (sVal.Contains("."))
        //{
        //    return float.Parse(sVal);
        //}
        //else
        {
            return (float)(float.Parse(sVal) * (Math.Pow(10, -precision)));//convert a number from a 4 place 
        }
    }

    public void ProcessFile(List<clsMotionRecord> gfxRecs, List<string> list)
    {
        mGfxRecs = gfxRecs;
        //mCodefile = ncFile;
        {
            if (jumpString != 1)
            {
                mMotion.SubCall.Label = mCurMachine.Subcall[0];
                mMotion.SubCall.Value = int.Parse(mCurMachine.Subcall.Substring(1));

                mMotion.SubReturn.Label = mCurMachine.SubReturn[0];
                mMotion.SubReturn.Value = int.Parse(mCurMachine.SubReturn.Substring(1));

                mMotion.Abs.Label = mCurMachine.Absolute[0];
                mMotion.Abs.Value = int.Parse(mCurMachine.Absolute.Substring(1));
                mMotion.CCArc.Label = mCurMachine.CCArc[0];
                mMotion.CCArc.Value = int.Parse(mCurMachine.CCArc.Substring(1));
                mMotion.CWArc.Label = mCurMachine.CWArc[0];
                mMotion.CWArc.Value = int.Parse(mCurMachine.CWArc.Substring(1));

                mMotion.Inc.Label = mCurMachine.Incremental[0];
                mMotion.Inc.Value = int.Parse(mCurMachine.Incremental.Substring(1));

                mMotion.Linear.Label = mCurMachine.Linear[0];
                mMotion.Linear.Value = int.Parse(mCurMachine.Linear.Substring(1));

                mMotion.Extrusion.Label = mCurMachine.Extrusion[0];
                mMotion.Extrusion.Value = int.Parse(mCurMachine.Extrusion.Substring(1));

                mMotion.RapidDose.Label = mCurMachine.RapidDose[0];
                mMotion.RapidDose.Value = int.Parse(mCurMachine.RapidDose.Substring(1));

                mMotion.Suction.Label = mCurMachine.Suction[0];
                mMotion.Suction.Value = int.Parse(mCurMachine.Suction.Substring(1));

                mMotion.Spheroidus.Label = mCurMachine.Spheroidus[0];
                mMotion.Spheroidus.Value = int.Parse(mCurMachine.Spheroidus.Substring(1));

                mMotion.Rapid.Label = mCurMachine.Rapid[0];
                mMotion.Rapid.Value = int.Parse(mCurMachine.Rapid.Substring(1));

                mMotion.Rotary.Label = mCurMachine.Rotary[0];
                mMotion.Rotary.Value = 0;

                mMotion.DrillRapid.Label = mCurMachine.DrillRapid[0];
                mMotion.DrillRapid.Value = 0;

                mMotion.Dwell.Label = mCurMachine.Dwell[0];
                mMotion.Dwell.Value = int.Parse(mCurMachine.Dwell.Substring(1));

                mMotion.Plane[0].Label = mCurMachine.XYplane[0];
                mMotion.Plane[0].Value = int.Parse(mCurMachine.XYplane.Substring(1));
                mMotion.Plane[1].Label = mCurMachine.XZplane[0];
                mMotion.Plane[1].Value = int.Parse(mCurMachine.XZplane.Substring(1));
                mMotion.Plane[2].Label = mCurMachine.YZplane[0];
                mMotion.Plane[2].Value = int.Parse(mCurMachine.YZplane.Substring(1));

                mMotion.ReturnLevel[0].Label = mCurMachine.ReturnLevel[0][0];
                mMotion.ReturnLevel[0].Value = int.Parse(mCurMachine.ReturnLevel[0].Substring(1));
                mMotion.ReturnLevel[1].Label = mCurMachine.ReturnLevel[1][0];
                mMotion.ReturnLevel[1].Value = int.Parse(mCurMachine.ReturnLevel[1].Substring(1));

                for (int r = 0; r <= mMotion.Drills.Length - 1; r++)
                {
                    if (mCurMachine.Drills[r].Length > 2)
                    {
                        mMotion.Drills[r].Label = mCurMachine.Drills[r][0];
                        mMotion.Drills[r].Value = int.Parse(mCurMachine.Drills[r].Substring(1));
                    }
                }
            }


            //Reset all positions. 
            mGfxRecs.Clear();
            mCurrentColor = Color.White;
            mPrevTool = -1;
            mXpos = 0;
            mYpos = 0;
            mZpos = 0;
            mPrevX = 0;
            mPrevY = 0;
            mPrevZ = 0;
            mPrevABC = 0;
            mABC = 0;
            mRpoint = 0;
            mSpeed = 0;
            mFeed = 0;
            mDrillClear = 0;
            mInitialZBeforeDrill = 0;
            mRotDir = 1;
            mAbsolute = true;
            mMode = Motion.RAPID;
            mDrillReturnMode = Motion.I_PLN;

            if (mCurMachine.MachineType == MachineType.MILL)
            {
                mPlane = Motion.XY_PLN; //Mill 
            }
            else
            {
                mPlane = Motion.XZ_PLN;//Lathe 
            }

            mEndmain = mCurMachine.Endmain.Trim();
            mSubcall = mCurMachine.Subcall.Trim();
            mSubRepeats = mCurMachine.SubRepeats.Trim();

            string sFileContents = null;

            //using (System.IO.StreamReader MyReader = new System.IO.StreamReader(ncFile))
            //List<string> list = new List<string>();
            foreach (string s in list)
            {
                //sFileContents = FilterJunk(MyReader.ReadToEnd());
                sFileContents += FilterJunk(s);
                sFileContents += "\n";
            }
            mNcProgs.Clear();
            int lastIndex = -1;
            int thisIndex = -1;
            clsProg p = default(clsProg);
            foreach (Match m in this.mRegSubs.Matches(sFileContents))
            {
                if (mCurMachine.ProgramId.Contains(m.Value[0].ToString()))
                {
                    thisIndex = m.Index;
                    //Each program 
                    if (lastIndex > -1)
                    {
                        mNcProgs[mNcProgs.Count - 1].Contents = sFileContents.Substring(lastIndex, thisIndex - lastIndex).TrimEnd();
                        if (mNcProgs[mNcProgs.Count - 1].Contents.Contains(mCurMachine.Endmain))
                        {
                            mNcProgs[mNcProgs.Count - 1].Main = true;
                        }
                    }
                    p = new clsProg();
                    p.Main = false;
                    p.Index = thisIndex;
                    p.Label = Char.ToUpper(m.Value[0]).ToString();
                    p.Value = int.Parse(m.Groups[1].Value);
                    mNcProgs.Add(p);
                    lastIndex = m.Index;
                }
            }

            mTotalLines = 1;
            if (mNcProgs.Count == 0)
            {
                //Just add all the text we found in the file 
                p = new clsProg();
                p.Main = true;
                p.Index = 0;
                p.Label = "MAIN";
                p.Value = 0;
                p.Contents = sFileContents;
                mNcProgs.Add(p);
                mTotalBites = sFileContents.Length;
                ProcessSubWords(p);
            }
            else
            {
                mNcProgs[mNcProgs.Count - 1].Contents = sFileContents.Substring(lastIndex).TrimEnd();
                if (mNcProgs[mNcProgs.Count - 1].Contents.Contains(mCurMachine.Endmain))
                {
                    mNcProgs[mNcProgs.Count - 1].Main = true;
                }
                foreach (clsProg pr in mNcProgs)
                {
                    mTotalBites = pr.Contents.Length;
                    ProcessSubWords(pr);
                }
            }
        }
        jumpString = 0;
    }

    private clsProg FindSubByValue(int val)
    {
        foreach (clsProg p in mNcProgs)
        {
            if (p.Value == val) return p;
        }
        return null;
    }

    private void DrawLayer()
    {
        if (ConnectionData.DrawMode == 0)
        {
            AddZeroRecord(0, 0);
        }
        else if (ConnectionData.DrawMode == 10) // Чашка Петри
        {
            AddCircleRecord(ConnectionData.PetriDishDiam, ConnectionData.DrawDishX, ConnectionData.DrawDishY, Motion.CWARC);
            AddCircleRecord(ConnectionData.PetriDishDiam, ConnectionData.DrawDishX, ConnectionData.DrawDishY, Motion.CCARC);
            AddZeroRecord(ConnectionData.DrawDishX - ConnectionData.PetriDishDiam / 2, ConnectionData.DrawDishY);
        }
       /* else if (ConnectionData.DrawMode == 20) // Колодцы
        {
            double XCoord;
            double YCoord;
            if (ConnectionData.WellNum == 6)
            {
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.47);
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.47, ConnectionData.DrawWellX + 127.75, ConnectionData.DrawWellY + 85.47);
                AddLineRecord(ConnectionData.DrawWellX + 127.75, ConnectionData.DrawWellY + 85.47, ConnectionData.DrawWellX + 127.75, ConnectionData.DrawWellY + 0);
                AddLineRecord(ConnectionData.DrawWellX + 127.75, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0);

                XCoord = 24.76 - 35.43 / 2;
                YCoord = 23.16;
                for (int r = 0; r <= 2; r++)
                {
                    YCoord = 23.16;
                    for (int t = 0; t <= 1; t++)
                    {
                        AddCircleRecord(35.43, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CWARC);
                        AddCircleRecord(35.43, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CCARC);
                        YCoord += 39.12;
                    }
                    XCoord += 39.12;
                }
                AddZeroRecord(ConnectionData.DrawWellX + XCoord + 35.43, ConnectionData.DrawWellY + YCoord);
            }
            else if (ConnectionData.WellNum == 12)
            {
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.6);
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.6, ConnectionData.DrawWellX + 127.91, ConnectionData.DrawWellY + 85.6);
                AddLineRecord(ConnectionData.DrawWellX + 127.91, ConnectionData.DrawWellY + 85.6, ConnectionData.DrawWellX + 127.91, ConnectionData.DrawWellY + 0);
                AddLineRecord(ConnectionData.DrawWellX + 127.91, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0);

                XCoord = 24.94 - 22.11 / 2;
                YCoord = 16.79;
                for (int r = 0; r <= 3; r++)
                {
                    YCoord = 16.79;
                    for (int t = 0; t <= 2; t++)
                    {
                        AddCircleRecord(22.11, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CWARC);
                        AddCircleRecord(22.11, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CCARC);
                        YCoord += 26.01;
                    }
                    XCoord += 26.01;
                }
                AddZeroRecord(ConnectionData.DrawWellX + XCoord + 22.11, ConnectionData.DrawWellY + YCoord);
            }
            else if (ConnectionData.WellNum == 24)
            {
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.47);
                AddLineRecord(ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 85.47, ConnectionData.DrawWellX + 131.48, ConnectionData.DrawWellY + 85.47);
                AddLineRecord(ConnectionData.DrawWellX + 131.48, ConnectionData.DrawWellY + 85.47, ConnectionData.DrawWellX + 131.48, ConnectionData.DrawWellY + 0);
                AddLineRecord(ConnectionData.DrawWellX + 131.48, ConnectionData.DrawWellY + 0, ConnectionData.DrawWellX + 0, ConnectionData.DrawWellY + 0);

                XCoord = 17.48 - 16.26 / 2;
                YCoord = 13.77;
                for (int r = 0; r <= 5; r++)
                {
                    YCoord = 13.77;
                    for (int t = 0; t <= 3; t++)
                    {
                        AddCircleRecord(16.26, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CWARC);
                        AddCircleRecord(16.26, ConnectionData.DrawWellX + XCoord, ConnectionData.DrawWellY + YCoord, Motion.CCARC);
                        YCoord += 19.304;
                    }
                    XCoord += 19.304;
                }
                AddZeroRecord(ConnectionData.DrawWellX + XCoord + 16.26, ConnectionData.DrawWellY + YCoord);
            }
        }*/
    }

    private void ProcessSubWords(clsProg p)
    {
        DrawLayer();

        mTool = 1;
        OldZ = 0;

        p.TimesCalled += 1;
        int lastIndex = 0;
        foreach (Match ncWord in this.mRegWords.Matches(p.Contents))
        {
            //Each word 
            //Is this a newline 
            if (ncWord.Value == "\n")
            {
                CurPos = 0;
                g89Code = 0;
                mTotalLines += 1;
                mCodeText = p.Contents.Substring(lastIndex, ncWord.Index - lastIndex);
                CreateGcodeBlock();
                if (RollBack == 1)
                {
                    mZpos = NewBlock;
                    mMode = Motion.CCARC;
                    //mCurrentColor = 3;
                    RollBack = 0;
                    CreateGcodeBlock();
                }
                if (OnAddBlock != null)
                {
                    OnAddBlock(ncWord.Index, mTotalBites);
                }
                Array.Clear(mBlockAddresses, 0, 26);
                lastIndex = ncWord.Index + 1;
            }
            else if (MatchIsComment(ncWord))
            {
                //Comment 
                mTotalLines += ncWord.Value.Split('\n').Length - 1;
            }
            else if (mCurMachine.BlockSkip.Contains(ncWord.Value[0].ToString()))
            {
                //Blockskip. 
                mTotalLines += 1;
            }
            else
            {
                try
                {
                    //Word 
                    mCurAddress.Label = Char.ToUpper(ncWord.Value[0]);
                    mCurAddress.StringValue = ncWord.Groups[1].Value;
                    Console.WriteLine(ncWord.Groups[1].Value);
                    mCurAddress.Value = float.Parse(ncWord.Groups[1].Value);
                    if (mCurAddress.Matches(mMotion.SubCall))
                    {
                        //M98 P. Use the next word value as the sub name 
                        clsProg retProg = FindSubByValue(int.Parse(ncWord.NextMatch().Groups[1].Value));
                        if ((retProg != null))
                        {
                            if (retProg.TimesCalled > 100) return;//Prevent infinate loop 
                            ProcessSubWords(retProg);//Call this subagain 
                        }
                    }
                    else
                    {
                        EvaluateWord();
                    }
                }
                catch
                {

                }
               
            }
        }
    }

    private void CreateGcodeBlock()
    {
        if (!mBlockAddresses[(int)letters.ANY]) return;

        if (mBlockAddresses[(int)letters.X])
        {
            if (mAbsolute == false)
            {
                mXpos = mXpos + mPrevX;
            }
            if (mCurMachine.MachineType == MachineType.LATHEDIA)
            {
                mXpos = mXpos / 2;
            }
        }
        if (mBlockAddresses[(int)letters.Y])
        {
            if (mAbsolute == false) mYpos = mYpos + mPrevY;
        }
        if (mBlockAddresses[(int)letters.Z])
        {
            if (mAbsolute == false) mZpos = mZpos + mPrevZ;
        }

        if (mBlockAddresses[(int)mMotion.Rotary.Letter] == true)
        {
            mRotating = true;
            //0>360 sign determines dir 
            if (mCurMachine.RotaryType == RotaryMotionType.BMC)
            {
                if (mAbsolute == false)
                {
                    mABC = mABC + mPrevABC;
                }
            }
            //like CAD 
            else
            {
                if (mAbsolute == false)
                {
                    mRotDir = System.Math.Sign(mABC);
                    mABC = mABC + mPrevABC;
                }
                else
                {
                    //In a scale that runs from zero to 360 
                    //we determine the direction based on the shortest distance. 
                    if (Math.Abs(mABC % ONE_RADIAN) > Math.PI & Math.Abs(mPrevABC % ONE_RADIAN) < Math.PI)
                    {
                        mPrevABC += ONE_RADIAN;
                    }
                    else if (Math.Abs(mABC % ONE_RADIAN) < Math.PI & Math.Abs(mPrevABC % ONE_RADIAN) > Math.PI)
                    {
                        mPrevABC -= ONE_RADIAN;
                    }

                    if (mABC < mPrevABC)
                    {
                        mRotDir = -1;
                    }
                    else
                    {
                        mRotDir = 1;
                    }
                }
            }
        }


        //Arc clockwise------------------- 
        if (mMode == Motion.CWARC)
        {
            Arc_Center();
            //Calculate arc center 
            if (mSang <= mEang)
            {
                mSang = mSang + ONE_RADIAN;
            }

            //re-calculate zpos if helix using k for pitch 
            if (mK > 0 & mPlane == Motion.XY_PLN)
            {
                if (mSang == mEang)
                {
                    mZpos = mZpos + mK;
                }
                else
                {
                    mZpos = mZpos + (mK * (System.Math.Abs(mSang - mEang)) / ONE_RADIAN);
                }
            }
        }


        //Arc anti-clockwise-------------- 
        if (mMode == Motion.CCARC)
        {
            Arc_Center();
            //Calculate arc center 
            if (mEang <= mSang)
            {
                mEang = mEang + ONE_RADIAN;
            }
            //re-calculate zpos if helix using k for pitch 
            if (mK > 0 & mPlane == Motion.XY_PLN)
            {
                if (mSang == mEang)
                {
                    mZpos = mZpos + mK;
                }
                else
                {
                    mZpos = mZpos + (mK * (System.Math.Abs(mSang - mEang)) / ONE_RADIAN);
                }
            }
        }

        /* if (mPrevTool != mTool)
         {
             mNewProfile = true;
             if (mCurrentColor == 0)
             {
                 mCurrentColor = 9;
             }
             mCurrentColor = Convert.ToInt16(mTool);
             if (mCurrentColor > 15)
             {
                 mCurrentColor = 1;
             }
             if (OnToolChanged != null)
             {
                 OnToolChanged(mTool);
             }
         }
         */
        if ((static_CreateGcodeBlock_mPrevMode == Motion.RAPID & mMode > Motion.RAPID) | static_CreateGcodeBlock_mPrevMode > Motion.RAPID & mMode == Motion.RAPID)
        {
            mNewProfile = true;
        }

        if (mMode > Motion.RAPID)
        {
            mInitialZBeforeDrill = mZpos;
        }

        //Create the graphics record here 
        AddMotionRecord();


        //Reset some values 
        static_CreateGcodeBlock_mPrevMode = Motion.RAPID;
        mPrevTool = mTool;
        mRotating = false;
        mPrevABC = mABC;
        //Lval = 0 
        mI = 0;
        mJ = 0;
        mK = 0;

        //Stores last position 
        mPrevX = mXpos;
        mPrevY = mYpos;
        mPrevZ = mZpos;
    }
    static MacGen.Motion static_CreateGcodeBlock_mPrevMode = Motion.RAPID;

    int jump;
    int g05Code_up;
    int g05Code_Color;
    int g04Code;
    int g89Code;
    int g88Code_Color;
    float NewBlock;
    int RollBack;

    private void EvaluateWord()
    {
        int r = 0;

        mBlockAddresses[(int)mCurAddress.Letter] = true;
        switch (mCurAddress.Letter)
        {
            case letters.P:
                if ((g04Code == 0) && (g89Code == 0))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    if (CurPos == 0)
                    {
                        mXpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                    }

                    if (CurPos == 1)
                    {
                        mYpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                    }

                    if (CurPos == 2)
                    {
                        mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                        if (OldZ != mZpos)
                        {
                            mTool = mTool + 1;
                            //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                            OldZ = mZpos;
                        }
                    }

                    if (CurPos == 3)
                    {
                        mFeed = mCurAddress.Value;
                    }

                    if (CurPos == 4)
                    {
                        if (g05Code_up == 1)
                        {
                            NewBlock = mZpos + FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                            RollBack = 1;
                            //mZpos = mZpos + FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                            g05Code_up = 0;
                        }
                        //    Console.WriteLine("NewPos = {0}", mZpos);
                    }

                    if (CurPos == 5)
                    {
                        if (g88Code_Color == 1)
                        {
                            if (mCurAddress.Value == 1)
                            {
                                mCurrentColor = Color.Magenta;
                            }
                            else if (mCurAddress.Value == 2)
                            {
                                mCurrentColor = Color.Teal;
                            }
                            else if (mCurAddress.Value == 3)
                            {
                                mCurrentColor = Color.Violet;
                            }
                            else if (mCurAddress.Value == 4)
                            {
                                mCurrentColor = Color.SkyBlue;
                            }
                            else if (mCurAddress.Value == 5)
                            {
                                mCurrentColor = Color.Plum;
                            }
                            else
                            {
                                mCurrentColor = Color.Gray;
                            }
                            g88Code_Color = 0;
                        }
                    }

                    if (CurPos == 6)
                    {
                        if (g05Code_Color == 1)
                        {
                            if (mCurAddress.Value == 1)
                            {
                                mCurrentColor = Color.Magenta;
                            }
                            else if (mCurAddress.Value == 2)
                            {
                                mCurrentColor = Color.Teal;
                            }
                            else if (mCurAddress.Value == 3)
                            {
                                mCurrentColor = Color.SeaGreen;
                            }
                            else if (mCurAddress.Value == 4)
                            {
                                mCurrentColor = Color.SkyBlue;
                            }
                            else if (mCurAddress.Value == 5)
                            {
                                mCurrentColor = Color.Plum;
                            }
                            else
                            {
                                mCurrentColor = Color.Gray;
                            }
                            g05Code_Color = 0;
                        }
                    }

                    if (CurPos == 7)
                    {
                        if (mCurAddress.Value == 1)
                        {
                            jump = 1;
                            mMode = Motion.LINE;
                        }
                        else if (mCurAddress.Value == 2)
                        {
                            jump = 2;
                            mMode = Motion.CWARC;
                        }
                        else if (mCurAddress.Value == 3)
                        {
                            jump = 3;
                            mMode = Motion.CCARC;
                        }
                        else
                        {
                            jump = 1;
                            mMode = Motion.LINE;
                        }
                    }

                    if (CurPos == 8)
                    {
                        mBlockAddresses[(int)letters.ANY] = true;
                        mI = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                    }

                    if (CurPos == 9)
                    {
                        mBlockAddresses[(int)letters.ANY] = true;
                        mJ = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                    }
                    CurPos += 1;

                    //Console.WriteLine("Jump = {0}", jump);
                    //Console.WriteLine("CurPos = {0}", CurPos);
                }
                g04Code = 0;
                break;
            // Motion X Coordinate
            case letters.X:
                mBlockAddresses[(int)letters.ANY] = true;
                mXpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                break;
            // Motion Y Coordinate
            case letters.Y:
                mBlockAddresses[(int)letters.ANY] = true;
                mYpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                break;
            // Motion Z Coordinate
            case letters.Z:

                mBlockAddresses[(int)letters.ANY] = true;
                mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                if (OldZ != mZpos)
                {
                    //mTool = mTool + 1;
                    //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                    OldZ = mZpos;
                }
                break;

            case letters.U:
                mBlockAddresses[(int)letters.ANY] = true;
                NewBlock = mZpos + FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                RollBack = 1;
                //mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                //if (OldZ != mZpos)
                //{
                //    mTool = mTool + 1;
                //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                //    OldZ = mZpos;
                //}
                break;

            case letters.V:
                mBlockAddresses[(int)letters.ANY] = true;
                //mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                //if (OldZ != mZpos)
                //{
                //    mTool = mTool + 1;
                //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                //    OldZ = mZpos;
                //}
                break;

            case letters.W:
                // mCurrentColor = Color.SkyBlue;
                mBlockAddresses[(int)letters.ANY] = true;
                // mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                //if (OldZ != mZpos)
                //{
                //   mTool = mTool + 1;
                //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                //   OldZ = mZpos;
                //}
                break;

            case letters.D:
              /*  if (mCurAddress.StringValue == "1")
                {
                    mCurrentColor = Color.Magenta;
                }
                else if ((mCurAddress.StringValue == "2"))
                {
                    mCurrentColor = Color.Teal;
                }
                else if ((mCurAddress.StringValue == "3"))
                {
                    mCurrentColor = Color.SeaGreen;
                }
                else if ((mCurAddress.StringValue == "4"))
                {
                    mCurrentColor = Color.SkyBlue;
                }
                else if ((mCurAddress.StringValue == "5"))
                {
                    mCurrentColor = Color.Plum;
                }
                else
                {
                    mCurrentColor = Color.Gray;
                }*/
                break;
            case letters.L:
                mBlockAddresses[(int)letters.ANY] = true;
                break;
            case letters.M:
                mBlockAddresses[(int)letters.ANY] = true;
                break;
            case letters.A:
                //mCurrentColor = Color.Plum;
                mBlockAddresses[(int)letters.ANY] = true;
                //mZpos = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                //if (OldZ != mZpos)
                //{
                //    mTool = mTool + 1;
                //ConnectionData.MaxLayers = Convert.ToInt16(mTool);
                //    OldZ = mZpos;
                //}
                break;
            case letters.I:
                mBlockAddresses[(int)letters.ANY] = true;
                mI = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                if (mCurMachine.MachineType == MachineType.LATHEDIA)
                {
                    if (mCurMachine.AbsArcCenter)
                    {
                        mI = mI / 2;
                    }
                }
                break;
            case letters.J:
                mBlockAddresses[(int)letters.ANY] = true;
                mJ = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                break;
            case letters.K:
                mBlockAddresses[(int)letters.ANY] = true;
                mK = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                break;
            case letters.R:
                mBlockAddresses[(int)letters.ANY] = true;
                mRpoint = FormatAxis(mCurAddress.StringValue, mCurMachine.Precision);
                mArcRad = mRpoint;
                break;
            case letters.S:
                mBlockAddresses[(int)letters.ANY] = true;
                //mSpeed = mCurAddress.Value;
                break;
            case letters.F:
                mBlockAddresses[(int)letters.ANY] = true;
                mFeed = mCurAddress.Value;
                break;
            case letters.T:
                mBlockAddresses[(int)letters.ANY] = true;
                //mTool = mCurAddress.Value;
                if (mCurAddress.Value == 1)
                {
                   // jump = 1;
                    mMode = Motion.LINE;
                }
                else if (mCurAddress.Value == 2)
                {
                    //jump = 2;
                    mMode = Motion.CWARC;
                }
                else if (mCurAddress.Value == 3)
                {
                    //jump = 3;
                    mMode = Motion.CCARC;
                }
                else
                {
                    //jump = 1;
                    mMode = Motion.LINE;
                }
                break;
            default:
                if (mBlockAddresses[(int)mMotion.Rotary.Letter] == true)
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mABC = FormatAxis(mCurAddress.StringValue, mCurMachine.RotPrecision) * 0.01745329f;
                    //Convert to radians 
                    //check for -0 
                    if (mCurAddress.StringValue.StartsWith("-"))
                    {
                        mRotDir = -1;
                        //CCW 
                    }
                    break;
                }

                //Absolute positioning 
                if (mCurAddress.Matches(mMotion.Abs))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mAbsolute = true;
                }

                //Incremental positioning 
                else if (mCurAddress.Matches(mMotion.Inc))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mAbsolute = false;
                }

                else if (mCurAddress.Matches(mMotion.Rapid))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mMode = Motion.RAPID;
                    mCurrentColor = Color.Magenta;
                }

                else if (mCurAddress.Matches(mMotion.Linear))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mMode = Motion.LINE;
                    mCurrentColor = Color.Teal;
                }

                else if (mCurAddress.Matches(mMotion.Spheroidus))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mMode = Motion.RAPID;
                    mCurrentColor = Color.SeaGreen;
                    g05Code_up = 1;
                    g05Code_Color = 1;
                }

                else if (mCurAddress.Matches(mMotion.Dwell))
                {
                    //mBlockAddresses[(int)letters.ANY] = true;
                    jumpString = 1;
                    g04Code = 1;
                }

                else if (mCurAddress.Matches(mMotion.Extrusion))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mCurrentColor = Color.CadetBlue;
                    // if (jump == 2)
                    // {
                    //     mMode = Motion.CWARC;
                    // mCurrentColor = 4;
                    // }
                    // else if (jump == 3)
                    // {
                    //     mMode = Motion.CCARC;
                    // mCurrentColor = 6;
                    // }
                    // else
                    // {
                    //     mMode = Motion.LINE;
                    //mCurrentColor = 7;
                    // }
                     g88Code_Color = 1;
                    //Console.WriteLine("Mode = {0}", mMode);
                    //{
                    //    if (FormatAxis(mCurAddress.StringValue, mCurMachine.Precision) == 1)
                    //    {
                    //        mMode = Motion.LINE;
                    //        mTool = 4;
                    //    }
                    //    else if (FormatAxis(mCurAddress.StringValue, mCurMachine.Precision) == 2)
                    //    {
                    //        mMode = Motion.CWARC;
                    //        mTool = 4;
                    //    }
                    //    else if (FormatAxis(mCurAddress.StringValue, mCurMachine.Precision) == 3)
                    //    {
                    //        mMode = Motion.CCARC;
                    //        mTool = 4;
                    //    }
                    // }
                }

                else if (mCurAddress.Matches(mMotion.RapidDose))
                {
                    mMode = Motion.RAPID;
                    mCurrentColor = Color.SkyBlue;
                }

                else if (mCurAddress.Matches(mMotion.Suction))
                {
                    //jumpString = 1;
                    g89Code = 1;
                    mMode = Motion.CWARC;
                    //mCurrentColor = Color.Gold;
                }
                //Arc clockwise 

                else if (mCurAddress.Matches(mMotion.CWArc))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    if (mPlane == Motion.XZ_PLN)
                    {
                        mMode = Motion.CCARC;
                    }
                    else
                    {
                        mMode = Motion.CWARC;
                    }
                }
                //Arc anti-clockwise 
                else if (mCurAddress.Matches(mMotion.CCArc))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    if (mPlane == Motion.XZ_PLN)
                    {
                        mMode = Motion.CWARC;
                    }
                    else
                    {
                        mMode = Motion.CCARC;
                    }
                }
                //Drill cancel found 
                else if (mCurAddress.Matches(mMotion.Drills[0]))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mMode = Motion.RAPID;
                    if (mDrillReturnMode == Motion.I_PLN)
                    {
                        mZpos = mInitialZBeforeDrill;
                    }
                    else
                    {
                        mZpos = mRpoint;
                    }
                }
                else if (mCurAddress.Matches(mMotion.ReturnLevel[0]))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mDrillReturnMode = Motion.I_PLN;
                    if (mMode > Motion.CCARC)
                    {
                        mMode = (Motion)((int)Motion.HOLE_I + mDrillReturnMode);
                    }
                }
                else if (mCurAddress.Matches(mMotion.ReturnLevel[1]))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mDrillReturnMode = Motion.R_PLN;
                    if (mMode > Motion.CCARC)
                    {
                        mMode = (Motion)((int)Motion.HOLE_I + mDrillReturnMode);
                    }
                }
                //Plane Change G17 
                else if (mCurAddress.Matches(mMotion.Plane[0]))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mPlane = Motion.XY_PLN;
                }
                //Plane Change G18 
                else if (mCurAddress.Matches(mMotion.Plane[1]))
                {
                    mBlockAddresses[(int)letters.ANY] = true;
                    mPlane = Motion.XZ_PLN;
                }
                //Plane Change G19 
                else if (mCurAddress.Matches(mMotion.Plane[2]))
                {
                    mPlane = Motion.YZ_PLN;
                    mBlockAddresses[(int)letters.ANY] = true;
                }
                else
                {
                    //Cycle through all 10 drilling cycles 
                    for (r = 1; r <= mMotion.Drills.Length - 1; r++)
                    {
                        //NOT an empty field 
                        if (mMotion.Drills[r].Value != 0.0f)
                        {
                            if (mCurAddress.Matches(mMotion.Drills[r]))
                            {
                                mMode = (Motion)((int)Motion.HOLE_I + mDrillReturnMode);
                                //Drill cycle found 
                                if (mMode == Motion.HOLE_I)
                                {
                                    mDrillClear = mInitialZBeforeDrill;
                                }
                                if (mMode == Motion.HOLE_R)
                                {
                                    mDrillClear = mRpoint;
                                }
                                mBlockAddresses[(int)letters.ANY] = true;
                                break; // TODO: might not be correct. Was : Exit For 
                            }
                        }
                    }
                }

                break;
        }
    }

    internal bool MatchIsComment(Match m)
    {
        return m.Groups["Comment"].Success;
    }

    public void Init(clsMachine machineSetup)
    {

        {
            mCurMachine = machineSetup;
            const string REG_NCWORDS = "[A-Z]([-+]?[0-9]*[\\.,]?[0-9]*)";

            if (machineSetup == null) return;
            string skipChars = "";
            foreach (char c in mCurMachine.BlockSkip.ToCharArray())
            {
                skipChars += Regex.Escape(c.ToString());
            }

            BuildCommentMatch();

            mRegWords = new Regex(InsertCommment() + "[" + skipChars + "][0-9]?|\\n|" + REG_NCWORDS, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //[:\$O]+([0-9]+) This will return the label and value of each program. 
            string progId = Regex.Escape(mCurMachine.ProgramId);
            mRegSubs = new Regex(InsertCommment() + "[" + progId + "]([0-9]+)", RegexOptions.Compiled);
        }
    }

    private string InsertCommment()
    {
        if (mCommentMatch.Length > 0)
        {
            return mCommentMatch + "|";
        }
        else
        {
            return "";
        }
    }

    private void BuildCommentMatch()
    {
        String STR_EOL = "\r?";
        mCommentMatch = "";
        string commentSetting = mCurMachine.Comments;
        //Legacy support 
        if (commentSetting.Contains("(*)") | commentSetting.Contains("()"))
        {
            mCommentMatch = "\\([^()]*\\)";
        }

        if (commentSetting.Contains("{}"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "{[^{}]*}";
        }

        if (commentSetting.Contains("[]"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "\\[[[]]*\\]";
        }
        if (commentSetting.Contains("<>"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "\\<[<>]*\\>";
        }

        if (commentSetting.Contains("\"\""))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "\".*\"";
        }

        //Single characters 
        if (commentSetting.Contains(";"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += ";.*" + STR_EOL;
        }
        if (commentSetting.Contains(":"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += ":.*" + STR_EOL;
        }
        if (commentSetting.Contains("'"))
        {
            if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "'.*" + STR_EOL;
        }

        if (commentSetting.Contains("%"))
        {
            // if (mCommentMatch.Length > 0) mCommentMatch += "|";
            mCommentMatch += "%.*";
        }

        if (mCommentMatch.Length > 0)
        {
            mCommentMatch = "(?<Comment>" + mCommentMatch + ")";
        }
    }

    public string FilterJunk(string sText)
    {
        return Regex.Replace(sText, "\x0A\x0A|\x0D\x0D|[\x00-\x09]|[\x0E-\x1F]|[\x7F-\xFF]", "", RegexOptions.Compiled);
    }

    private System.Drawing.Color Color16(int i)
    {
        return System.Drawing.Color.FromArgb(mColors16[i]);
    }
}