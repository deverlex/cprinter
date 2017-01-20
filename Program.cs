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

                printDoc.DefaultPageSettings.PaperSize = printDialog.PrinterSettings.DefaultPageSettings.PaperSize;
                printDoc.DefaultPageSettings.PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
                printDoc.DefaultPageSettings.PrinterSettings.FromPage = printDialog.PrinterSettings.FromPage;
                printDoc.DefaultPageSettings.PrinterSettings.ToPage = printDialog.PrinterSettings.ToPage;

                printDoc.DefaultPageSettings.PrinterSettings = printDialog.PrinterSettings.DefaultPageSettings.PrinterSettings;

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
