// Decompiled with JetBrains decompiler
// Type: MacGen.clsDisplayList
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using System.Drawing;

namespace MacGen
{
  public class clsDisplayList
  {
    public bool InView;
    public bool Rapid;
    public int ParentIndex;
    public Color Color;
    public PointF[] Points;

    public clsDisplayList()
    {
      this.InView = true;
      this.Rapid = true;
    }
  }
}
