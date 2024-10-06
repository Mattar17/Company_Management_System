using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        //Upload File  
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1- Allocate Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Files", FolderName);
            //2-Get File Name and make it unique using guid
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            //3-Get File Path
            string FilePath = Path.Combine(FolderPath, FileName);
            //4- Save File as streams
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);
            //5- return filename
            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Files", FolderName, FileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
