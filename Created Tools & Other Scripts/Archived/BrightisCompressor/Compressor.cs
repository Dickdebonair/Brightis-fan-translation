using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace BrightisCompressor
{
    public class Compressor
    {


        public static byte[] ExtractRawBytes(ref MemoryStream source, int nbRawBytes)
        {
            byte[] res = new byte[nbRawBytes];

            for (int i = 0; i < nbRawBytes; i++)
            {
                res[i] = (byte)source.ReadByte();
            }
            return res;
        }

        public static byte[] ExtractRepeat(ref MemoryStream source, int element, int nbRepeat)
        {
            byte[] res = new byte[nbRepeat];

            for (int i =0; i < nbRepeat; i++)
            {
                res[i] = (byte)element;
            }
            return res;
        }

        public static byte[] ExtractSlidingWindow(ref MemoryStream dest, int distance, int size)
        {
            byte[] res = new byte[size];
            dest.Position -= distance;

            for (int i=0;i < size; i++)
            {
                res[i] = (byte)dest.ReadByte();
            }
            return res;
        }

       
        public static void Decompress(MemoryStream source, string dest_file, int offset = 0)
        {

            MemoryStream dest = new MemoryStream();
            source.Position = offset;
            int currentVal = source.ReadByte();

            while (currentVal != 0)
            {
                int nbRawBytes = currentVal;

                if ((currentVal & 0x80) == 0)
                {

                    
                    if ((currentVal & 0x40) == 0)
                    {
                        Console.WriteLine($"Raw - Pos: {(source.Position - 1).ToString("X")} - Byte1: {currentVal.ToString("X")} - Dest pos: {dest.Position.ToString("X")}");
                        //Extract Raw Bytes

                        if ((source.Position - 1) == 0x64)
                        {
                            int t = 2;
                        }

                        if ((currentVal & 0x20) == 0)
                        {
                            nbRawBytes = currentVal & 0x1F;
                        }
                        else
                        {
                            currentVal = source.ReadByte();
                            nbRawBytes = currentVal | (nbRawBytes & 0x1F) << 8;
                        }

                        
                        if (nbRawBytes > 0)
                        {
                            byte[] res = ExtractRawBytes(ref source, nbRawBytes);
                            dest.Write(res, 0, res.Count());
                        }
                    }

                    //Extract Repeat
                    else
                    {
                        Console.WriteLine($"Repeat - Pos: {(source.Position - 1).ToString("X")} - Byte1: {currentVal.ToString("X")} - Dest pos: {dest.Position.ToString("X")}");
                        nbRawBytes = nbRawBytes & 0xF;

                        if ((currentVal & 0x10) != 0)
                        {
                            currentVal = source.ReadByte();
                            nbRawBytes = currentVal | nbRawBytes << 8;
                        }

                        nbRawBytes += 4;
                        currentVal = source.ReadByte();

                        if (nbRawBytes > 0)
                        {
                            byte[] res = ExtractRepeat(ref source, currentVal, nbRawBytes);
                            dest.Write(res, 0, res.Count());
                        }
                    }
                }

                //Extract Sliding Window
                else
                {
                    List<byte> sliding = new List<byte>();
                    Console.WriteLine($"Sliding - Pos: {(source.Position - 1).ToString("X")} - Byte1: {currentVal.ToString("X")} - Dest pos: {dest.Position.ToString("X")}");
                    int size = currentVal >> 5 | 4;
                    currentVal = source.ReadByte();
                    Console.WriteLine($"Sliding - Pos: {(source.Position - 1).ToString("X")} - Byte2: {currentVal.ToString("X")}");
                    int distance = currentVal | (nbRawBytes & 0x1F) << 8;

                    long refDestPosition = dest.Position;
                    int totalSize = size;
                    sliding.AddRange(ExtractSlidingWindow(ref dest, distance, size));

                    currentVal = source.ReadByte();

                    if ((currentVal & 0xE0) == 0x60)
                    {
                        while ((currentVal & 0xE0) == 0x60)
                        {
                            nbRawBytes = currentVal & 0x1F;
                            Console.WriteLine($"Sliding+ - Pos: {(source.Position - 1).ToString("X")} - Byte2: {currentVal.ToString("X")}");
                            if (nbRawBytes > 0)
                            {
                                sliding.AddRange(ExtractRawBytes(ref dest, nbRawBytes));
                                totalSize += size;
                            }
                            currentVal = source.ReadByte();

                        
                        }
                    }
                    source.Position -= 1;



                    dest.Position = refDestPosition;
                    dest.Write(sliding.ToArray(), 0, sliding.Count());

                }

                currentVal = source.ReadByte();
            }

            //Write the final file
            using (FileStream file = new FileStream(dest_file, FileMode.Create, System.IO.FileAccess.Write))
                dest.WriteTo(file);
        }

        /* Compress */
        static MemoryStream compressedData = new MemoryStream();
        static List<byte> currentCompressedData = new List<byte>();
        static int type;
        static List<string> resDebug = new List<string>();
        const int RAW = 0;
        const int SLIDING = 1;
        const int REPEAT = 2;
        public static string logcompressed = "Log for compression\n";
        private static void InitCompression()
        {
            compressedData = new MemoryStream();
            currentCompressedData = new List<byte>();
            type = RAW;
        }

        public static byte[] GetEmbedRaw(uint nbBytes)
        {

            List<byte> res = new List<byte>();
            if (nbBytes < 0x20)
            {
                res.Add((byte)nbBytes);
                logcompressed += $"Raw Embed: ({nbBytes.ToString("X")})\n";
            }
            else
            {
                uint second = nbBytes % 256;
                uint first = 0x20 + nbBytes / 256;
                res.Add((byte)first);
                res.Add((byte)second);
                logcompressed += $"Raw Embed: ({first.ToString("X")},{second.ToString("X")})\n";
            }


            return res.ToArray();

        }

        public static byte[] GetEmbedSliding(int distance, int size)
        {
            int first = 128;
            int second;

            if (distance < 256)
            {
                second = distance;
            }
            else
            {
                second = distance % 256;
                first += distance / 256;
            }

            int ct = 0;
            while (size != (first >> 5 | 4))
            {
                first += 32;
                ct++;
            }

            byte[] results = new byte[2];
            results[0] = (byte)first;
            results[1] = (byte)second;

            logcompressed += $"Sliding Embed: ({first.ToString("X")}, {second.ToString("X")}) - Size:{size} - Distance: {distance}\n";

            if ((first == 0x9E) && (second == 0x6F))
            {
                int t = 2;
            }


            return results;

        }

        public static byte[] GetEmbedSlidingPlus(int size)
        {
            List<byte> res = new List<byte>();
            int mult_31 = size / 31;
            int rest = size % 31;

            for (int i = 1; i < mult_31 + 1; i++)
            {
                res.Add((byte)(31 + 96));
                logcompressed += $"Sliding+: ({(31 + 96).ToString("X")}), size:{(31 + 96) & 0x1F}\n";
            }

            if (rest > 0)
            {
                res.Add((byte)(rest + 96));
                logcompressed += $"Sliding+: ({(rest + 96).ToString("X")}), size:{(rest + 96) & 0x1F}\n";
            }

            return res.ToArray();
        }

        public static byte[] GetEmbedRepeat(int repeat, int repeatedValue)
        {
            uint first;
            repeat -= 4;
            uint second = (uint)(repeat) % 256;
            uint div = (uint)(repeat) / 256;
            List<byte> res = new List<byte>();

            if (repeat >= 16)
            {
                first = 0x50;
                if (div > 0)
                {
                    first = 0x50 + div;
                }

                res.Add((byte)first);
                res.Add((byte)second);
                res.Add((byte)repeatedValue);
                logcompressed += $"Repeat: ({first.ToString("X")}, {second.ToString("X")}), Value:{repeatedValue.ToString("X")}, Size: {repeat}\n";
            }
            else
            {
                first = 0x40 + (uint)repeat;
                res.Add((byte)first);
                res.Add((byte)repeatedValue);
                logcompressed += $"Repeat: ({first.ToString("X")}), Value:{repeatedValue.ToString("X")}, Size: {repeat}\n";

            }

            return res.ToArray();

        }

        public static void WritePreviousRaw(ref MemoryStream compressedData, int typeCompare)
        {
            if ((type != typeCompare) && (currentCompressedData.Count > 0))
            {
                //Write the raw bytes
                byte[] res = GetEmbedRaw((uint)currentCompressedData.Count);
                compressedData.Write(res, 0, res.Length);
                compressedData.Write(currentCompressedData.ToArray(), 0, currentCompressedData.Count);
                if (compressedData.Position >= 0xA3)
                {
                    long t = compressedData.Position;
                }

                logcompressed += $"--- {string.Join(", ", Array.ConvertAll(currentCompressedData.ToArray(), x => x.ToString("X")))}";
                logcompressed += "\n";
                type = typeCompare;
                currentCompressedData.Clear();
            }

        }

        public static void HandleSliding(ref MemoryStream compressedData, int distance, int size)
        {


            int size_7 = size > 7 ? size - 7 : size;
            int first_size = size > 7 ? 7 : size;
            byte[] res = GetEmbedSliding(distance, first_size);
            compressedData.Write(res, 0, res.Length);


            if (size > 7)
            {
                res = GetEmbedSlidingPlus(size_7);
                compressedData.Write(res, 0, res.Length);

            }



        }

        public static void Pad16(ref MemoryStream compressedData)
        {
            long length = compressedData.Length;

            long rest = length % 4;
            long added_zeroes = 4 - rest;

            for (int i = 0; i < added_zeroes; i++)
                compressedData.WriteByte(0x00);
        }
        public static void Compress(MemoryStream data, string compressedFile)
        {

            InitCompression();

            var decompressedSize = (uint)data.Length;
            byte[] decompressedData = data.ToArray();

            uint sourcePointer = 0x0;

            // Test if the file is too large to be compressed
            if (data.Length > 0xFFFFFF)
                throw new Exception("Input file is too large to compress.");

            // Set up the Lz Compression Dictionary
            var lzDictionary = new LzWindowDictionary();
            //lzDictionary.SetWindowSize(0x7FF);
            lzDictionary.SetWindowSize(0x2000);
            lzDictionary.SetMinMatchAmount(4);
            //lzDictionary.SetMaxMatchAmount(0x1F + 2);
            lzDictionary.SetMaxMatchAmount(1000);

            // Start compression
            while (sourcePointer < decompressedSize)
            {

                if (compressedData.Position == 0x16D)
                {
                    int t = 2;
                }

                //Console.WriteLine($"Position: {sourcePointer.ToString("X")}");
                var lzSearchMatch = lzDictionary.Search(decompressedData, sourcePointer, decompressedSize);
                var lzRepetition = lzDictionary.SearchRepetition(decompressedData, sourcePointer, decompressedSize);

                //lzSearchMatch[0] - Position
                //lzSearchMatch[1] - Match
                if ((lzSearchMatch[1] > lzRepetition) && (lzSearchMatch[1] > 0)) // There is a compression match
                {
                    //Console.WriteLine($"Current pos: {sourcePointer.ToString("X")} - Ref Pos Found: {(sourcePointer - lzSearchMatch[0]).ToString("X")} - Size: {lzSearchMatch[1]}");
                    WritePreviousRaw(ref compressedData, SLIDING);

                    int distance = lzSearchMatch[0];
                    int size = lzSearchMatch[1];
                    logcompressed += $"Source pos: {sourcePointer.ToString("X")}\n";
                    HandleSliding(ref compressedData, distance, size);
                    resDebug.Add($"Sliding - Pos: {sourcePointer.ToString("X")} Dist:{distance} - size:{size}");

                    lzDictionary.AddEntryRange(decompressedData, (int)sourcePointer, size);
                    lzDictionary.SlideWindow(size);

                    sourcePointer += (uint)size;

                }
                else if (lzRepetition >= 4)
                {
                    WritePreviousRaw(ref compressedData, REPEAT);
                    int adjustedSize = lzRepetition;

                    if (lzRepetition >= 30000)
                        adjustedSize = 30000;

                    byte[] res = GetEmbedRepeat(adjustedSize, decompressedData[sourcePointer]);
                    compressedData.Write(res, 0, res.Length);
                    lzDictionary.AddEntryRange(decompressedData, (int)sourcePointer, adjustedSize);
                    lzDictionary.SlideWindow(adjustedSize);

                    sourcePointer += (uint)adjustedSize;
                }
                else // There wasn't a match, Write decompressed informartion
                {
                    type = RAW;
                    currentCompressedData.Add(decompressedData[sourcePointer]);
                    lzDictionary.AddEntry(decompressedData, (int)sourcePointer);
                    lzDictionary.SlideWindow(1);
                    sourcePointer++;
                }

                // Check for out of bounds
                if (sourcePointer >= decompressedSize)
                    break;
            }

            //Pad
            Pad16(ref compressedData);

            //Write Debug
            File.WriteAllText($"log_{compressedFile}.txt", logcompressed);

            //Write final file
            using (FileStream file = new FileStream(compressedFile, FileMode.Create, System.IO.FileAccess.Write))
                compressedData.WriteTo(file);

        }
    }

    class LzWindowDictionary
    {
        int _windowSize = 0x1000;
        int _windowStart;
        int _windowLength;
        int _minMatchAmount = 3;
        int _maxMatchAmount = 18;
        int _blockSize;
        readonly List<int>[] _offsetList;

        public LzWindowDictionary()
        {
            // Build the offset list, so Lz compression will become significantly faster
            _offsetList = new List<int>[0x100];
            for (var i = 0; i < _offsetList.Length; i++)
                _offsetList[i] = new List<int>();
        }

        public int SearchRepetition(byte[] decompressedData, uint offset, uint decompressedSize)
        {
            int size = 1;
            int maxRep = 2000;

            if (offset < (decompressedSize - 4))
            {

                int value = decompressedData[offset];
                int it = 0;
                while ((offset + size < decompressedSize) && (value == decompressedData[offset + size]) && (size < maxRep))
                {
                    size++;
                    it++;
                }
                //Console.WriteLine($"Iteration: {it}");

            }

            return size;
        }

        public int[] Search(byte[] decompressedData, uint offset, uint length)
        {
            RemoveOldEntries(decompressedData[offset]); // Remove old entries for this index

            if (offset < _minMatchAmount || length - offset < _minMatchAmount) // Can't find matches if there isn't enough data
                return new[] { 0, 0 };

            // Start finding matches
            var match = new[] { 0, 0 };

            for (var i = _offsetList[decompressedData[offset]].Count - 1; i >= 0; i--)
            {
                var matchStart = _offsetList[decompressedData[offset]][i];
                var matchSize = 1;

                while (matchSize < _maxMatchAmount &&
                    matchSize < _windowLength &&
                    matchStart + matchSize < offset &&
                    offset + matchSize < length &&
                    decompressedData[offset + matchSize] == decompressedData[matchStart + matchSize])
                    matchSize++;

                if (matchSize >= _minMatchAmount && matchSize > match[1]) // This is a good match
                {
                    match = new[] { (int)(offset - matchStart), matchSize };

                    if (matchSize == _maxMatchAmount) // Don't look for more matches
                        break;
                }
            }

            // Return the match.
            // If no match was made, the distance & length pair will be zero
            return match;
        }

        // Slide the window
        public void SlideWindow(int amount)
        {
            if (_windowLength == _windowSize)
                _windowStart += amount;
            else
            {
                if (_windowLength + amount <= _windowSize)
                    _windowLength += amount;
                else
                {
                    amount -= (_windowSize - _windowLength);
                    _windowLength = _windowSize;
                    _windowStart += amount;
                }
            }
        }

        // Slide the window to the next block
        public void SlideBlock()
        {
            _windowStart += _blockSize;
        }

        // Remove old entries
        private void RemoveOldEntries(byte index)
        {
            for (var i = 0; i < _offsetList[index].Count;) // Don't increment i
            {
                if (_offsetList[index][i] >= _windowStart)
                    break;
                _offsetList[index].RemoveAt(0);
            }
        }

        // Set variables
        public void SetWindowSize(int size)
        {
            _windowSize = size;
        }
        public void SetMinMatchAmount(int amount)
        {
            _minMatchAmount = amount;
        }
        public void SetMaxMatchAmount(int amount)
        {
            _maxMatchAmount = amount;
        }
        public void SetBlockSize(int size)
        {
            _blockSize = size;
            _windowLength = size; // The window will work in blocks now
        }

        // Add entries
        public void AddEntry(byte[] decompressedData, int offset)
        {
            _offsetList[decompressedData[offset]].Add(offset);
        }
        public void AddEntryRange(byte[] decompressedData, int offset, int length)
        {
            for (int i = 0; i < length; i++)
                AddEntry(decompressedData, offset + i);
        }

        public int windowPosition
        {
            get { return _windowLength; }
        }
    }
}
