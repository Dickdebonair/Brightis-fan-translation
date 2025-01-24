// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using System.Linq;
using Pointer.Finder;
using Pointer.Finder.Clients;
using System.Threading.Tasks;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Console.WriteLine("Starting of the Pointing Game. Lets find them ALL!");

ThreadPool.SetMaxThreads(int.MaxValue, int.MaxValue);
ThreadPool.SetMinThreads(48, 100);

ThreadPool.GetMinThreads(out var workerThreads, out var completePorThreads);

Console.WriteLine($"worker: {workerThreads}; completePorThreads: {completePorThreads}");



string folderLocation = "./OVR";

var baseOffset = 0x80158138;

var fileHelper = new FileHelper();

var translator = new DeepLClient(Environment.GetEnvironmentVariable("deeplenv") ?? "boop");

HexHelper hexHelper = new HexHelper(translator);

var fileLocations = fileHelper.GetFilesForFolder(folderLocation).ToList();

// var fileLocations = new List<string> { "./OVR/044_OVR.bin" };

var allCompleteHexes = fileHelper.ReadFiles(fileLocations);

var tasks = new List<Task>() { };

foreach (var hexFile in allCompleteHexes)
{
	var task = Task.Run( async() =>
	{
		Console.WriteLine($"Working on file {hexFile.FileName}");
		var CSVHelper = new SpreadSheetHelper();	

		var splitFileName = hexFile.FileName.Split("/");

		var sheetName = splitFileName[splitFileName.Length - 1] ?? "";

		await CSVHelper.CreateSheetAndHeaders(sheetName);

		var foundHexPointers = hexHelper.FindPointers(hexFile);

		using var ovrStream = File.OpenRead(hexFile.FileName);

		List<CSVDataModel> completedCSVFile = new List<CSVDataModel>();

		var rowCounter = 2;

		for (var i = 0; i < foundHexPointers.Count - 1; i++)
		{
			try
			{

				var convertedCurrentPosition = hexHelper.ConvertHexStringToUnit(foundHexPointers[i]);

				var convertedNextPosition = hexHelper.ConvertHexStringToUnit(foundHexPointers[i + 1]);

				ovrStream.Position = convertedCurrentPosition - baseOffset;

				var bufferSize = convertedNextPosition - convertedCurrentPosition;

				Console.WriteLine($"Current Buffer Size {bufferSize}");

				var buffer = new byte[bufferSize];

				ovrStream.Read(buffer);

				var completed = await hexHelper.DumpText(buffer, convertedCurrentPosition);

				completedCSVFile.Add(completed);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				var error = new CSVDataModel()
				{
					Pointer = foundHexPointers[i],
					PointerOffset = "",
					OriginalText = e.Message,
					TranslatedText = ""
				};

				completedCSVFile.Add(error);
			}

			rowCounter = CSVHelper.AddTranslationData(sheetName, completedCSVFile, rowCounter);
		}

		await CSVHelper.BulkUpdate();
	});

	tasks.Add(task);

	if(tasks.Count > 3) {
		await Task.WhenAll(tasks);
		tasks = new List<Task>();
	}
}

await Task.WhenAll(tasks);
Console.WriteLine("Getting all tasks completed");
