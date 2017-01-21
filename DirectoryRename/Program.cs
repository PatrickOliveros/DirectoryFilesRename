using System;
using System.Collections;
using System.IO;

namespace DirectoryFilesRename
{
    class Program
    {
        // Todo: Provide path of the lookup directory 
        static string filePath = @"F:\Training Materials\_Booksx\";

        // Todo: This program renames a list of folder/s and/or file/s by removing the matched string
        // from the list in the scoped directory 

        static void Main(string[] args)
        {
            Console.Title = "Directory/File Renaming";
            DirectoryInfo di = new DirectoryInfo(filePath);

            if (!di.Exists)
            {
                Console.WriteLine($"Directory \"{filePath}\" does not exist. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            ArrayList alExceptions = null;
            

            // Todo : Show Summary of total files matched, and those failed
            //var affectedObjects = 0;

            // Todo: Provide the string that will be checked in the path
            ArrayList al = new ArrayList
            {
                "pattern1", "pattern2", "pattern3"
            };

            if (al.Count < 1)
            {
                Console.WriteLine("Nothing to process. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            // Todo: Change value according to preferred change
            var strReplace = "";

            foreach (string strSrc in al)
            {
                // Re-initialize affectedObjects for every time a new pattern is checked
                var affectedObjects = 0;

                // start checking for directories
                foreach (var objDirectory in di.GetDirectories())
                {
                    try
                    {
                        if (ObjectContains(objDirectory.Name, strSrc))
                        {
                            affectedObjects += 1;
                            Directory.Move(SetPath(objDirectory.Name), 
                                SetPath(ReplaceName(objDirectory.Name, strSrc, strReplace)));
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

                // reset counter to 0 to reflect changes in files for each pattern to check
                affectedObjects = 0;

                // start checking for files
                foreach (var objFile in di.GetFiles())
                {
                    try
                    {
                        if (ObjectContains(objFile.Name, strSrc))
                        {
                            affectedObjects += 1;
                            File.Move(SetPath(objFile.Name), 
                                SetPath(ReplaceName(objFile.Name, strSrc, strReplace)));
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

        private static string ReplaceName(string strName, string strPattern, string strReplacement)
        {
            return strName.Replace(strPattern, strReplacement);
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
