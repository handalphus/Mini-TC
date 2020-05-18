using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace PartialCommander.Model
{
    public class PanelTC
    {
        private string[] drives;
        private string currentPath;
        private string[]currentDirectoryContent;
        private string currentDrive;
        private string currentItem = null;
        #region konstruktor
        public PanelTC(string currentPath)
        {
            this.currentPath = currentPath;

        }
        #endregion

        #region properties

        public string CurrentDrive
        {
            get => currentDrive;
            set
            {
                currentDrive = value;
                //zmiana dysku = zmiana ścieżki
                currentPath = value;

            }
        }
        public string[] Drives
        {
            get
            {
                return Directory.GetLogicalDrives(); 
            }
        }
        public string CurrentPath
        {
            get
            {
                return currentPath;
            }
            set
            {

                if (value.StartsWith(Properties.Resources.signOfFolder))
                {
                    if (PathNavigation.IsLocationAccessible(PathNavigation.ClearDirectory(value)))
                    {
                        currentPath = PathNavigation.ExtractNextDirectory(value);
                        currentItem = PathNavigation.ClearDirectory(value);
                    }
                }
                else if (value == Properties.Resources.goToParentFolder)
                {
                    currentPath = PathNavigation.ExtractPreviousDirectory(currentPath);
                    currentItem = currentPath;
                }
                else
                {
                    currentItem = value;
                }
            }
        }


        public string[] CurrentDirectoryContent
        {
            get
            {
                string[] tree = PathNavigation.GetDirectoryContent(CurrentPath, CurrentDrive);
                if (tree != null)
                {
                    currentDirectoryContent = tree;
                }
                return currentDirectoryContent;
            }
        }
        #endregion

        
        public void Copy(string path, string desiredLocation)
        {
            if (path.StartsWith(Properties.Resources.signOfFolder))
            {
                path = PathNavigation.ClearDirectory(path);
                CopyDirectory(path, desiredLocation);

            }
            else
            {
                string nameOfFile = Path.GetFileName(path);
                if (!File.Exists(desiredLocation + nameOfFile))
                {
                    File.Copy(CurrentPath + nameOfFile, desiredLocation + nameOfFile);
                }

            }

        }

        private static void CopyDirectory(string path, string desiredLocation)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            string dirName = directory.Name;
            string desiredDirectoryName = desiredLocation + dirName + @"\";
            if (!Directory.Exists(desiredDirectoryName))
            {
                Directory.CreateDirectory(desiredDirectoryName);
            }
            DirectoryInfo[] directories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (PathNavigation.IsFileAccessible(files[i].FullName))
                {
                    string tmpPath = Path.Combine(desiredDirectoryName, files[i].Name);
                    files[i].CopyTo(tmpPath, false);
                }
            }
            for (int i = 0; i < directories.Length; i++)
            {
                string tmpPath = Path.Combine(desiredDirectoryName, directories[i].Name);
                CopyDirectory(directories[i].FullName, desiredDirectoryName);
                }
        }

        public bool IsCopyingPossible(string path)
        {
            if (path.StartsWith(Properties.Resources.signOfFolder))
            {
               
                return PathNavigation.IsLocationAccessible(PathNavigation.ClearDirectory(path));
            }
            else if (path == Properties.Resources.goToParentFolder)
            {
                return false;
            }
            else
            {
                return PathNavigation.IsFileAccessible(path);
            }
        }
    }

}
