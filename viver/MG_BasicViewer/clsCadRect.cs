// Decompiled with JetBrains decompiler
// Type: MacGen.clsCadRect
// Assembly: MG_BasicViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 995351A4-6479-4341-A2E3-44F14AEC55FB
// Assembly location: C:\Users\tridb\Desktop\BTP\bin\Debug\MG_BasicViewer.dll

using System.Drawing;

namespace MacGen
{
  public class clsCadRect
  {
    private float mX;
    private float my;
    private float mLeft;
    private float mRight;
    private float mWidth;
    private float mHeight;
    private float mTop;
    private float mBottom;

    public float X
    {
      get => this.mX;
      set
      {
        this.mX = value;
        this.mLeft = this.mX;
        this.mRight = this.mLeft + this.mWidth;
      }
    }

    public float Y
    {
      get => this.my;
      set
      {
        this.my = value;
        this.mTop = this.my + this.mHeight;
        this.mBottom = this.my;
      }
    }

    public float Left => this.mLeft;

    public float Right => this.mRight;

    public float Width
    {
      get => this.mWidth;
      set
      {
        this.mWidth = value;
        this.mRight = this.mLeft + this.mWidth;
      }
    }

    public float Height
    {
      get => this.mHeight;
      set
      {
        this.mHeight = value;
        this.mTop = this.my + this.mHeight;
      }
    }

    public float Top => this.mTop;

    public float Bottom => this.mBottom;

    public clsCadRect()
    {
      this.X = 0.0f;
      this.Y = 0.0f;
      this.Width = 0.0f;
      this.Height = 0.0f;
    }

    public clsCadRect(float x, float y, float width, float height)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    public bool IntersectsLine(PointF p1, PointF p2) => this.IntersectsLine(p1.X, p1.Y, p2.X, p2.Y);

    public bool Contains(float x, float y) => (double) x > (double) this.Left & (double) x < (double) this.Right & (double) y > (double) this.Bottom & (double) y < (double) this.Top;

    public bool IntersectsLine(float x1, float y1, float x2, float y2)
    {
      if (this.Contains(x1, y1) | this.Contains(x2, y2))
        return true;
      if ((double) x1 < (double) this.Left & (double) x2 < (double) this.Left || (double) x1 > (double) this.Right & (double) x2 > (double) this.Right || (double) y1 < (double) this.Bottom & (double) y2 < (double) this.Bottom || (double) y1 > (double) this.Top & (double) y2 > (double) this.Top)
        return false;
      if ((double) x1 == (double) x2 || (double) y1 == (double) y2)
        return true;
      float num1 = (float) (((double) y2 - (double) y1) / ((double) x2 - (double) x1));
      float num2 = y1 - num1 * x1;
      float left = this.Left;
      float num3 = num1 * left + num2;
      if ((double) num3 > (double) this.Bottom & (double) num3 < (double) this.Top)
        return true;
      float right = this.Right;
      if ((double) num3 > (double) this.Bottom & (double) num3 < (double) this.Top)
        return true;
      float num4 = (this.Top - num2) / num1;
      if ((double) num4 > (double) this.Left & (double) num4 < (double) this.Right)
        return true;
      float num5 = (this.Bottom - num2) / num1;
      return (double) num5 > (double) this.Left & (double) num5 < (double) this.Right;
    }
  }
}
