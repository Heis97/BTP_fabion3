// Decompiled with JetBrains decompiler
// Type: MacGen.MG_BasicViewer
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MacGen
{
  [DesignerGenerated]
  public class GcodeViewer : UserControl
  {
    private IContainer components;
    public static Dictionary<float, clsToolLayer> ToolLayers = new Dictionary<float, clsToolLayer>();
    public static List<GcodeViewer> Siblings = new List<GcodeViewer>();
    public static List<clsMotionRecord> MotionBlocks = new List<clsMotionRecord>();
    private const int INT_MAXHITS = 64;
    private const float ONE_RADIAN = 6.283185f;
    private const float PI_S = 3.141593f;
    private float mPixelF;
    private float mBlipSize;
    private float mSinPitch;
    private float mSinYaw;
    private float mSinRoll;
    private float mCosPitch;
    private float mCosYaw;
    private float mCosRoll;
    private float mCosRot;
    private float mSinRot;
    private bool mBackStep;
    private Motion mCurMotion;
    private Color mCurColor;
    private float mLongside;
    private PointF mLastPos;
    private clsMotionRecord mCurGfxRec;
    private int mGfxIndex;
    private float[] mExtentX;
    private float[] mExtentY;
    private List<PointF> mPoints;
    private List<clsDisplayList> mSelectionHitLists;
    private List<clsMotionRecord> mSelectionHits;
    private List<clsDisplayList> mDisplayLists;
    private List<clsDisplayList> mWcsDisplayLists;
    private bool mMouseDownAndMoving;
    private Point mMouseDownPt;
    private Point mLastPt;
    private Matrix mMtxDraw;
    private Matrix mMtxWCS;
    private Matrix mMtxFeedback;
    private Matrix mMtxGeo;
    private RectangleF mViewRect;
    private Rectangle mClientRect;
    private RectangleF mSelectionRect;
    private Rectangle mSelectionPixRect;
    private PointF mViewportCenter;
    private float mScaleToReal;
    private PointF[] mMousePtF;
    private Pen mCurPen;
    private Pen mWCSPen;
    private float[] mRapidDashStyle;
    private float[] mAxisDashStyle;
    private BufferedGraphicsContext mContext;
    private BufferedGraphics mGfxBuff;
    private Graphics mGfx;
    private static bool mDynamicViewManipulation = false;
    private static GcodeViewer.ManipMode mViewManipMode = GcodeViewer.ManipMode.NONE;
    private static float mAxisIndicatorScale = 1f;
    private static bool mDrawAxisLines = true;
    private static bool mDrawAxisIndicator = true;
    private static bool mDrawRapidLines = true;
    private static bool mDrawRapidPoints = true;
    private static Axis mArcAxis = Axis.Z;
    private static RotaryMotionType mRotaryType = RotaryMotionType.CAD;
    private static Axis mRotaryPlane = Axis.X;
    private static RotaryDirection mRotaryDirection = RotaryDirection.CW;
    private float mPitch;
    private float mRoll;
    private float mYaw;
    private float mRotary;
    private float mSegAngle;
    private static int mBreakPoint;
        private float MouseMove_Xold = 0;
        private float MouseMove_Yold = 0;
        //  [SpecialName]
                                                   //" private float \u0024 STATIC\u0024 MG_BasicViewer_MouseMove\u002420211C125D\u0024 Xold;"
                                                   //  [SpecialName]
                                                   // " private float \u0024STATIC\u0024MG_BasicViewer_MouseMove\u002420211C125D\u0024Yold;"

        [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (GcodeViewer);
      this.ResumeLayout(false);
    }

    public event GcodeViewer.AfterViewManipEventHandler AfterViewManip;

    public event GcodeViewer.OnStatusEventHandler OnStatus;

    public event GcodeViewer.OnSelectionEventHandler OnSelection;

    public event GcodeViewer.MouseLocationEventHandler MouseLocation;

    [DefaultValue(false)]
    [Category("Custom")]
    [Description("Determines if the graphics are redrawn during view manipulation.")]
    public bool DynamicViewManipulation
    {
      get => GcodeViewer.mDynamicViewManipulation;
      set => GcodeViewer.mDynamicViewManipulation = value;
    }

    [Category("Custom")]
    [Description("Sets or gets the view manipulation mode")]
    [DefaultValue(0)]
    public GcodeViewer.ManipMode ViewManipMode
    {
      get => GcodeViewer.mViewManipMode;
      set
      {
        GcodeViewer.mViewManipMode = value;
        try
        {
          foreach (GcodeViewer sibling in GcodeViewer.Siblings)
          {
            switch (GcodeViewer.mViewManipMode)
            {
              case GcodeViewer.ManipMode.NONE:
                sibling.Cursor = Cursors.Default;
                continue;
              case GcodeViewer.ManipMode.FENCE:
                sibling.Cursor = Cursors.Cross;
                continue;
              case GcodeViewer.ManipMode.PAN:
                sibling.Cursor = Cursors.SizeAll;
                continue;
              case GcodeViewer.ManipMode.ZOOM:
                sibling.Cursor = Cursors.SizeNS;
                continue;
              case GcodeViewer.ManipMode.ROTATE:
                sibling.Cursor = Cursors.SizeNESW;
                continue;
              case GcodeViewer.ManipMode.SELECTION:
                sibling.Cursor = Cursors.Hand;
                continue;
              default:
                continue;
            }
          }
        }
        finally
        {
          List<GcodeViewer>.Enumerator enumerator;
         //enumerator.Dispose();
        }
      }
    }

    [Category("Custom")]
    [DefaultValue(1f)]
    [Description("Sets or gets the scale if the axis indicator")]
    public float AxisIndicatorScale
    {
      get => GcodeViewer.mAxisIndicatorScale;
      set => GcodeViewer.mAxisIndicatorScale = value;
    }

    [Category("Custom")]
    [Description("Draw axis lines")]
    [DefaultValue(true)]
    public bool DrawAxisLines
    {
      get => GcodeViewer.mDrawAxisLines;
      set => GcodeViewer.mDrawAxisLines = value;
    }

    [DefaultValue(true)]
    [Description("Draw wcs XYZ indicator")]
    [Category("Custom")]
    public bool DrawAxisIndicator
    {
      get => GcodeViewer.mDrawAxisIndicator;
      set => GcodeViewer.mDrawAxisIndicator = value;
    }

    [DefaultValue(true)]
    [Description("Draw raid tool motion lines")]
    [Category("Custom")]
    public bool DrawRapidLines
    {
      get => GcodeViewer.mDrawRapidLines;
      set => GcodeViewer.mDrawRapidLines = value;
    }

    [DefaultValue(true)]
    [Description("Draw raid tool motion points")]
    [Category("Custom")]
    public bool DrawRapidPoints
    {
      get => GcodeViewer.mDrawRapidPoints;
      set => GcodeViewer.mDrawRapidPoints = value;
    }

    [DefaultValue(0)]
    [Category("Custom")]
    [Description("Sets or gets the plane that arcs will be drawn on")]
    public Axis ArcAxis
    {
      get => GcodeViewer.mArcAxis;
      set => GcodeViewer.mArcAxis = value;
    }

    [Description("Sets or gets the way that fourth axis motion is interpreted")]
    [DefaultValue(1)]
    [Category("Custom")]
    public RotaryMotionType RotaryType
    {
      get => GcodeViewer.mRotaryType;
      set => GcodeViewer.mRotaryType = value;
    }

    [DefaultValue(2)]
    [Category("Custom")]
    [Description("Sets or gets the plane that the fourth axis rotates on")]
    public Axis RotaryPlane
    {
      get => GcodeViewer.mRotaryPlane;
      set => GcodeViewer.mRotaryPlane = value;
    }

    [DefaultValue(1)]
    [Description("Sets or gets the direction of the fourth axis")]
    [Category("Custom")]
    public RotaryDirection RotaryDirection
    {
      get => GcodeViewer.mRotaryDirection;
      set => GcodeViewer.mRotaryDirection = value;
    }

    [DefaultValue(0)]
    [Category("Custom")]
    [Description("Sets or gets the X axis rotation")]
    public float Pitch
    {
      get => this.mPitch * 57.29578f;
      set
      {
        this.mPitch = value * ((float) Math.PI / 180f);
        this.CalcAngle();
      }
    }

    [Description("Sets or gets the Y axis rotation")]
    [Category("Custom")]
    [DefaultValue(0)]
    public float Roll
    {
      get => this.mRoll * 57.29578f;
      set
      {
        this.mRoll = value * ((float) Math.PI / 180f);
        this.CalcAngle();
      }
    }

    [Description("Sets or gets the Z axis rotation")]
    [DefaultValue(0)]
    [Category("Custom")]
    public float Yaw
    {
      get => this.mYaw * 57.29578f;
      set
      {
        this.mYaw = value * ((float) Math.PI / 180f);
        this.CalcAngle();
      }
    }

    [DefaultValue(0)]
    [Category("Custom")]
    [Description("Sets or gets the fourth axis position")]
    public float FourthAxis
    {
      set
      {
        this.mRotary = value * (float) -(int) this.RotaryDirection;
        this.CalcAngle();
      }
      get => this.mRotary;
    }

    [DefaultValue(16)]
    [Category("Custom")]
    [Description("Sets the quality of arcs. >=16 AND <=720")]
    public int ArcSegmentCount
    {
      set
      {
        if (value < 16)
          value = 16;
        if (value > 720)
          value = 720;
        this.mSegAngle = 6.283185f / (float) value;
      }
    }

    [Browsable(false)]
    public int BreakPoint
    {
      set
      {
        if (value == 0)
          GcodeViewer.mBreakPoint = GcodeViewer.MotionBlocks.Count - 1;
        else if (value > GcodeViewer.MotionBlocks.Count)
          GcodeViewer.mBreakPoint = GcodeViewer.MotionBlocks.Count - 1;
        else
          GcodeViewer.mBreakPoint = value;
      }
      get => GcodeViewer.mBreakPoint;
    }

    private void CalcAngle()
    {
      this.mCosRot = (float) Math.Cos((double) this.mRotary);
      this.mSinRot = (float) Math.Sin((double) this.mRotary);
      this.mSinYaw = (float) Math.Sin((double) this.mYaw);
      this.mCosYaw = (float) Math.Cos((double) this.mYaw);
      this.mSinRoll = (float) Math.Sin((double) this.mRoll);
      this.mCosRoll = (float) Math.Cos((double) this.mRoll);
      this.mSinPitch = (float) Math.Sin((double) this.mPitch);
      this.mCosPitch = (float) Math.Cos((double) this.mPitch);
    }

    private void MG_BasicViewer_MouseWheel(object sender, MouseEventArgs e)
    {
      if (Math.Sign(e.Delta) == -1)
        this.ZoomScene(1.1f);
      else
        this.ZoomScene(0.9f);
      this.CreateDisplayListsAndDraw();
    }

    private void MG_BasicViewer_MouseDown(object sender, MouseEventArgs e)
    {
      this.mMouseDownAndMoving = true;
      switch (this.ViewManipMode)
      {
        case GcodeViewer.ManipMode.SELECTION:
          if (this.mSelectionHits.Count > 0)
          {
            GcodeViewer.OnSelectionEventHandler onSelectionEvent = OnSelection;
            if (onSelectionEvent != null)
            {
              onSelectionEvent(this.mSelectionHits);
              break;
            }
            break;
          }
          break;
      }
      this.mLastPt.X = -1;
      this.mLastPt.Y = -1;
      this.mMouseDownPt.X = e.X;
      this.mMouseDownPt.Y = e.Y;
    }

    private void MG_BasicViewer_MouseMove(object sender, MouseEventArgs e)
    {
      Point p2 = new Point();
      p2.X = e.X;
      p2.Y = e.Y;
      this.mMousePtF[0].X = (float) this.mMouseDownPt.X;
      this.mMousePtF[0].Y = (float) this.mMouseDownPt.Y;
      this.mMousePtF[1].X = (float) e.X;
      this.mMousePtF[1].Y = (float) e.Y;
     // this.mMousePtF[2].X = this.\u0024STATIC\u0024MG_BasicViewer_MouseMove\u002420211C125D\u0024Xold;
     // this.mMousePtF[2].Y = this.\u0024STATIC\u0024MG_BasicViewer_MouseMove\u002420211C125D\u0024Yold;
      this.mMtxFeedback.TransformPoints(this.mMousePtF);
      GcodeViewer.MouseLocationEventHandler mouseLocationEvent = MouseLocation;
      if (mouseLocationEvent != null)
        mouseLocationEvent(this.mMousePtF[1].X, this.mMousePtF[1].Y);
      switch (this.ViewManipMode)
      {
        case GcodeViewer.ManipMode.FENCE:
          if (this.mMouseDownAndMoving)
          {
            if (this.mLastPt.X != -1)
              this.DrawWinMouseRect(this.mMouseDownPt, this.mLastPt);
            this.DrawWinMouseRect(this.mMouseDownPt, p2);
            break;
          }
          break;
        case GcodeViewer.ManipMode.PAN:
          if (this.mMouseDownAndMoving)
          {
            if (GcodeViewer.mDynamicViewManipulation)
            {
              this.PanScene(this.mMousePtF[1].X - this.mMousePtF[2].X, this.mMousePtF[1].Y - this.mMousePtF[2].Y);
              this.CreateDisplayListsAndDraw();
              break;
            }
            if (this.mLastPt.X != -1)
              this.DrawWinMouseLine(this.mMouseDownPt, this.mLastPt);
            this.DrawWinMouseLine(this.mMouseDownPt, p2);
            break;
          }
          break;
        case GcodeViewer.ManipMode.ZOOM:
          if (this.mMouseDownAndMoving)
          {
            this.ZoomScene(e.Y <= this.mMouseDownPt.Y ? 1f / (float) (1.0 + (double) Math.Abs((float) e.Y - this.MouseMove_Yold) / (double) this.Height) : (float) (1.0 + ((double) e.Y - (double) this.MouseMove_Yold) / (double) this.Height));
            if (GcodeViewer.mDynamicViewManipulation)
            {
              this.CreateDisplayListsAndDraw();
              break;
            }
            break;
          }
          break;
        case GcodeViewer.ManipMode.ROTATE:
          if (this.mMouseDownAndMoving)
          {
            this.Pitch += (float) Conversion.Int(-Math.Sign(this.MouseMove_Yold - (float) e.Y));
            this.Roll += (float) Conversion.Int(-Math.Sign(this.MouseMove_Xold - (float) e.X));
            if (GcodeViewer.mDynamicViewManipulation)
            {
              this.CreateDisplayListsAndDraw();
              break;
            }
            this.DrawWcsOnlyToBuffer();
            break;
          }
          break;
        case GcodeViewer.ManipMode.SELECTION:
          this.mSelectionRect.X = this.mMousePtF[1].X - (float) ((double) this.mPixelF * (double) this.mSelectionPixRect.Width / 2.0);
          this.mSelectionRect.Y = this.mMousePtF[1].Y - (float) ((double) this.mPixelF * (double) this.mSelectionPixRect.Height / 2.0);
          this.mSelectionRect.Width = this.mPixelF * (float) this.mSelectionPixRect.Width;
          this.mSelectionRect.Height = this.mPixelF * (float) this.mSelectionPixRect.Height;
          this.GetSelectionHits(this.mSelectionRect);
          this.DrawSelectionOverlay();
          break;
      }
      this.mLastPt = p2;
      this.MouseMove_Xold = (float) e.X;
      this.MouseMove_Yold = (float) e.Y;
    }

    private void MG_BasicViewer_MouseUp(object sender, MouseEventArgs e)
    {
      this.mMouseDownAndMoving = false;
      this.mLastPt.X = -1;
      this.mLastPt.Y = -1;
      if (this.mMouseDownPt.X == e.X | this.mMouseDownPt.Y == e.Y)
        return;
      switch (this.ViewManipMode)
      {
        case GcodeViewer.ManipMode.FENCE:
          if (this.mMouseDownAndMoving & this.mLastPt.X != -1)
            this.DrawWinMouseRect(this.mMouseDownPt, this.mLastPt);
          this.WindowViewport(this.mMousePtF[0].X, this.mMousePtF[0].Y, this.mMousePtF[1].X, this.mMousePtF[1].Y);
          this.CreateDisplayListsAndDraw();
          break;
        case GcodeViewer.ManipMode.PAN:
          if (GcodeViewer.mDynamicViewManipulation)
            break;
          this.PanScene(this.mMousePtF[1].X - this.mMousePtF[0].X, this.mMousePtF[1].Y - this.mMousePtF[0].Y);
          this.CreateDisplayListsAndDraw();
          break;
        case GcodeViewer.ManipMode.ZOOM:
          if (GcodeViewer.mDynamicViewManipulation)
            break;
          this.CreateDisplayListsAndDraw();
          break;
        case GcodeViewer.ManipMode.ROTATE:
          if (GcodeViewer.mDynamicViewManipulation)
            break;
          this.CreateDisplayListsAndDraw();
          break;
      }
    }

    private void DrawWinMouseRect(Point p1, Point p2)
    {
      p1 = this.PointToScreen(p1);
      p2 = this.PointToScreen(p2);
      Rectangle rectangle = new Rectangle();
      if (p1.X < p2.X)
      {
        rectangle.X = p1.X;
        rectangle.Width = p2.X - p1.X;
      }
      else
      {
        rectangle.X = p2.X;
        rectangle.Width = p1.X - p2.X;
      }
      if (p1.Y < p2.Y)
      {
        rectangle.Y = p1.Y;
        rectangle.Height = p2.Y - p1.Y;
      }
      else
      {
        rectangle.Y = p2.Y;
        rectangle.Height = p1.Y - p2.Y;
      }
      ControlPaint.DrawReversibleFrame(rectangle, Color.White, FrameStyle.Dashed);
    }

    private void DrawWinMouseLine(Point p1, Point p2)
    {
      p1 = this.PointToScreen(p1);
      p2 = this.PointToScreen(p2);
      ControlPaint.DrawReversibleLine(p1, p2, Color.White);
    }

    public void ZoomScene(float zoomFactor)
    {
      if ((double) Math.Abs(this.mViewRect.Width * zoomFactor) < 0.01 || (double) Math.Abs(this.mViewRect.Width * zoomFactor) > 1000.0)
        return;
      float num1 = this.mViewRect.Width * zoomFactor;
      float num2 = this.mViewRect.Height * zoomFactor;
      this.mViewRect.X += (float) (((double) this.mViewRect.Width - (double) num1) / 2.0);
      this.mViewRect.Y += (float) (((double) this.mViewRect.Height - (double) num2) / 2.0);
      this.mViewRect.Width = num1;
      this.mViewRect.Height = num2;
      this.SetViewMatrix();
    }

    private void PanScene(float deltaX, float deltaY)
    {
      this.mViewRect.X -= deltaX;
      this.mViewRect.Y -= deltaY;
      this.mViewportCenter.X -= deltaX;
      this.mViewportCenter.Y -= deltaY;
      this.SetViewMatrix();
    }

    public void WindowViewport(float X1, float Y1, float X2, float Y2)
    {
      if ((double) X1 > (double) X2)
      {
        float num = X2;
        X2 = X1;
        X1 = num;
      }
      if ((double) Y1 > (double) Y2)
      {
        float num = Y2;
        Y2 = Y1;
        Y1 = num;
      }
      if ((double) Math.Abs(X2 - X1) < 0.01 || (double) Math.Abs(Y2 - Y1) > 1000.0)
        return;
      this.mViewRect.X = X1;
      this.mViewRect.Y = Y1;
      this.mViewRect.Width = X2 - X1;
      this.mViewRect.Height = Y2 - Y1;
      this.AdjustAspect();
    }

    private void SetBufferContext()
    {
      if (this.mGfxBuff != null)
      {
        this.mGfxBuff.Dispose();
        this.mGfxBuff = (BufferedGraphics) null;
      }
      this.mContext = BufferedGraphicsManager.Current;
      this.mContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
      this.mGfxBuff = this.mContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
      this.mGfx = this.mGfxBuff.Graphics;
    }

    private void SetViewMatrix()
    {
      if (float.IsInfinity(this.mViewRect.Width) | float.IsInfinity(this.mViewRect.Height) || (double) this.mViewRect.Width == 0.0 | (double) this.mViewRect.Height == 0.0)
        return;
      this.mScaleToReal = (float) this.mClientRect.Width / this.mGfx.DpiX / this.mViewRect.Width;
      this.mMtxDraw.Reset();
      this.mMtxDraw.Scale(this.mScaleToReal, this.mScaleToReal);
      this.mMtxDraw.Translate(-this.mViewportCenter.X, this.mViewportCenter.Y);
      this.mMtxDraw.Translate(this.mViewRect.Width / 2f, this.mViewRect.Height / 2f);
      this.mMtxDraw.Scale(1f, -1f);
      this.mMtxWCS.Reset();
      this.mMtxWCS.Multiply(this.mMtxDraw);
      this.mMtxWCS.Scale(1f / this.mScaleToReal, 1f / this.mScaleToReal);
      this.mPixelF = 1f / this.mGfx.DpiX / this.mScaleToReal;
      this.mBlipSize = this.mPixelF * 3f;
      this.SetFeedbackMatrix();
    }

    private void AdjustAspect()
    {
      if ((double) this.mGfx.DpiX == 0.0 || float.IsInfinity(this.mViewRect.Width) | float.IsInfinity(this.mViewRect.Height))
        return;
      this.mViewportCenter.X = this.mViewRect.X + this.mViewRect.Width / 2f;
      this.mViewportCenter.Y = this.mViewRect.Y + this.mViewRect.Height / 2f;
      this.mLongside = Math.Max(this.mViewRect.Width, this.mViewRect.Height);
      this.mViewRect.Width = this.mLongside;
      this.mViewRect.Height = this.mLongside;
      float num = (float) this.mClientRect.Width / (float) this.mClientRect.Height;
      if ((double) num >= 1.0)
      {
        this.mViewRect.X = this.mViewportCenter.X - (float) ((double) this.mLongside * (double) num * 0.5);
        this.mViewRect.Width = this.mLongside * num;
        this.mViewRect.Y = this.mViewportCenter.Y - this.mLongside * 0.5f;
        this.mViewRect.Height = this.mLongside;
      }
      else
      {
        this.mViewRect.X = this.mViewportCenter.X - this.mLongside * 0.5f;
        this.mViewRect.Width = this.mLongside;
        this.mViewRect.Y = this.mViewportCenter.Y - (float) ((double) this.mLongside / (double) num * 0.5);
        this.mViewRect.Height = this.mLongside / num;
      }
      this.SetViewMatrix();
    }

    private void SetFeedbackMatrix()
    {
      this.mMtxFeedback.Reset();
      this.mMtxFeedback.Scale(this.mGfx.DpiX, this.mGfx.DpiY);
      this.mMtxFeedback.Multiply(this.mMtxDraw);
      this.mMtxFeedback.Invert();
    }

    private void DrawListsToGraphics(ref Graphics g)
    {
      if (this.mGfxBuff == null)
        return;
      this.mCurPen.Width = -1f;
      this.mWCSPen.Width = -1f;
      Graphics graphics = g;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.PageUnit = GraphicsUnit.Inch;
      graphics.ResetTransform();
      graphics.MultiplyTransform(this.mMtxWCS);
      try
      {
        foreach (clsDisplayList mWcsDisplayList in this.mWcsDisplayLists)
        {
          if (mWcsDisplayList.Rapid)
          {
            this.mWCSPen.DashStyle = DashStyle.Custom;
            this.mWCSPen.DashPattern = this.mAxisDashStyle;
          }
          else
            this.mWCSPen.DashStyle = DashStyle.Solid;
          this.mWCSPen.Color = mWcsDisplayList.Color;
          //this.mWCSPen.Color = Color.Red;
          graphics.DrawLines(this.mWCSPen, mWcsDisplayList.Points);
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
      graphics.ResetTransform();
      this.mRapidDashStyle[0] = 0.05f / this.mScaleToReal;
      this.mRapidDashStyle[1] = 0.05f / this.mScaleToReal;
      graphics.MultiplyTransform(this.mMtxDraw);
      try
      {
        foreach (clsDisplayList mDisplayList in this.mDisplayLists)
        {
          if (mDisplayList.InView)
          {
            if (mDisplayList.Rapid)
            {
              this.mCurPen.DashStyle = DashStyle.Custom;
              this.mCurPen.DashPattern = this.mRapidDashStyle;
            }
            else
              this.mCurPen.DashStyle = DashStyle.Solid;
            this.mCurPen.Color = mDisplayList.Color;
            this.LineFixUp(ref mDisplayList.Points);
            graphics.DrawLines(this.mCurPen, mDisplayList.Points);
          }
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
    }

    private void LineFixUp(ref PointF[] pts)
    {
      if (pts.Length != 2 || Math.Sqrt(Math.Pow((double) pts[0].X - (double) pts[1].X, 2.0) + Math.Pow((double) pts[0].Y - (double) pts[1].Y, 2.0)) <= (double) this.mViewRect.Width)
        return;
      if ((double) Math.Abs(pts[0].X - pts[1].X) < 0.001)
      {
        pts[0].X = (float) (((double) pts[0].X + (double) pts[1].X) / 2.0);
        pts[1].X = pts[0].X;
      }
      else
      {
        if ((double) Math.Abs(pts[0].Y - pts[1].Y) >= 0.001)
          return;
        pts[0].Y = (float) (((double) pts[0].Y + (double) pts[1].Y) / 2.0);
        pts[1].Y = pts[0].Y;
      }
    }

    private void DrawWcsOnlyToBuffer()
    {
      if (this.mGfxBuff == null)
        return;
      this.CreateWcs();
      Graphics mGfx = this.mGfx;
      mGfx.Clear(this.BackColor);
      mGfx.PageUnit = GraphicsUnit.Inch;
      mGfx.ResetTransform();
      mGfx.MultiplyTransform(this.mMtxWCS);
      mGfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
      try
      {
        foreach (clsDisplayList mWcsDisplayList in this.mWcsDisplayLists)
        {
          if (mWcsDisplayList.Rapid)
          {
            this.mWCSPen.DashStyle = DashStyle.Custom;
            this.mWCSPen.DashPattern = this.mAxisDashStyle;
          }
          else
            this.mWCSPen.DashStyle = DashStyle.Solid;
          this.mWCSPen.Color = mWcsDisplayList.Color;
                    mGfx.DrawLines(this.mWCSPen, mWcsDisplayList.Points);
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
      this.mGfxBuff.Render();
    }

    private void DrawSelectionOverlay()
    {
      if (this.mGfxBuff != null)
        this.mGfxBuff.Render();
      this.mCurPen.Width = (float) (1.0 / (double) this.mGfx.DpiX / (double) this.mScaleToReal * 4.0);
      this.mCurPen.EndCap = LineCap.ArrowAnchor;
      Graphics graphics = Graphics.FromHwnd(this.Handle);
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.PageUnit = GraphicsUnit.Inch;
      graphics.ResetTransform();
      graphics.MultiplyTransform(this.mMtxDraw);
      try
      {
        foreach (clsDisplayList selectionHitList in this.mSelectionHitLists)
        {
          this.mCurPen.Color = selectionHitList.Color;
          this.mCurPen.DashStyle = !selectionHitList.Rapid ? DashStyle.Solid : DashStyle.Dash;
          graphics.DrawLines(this.mCurPen, selectionHitList.Points);
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
      this.mCurPen.EndCap = LineCap.Flat;
    }

    private void MG_BasicViewer_Paint(object sender, PaintEventArgs e)
    {
      if (this.mGfxBuff == null)
        return;
      this.mGfxBuff.Render();
    }

    public void Init()
    {
      this.SetBufferContext();
      this.mClientRect = this.ClientRectangle;
      this.ArcSegmentCount = 16;
      this.WindowViewport(-2f, -2f, 2f, 2f);
      this.SetViewMatrix();
      this.DrawWcsOnlyToBuffer();
    }

    private void MG_BasicViewer_SizeChanged(object sender, EventArgs e) => this.Init();

    private void PolyCircle(
      float Xctr,
      float Yctr,
      float Zctr,
      float Xe,
      float Xs,
      float Ye,
      float Ys,
      float Ze,
      float Zs,
      float r,
      ref float Startang,
      ref float Endang,
      int ArcDir,
      ref int Wplane)
    {
      float num1 = Math.Abs(Startang - Endang);
      int num2 = (int) Math.Round((double) Conversion.Int(num1 / this.mSegAngle));
      float num3 = (float) ArcDir * (num1 / (float) num2);
      this.LineEnd4D(Xs, Ys, Zs);
      this.mPoints.Clear();
      switch (Wplane)
      {
        case 0:
          float num4 = (Ze - Zs) / (float) num2;
          int num5 = num2;
          for (int index = 1; index <= num5; ++index)
            this.LineEnd4D(Xctr + r * (float) Math.Cos((double) index * (double) num3 + (double) Startang), Yctr + r * (float) Math.Sin((double) index * (double) num3 + (double) Startang), Zs + num4 * (float) index);
          break;
        case 1:
          float num6 = (Ye - Ys) / (float) num2;
          int num7 = num2;
          for (int index = 1; index <= num7; ++index)
            this.LineEnd4D(Xctr + r * (float) Math.Cos((double) index * (double) num3 + (double) Startang), Ys + num6 * (float) index, Zctr + r * (float) Math.Sin((double) index * (double) num3 + (double) Startang));
          break;
        case 2:
          float num8 = (Xe - Xs) / (float) num2;
          int num9 = num2;
          for (int index = 1; index <= num9; ++index)
            this.LineEnd4D(Xs + num8 * (float) index, Yctr + r * (float) Math.Cos((double) index * (double) num3 + (double) Startang), Zctr + r * (float) Math.Sin((double) index * (double) num3 + (double) Startang));
          break;
      }
      this.LineEnd4D(Xe, Ye, Ze);
    }

    private void RotaryCircle(
      float Xe,
      float Xs,
      float Ye,
      float Ys,
      float Ze,
      float Zs,
      float Startang,
      float Endang,
      float ArcDir)
    {
      if (this.RotaryType == RotaryMotionType.BMC)
      {
        if ((double) ArcDir == -1.0)
          Endang -= 6.283185f;
        else if ((double) ArcDir == 1.0 & (double) Endang < (double) Startang)
          Endang = 6.283185f + Endang;
      }
      float num1 = Endang - Startang;
      int num2 = (int) Math.Round((double) Math.Abs(num1 / this.mSegAngle));
      switch (this.RotaryPlane)
      {
        case Axis.Y:
          Startang = (Startang - GcodeViewer.AngleFromPoint(Zs, Xs, false) * (float) this.RotaryDirection) * (float) -(int) this.RotaryDirection;
          Endang = (Endang - GcodeViewer.AngleFromPoint(Zs, Xs, false) * (float) this.RotaryDirection) * (float) -(int) this.RotaryDirection;
          float num3 = num1 / (float) num2 * (float) -(int) this.RotaryDirection;
          float num4 = (Ye - Ys) / (float) num2;
          float num5 = (Ze - Zs) / (float) num2;
          float num6 = GcodeViewer.VectorLength(Xs, 0.0f, Zs, 0.0f, 0.0f, 0.0f);
          this.LineEnd3D(num6 * (float) Math.Sin((double) Startang), Ys, num6 * (float) Math.Cos((double) Startang));
          this.mPoints.Clear();
          int num7 = num2;
          for (int index = 1; index <= num7; ++index)
          {
            float num8 = GcodeViewer.VectorLength(Xs, 0.0f, Zs + num5 * (float) index, 0.0f, 0.0f, 0.0f);
            this.LineEnd3D(num8 * (float) Math.Sin((double) index * (double) num3 + (double) Startang), Ys + num4 * (float) index, num8 * (float) Math.Cos((double) index * (double) num3 + (double) Startang));
          }
          break;
        case Axis.X:
          Startang = (Startang + GcodeViewer.AngleFromPoint(Zs, Ys, false) * (float) this.RotaryDirection) * (float) this.RotaryDirection;
          Endang = (Endang + GcodeViewer.AngleFromPoint(Zs, Ys, false) * (float) this.RotaryDirection) * (float) this.RotaryDirection;
          float num9 = num1 / (float) num2 * (float) this.RotaryDirection;
          float num10 = (Xe - Xs) / (float) num2;
          float num11 = (Ze - Zs) / (float) num2;
          float num12 = GcodeViewer.VectorLength(0.0f, Ys, Zs, 0.0f, 0.0f, 0.0f);
          this.LineEnd3D(Xs, num12 * (float) Math.Sin((double) Startang), num12 * (float) Math.Cos((double) Startang));
          this.mPoints.Clear();
          int num13 = num2;
          for (int index = 1; index <= num13; ++index)
          {
            float num14 = GcodeViewer.VectorLength(0.0f, Ys, Zs + num11 * (float) index, 0.0f, 0.0f, 0.0f);
            this.LineEnd3D(Xs + num10 * (float) index, num14 * (float) Math.Sin((double) index * (double) num9 + (double) Startang), num14 * (float) Math.Cos((double) index * (double) num9 + (double) Startang));
          }
          break;
      }
      this.LineEnd4D(Xe, Ye, Ze);
    }

    private void Line3D(float Xs, float Ys, float Zs, float Xe, float Ye, float Ze)
    {
      switch (this.RotaryPlane)
      {
        case Axis.Z:
          float num1 = (float) ((double) this.mCosRot * (double) Xs - (double) this.mSinRot * (double) Ys);
          Xs = (float) ((double) this.mSinRot * (double) Xs + (double) this.mCosRot * (double) Ys);
          Ys = num1;
          float num2 = (float) ((double) this.mCosRot * (double) Xe - (double) this.mSinRot * (double) Ye);
          Xe = (float) ((double) this.mSinRot * (double) Xe + (double) this.mCosRot * (double) Ye);
          Ye = num2;
          break;
        case Axis.Y:
          float num3 = (float) ((double) this.mCosRot * (double) Zs - (double) this.mSinRot * (double) Xs);
          Xs = (float) ((double) this.mCosRot * (double) Xs + (double) this.mSinRot * (double) Zs);
          Zs = num3;
          float num4 = (float) ((double) this.mCosRot * (double) Ze - (double) this.mSinRot * (double) Xe);
          Xe = (float) ((double) this.mCosRot * (double) Xe + (double) this.mSinRot * (double) Ze);
          Ze = num4;
          break;
        case Axis.X:
          float num5 = (float) ((double) this.mCosRot * (double) Ys - (double) this.mSinRot * (double) Zs);
          Zs = (float) ((double) this.mSinRot * (double) Ys + (double) this.mCosRot * (double) Zs);
          Ys = num5;
          float num6 = (float) ((double) this.mCosRot * (double) Ye - (double) this.mSinRot * (double) Ze);
          Ze = (float) ((double) this.mSinRot * (double) Ye + (double) this.mCosRot * (double) Ze);
          Ye = num6;
          break;
      }
      float num7 = (float) ((double) this.mCosYaw * (double) Xs - (double) this.mSinYaw * (double) Ys);
      float num8 = (float) ((double) this.mSinYaw * (double) Xs + (double) this.mCosYaw * (double) Ys);
      float num9 = (float) ((double) this.mCosRoll * (double) Zs - (double) this.mSinRoll * (double) num7);
      float x1 = (float) ((double) this.mCosRoll * (double) num7 + (double) this.mSinRoll * (double) Zs);
      float y1 = (float) ((double) this.mCosPitch * (double) num8 - (double) this.mSinPitch * (double) num9);
      float num10 = (float) ((double) this.mCosYaw * (double) Xe - (double) this.mSinYaw * (double) Ye);
      float num11 = (float) ((double) this.mSinYaw * (double) Xe + (double) this.mCosYaw * (double) Ye);
      float num12 = (float) ((double) this.mCosRoll * (double) Ze - (double) this.mSinRoll * (double) num10);
      float x2 = (float) ((double) this.mCosRoll * (double) num10 + (double) this.mSinRoll * (double) Ze);
      float y2 = (float) ((double) this.mCosPitch * (double) num11 - (double) this.mSinPitch * (double) num12);
      this.Line(x1, y1, x2, y2);
    }

    public static float VectorLength(float X1, float Y1, float Z1, float x2, float y2, float Z2) => (float) Math.Sqrt(Math.Pow((double) X1 - (double) x2, 2.0) + Math.Pow((double) Y1 - (double) y2, 2.0) + Math.Pow((double) Z1 - (double) Z2, 2.0));

    public static float AngleFromPoint(float x, float y, bool deg)
    {
      float num = 0;
      if ((double) x > 0.0 & (double) y > 0.0)
        num = (float) Math.Atan((double) y / (double) x);
      else if ((double) x < 0.0 & (double) y > 0.0)
        num = (float) (Math.Atan((double) y / (double) x) + Math.PI);
      else if ((double) x < 0.0 & (double) y < 0.0)
        num = (float) (Math.Atan((double) y / (double) x) + Math.PI);
      else if ((double) x > 0.0 & (double) y < 0.0)
        num = (float) (Math.Atan((double) y / (double) x) + 2.0 * Math.PI);
      if ((double) x > 0.0 & (double) y == 0.0)
        num = 0.0f;
      else if ((double) x == 0.0 & (double) y > 0.0)
        num = 1.570796f;
      else if ((double) x < 0.0 & (double) y == 0.0)
        num = 3.141593f;
      else if ((double) x == 0.0 & (double) y < 0.0)
        num = 4.712389f;
      if (deg)
        num *= 57.29578f;
      return num;
    }

    private void LineEnd4D(float Xe, float Ye, float Ze)
    {
      switch (this.RotaryPlane)
      {
        case Axis.Z:
          float num1 = (float) ((double) this.mCosRot * (double) Xe - (double) this.mSinRot * (double) Ye);
          Xe = (float) ((double) this.mSinRot * (double) Xe + (double) this.mCosRot * (double) Ye);
          Ye = num1;
          break;
        case Axis.Y:
          float num2 = (float) ((double) this.mCosRot * (double) Ze - (double) this.mSinRot * (double) Xe);
          Xe = (float) ((double) this.mCosRot * (double) Xe + (double) this.mSinRot * (double) Ze);
          Ze = num2;
          break;
        case Axis.X:
          float num3 = (float) ((double) this.mCosRot * (double) Ye - (double) this.mSinRot * (double) Ze);
          Ze = (float) ((double) this.mSinRot * (double) Ye + (double) this.mCosRot * (double) Ze);
          Ye = num3;
          break;
      }
      float num4 = (float) ((double) this.mCosYaw * (double) Xe - (double) this.mSinYaw * (double) Ye);
      float num5 = (float) ((double) this.mSinYaw * (double) Xe + (double) this.mCosYaw * (double) Ye);
      float num6 = (float) ((double) this.mCosRoll * (double) Ze - (double) this.mSinRoll * (double) num4);
      this.LineEnd((float) ((double) this.mCosRoll * (double) num4 + (double) this.mSinRoll * (double) Ze), (float) ((double) this.mCosPitch * (double) num5 - (double) this.mSinPitch * (double) num6));
    }

    private void LineEnd3D(float Xe, float Ye, float Ze)
    {
      float num1 = (float) ((double) this.mCosYaw * (double) Xe - (double) this.mSinYaw * (double) Ye);
      float num2 = (float) ((double) this.mSinYaw * (double) Xe + (double) this.mCosYaw * (double) Ye);
      float num3 = (float) ((double) this.mCosRoll * (double) Ze - (double) this.mSinRoll * (double) num1);
      this.LineEnd((float) ((double) this.mCosRoll * (double) num1 + (double) this.mSinRoll * (double) Ze), (float) ((double) this.mCosPitch * (double) num2 - (double) this.mSinPitch * (double) num3));
    }

    private void DrawEachElmt()
    {
      if (GcodeViewer.ToolLayers.ContainsKey(this.mCurGfxRec.Tool) && GcodeViewer.ToolLayers[this.mCurGfxRec.Tool].Hidden)
      {
        this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Zpos);
        this.mPoints.Clear();
      }
      else
      {
                this.mCurColor = this.mCurGfxRec.DrawClr;

                //this.mCurColor = Color.Black;
                this.mCurMotion = this.mCurGfxRec.MotionType;
        if (this.mCurGfxRec.Rotate)
        {
          this.FourthAxis = this.mCurGfxRec.NewRotaryPos;
          this.ArcSegmentCount = (int) Math.Round((double) Conversion.Int((float) ((double) this.mCurGfxRec.Zpos / (double) this.mLongside * 90.0)));
        }
        switch (this.mCurMotion)
        {
          case Motion.RAPID:
            if (this.DrawRapidLines)
            {
              if (this.mCurGfxRec.Rotate)
              {
                this.RotaryCircle(this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.Zpos, this.mCurGfxRec.Zold, this.mCurGfxRec.OldRotaryPos, this.mCurGfxRec.NewRotaryPos, (float) this.mCurGfxRec.RotaryDir);
              }
              else
              {
                int num1 = Math.Sign(this.mCurGfxRec.Xpos - this.mCurGfxRec.Xold);
                int num2 = Math.Sign(this.mCurGfxRec.Ypos - this.mCurGfxRec.Yold);
                int num3 = Math.Sign(this.mCurGfxRec.Zpos - this.mCurGfxRec.Zold);
                float num4 = Math.Abs(this.mCurGfxRec.Xpos - this.mCurGfxRec.Xold);
                float num5 = Math.Abs(this.mCurGfxRec.Ypos - this.mCurGfxRec.Yold);
                float num6 = Math.Abs(this.mCurGfxRec.Zpos - this.mCurGfxRec.Zold);
                                float Xe1 = 0;
                float Ye1 = 0;
                                float Ze1 = 0;
                                float Xe2 = 0;
                                float Ye2 = 0;
                                float Ze2 = 0;
                                if ((double) num4 <= (double) num5 & (double) num5 <= (double) num6)
                {
                  Xe1 = this.mCurGfxRec.Xpos;
                  Ye1 = this.mCurGfxRec.Yold + num4 * (float) num2;
                  Ze1 = this.mCurGfxRec.Zold + num4 * (float) num3;
                  Xe2 = this.mCurGfxRec.Xpos;
                  Ye2 = this.mCurGfxRec.Ypos;
                  Ze2 = this.mCurGfxRec.Zold + num5 * (float) num3;
                }
                else if ((double) num4 <= (double) num6 & (double) num6 <= (double) num5)
                {
                  Xe1 = this.mCurGfxRec.Xpos;
                  Ye1 = this.mCurGfxRec.Yold + num4 * (float) num2;
                  Ze1 = this.mCurGfxRec.Zold + num4 * (float) num3;
                  Xe2 = this.mCurGfxRec.Xpos;
                  Ye2 = this.mCurGfxRec.Yold + num6 * (float) num2;
                  Ze2 = this.mCurGfxRec.Zpos;
                }
                else if ((double) num6 <= (double) num5 & (double) num5 <= (double) num4)
                {
                  Xe1 = this.mCurGfxRec.Xold + num6 * (float) num1;
                  Ye1 = this.mCurGfxRec.Yold + num6 * (float) num2;
                  Ze1 = this.mCurGfxRec.Zpos;
                  Xe2 = this.mCurGfxRec.Xold + num5 * (float) num1;
                  Ye2 = this.mCurGfxRec.Ypos;
                  Ze2 = this.mCurGfxRec.Zpos;
                }
                else if ((double) num6 <= (double) num4 & (double) num4 <= (double) num5)
                {
                  Xe1 = this.mCurGfxRec.Xold + num6 * (float) num1;
                  Ye1 = this.mCurGfxRec.Yold + num6 * (float) num2;
                  Ze1 = this.mCurGfxRec.Zpos;
                  Xe2 = this.mCurGfxRec.Xpos;
                  Ye2 = this.mCurGfxRec.Yold + num4 * (float) num2;
                  Ze2 = this.mCurGfxRec.Zpos;
                }
                else if ((double) num5 <= (double) num6 & (double) num6 <= (double) num4)
                {
                  Xe1 = this.mCurGfxRec.Xold + num5 * (float) num1;
                  Ye1 = this.mCurGfxRec.Ypos;
                  Ze1 = this.mCurGfxRec.Zold + num5 * (float) num3;
                  Xe2 = this.mCurGfxRec.Xold + num6 * (float) num1;
                  Ye2 = this.mCurGfxRec.Ypos;
                  Ze2 = this.mCurGfxRec.Zpos;
                }
                else if ((double) num5 <= (double) num4 & (double) num4 <= (double) num6)
                {
                  Xe1 = this.mCurGfxRec.Xold + num5 * (float) num1;
                  Ye1 = this.mCurGfxRec.Ypos;
                  Ze1 = this.mCurGfxRec.Zold + num5 * (float) num3;
                  Xe2 = this.mCurGfxRec.Xpos;
                  Ye2 = this.mCurGfxRec.Ypos;
                  Ze2 = this.mCurGfxRec.Zold + num4 * (float) num3;
                }
                this.LineEnd3D(Xe1, Ye1, Ze1);
                this.LineEnd4D(Xe2, Ye2, Ze2);
                this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Zpos);
              }
            }
            this.CreateDisplayList(true);
            if (this.DrawRapidPoints)
            {
              this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Zpos);
              this.mPoints.Clear();
              this.DrawBlip(this.mLastPos);
              this.CreateDisplayList(false);
              this.mCurMotion = this.mCurGfxRec.MotionType;
              break;
            }
            break;
          case Motion.LINE:
            if (this.mCurGfxRec.Rotate)
            {
              this.RotaryCircle(this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.Zpos, this.mCurGfxRec.Zold, this.mCurGfxRec.OldRotaryPos, this.mCurGfxRec.NewRotaryPos, (float) this.mCurGfxRec.RotaryDir);
              break;
            }
            this.Line3D(this.mCurGfxRec.Xold, this.mCurGfxRec.Yold, this.mCurGfxRec.Zold, this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Zpos);
            break;
          case Motion.CWARC:
            this.ArcSegmentCount = (int) Math.Round((double) Conversion.Int((float) ((double) this.mCurGfxRec.Rad / (double) this.mLongside * 360.0)));
            this.PolyCircle(this.mCurGfxRec.Xcentr, this.mCurGfxRec.Ycentr, this.mCurGfxRec.Zcentr, this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.Zpos, this.mCurGfxRec.Zold, this.mCurGfxRec.Rad, ref this.mCurGfxRec.Sang, ref this.mCurGfxRec.Eang, -1, ref this.mCurGfxRec.WrkPlane);
            break;
          case Motion.CCARC:
            this.ArcSegmentCount = (int) Math.Round((double) Conversion.Int((float) ((double) this.mCurGfxRec.Rad / (double) this.mLongside * 360.0)));
            this.PolyCircle(this.mCurGfxRec.Xcentr, this.mCurGfxRec.Ycentr, this.mCurGfxRec.Zcentr, this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.Zpos, this.mCurGfxRec.Zold, this.mCurGfxRec.Rad, ref this.mCurGfxRec.Sang, ref this.mCurGfxRec.Eang, 1, ref this.mCurGfxRec.WrkPlane);
            break;
          case Motion.HOLE_I:
          case Motion.HOLE_R:
            if (this.DrawRapidLines)
            {
              int num7 = Math.Sign(this.mCurGfxRec.Xpos - this.mCurGfxRec.Xold);
              int num8 = Math.Sign(this.mCurGfxRec.Ypos - this.mCurGfxRec.Yold);
              float num9 = Math.Abs(this.mCurGfxRec.Xpos - this.mCurGfxRec.Xold);
              float num10 = Math.Abs(this.mCurGfxRec.Ypos - this.mCurGfxRec.Yold);
              if (this.mCurGfxRec.Rotate)
              {
                if (this.mCurGfxRec.MotionType == Motion.HOLE_I)
                  this.RotaryCircle(this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.DrillClear, this.mCurGfxRec.DrillClear, this.mCurGfxRec.OldRotaryPos, this.mCurGfxRec.NewRotaryPos, (float) this.mCurGfxRec.RotaryDir);
                else
                  this.RotaryCircle(this.mCurGfxRec.Xpos, this.mCurGfxRec.Xold, this.mCurGfxRec.Ypos, this.mCurGfxRec.Yold, this.mCurGfxRec.Rpoint, this.mCurGfxRec.Rpoint, this.mCurGfxRec.OldRotaryPos, this.mCurGfxRec.NewRotaryPos, (float) this.mCurGfxRec.RotaryDir);
                this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Rpoint);
              }
              else
              {
                float Xe = 0;
                                float Ye = 0;
                                if ((double) num9 <= (double) num10)
                {
                  Xe = this.mCurGfxRec.Xold + num9 * (float) num7;
                  Ye = this.mCurGfxRec.Yold + num9 * (float) num8;
                }
                if ((double) num9 >= (double) num10)
                {
                  Xe = this.mCurGfxRec.Xold + num10 * (float) num7;
                  Ye = this.mCurGfxRec.Yold + num10 * (float) num8;
                }
                if (this.mCurGfxRec.MotionType == Motion.HOLE_I)
                {
                  this.Line3D(this.mCurGfxRec.Xold, this.mCurGfxRec.Yold, this.mCurGfxRec.DrillClear, Xe, Ye, this.mCurGfxRec.DrillClear);
                  this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.DrillClear);
                  this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Rpoint);
                }
                else
                {
                  this.Line3D(this.mCurGfxRec.Xold, this.mCurGfxRec.Yold, this.mCurGfxRec.Zold, this.mCurGfxRec.Xold, this.mCurGfxRec.Yold, this.mCurGfxRec.Rpoint);
                  this.LineEnd4D(Xe, Ye, this.mCurGfxRec.Rpoint);
                  this.LineEnd4D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Rpoint);
                }
              }
            }
            this.CreateDisplayList(true);
            this.Line3D(this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Rpoint, this.mCurGfxRec.Xpos, this.mCurGfxRec.Ypos, this.mCurGfxRec.Zpos);
            if (this.DrawRapidPoints)
            {
              this.CreateDisplayList(false);
              this.ArcSegmentCount = 8;
              double xpos = (double) this.mCurGfxRec.Xpos;
              double ypos1 = (double) this.mCurGfxRec.Ypos;
              double zpos1 = (double) this.mCurGfxRec.Zpos;
              double num11 = (double) this.mCurGfxRec.Xpos + (double) this.mBlipSize;
              double num12 = (double) this.mCurGfxRec.Xpos + (double) this.mBlipSize;
              double ypos2 = (double) this.mCurGfxRec.Ypos;
              double ypos3 = (double) this.mCurGfxRec.Ypos;
              double zpos2 = (double) this.mCurGfxRec.Zpos;
              double zpos3 = (double) this.mCurGfxRec.Zpos;
              double mBlipSize = (double) this.mBlipSize;
              float num13 = 0.0f;
              ref float local1 = ref num13;
              float num14 = 6.283185f;
              ref float local2 = ref num14;
              int num15 = 0;
              ref int local3 = ref num15;
              this.PolyCircle((float) xpos, (float) ypos1, (float) zpos1, (float) num11, (float) num12, (float) ypos2, (float) ypos3, (float) zpos2, (float) zpos3, (float) mBlipSize, ref local1, ref local2, -1, ref local3);
              break;
            }
            break;
        }
        this.CreateDisplayList(false);
      }
    }

    private void DrawBlip(PointF p)
    {
      this.mPoints.Clear();
      this.Line(p.X - this.mBlipSize, p.Y - this.mBlipSize, p.X + this.mBlipSize, p.Y - this.mBlipSize);
      this.LineEnd(p.X + this.mBlipSize, p.Y + this.mBlipSize);
      this.LineEnd(p.X - this.mBlipSize, p.Y + this.mBlipSize);
      this.LineEnd(p.X - this.mBlipSize, p.Y - this.mBlipSize);
    }

    public void Redraw(bool allSiblings)
    {
      if (allSiblings)
      {
        try
        {
          foreach (GcodeViewer sibling in GcodeViewer.Siblings)
          {
            if (Operators.CompareString(sibling.ParentForm.Name, this.ParentForm.Name, false) == 0)
              sibling.CreateDisplayListsAndDraw();
          }
        }
        finally
        {
          List<GcodeViewer>.Enumerator enumerator;
          //enumerator.Dispose();
        }
      }
      else
        this.CreateDisplayListsAndDraw();
    }

    public void FindExtents()
    {
      if (!this.Visible || GcodeViewer.MotionBlocks.Count == 0)
        return;
      this.mExtentX[0] = float.MaxValue;
      this.mExtentX[1] = float.MinValue;
      this.mExtentY[0] = float.MaxValue;
      this.mExtentY[1] = float.MinValue;
      bool drawRapidPoints = this.DrawRapidPoints;
      this.DrawRapidPoints = false;
      this.CreateDisplayLists();
      this.DrawRapidPoints = drawRapidPoints;
      if (GcodeViewer.MotionBlocks.Count > 0)
      {
        try
        {
          foreach (clsDisplayList mDisplayList in this.mDisplayLists)
          {
            foreach (PointF point in mDisplayList.Points)
            {
              this.mExtentX[0] = Math.Min(this.mExtentX[0], point.X);
              this.mExtentX[1] = Math.Max(this.mExtentX[1], point.X);
              this.mExtentY[0] = Math.Min(this.mExtentY[0], point.Y);
              this.mExtentY[1] = Math.Max(this.mExtentY[1], point.Y);
            }
          }
        }
        finally
        {
          List<clsDisplayList>.Enumerator enumerator;
          //enumerator.Dispose();
        }
      }
      else
      {
        this.mExtentX[0] = -1f;
        this.mExtentX[1] = 1f;
        this.mExtentY[0] = -1f;
        this.mExtentY[1] = 1f;
      }
      this.mViewRect.X = this.mExtentX[0];
      this.mViewRect.Width = this.mExtentX[1] - this.mExtentX[0];
      this.mViewRect.Y = this.mExtentY[0];
      this.mViewRect.Height = this.mExtentY[1] - this.mExtentY[0];
      if (float.IsNegativeInfinity(this.mViewRect.Width) || float.IsNegativeInfinity(this.mViewRect.Height))
        return;
      this.mViewRect.Inflate(this.mViewRect.Width * 0.01f, this.mViewRect.Height * 0.01f);
      this.AdjustAspect();
    }

    private void CreateDisplayLists()
    {
      this.mDisplayLists.Clear();
      this.mPoints.Clear();
      this.mLastPos.X = 0.0f;
      this.mLastPos.Y = 0.0f;
      int mBreakPoint = GcodeViewer.mBreakPoint;
      for (this.mGfxIndex = 0; this.mGfxIndex <= mBreakPoint; ++this.mGfxIndex)
      {
        this.mCurGfxRec = GcodeViewer.MotionBlocks[this.mGfxIndex];
        this.DrawEachElmt();
      }
    }

    private void DrawDisplayLists()
    {
      this.CreateWcs();
      this.SetInViewStatus(this.mViewRect);
      this.mGfx.Clear(this.BackColor);
      this.DrawListsToGraphics(ref this.mGfx);
      this.mGfxBuff.Render();
    }

    private void CreateDisplayListsAndDraw()
    {
      this.CreateDisplayLists();
      this.DrawDisplayLists();
    }

    private void CreateWcs()
    {
      if (!this.Visible)
        return;
      this.mWcsDisplayLists.Clear();
      this.mPoints.Clear();
      this.FourthAxis = 0.0f;
      if (this.DrawAxisLines)
      {
        this.mCurMotion = Motion.RAPID;
        this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, (float) (-(double) this.mLongside * 10.0), 0.0f);
        this.CreateWcsPath(Color.Gray);
        this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, this.mLongside * 10f, 0.0f);
        this.CreateWcsPath(Color.Gray);
        this.Line3D(0.0f, 0.0f, 0.0f, this.mLongside * 10f, 0.0f, 0.0f);
        this.CreateWcsPath(Color.Gray);
        this.Line3D(0.0f, 0.0f, 0.0f, (float) (-(double) this.mLongside * 10.0), 0.0f, 0.0f);
        this.CreateWcsPath(Color.Gray);
        this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, this.mLongside * 10f);
        this.CreateWcsPath(Color.Gray);
        this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, (float) (-(double) this.mLongside * 10.0));
        this.CreateWcsPath(Color.Gray);
      }
      if (!this.DrawAxisIndicator)
        return;

            //x
            var color_x = Color.Red;
            var color_y = Color.Green;
            var color_z = Color.Blue;


            this.mCurMotion = Motion.LINE;
      this.Line3D(0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
      this.Line3D(1f, 0.0f, 0.0f, 0.9f, 0.05f, 0.0f);
      this.Line3D(1f, 0.0f, 0.0f, 0.9f, -0.05f, 0.0f);
      this.CreateWcsPath(color_x);
      this.Line3D(0.7f, 0.1f, 0.0f, 0.9f, 0.4f, 0.0f);
      this.CreateWcsPath(color_x);
      this.Line3D(0.9f, 0.1f, 0.0f, 0.7f, 0.4f, 0.0f);
      this.CreateWcsPath(color_x);

            //y
            
            this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(0.0f, 1f, 0.0f, -0.05f, 0.9f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(0.0f, 1f, 0.0f, 0.05f, 0.9f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(-0.2f, 0.7f, 0.0f, -0.2f, 0.85f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(-0.2f, 0.85f, 0.0f, -0.3f, 1f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(-0.2f, 0.85f, 0.0f, -0.1f, 1f, 0.0f);
      this.CreateWcsPath(color_y);
      this.Line3D(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1f);
      this.Line3D(0.0f, 0.0f, 1f, 0.1f, 0.0f, 0.8f);

            //z
            
            this.CreateWcsPath(color_z);
      float num1 = 0.0f;
      ref float local1 = ref num1;
      float num2 = 6.283185f;
      ref float local2 = ref num2;
      int num3 = 0;
      ref int local3 = ref num3;
      this.PolyCircle(0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 0.8f, 0.8f, 0.1f, ref local1, ref local2, 1, ref local3);
      this.CreateWcsPath(color_z);
      this.Line3D(-0.2f, 0.0f, 0.7f, -0.4f, 0.0f, 0.7f);
      this.Line3D(-0.2f, 0.0f, 0.95f, -0.4f, 0.0f, 0.95f);
      this.Line3D(-0.2f, 0.0f, 0.95f, -0.4f, 0.0f, 0.7f);
      this.CreateWcsPath(color_z);
    }

    public void GatherTools()
    {
      GcodeViewer.ToolLayers.Clear();
      try
      {
        foreach (clsMotionRecord motionBlock in GcodeViewer.MotionBlocks)
        {
          float tool = 0;
                    if ((double) tool != (double) motionBlock.Tool)
          {
            tool = motionBlock.Tool;
            if (!GcodeViewer.ToolLayers.ContainsKey(motionBlock.Tool))
              GcodeViewer.ToolLayers.Add(motionBlock.Tool, new clsToolLayer(motionBlock.Tool, motionBlock.DrawClr));
          }
        }
      }
      finally
      {
        List<clsMotionRecord>.Enumerator enumerator;
        //enumerator.Dispose();
      }
    }

    private void GetSelectionHits(RectangleF rect)
    {
      int num1 = 0;
      clsCadRect clsCadRect = new clsCadRect(rect.X, rect.Y, rect.Width, rect.Height);
      this.mSelectionHits.Clear();
      this.mSelectionHitLists.Clear();
      if (GcodeViewer.MotionBlocks.Count <= 0)
        return;
      try
      {
        foreach (clsDisplayList mDisplayList in this.mDisplayLists)
        {
          if (mDisplayList.InView)
          {
            int num2 = mDisplayList.Points.Length - 2;
            for (int index = 0; index <= num2; ++index)
            {
              if (num1 >= 64)
                return;
              if (clsCadRect.IntersectsLine(mDisplayList.Points[index], mDisplayList.Points[index + 1]))
              {
                this.mSelectionHits.Add(GcodeViewer.MotionBlocks[mDisplayList.ParentIndex]);
                this.mSelectionHitLists.Add(mDisplayList);
                ++num1;
                break;
              }
            }
          }
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
    }

    private void SetInViewStatus(RectangleF rect)
    {
      clsCadRect clsCadRect = new clsCadRect(rect.X, rect.Y, rect.Width, rect.Height);
      try
      {
        foreach (clsDisplayList mDisplayList in this.mDisplayLists)
        {
          int num = mDisplayList.Points.Length - 2;
          for (int index = 0; index <= num; ++index)
          {
            mDisplayList.InView = false;
            if (clsCadRect.IntersectsLine(mDisplayList.Points[index], mDisplayList.Points[index + 1]))
            {
              mDisplayList.InView = true;
              break;
            }
          }
        }
      }
      finally
      {
        List<clsDisplayList>.Enumerator enumerator;
        //enumerator.Dispose();
      }
    }

    private void ClearDisplayList()
    {
      this.mDisplayLists.Clear();
      this.mPoints.Clear();
    }

    private void CreateDisplayList(bool rapid)
    {
      clsDisplayList clsDisplayList1 = new clsDisplayList();
      if (this.mPoints.Count < 2)
        return;
      clsDisplayList clsDisplayList2 = clsDisplayList1;
      clsDisplayList2.Color = this.mCurColor;
      clsDisplayList2.Rapid = rapid;
      clsDisplayList2.ParentIndex = this.mGfxIndex;
      clsDisplayList2.Points = this.mPoints.ToArray();
      this.mDisplayLists.Add(clsDisplayList1);
      this.mPoints.Clear();
    }

    private void CreateWcsPath(Color clr)
    {
      clsDisplayList clsDisplayList1 = new clsDisplayList();
      if (this.mPoints.Count < 2)
        return;
      clsDisplayList clsDisplayList2 = clsDisplayList1;
      clsDisplayList2.Color = clr;
      clsDisplayList2.Rapid = this.mCurMotion == Motion.RAPID;
      clsDisplayList2.Points = this.mPoints.ToArray();
      this.mWcsDisplayLists.Add(clsDisplayList1);
      this.mPoints.Clear();
    }

    private void LineEnd(float x2, float y2)
    {
      if ((double) this.mLastPos.X != (double) x2 & (double) this.mLastPos.Y != (double) y2)
        this.mPoints.Add(this.mLastPos);
      this.mPoints.Add(new PointF(x2, y2));
      this.mLastPos.X = x2;
      this.mLastPos.Y = y2;
    }

    private void Line(float x1, float y1, float x2, float y2)
    {
      List<PointF> mPoints1 = this.mPoints;
      PointF pointF1 = new PointF(x1, y1);
      PointF pointF2 = pointF1;
      mPoints1.Add(pointF2);
      List<PointF> mPoints2 = this.mPoints;
      pointF1 = new PointF(x2, y2);
      PointF pointF3 = pointF1;
      mPoints2.Add(pointF3);
      this.mLastPos.X = x2;
      this.mLastPos.Y = y2;
    }

    public GcodeViewer()
    {
      this.MouseMove += new MouseEventHandler(this.MG_BasicViewer_MouseMove);
      this.MouseUp += new MouseEventHandler(this.MG_BasicViewer_MouseUp);
      this.VisibleChanged += new EventHandler(this.MG_BasicViewer_VisibleChanged);
      this.Paint += new PaintEventHandler(this.MG_BasicViewer_Paint);
      this.SizeChanged += new EventHandler(this.MG_BasicViewer_SizeChanged);
      this.MouseWheel += new MouseEventHandler(this.MG_BasicViewer_MouseWheel);
      this.MouseDown += new MouseEventHandler(this.MG_BasicViewer_MouseDown);
      this.mLongside = 2f;
      this.mExtentX = new float[2];
      this.mExtentY = new float[2];
      this.mPoints = new List<PointF>();
      this.mSelectionHitLists = new List<clsDisplayList>();
      this.mSelectionHits = new List<clsMotionRecord>();
      this.mDisplayLists = new List<clsDisplayList>();
      this.mWcsDisplayLists = new List<clsDisplayList>();
      this.mMtxDraw = new Matrix();
      this.mMtxWCS = new Matrix();
      this.mMtxFeedback = new Matrix();
      this.mMtxGeo = new Matrix();
      this.mViewRect = new RectangleF();
      this.mClientRect = new Rectangle();
      this.mSelectionRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      this.mSelectionPixRect = new Rectangle(0, 0, 4, 4);
      this.mViewportCenter = new PointF();
      this.mScaleToReal = 1f;
      this.mMousePtF = new PointF[3];
      this.mCurPen = new Pen(Color.Blue, 0.0f);
      this.mWCSPen = new Pen(Color.Blue, 0.0f);
      this.mRapidDashStyle = new float[2]{ 0.1f, 0.1f };
      this.mAxisDashStyle = new float[2]{ 0.05f, 0.2f };
      this.mPitch = 0.0f;
      this.mRoll = 0.0f;
      this.mYaw = 0.0f;
      this.mRotary = 0.0f;
      this.mSegAngle = 0.3926991f;
      this.InitializeComponent();
      GcodeViewer.Siblings.Add(this);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
      this.mContext = BufferedGraphicsManager.Current;
    }

    ~GcodeViewer()
    {
      if (this.mGfxBuff != null)
        this.mGfxBuff.Dispose();
      if (this.mMtxDraw != null)
        this.mMtxDraw.Dispose();
      if (this.mCurPen != null)
        this.mCurPen.Dispose();
      if (this.mWCSPen != null)
        this.mWCSPen.Dispose();
      // ISSUE: explicit finalizer call
      //base.Finalize();
    }

    protected override void Dispose(bool disposing)
    {
      GcodeViewer.Siblings.Remove(this);
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
      GC.SuppressFinalize((object) this);
    }

    private void MG_BasicViewer_VisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible)
        return;
      this.mDisplayLists.Clear();
    }

    public enum ManipMode
    {
      NONE,
      FENCE,
      PAN,
      ZOOM,
      ROTATE,
      SELECTION,
    }

    public delegate void AfterViewManipEventHandler(
      GcodeViewer.ManipMode mode,
      RectangleF viewRect);

    public delegate void OnStatusEventHandler(string msg, int index, int max);

    public delegate void OnSelectionEventHandler(List<clsMotionRecord> hits);

    public delegate void MouseLocationEventHandler(float x, float y);
  }
}
