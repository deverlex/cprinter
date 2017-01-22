using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CPrinter
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Allow the user to select a printer.
            // Print the file to the printer.

            PdfiumViewer.PdfDocument pdfDoc = onLoadDocumentFromFile(args[0]);
            if (pdfDoc == null)
                return; 

            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowCurrentPage = true;
            printDialog.AllowPrintToFile = false;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;

            printDialog.PrinterSettings.Copies = 0;
            printDialog.PrinterSettings.MinimumPage = 1;
            printDialog.PrinterSettings.MaximumPage = pdfDoc.PageCount;
            printDialog.PrinterSettings.FromPage = 1;
            printDialog.PrinterSettings.ToPage = pdfDoc.PageCount;

            if (DialogResult.OK == printDialog.ShowDialog())
            {
                PrintDocument printDoc = pdfDoc.CreatePrintDocument();

                printDoc.PrinterSettings = printDialog.PrinterSettings;

                printDoc.DefaultPageSettings.PaperSize = printDialog.PrinterSettings.DefaultPageSettings.PaperSize;
                printDoc.DefaultPageSettings.PrinterResolution = printDialog.PrinterSettings.DefaultPageSettings.PrinterResolution;
                printDoc.DefaultPageSettings.Margins = printDialog.PrinterSettings.DefaultPageSettings.Margins;
                printDoc.DefaultPageSettings.Landscape = printDialog.PrinterSettings.DefaultPageSettings.Landscape;
                printDoc.DefaultPageSettings.Color = printDialog.PrinterSettings.DefaultPageSettings.Color;

                printDialog.Document = printDoc;
                printDoc.Print();
            }

        }

        static PdfiumViewer.PdfDocument onLoadDocumentFromFile(String pathFile)
        {
            try
            {
                return PdfiumViewer.PdfDocument.Load(pathFile);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
