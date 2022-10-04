// Decompiled with JetBrains decompiler
// Type: MacGen.clsToolLayer
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using System.Drawing;

namespace MacGen
{
  public class clsToolLayer
  {
    public Color Color;
    public float Number;
    public bool Hidden;

    public clsToolLayer(float number, Color color)
    {
      this.Number = number;
      this.Color = color;
      this.Hidden = false;
    }
  }
}
