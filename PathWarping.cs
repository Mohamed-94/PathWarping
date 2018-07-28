using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class PathWarping : Form
{
    MenuItem miWarpMod;
    GraphicsPath path;
    PointF[] apDes = new PointF[4];

    //public static void Main()
    //{
    //    Application.Run(new PathWarping());
    //}
    public PathWarping()
    {
        Text = "Path Warping";
        Menu = new MainMenu();
        Menu.MenuItems.Add("&WarpMode");
        EventHandler ecClick = new EventHandler(MenuEvent);
        miWarpMod = new MenuItem("&" + (WarpMode )0, ecClick);
        miWarpMod.RadioCheck = true;
        miWarpMod.Checked = true;
        Menu.MenuItems[0].MenuItems.Add(miWarpMod);
        MenuItem mi = new MenuItem("&" + (WarpMode)1, ecClick);
        mi.RadioCheck = true;
        Menu.MenuItems[0].MenuItems.Add(mi);
        ResizeRedraw = true;
        //create path..
        path = new GraphicsPath();
        for (int i = 0; i <= 8; i++)
        {
            path.StartFigure();
            path.AddLine(0, 100 * i, 800, 100 * i);
            path.StartFigure();

            path.AddLine(100 * i, 0, 100 * i, 800);
        }
        //intialize array
        apDes[0] = new Point(100, 100);
        apDes[1] = new Point(400, 100);
        apDes[2] = new Point(100, 400);
        apDes[3] = new Point(400, 400);

    }
    void MenuEvent(object obj, EventArgs ea)
    {
        miWarpMod.Checked = false;
        miWarpMod = (MenuItem)obj;
        miWarpMod.Checked = true;

        Invalidate();
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        Point pt;
        if (e.Button == MouseButtons.Left)
        {
            if (ModifierKeys == Keys.None)
                pt = Point.Round(apDes[0]);
            else if (ModifierKeys == Keys.Shift)
                pt = Point.Round(apDes[2]);
            else
                return;

        }
        else if (e.Button == MouseButtons.Right)
        {
            if (ModifierKeys == Keys.None)
                pt = Point.Round(apDes[1]);
            else if (ModifierKeys == Keys.Shift)
                pt = Point.Round(apDes[3]);
            else
                return;
        }
        else
            return;
        Cursor.Position = PointToScreen(pt);
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        Point pt = new Point(e.X, e.Y);
        if (e.Button == MouseButtons.Left)
        {
            if (ModifierKeys == Keys.None)
                apDes[0] = pt;
            else if (ModifierKeys == Keys.Shift)
                apDes[2] = pt;
            else
                return;

        }
        else if (e.Button == MouseButtons.Right)
        {
            if (ModifierKeys == Keys.None)
                apDes[1] = pt;
            else if (ModifierKeys == Keys.Shift)
                apDes[3] = pt;
            else
                return;
        }
        else
            return;  
        
        Invalidate();
    } 
    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            Graphics graf = e.Graphics;
            GraphicsPath pathwraped = (GraphicsPath)path.Clone();
            WarpMode wm = (WarpMode)miWarpMod.Index;
            pathwraped.Warp(apDes, path.GetBounds(), new Matrix(), wm);
            graf.DrawPath(new Pen(ForeColor), pathwraped);
        }
        catch (Exception er)
        {
            MessageBox.Show(er.Message);
        }
    }

}