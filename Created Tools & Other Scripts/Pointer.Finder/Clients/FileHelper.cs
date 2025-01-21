
namespace Pointer.Finder.Clients {

    class FileHelper {

        public string[] GetFilesForFolder(string folderLocation) {

            string directoryPath = folderLocation;
            return Directory.GetFiles(directoryPath, "*.bin", SearchOption.AllDirectories);  
        }

        public ReadFileToHexReadableStringModel ReadFileToHexReadableString(string fileLocation) {

            FileStream fs = new FileStream(fileLocation, FileMode.Open);
            int hexIn;
            var completeHex = new List<string>();

            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++){

                completeHex.Add(string.Format("{0:X2}", hexIn));
            }

            return new ReadFileToHexReadableStringModel() { FileName = fileLocation, HexString = completeHex };
        }

        public List<ReadFileToHexReadableStringModel> ReadFiles(List<string> fileLocations) {

            var allCompleteHexes = new List<ReadFileToHexReadableStringModel>();  

            foreach(var item in fileLocations) {
                allCompleteHexes.Add(ReadFileToHexReadableString(item));
            }

            return allCompleteHexes;
        }
    }

    class ReadFileToHexReadableStringModel {

        public string FileName { get; set; }

        public List<string> HexString { get; set; }

    }

}