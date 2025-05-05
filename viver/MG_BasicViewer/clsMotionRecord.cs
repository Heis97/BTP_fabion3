// Decompiled with JetBrains decompiler
// Type: MacGen.clsMotionRecord
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using System.Diagnostics;
using System.Drawing;

namespace MacGen
{
  public class clsMotionRecord
  {
    public string Codestring;
    public Motion MotionType;
    public int Linenumber;
    public float Xold;
    public float Yold;
    public float Zold;
    public float Xpos;
    public float Ypos;
    public float Zpos;
    public float Rpoint;
    public float Rad;
    public float Sang;
    public float Eang;
    public float Xcentr;
    public float Ycentr;
    public float Zcentr;
    public Color DrawClr;
    public float Tool;
    public float Speed;
    public float Feed;
    public bool BeginProfile;
    public bool Rotate;
    public float OldRotaryPos;
    public float NewRotaryPos;
    public int RotaryDir;
    public int WrkPlane;
    public float DrillClear;
    public bool Inview;

    [DebuggerNonUserCode]
    public clsMotionRecord()
    {
    }
  }
}
