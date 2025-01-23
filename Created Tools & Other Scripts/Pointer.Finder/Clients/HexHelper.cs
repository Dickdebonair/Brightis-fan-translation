using System.Text;

namespace Pointer.Finder.Clients
{
    class HexHelper
    {
        public DeepLClient Translator { get; }

        public HexHelper(DeepLClient translator)
        {
            Translator = translator;
        }

        public async Task<CSVDataModel> DumpText(byte[] buffer, uint offset)
        {
            var sjis = Encoding.GetEncoding("Shift-JIS");

            var pointerHex = $"0x{offset:X2}";
            var bufferLength = $"0x{buffer.Length:X4}";

            var sb = new StringBuilder();
            int i;
            
            for (i = 0; i < buffer.Length; i ++)
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
            var translatedText = new DeepL.Model.TextResult[] {
                new DeepL.Model.TextResult("There was no text to translate", "", 0, null)
            };

            if(sb.Length > 0) {
                translatedText = await Translator.JISToEnglish(sb.ToString());
            }

            var CSVData = new CSVDataModel()
            {
                Pointer = pointerHex,
                PointerOffset = bufferLength,
                OriginalText = sb.ToString(),
                TranslatedText = translatedText[0].Text
            };

            Console.WriteLine($"pointerHex: {pointerHex} ===== bufferLenght: {bufferLength}");

            Console.WriteLine(sb.ToString());

            Console.WriteLine(translatedText[0].Text);
            Console.WriteLine("");
            return CSVData;
        }

        public uint ConvertHexStringToUnit(string text) {
    
            return Convert.ToUInt32(text, 16);

            // return uint.Parse(text, System.Globalization.NumberStyles.HexNumber);
        }

        public List<string> FindPointers(ReadFileToHexReadableStringModel hexFile) {

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

            return foundHexPointers.OrderBy(ConvertHexStringToUnit).ToList();
        }
    }
}