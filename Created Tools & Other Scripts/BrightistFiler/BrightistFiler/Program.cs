using BrightistFiler;
using BrightistFiler.Formats.Archives;
using BrightistFiler.Formats.Archives.Models;
using Kontract.Models;

// Read arguments
OperationMode operationMode = GetOperationMode();
string inputPath = GetInputPath(operationMode);

switch (operationMode)
{
    case OperationMode.Extract:
        Extract();
        break;

    case OperationMode.Create:
        Create();
        break;
}

#region Arguments

OperationMode GetOperationMode()
{
    if (args.Length < 1)
    {
        Console.WriteLine("No operation mode given.\n  Use '-e' to extract the file or '-c' to recreate it from a dictionary.");
        Environment.Exit(0);
    }

    if (args[0] is not "-c" and not "-e")
    {
        Console.WriteLine("Unknown operation mode.\n  Use '-e' to extract a file or '-c' to recreate it from a dictionary.");
        Environment.Exit(0);
    }

    return args[0] switch
    {
        "-c" => OperationMode.Create,
        "-e" => OperationMode.Extract,
        _ => throw new ArgumentOutOfRangeException(nameof(args))
    };
}

string GetInputPath(OperationMode mode)
{
    if (args.Length < 2)
    {
        Console.WriteLine("No input path given.\n  For '-e' provide an existing file and for '-c' provide an existing dictionary.");
        Environment.Exit(0);
    }

    if (mode is OperationMode.Create && !Directory.Exists(args[1]))
    {
        Console.WriteLine("No input directory given.\n  For '-e' provide an existing file and for '-c' provide an existing dictionary.");
        Environment.Exit(0);
    }
    else if (mode is OperationMode.Extract && !File.Exists(args[1]))
    {
        Console.WriteLine("No input file given.\n  For '-e' provide an existing file and for '-c' provide an existing dictionary.");
        Environment.Exit(0);
    }

    return Path.GetFullPath(args[1]);
}

#endregion

#region Extract

void Extract()
{
    using Stream inputStream = File.OpenRead(inputPath);
    var streamFile = new StreamFile(inputStream, inputPath);

    // Read files from archive
    var archiveTypeReader = new ArchiveTypeReader();
    ArchiveType archiveType = archiveTypeReader.Determine(streamFile);

    if (archiveType is ArchiveType.None)
    {
        Console.WriteLine($"Unsupported file {streamFile.Path}.");
        return;
    }

    if (archiveType is not ArchiveType.Ovr and not ArchiveType.OnMovR)
    {
        Console.WriteLine($"Unsupported file {streamFile.Path} ({archiveType}).");
        return;
    }

    var archiveReader = new ArchiveReader();
    if (!archiveReader.TryRead(streamFile, archiveType, out ArchiveData? archiveData))
    {
        Console.WriteLine($"Unknown file {streamFile.Path}.");
        return;
    }

    // Extract files to dictionary
    string outputFolder = Path.GetDirectoryName(streamFile.Path.FullName)!;
    outputFolder = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(streamFile.Path.FullName));

    foreach (ArchiveFileData archiveFile in archiveData.Files)
        ExtractArchiveFile(archiveFile, outputFolder);
}

void ExtractArchiveFile(ArchiveFileData archiveFile, string outputPath)
{
    switch (archiveFile)
    {
        case SingleArchiveFileData singleArchiveFile:
            ExtractSingleArchiveFile(singleArchiveFile, outputPath);
            break;

        case MultiArchiveFileData multiArchiveFile:
            ExtractMultiArchiveFile(multiArchiveFile, outputPath);
            break;
    }
}

void ExtractSingleArchiveFile(SingleArchiveFileData archiveFile, string outputPath)
{
    if (!Directory.Exists(outputPath))
        Directory.CreateDirectory(outputPath);

    outputPath = Path.Combine(outputPath, $"{archiveFile.Index:000}");
    switch (archiveFile.Type)
    {
        case ArchiveFileType.Psm:
            outputPath += ".psm";
            break;

        case ArchiveFileType.Tex:
            outputPath += ".tex";
            break;

        case ArchiveFileType.Img:
            outputPath += ".img";
            break;

        default:
            outputPath += ".bin";
            break;
    }

    using Stream outputStream = File.Create(outputPath);
    archiveFile.Data.CopyTo(outputStream);
}

