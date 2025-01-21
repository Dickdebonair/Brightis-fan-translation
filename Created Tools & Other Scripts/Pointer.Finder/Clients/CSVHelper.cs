


using System.Configuration.Assemblies;
using System.Diagnostics.Metrics;
using Syncfusion.XlsIO;

namespace Pointer.Finder.Clients {

    class CSVHelper {

        public ExcelEngine excelEngine { get; private set; }
        public IApplication application { get; private set; }
        public IWorkbook workbook { get; private set; }

        public CSVHelper()
        {
            excelEngine = new ExcelEngine();
            //Instantiate the Excel application object
            application = excelEngine.Excel;
            //Assigns default application version
            application.DefaultVersion = ExcelVersion.Xlsx;

            workbook = application.Workbooks.Create(1);
            //Access a worksheet from workbook
        }



        public void CreateCSVFile(List<CSVDataModel> fileContent) {

            int counter = 1;
            IWorksheet worksheet = workbook.Worksheets[0];

            worksheet.AutofitColumn(1);

            foreach (var record in fileContent)
            {
                worksheet.Range[$"A{counter}"].Text = record.Pointer;
                worksheet.Range[$"B{counter}"].Text = record.PointerOffset;
                worksheet.Range[$"C{counter}"].Text = record.OriginalText;
                worksheet.Range[$"D{counter}"].Text = record.TranslatedText;
                // writer.WriteLine($"{record.Pointer},{record.PointerOffset},{record.OriginalText},{record.TranslatedText}");
                counter++;
            }
        }

        public void CreateNewSheet(IWorksheet worksheet) {
            worksheet.Range["A1"].Text = "Pointer";
            worksheet.Range["B1"].Text = "Pointer Offset";
            worksheet.Range["C1"].Text = "Original Text";
            worksheet.Range["D1"].Text = "Translated Text";
        }

        public void CreateXlsFile(string fileName) {
            //Saving the workbook to disk in XLSX format
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            workbook.SaveAs(stream);

            //Dispose stream
            stream.Dispose();
        }

    }

    class CSVDataModel {
        public string Pointer { get; set; }
        public string PointerOffset { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
    }

}