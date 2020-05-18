using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialCommander.Model
{
    public static class PathNavigation
    {
        public static string ExtractPreviousDirectory(string directory)
        {

            directory = directory.Substring(0, directory.LastIndexOf(@"\"));
            directory = directory.Substring(0, directory.LastIndexOf(@"\") + 1);
            return directory;
        }

        public static string ExtractNextDirectory(string directory)
        {

            string currPath = ClearDirectory(directory);
            if (!currPath.EndsWith(@"\"))
            {
                currPath += @"\";
            }
            return currPath;


        }

        public static string ClearDirectory(string value)
        {
            return value.Substring(Properties.Resources.signOfFolder.Length);
        }
        public static string[] GetDirectoryContent(string currentPath, string currentDrive)
        {
            string[][] currDirCont = new string[2][];
            try
            {
                currDirCont[0] = Directory.GetDirectories(currentPath);
                currDirCont[1] = Directory.GetFiles(currentPath);
                int nOfDirs = currDirCont[0].Length;
                int nOfFiles = currDirCont[1].Length;
                string[] tree;
                if (currentPath == currentDrive)
                {
                    tree = new string[nOfDirs + nOfFiles];
                }
                else
                {
                    tree = new string[nOfDirs + nOfFiles + 1];
                    tree[0] = Properties.Resources.goToParentFolder;
                }

                for (int i = 0; i < nOfDirs; i++)
                {
                    tree[tree.Length - (nOfDirs + nOfFiles) + i] = $@"{Properties.Resources.signOfFolder}{currDirCont[0][i]}";
                }
                for (int i = 0; i < nOfFiles; i++)
                {
                    tree[tree.Length - nOfFiles + i] = currDirCont[1][i];
                }
                
                return tree;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static bool IsLocationAccessible(string path)
        {
            try
            {
                Directory.GetDirectories(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool IsFileAccessible(string path)
        {
            FileInfo file = new FileInfo(path);
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
            
        }
    }
}
