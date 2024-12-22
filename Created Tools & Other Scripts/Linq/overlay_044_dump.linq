<Query Kind="Program" />

void Main()
{
	Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

	var baseOffset = 0x80158138;
	var offsetList = new[]
	{
		0x80158138,
		0x8015830c,
		0x80158400,
		0x80158574,
		0x801585e8,
		0x80158658,
		0x801586c8,
		0x80158748,
		0x80158908,
		0x8015899c,
		0x80158a1c,
		0x80158bd8,
		0x80158bec,
		0x80158c18,
		0x80158cb4,
		0x80158da8,
		0x80158eb4,
		0x80158eec,
		0x801592c4,
		0x80159428,
		0x801595e8,
		0x80159810,
		0x80159834,
		0x80159ccc,
		0x80159e70,
		0x80159ff8,
		0x8015a048,
		0x8015a148,
		0x8015a550,
		0x8015aef4,
		0x8015b344,
		0x8015b62c,
		0x8015b884,
		0x8015b8c4,
		0x8015b954,
		0x80159cec,
		0x801597b0,
		0x8015a514,
		0x8015b30c,
		0x8015a2fc,
		0x8015905c,
		0x8015BCA8
	};

	offsetList = offsetList.OrderBy(x => x).ToArray();

	var path = @"D:\Users\Kirito\Desktop\reverse_engineering\brightis\game_files\patch_work\original\OVR\044.bin";
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

	sb.ToString().Dump();
	"".Dump();
}