void ExtractMultiArchiveFile(MultiArchiveFileData archiveFile, string outputPath)
{
    outputPath = Path.Combine(outputPath, $"{archiveFile.Index:000}");

    foreach (SingleArchiveFileData singleArchiveFile in archiveFile.Files)
        ExtractSingleArchiveFile(singleArchiveFile, outputPath);
}

#endregion

#region Create

void Create()
{
    var archiveTypeReader = new ArchiveTypeReader();
    ArchiveType archiveType = archiveTypeReader.Determine(inputPath);

    if (archiveType is ArchiveType.None)
    {
        Console.WriteLine($"Unsupported directory {inputPath}.");
        return;
    }

    if (archiveType is not ArchiveType.Ovr and not ArchiveType.OnMovR)
    {
        Console.WriteLine($"Unsupported directory {inputPath} ({archiveType}).");
        return;
    }

    // Read files from dictionary
    ArchiveFileData[] files = ReadFiles(inputPath);

    // Write output
    var archiveData = new ArchiveData
    {
        Type = archiveType,
        Files = files
    };

    using Stream outputStream = File.Create(inputPath + ".BIN");

    var archiveWriter = new ArchiveWriter();
    archiveWriter.Write(archiveData, outputStream);
}

ArchiveFileData[] ReadFiles(string inputDirectory)
{
    var result = new List<ArchiveFileData>();

    result.AddRange(ReadSingleFiles(inputDirectory));
    result.AddRange(ReadDirectoryFiles(inputDirectory));

    return result.OrderBy(x => x.Index).ToArray();
}

MultiArchiveFileData[] ReadDirectoryFiles(string inputDirectory)
{
    var result = new List<MultiArchiveFileData>();

    foreach (string directoryPath in Directory.GetDirectories(inputDirectory, "*", SearchOption.TopDirectoryOnly))
    {
        if (!TryGetDirectoryIndex(directoryPath, out int index))
            continue;

        result.Add(new MultiArchiveFileData
        {
            Index = index,
            Files = ReadSingleFiles(directoryPath)
        });
    }

    return result.OrderBy(x => x.Index).ToArray();
}

SingleArchiveFileData[] ReadSingleFiles(string inputDirectory)
{
    var result = new List<SingleArchiveFileData>();

    foreach (string filePath in Directory.GetFiles(inputDirectory, "*", SearchOption.TopDirectoryOnly))
    {
        if (!TryGetFileIndex(filePath, out int index))
            continue;

        result.Add(new SingleArchiveFileData
        {
            Index = index,
            Type = GetFileType(filePath),
            Data = File.OpenRead(filePath)
        });
    }

    return result.OrderBy(x => x.Index).ToArray();
}

bool TryGetFileIndex(string filePath, out int index)
{
    index = 0;

    string fileName = Path.GetFileNameWithoutExtension(filePath);
    if (fileName.Length < 3)
        return false;

    string indexString = fileName[..3];
    return int.TryParse(indexString, out index);
}

bool TryGetDirectoryIndex(string directoryPath, out int index)
{
    index = 0;

    string directoryName = Path.GetDirectoryName(directoryPath)!;
    if (directoryName.Length < 3)
        return false;

    string indexString = directoryName[..3];
    return int.TryParse(indexString, out index);
}

ArchiveFileType GetFileType(string filePath)
{
    switch (Path.GetExtension(filePath))
    {
        case ".psm":
            return ArchiveFileType.Psm;

        case ".tex":
            return ArchiveFileType.Tex;

        case ".img":
            return ArchiveFileType.Img;

        default:
            return ArchiveFileType.None;
    }
}

#endregion
