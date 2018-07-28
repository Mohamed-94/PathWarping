using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

class GradientPath:PrintabelForm
{
    //public static void Main()
    //{
    //    Application.Run(new GradientPath());
    //}

    public GradientPath()
    {
        Text = "Gradient Path Brush";
        ResizeRedraw = true;

    }
    protected override void DoPage(Graphics graf, Color clr, int cx, int cy)
    {
        Point[] apo = { new Point(cx, 0), new Point(cx, cy), new Point(0, cy) };

        PathGradientBrush pgb = new PathGradientBrush(apo);

        pgb.SurroundColors = new Color[] { Color.Red, Color.Green, Color.Blue };
        graf.FillRectangle(pgb, 0, 0, cx, cy);
    }
}