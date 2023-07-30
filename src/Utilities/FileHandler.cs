using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CleanArchitectureSample.Utilities
{
    public partial class FileHandler
    {
        private readonly static Encoding DefaultEncoding = Encoding.UTF8; //Encoding.UTF8;
        /// <summary>
        /// Don't have suffix '/'.
        /// </summary>
        /// <returns></returns>
        public static string GetAppDefaultDirectory()
        {
            //var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return basePath?.TrimEnd('/').TrimEnd('\\');
        }
        /// <summary>
        /// return as 'C' letter
        /// </summary>
        /// <returns></returns>
        public static string GetSystemDisk()
        {
            return Environment.SystemDirectory.Substring(0, 1);
        }
        /// <summary>
        /// return as 'C' letter
        /// </summary>
        /// <returns></returns>
        public static string GetAppInstallDisk()
        {
            return Environment.CurrentDirectory.Substring(0, 1);
        }
        public static void Write(string filePath, string text)
        {
            Write(filePath, text, DefaultEncoding);
        }
        public static void Write(string filePath, string text, Encoding encoding)
        {
            CreateParentDirectory(filePath);
            byte[] dataBuffer = encoding.GetBytes(text);
            WriteAllBytes(filePath, dataBuffer);
        }
        public static void WriteAllBytes(string filePath, byte[] dataBuffer)
        {
            CreateParentDirectory(filePath);
            if (dataBuffer == null)
            {
                WriteAllBytes(filePath, new byte[0]);
            }
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(dataBuffer, 0, dataBuffer.Length);
            }
        }
        public static void AppendAllText(string filePath, string text)
        {
            AppendAllText(filePath, text, DefaultEncoding);
        }
        public static void AppendAllText(string filePath, string text, Encoding encoding)
        {
            byte[] dataBuffer = encoding.GetBytes(text);
            AppendDataBuff(filePath, dataBuffer);
        }
        public static void AppendDataBuff(string filePath, byte[] dataBuffer)
        {
            if (dataBuffer == null)
                return;
            CreateParentDirectory(filePath);
            using (var fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(dataBuffer, 0, dataBuffer.Length);
            }
        }
        public static void CreateDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }
        public static string GetParentDirectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
        public static void CreateParentDirectory(string filePath)
        {
            string parentDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(parentDirectory))
            {
                Directory.CreateDirectory(parentDirectory);
            }
        }
        public static string GetFileFullPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
            var path = filePath.TrimStart('/').TrimStart('\\');
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            else
            {
                string appDir = GetAppDefaultDirectory();
                return appDir + "\\" + path;
            }
        }
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static void CopyFile(string srcFilePath, string destFilePath, bool overwriteIfExist = true)
        {
            if (File.Exists(srcFilePath))
            {
                CreateParentDirectory(destFilePath);
                File.Copy(srcFilePath, destFilePath, overwriteIfExist);
            }
        }
        public static void MoveFile(string srcFilePath, string destFilePath, bool overwriteIfExist = true)
        {
            if (File.Exists(srcFilePath))
            {
                CreateParentDirectory(destFilePath);
                File.Move(srcFilePath, destFilePath, overwriteIfExist);
            }
        }
        public static string GetTempFileName(string fileExtension = null)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                return Path.GetTempFileName();
            }
            else
            {
                var fileName = Path.GetTempFileName();
                string tempFileName = fileName + "." + fileExtension.TrimStart('.');
                File.Move(fileName, tempFileName);
                return tempFileName;
            }
        }
        public static string GetFolerPath(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath)?.ToLower();
        }
        public static string GetFileNameWithExtension(string filePath)
        {
            return Path.GetFileName(filePath)?.ToLower();
        }
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath)?.ToLower();
        }
        public static string ReadAllText(string filePath)
        {
            return ReadAllText(filePath, DefaultEncoding);
        }
        public static string ReadAllText(string filePath, Encoding encoding)
        {
            byte[] buffer = ReadAllBytes(filePath);
            if (buffer == null)
            {
                return null;
            }
            return encoding.GetString(buffer);
        }
        public static byte[] ReadAllBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        public static byte[] ReadBytes(string filePath, int offset, int count, bool returnNullIfLessCount = false)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (returnNullIfLessCount)
                {
                    if (fs.Length < (offset + count))
                    {
                        return null;
                    }
                }
                byte[] buffer = new byte[count];
                fs.Read(buffer, offset, count);
                return buffer;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        public static byte[] ReadBytes(FileStream fs, long offset, int length)
        {
            byte[] dataBuff = new byte[length];
            fs.Seek(offset, SeekOrigin.Begin);
            fs.Read(dataBuff, 0, length);
            return dataBuff;
        }
        public static string[] ReadAllLines(string filePath)
        {
            return ReadAllLines(filePath, DefaultEncoding);
        }
        public static string[] ReadAllLines(string filePath, Encoding encoding)
        {
            if (!File.Exists(filePath))
            {
                return new string[0];
            }
            var text = ReadAllText(filePath, encoding);
            if (text != null)
            {
                List<string> lines = new List<string>();
                using (StringReader reader = new StringReader(text))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines.ToArray();
            }
            return new string[0];
        }
        public static List<string> GetSubFiles(string dirPath, string fileExtension = null)
        {
            List<string> result = new List<string>();
            if (!Directory.Exists(dirPath))
            {
                return result;
            }
            string lowerFileExtension = fileExtension?.Trim().ToLower();
            bool checkFileExtension = !string.IsNullOrEmpty(lowerFileExtension);
            var dir = new DirectoryInfo(dirPath);
            foreach (var file in dir.GetFiles())
            {
                if (checkFileExtension)
                {
                    if (file.Extension?.ToLower() == lowerFileExtension)
                    {
                        result.Add(file.FullName);
                    }
                }
                else
                {
                    result.Add(file.FullName);
                }
            }
            return result;
        }
        /// <summary>
        /// The fileExtension like ".xml", not include sub dir. When fileExtension is null, then all.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static List<string> GetSubFileNames(string dirPath, string fileExtension = null)
        {
            List<string> result = new List<string>();
            if (!Directory.Exists(dirPath))
            {
                return result;
            }
            string lowerFileExtension = fileExtension?.Trim().ToLower();
            bool checkFileExtension = !string.IsNullOrEmpty(lowerFileExtension);
            var dir = new DirectoryInfo(dirPath);
            foreach (var file in dir.GetFiles())
            {
                if (checkFileExtension)
                {
                    if (file.Extension?.ToLower() == lowerFileExtension)
                    {
                        result.Add(file.Name);
                    }
                }
                else
                {
                    result.Add(file.Name);
                }
            }
            return result;
        }
        /// <summary>
        /// Get sub dir.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static List<string> GetSubFolderNames(string dirPath)
        {
            var result = new List<string>();
            if (!Directory.Exists(dirPath))
            {
                return result;
            }
            var dir = new DirectoryInfo(dirPath);
            foreach (var subDir in dir.GetDirectories())
            {
                result.Add(subDir.Name);
            }
            return result;
        }
        public static bool ExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }
        public static bool ExistsDirectory(string dirPath)
        {
            return Directory.Exists(dirPath);
        }
        /// <summary>
        /// Check whether has file.
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsExistSubFile(string dirPath, string checkFileName)
        {
            if (!Directory.Exists(dirPath) || string.IsNullOrWhiteSpace(checkFileName))
            {
                return false;
            }
            var dir = new DirectoryInfo(dirPath);
            foreach (var file in dir.GetFiles())
            {
                if (file.Name.ToLower() == checkFileName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
