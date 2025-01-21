using System.Configuration.Assemblies;
using System.Diagnostics.Metrics;
using Independentsoft.Office.Odf;
using Syncfusion.XlsIO;

namespace Pointer.Finder.Clients
{

    class CSVHelper
    {

        Spreadsheet Spreadsheet { get; }

        public CSVHelper()
        {
            Spreadsheet = new Spreadsheet();
        }

        public void CreateSheet(string tableName, List<CSVDataModel> fileContent)
        {

            Table workSheet = new Table(tableName);

            CreateSheetHeaders(workSheet);


            foreach (var record in fileContent)
            {
                var row = new Row();

                row.Cells.Add(new Cell(record.Pointer));
                row.Cells.Add(new Cell(record.PointerOffset));
                row.Cells.Add(new Cell(record.OriginalText));
                row.Cells.Add(new Cell(record.TranslatedText));
                row.Cells.Add(new Cell(record.Pointer));

                workSheet.Rows.Add(row);

                // writer.WriteLine($"{record.Pointer},{record.PointerOffset},{record.OriginalText},{record.TranslatedText}");
            }

            Spreadsheet.Tables.Add(workSheet);
        }

        public void CreateSheetHeaders(Table workSheet)
        {

            var headersList = new List<string>() {
                "Pointer Offset",
                "Pointer",
                "Original Text",
                "Translated Text",
                "Base OVeralay Addr.",
                "ProgOverlay Addr.",
                "CnstOVerlay Addr.",
                "Sub Overlay"
            };

            Row headerRow = new Row();
            headerRow.IsHeader = true;

            foreach (var headerText in headersList)
            {
                headerRow.Cells.Add(new Cell(headerText));
            }

            workSheet.HeaderRows.Add(headerRow);
        }

        public void WriteFile(string fileName)
        {
            Spreadsheet.Save("./trying.ods", true);
        }

    }

    class CSVDataModel
    {
        public string Pointer { get; set; }
        public string PointerOffset { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
    }

}