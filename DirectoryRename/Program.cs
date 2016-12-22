using System;
using System.IO;

namespace DirectoryFilesRename
{
    class Program
    {
        // Todo: Provide path of the lookup directory 
        static string filePath = @"[put directory name here with leading slash]";

        // Todo: This program renames a list of folders that satisfies a particular string 
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            
            // Todo: Provide the string that will be checked in the path
            var strSrc = "[put string to replace]";
            
            foreach (var objDirectory in di.GetDirectories())
            {
                if (objDirectory.Name.Contains(strSrc))
                {
                    //Console.WriteLine(objDirectory.Name);
                    string toRemove = objDirectory.Name.Replace(strSrc, "");
                    Directory.Move(SetPath(objDirectory.Name), SetPath(toRemove));
                    //Console.WriteLine(objDirectory.Name);
                }
            }

            foreach (var objFile in di.GetFiles())
            {
                if (objFile.Name.Contains(strSrc))
                {
                    string toRemove = objFile.Name.Replace(strSrc, "").Trim();
                    File.Move(SetPath(objFile.Name), SetPath(toRemove));
                }
            }

            Console.Read();
        }

        static string SetPath(string str1)
        {
            return string.Format("{0}{1}", filePath, str1.Trim());
        }
    }
}
