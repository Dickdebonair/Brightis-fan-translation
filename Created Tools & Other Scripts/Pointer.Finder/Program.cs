// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using System.Linq;
using Pointer.Finder;
using Pointer.Finder.Clients;
using System.Threading.Tasks;

uint ConvertHexStringToUnit(string text) {
    
    return Convert.ToUInt32(text, 16);

    // return uint.Parse(text, System.Globalization.NumberStyles.HexNumber);
}


Console.WriteLine("Starting of the Pointing Game. Lets find them ALL!");

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string folderLocation = "./OVR";

var baseOffset = 0x80158138;



var fileHelper = new FileHelper();

var translator = new DeepLClient(Environment.GetEnvironmentVariable("deeplenv") ?? "boop");

// var fileLocations = fileHelper.GetFilesForFolder(folderLocation);

var fileLocations = new List<string> { "./OVR/044_OVR.bin" };

var allCompleteHexes = fileHelper.ReadFiles(fileLocations);

var CSVHelper = new CSVHelper();

foreach(var hexFile in allCompleteHexes) {

    var foundHexPointers = new List<string>();

    var completeHex = hexFile.HexString;

    foreach (var (item, index) in completeHex.Select((value, index) => (value, index)))
    {
        if(index >= 4 && item == "80") {
            var oneHead = completeHex[index -1];

            if(oneHead == "15") {
                foundHexPointers.Add($"0x{completeHex[index]}{completeHex[index-1]}{completeHex[index-2]}{completeHex[index-3]}");
            }
        }
    }

    completeHex.OrderBy(x => x).ToArray();

	// var path = @"D:\Users\Kirito\Desktop\reverse_engineering\brightis\game_files\patch_work\original\OVR\044.bin";

    using var ovrStream = File.OpenRead(hexFile.FileName);

	List<CSVDataModel> completedCSVFile = new List<CSVDataModel>();

	HexHelper HexHelper = new HexHelper(translator);

	for (var i = 0; i < foundHexPointers.Count - 1; i++)
	{
		try {
			Console.WriteLine($"Current Buffer Position");
			ovrStream.Position = ConvertHexStringToUnit(foundHexPointers[i]) - baseOffset;

			var bufferSize = ConvertHexStringToUnit(foundHexPointers[i+1])- ConvertHexStringToUnit(foundHexPointers[i]);

			var buffer = new byte[bufferSize];
			
			ovrStream.Read(buffer);

			var completed = await HexHelper.DumpText(buffer, ConvertHexStringToUnit(foundHexPointers[i]));

			completedCSVFile.Add(completed);

		} catch(Exception e) {
			Console.WriteLine(e);
				var error = new CSVDataModel() {
					Pointer = foundHexPointers[i],
					PointerOffset = "",
					OriginalText = e.Message,
					TranslatedText = ""
				};

				completedCSVFile.Add(error);
		}
	}

	CSVHelper.CreateSheet(hexFile.FileName, completedCSVFile);
}

CSVHelper.WriteFile("./JustTryingOut.xlsx");
