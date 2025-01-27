// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string fileName = "blah";

using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName,false)) {
    using (FileStream fs = File.OpenRead(fileName)) 
    {
        byte[] b = new byte[50];

        while (fs.Read(b,0,b.Length) > 0) 
        {
            writer.WriteLine(BitConverter.ToString(b).Replace("-", " "));
        }
    }
}