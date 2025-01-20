


using System.Configuration.Assemblies;
using Syncfusion.XlsIO;

namespace Pointer.Finder.Clients {

    class CSVHelper {

        public CSVHelper()
        {
            ExcelEngine excelEngine = new ExcelEngine();
            //Instantiate the Excel application object
            IApplication application = excelEngine.Excel;
        }

        public void CreateCSVFile(string fileName, List<CSVDataModel> fileContent) {

        using (var writer = new StreamWriter("output.csv"))
        {
            writer.WriteLine($"Pointer///PointerOffset///OriginalText,TranslatedText");
            foreach (var record in fileContent)
            {
                writer.WriteLine($"{record.Pointer},{record.PointerOffset},{record.OriginalText},{record.TranslatedText}");
            }
        }

        }

    }

    class CSVDataModel {
        public string Pointer { get; set; }
        public string PointerOffset { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
    }

}