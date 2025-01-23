using System.Configuration.Assemblies;
using System.Diagnostics.Metrics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Independentsoft.Office.Odf;

namespace Pointer.Finder.Clients
{

    class SpreadSheetHelper
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string SpreadsheetId = "1Rflah7eg7Xd9Ir-wCPUm1Hp5s39090MCwiL6MQ0Ryeo";
        private const string GoogleCredentialsFileName = "./google-api-key.json";
        private SheetsService SheetsService { get; }

        public SpreadSheetHelper()
        {
            using (var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read))
            {
                var serviceInitializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
                };
                SheetsService = new SheetsService(serviceInitializer);
            }

            // CreateSheetsForBinFiles();
        }

        public void AddTranslationData(string sheetName, List<CSVDataModel> fileContent)
        {

            try
            {
                var batching = new BatchUpdateSpreadsheetRequest() {
                    Requests = new List<Request>() {
                    }
                };

                int counter = 2;
                // var valueRange = new ValueRange();

                // valueRange.Values = new List<IList<object>>();
                
                foreach (var record in fileContent)
                {
                    var row = new List<RowData>
                    {   
                        new RowData() {
                            Values = new List<CellData>() {
                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = record.Pointer
                                    }
                                },

                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = record.PointerOffset
                                    }
                                },

                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = "0"
                                    }
                                },

                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = record.OriginalText
                                    }
                                },

                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = record.TranslatedText
                                    }
                                },

                                new CellData() {
                                    EffectiveValue = new ExtendedValue() {
                                        StringValue = record.Pointer
                                    }
                                }
                            }
                        
                        }            
                    };


                     var updateCells = new UpdateCellsRequest() {
                        Rows = row,
                        Start = new GridCoordinate() {
                            SheetId = int.Parse(sheetName),
                            RowIndex = 1,
                            ColumnIndex = 0
                        }

                    };

                    batching.Requests.Add( new Request {
                        UpdateCells = updateCells
                    });

                    counter++;

                    // writer.WriteLine($"{record.Pointer},{record.PointerOffset},{record.OriginalText},{record.TranslatedText}");
                }

                // var request = SheetsService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, $"{sheetName}!A2");
                // request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

                // var response = request.Execute();

                SheetsService.Spreadsheets.BatchUpdate(batching, SpreadsheetId).Execute();

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void CreateSheetAndHeaders(string sheetName)
        {
            try
            {

                var headersList = new List<string>() {
                    "Offset",
                    "Code Data Offset",
                    "Code Print Offset",
                    "Text Type",
                    "Original Text",
                    "Translated Text",
                    "Comments",
                };

                var valueRange = new ValueRange();

                valueRange.Values = new List<IList<object>>();

                var row = new List<object>();

                foreach (var record in headersList)
                {
                    row.Add(record);
                }

                valueRange.Values.Add(row);

                var request = SheetsService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, $"{sheetName}!A1");
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                var response = request.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void WriteFile(string sheetName, ValueRange valueRange)
        {
            var request = SheetsService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, $"{sheetName}!A1");
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var response = request.Execute();

            Console.WriteLine($"Updated rows: {response.UpdatedRows}");
        }

        public void CreateSheetsForBinFiles()
        {
            try
            {
                int counter = 0;

                var batchUpdates = new BatchUpdateSpreadsheetRequest()
                {
                    Requests = new List<Request>() { }
                };

                string fileNameSuffix = "_OVR.bin";

                while (counter <= 61)
                {

                    string fileName = "";

                    if (counter > 9)
                    {
                        fileName = $"0{counter}{fileNameSuffix}";
                    }
                    else
                    {
                        fileName = $"00{counter}{fileNameSuffix}";
                    }

                    var creatSheetRequest = new AddSheetRequest()
                    {
                        Properties = new()
                        {
                            Title = fileName
                        }
                    };

                    batchUpdates.Requests.Add(new Request()
                    {
                        AddSheet = creatSheetRequest
                    }
                    );

                    counter++;
                }

                SheetsService.Spreadsheets.BatchUpdate(batchUpdates, SpreadsheetId).Execute();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    class CSVDataModel
    {
        public string? Pointer { get; set; }
        public string? PointerOffset { get; set; }
        public string? OriginalText { get; set; }
        public string? TranslatedText { get; set; }
    }

}