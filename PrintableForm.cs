using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Drawing.Printing;

class PrintabelForm : Form
{
    int[] iValues = { 50, 100, 25,150,100, 75 };
    //public static void Main()
    //{
    //    Application.Run(new PrintabelForm());
    //}

    public PrintabelForm()
    {
        Text = "PrintableForm";

    }
    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graf = e.Graphics;
        DoPage(graf, ForeColor, ClientRectangle.Width, ClientRectangle.Height);
    }

    protected virtual void DoPage(Graphics graf, Color clr, int cx, int cy)
    {

    }    
         
        void PrintDocumentOnPrintPage(Object opj, PrintPageEventArgs pea)
        {
            Graphics graf = pea.Graphics;
            SizeF sizf = graf.VisibleClipBounds.Size;
            DoPage(graf, Color.Black, (int)sizf.Width, (int)sizf.Height);
        }


         protected override void OnClick(EventArgs e)
    {
        PrintDocument prinD = new PrintDocument();
        prinD.DocumentName = Text;
        prinD.PrintPage += new PrintPageEventHandler(PrintDocumentOnPrintPage);
        prinD.Print();
    }
}
