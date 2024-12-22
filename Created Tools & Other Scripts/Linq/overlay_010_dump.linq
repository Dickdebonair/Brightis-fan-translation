<Query Kind="Program" />

void Main()
{
	Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

	var baseOffset = 0x80158138;
	var offsetList = new[]
	{
		0x80158138,
		0x80158148,
		0x80158158,
		0x80158163
	};

	var path = @"D:\Users\Kirito\Desktop\reverse_engineering\brightis\game_files\patch_work\original\OVR\010.bin";
	using var ovrStream = File.OpenRead(path);

	for (var i = 0; i < offsetList.Length - 1; i++)
	{
		ovrStream.Position = offsetList[i] - baseOffset;

		var buffer = new byte[offsetList[i + 1] - offsetList[i]];
		ovrStream.Read(buffer);

		DumpText(buffer, offsetList[i]);
	}
}

void DumpText(byte[] buffer, uint offset)
{
	var sjis = Encoding.GetEncoding("Shift-JIS");

	var sb = new StringBuilder();
	sb.AppendLine($"0x{offset:X8};0x{buffer.Length:X4}");

	int i = 0;
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

	sb.ToString().Dump();
	"".Dump();
}