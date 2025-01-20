// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text;
using Microsoft.VisualBasic;
using System.Linq;
using Pointer.Finder;

Console.WriteLine("Starting of the Pointing Game. Lets find them ALL!");

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string folderLocation = "./OVR";

var baseOffset = 0x80158138;

var fileHelper = new FileHelper();

// var fileLocations = fileHelper.GetFilesForFolder(folderLocation);

var fileLocations = new List<string> { "./OVR/044_OVR.bin" };

var allCompleteHexes = new List<ReadFileToHexReadableStringModel>();

foreach(var item in fileLocations) {
    allCompleteHexes.Add(fileHelper.ReadFileToHexReadableString(item));
}

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

	for (var i = 0; i < foundHexPointers.Count - 1; i++)
	{
		ovrStream.Position = ConvertHexStringToUnit(foundHexPointers[i]) - baseOffset;

        var bufferSize = ConvertHexStringToUnit(foundHexPointers[i+1])- ConvertHexStringToUnit(foundHexPointers[i]);

		var buffer = new byte[bufferSize];
		ovrStream.Read(buffer);

		DumpText(buffer, ConvertHexStringToUnit(foundHexPointers[i]));
	}
}

uint ConvertHexStringToUnit(string text) {
    
    return Convert.ToUInt32(text, 16);

    // return uint.Parse(text, System.Globalization.NumberStyles.HexNumber);
}

void DumpText(byte[] buffer, uint offset)
{
	var sjis = Encoding.GetEncoding("Shift-JIS");

	var sb = new StringBuilder();
	sb.AppendLine($"0x{offset:X2};0x{buffer.Length:X4}");

	int i;
	for (i = 0; i < buffer.Length; i++)
	{
		if (buffer[i] == 0)
		{
			sb.Append($"<{buffer[i++]:00}>");
			break;
		}

		if (buffer[i] < 0x80)
		{
			if (buffer[i] < 0x20)
			{
				switch (buffer[i])
				{
					case 4: // Linebreak with button prompt, new textbox
						sb.Append($"<{buffer[i]:00}>");
						sb.AppendLine();
						continue;

					case 5: // Linebreak with button prompt
						sb.Append($"<{buffer[i]:00}>");
						sb.AppendLine();
						continue;

					case 7: // Linebreak
						sb.Append($"<{buffer[i]:00}>");
						sb.AppendLine();
						continue;

					case 11:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						continue;

					case 12:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						continue;

					case 13:
						sb.Append($"<{buffer[i++]:00}:{buffer[i++]},{buffer[i++]},{buffer[i]}>");
						continue;

					case 14:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						continue;

					case 15:
						sb.Append($"<{buffer[i++]:00}:{buffer[i++]},{buffer[i++]},{buffer[i]}>");
						continue;

					case 16:
						sb.Append($"<{buffer[i++]:00}:");

						var flag = buffer[i++] << 8 | buffer[i++];
						sb.Append($"{flag},{buffer[i]}>");
						continue;

					case 17:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						sb.AppendLine();
						continue;

					case 19:
						sb.Append($"<{buffer[i++]:00}:");
						
						var flag1 = buffer[i++] << 8 | buffer[i];
						sb.Append($"{flag1}>");
						continue;

					case 20:
						sb.Append($"<{buffer[i++]:00}:{buffer[i++]},{buffer[i]}>");
						continue;

					case 22:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						continue;

					case 24:
						sb.Append($"<{buffer[i++]:00}:{buffer[i]}>");
						continue;

					default:
						sb.Append($"<{buffer[i]:00}>");
						continue;
				}
			}

			sb.Append((char)buffer[i]);
			continue;
		}

		sb.Append(sjis.GetString(buffer[i..(i + 2)]));
		i++;
	}

    Console.Write(sb.ToString());
    Console.WriteLine("");
}