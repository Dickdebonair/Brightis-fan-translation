using BrightisCompressor;
using System;
using System.Diagnostics;
using System.IO;



namespace BrightisCompressor
{
    class Program
    {
        static void Main(string[] args)
        {

            MemoryStream input = new MemoryStream();
            //string fileName = "OVR_93800";

            if (args.Length > 0)
            {
                string action = args[0];

                if ((action == "-d") && (args.Length >= 3))
                {
                    string sourceFile = args[1];
                    string decompressedFile = args[2];

                    if (File.Exists(sourceFile))
                    {
                        using (FileStream file = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                        {
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            input.Write(bytes, 0, (int)file.Length);
                        }

                        int offset = 0;
                        if (args.Length == 4)
                        {
                            offset = Convert.ToInt32(args[3], 16);
                        }
                        Compressor.Decompress(input, decompressedFile, offset);
                    }
                    else
                    {
                        Console.WriteLine($"{sourceFile} is not found on disc");
                    }
                    

                }
                else if ((action == "-c") && (args.Length > 3))
                {
                    string decompressedFile = args[1];
                    string compressedFile = args[2];

                    if (File.Exists(decompressedFile))
                    {
                        using (FileStream file = new FileStream(decompressedFile, FileMode.Open, FileAccess.Read))
                        {
                            byte[] bytes = new byte[file.Length];
                            file.Read(bytes, 0, (int)file.Length);
                            input.Write(bytes, 0, (int)file.Length);
                        }

                        Compressor.Compress(input, compressedFile);
                    }
                    else
                    {
                        Console.WriteLine($"{decompressedFile} is not found on disc");
                    }

                }
                else
                {
                    Console.WriteLine("You need to use either action -d or -c\n\nEx:" +
                        "\n1) BrightisComp -d comp.bin dec.bin" +
                        "\n2) BrightisComp -d comp.bin dec.bin 9C800" +
                        "\n3) BrightisComp -c dec.bin comp.bin");
                }


                
            }

        }
    }
}
