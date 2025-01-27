<Query Kind="Program" />

void Main()
{
	Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

	var baseOffset = 0x80158138;
	var offsetList = new[]
	{
		0x8015c9a4,
		0x8015c9e4,
		0x8015ca04,
		0x8015cacc,
		0x8015cd50,
		0x8015cf48,
		0x8015cfd4,
		0x8015d050,
		0x8015d0c4,
		0x8015d2dc,
		0x8015d4f4,
		0x8015d5b8,
		0x8015d608,
		0x8015d730,
		0x8015d794,
		0x8015d8b4,
		0x8015d928,
		0x8015d970,
		0x8015dab0,
		0x8015db5c,
		0x8015dbfc,
		0x8015dca8,
		0x8015dcc4,
		0x8015dd84,
		0x8015de60,
		0x8015df2c,
		0x8015dfa0,
		0x8015e0bc,
		0x8015e1e0,
		0x8015e228,
		0x8015e300,
		0x8015e350,
		0x8015e3dc,
		0x8015e590,
		0x8015e770,
		0x8015e7dc,
		0x8015e870,
		0x8015e8e8,
		0x8015e918,
		0x8015e960,
		0x8015eb3c,
		0x8015eb4c,
		0x8015eb53
	};

	var path = @"D:\Users\Kirito\Desktop\reverse_engineering\brightis\game_files\patch_work\original\OVR\008.bin";
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