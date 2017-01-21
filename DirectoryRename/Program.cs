using System;
using System.Collections;
using System.IO;

namespace DirectoryFilesRename
{
    class Program
    {
        // Todo: Provide path of the lookup directory 
        static string filePath = @"F:\Training Materials\_Books\";

        // Todo: This program renames a list of folders that satisfies a particular string 
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            ArrayList alExceptions = null;


            // Todo : Show Summary of total files matched, and those failed
            var affectedObjects = 0;

            // Todo: Provide the string that will be checked in the path


            ArrayList al = new ArrayList 
            {
                "pattern1", "pattern2", "pattern3"
            };


            foreach (string strSrc in al)
            {
                // start checking for directories
                foreach (var objDirectory in di.GetDirectories())
                {
                    try
                    {
                        if (ObjectContains(objDirectory.Name, strSrc))
                        {
                            affectedObjects += 1;
                            var newName = objDirectory.Name.Replace(strSrc, "");
                            Directory.Move(SetPath(objDirectory.Name), SetPath(newName));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (alExceptions == null)
                        {
                            alExceptions = new ArrayList();
                        }

                        alExceptions.Add(ex.Message);
                    }

                } // end directories


                ShowResults(affectedObjects, alExceptions, strSrc);

                // start checking for files
                foreach (var objFile in di.GetFiles())
                {
                    try
                    {
                        if (ObjectContains(objFile.Name, strSrc))
                        {
                            affectedObjects += 1;
                            var newName = objFile.Name.Replace(strSrc, "");
                            File.Move(SetPath(objFile.Name), SetPath(newName));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (alExceptions == null)
                        {
                            alExceptions = new ArrayList();
                        }

                        alExceptions.Add(ex.Message);
                    }
                }

                ShowResults(affectedObjects, alExceptions, strSrc, "Files");

            } // end files

            Console.Read();
        }

        private static bool ObjectContains(string strSource, string strToCheck)
        {
            return strSource.IndexOf(strToCheck, StringComparison.CurrentCultureIgnoreCase) > -1;
        }

        private static void ShowResults(int affectedObjects, ArrayList alExceptions, string strPattern, string strType = "Directory")
        {
            var errorFiles = 0;

            if (alExceptions != null)
            {
                errorFiles = alExceptions.Count;
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"{strType} Rename Results (\"{strPattern}\")");
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Processed - {affectedObjects}");
            Console.WriteLine($"Completed - {affectedObjects - errorFiles}");
            Console.WriteLine($"Errors - {errorFiles}");

            if (errorFiles > 0)
            {
                foreach (var msg in alExceptions)
                {
                    Console.WriteLine(msg);
                }
            }
            Console.WriteLine("-----------------------------");
        }

        static string SetPath(string str1)
        {
            return $"{filePath}{str1.Trim()}";
        }
    }
}
