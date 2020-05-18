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

        
        public void Copy(string pathToFile, string desiredLocation)
        {
           

            string nameOfFile = Path.GetFileName(pathToFile);
            if (!File.Exists(desiredLocation + nameOfFile))
            {
                File.Copy(CurrentPath + nameOfFile, desiredLocation + nameOfFile);
            }
            
        }
       public bool IsCopyingPossible(string path)
        {
            if (path.StartsWith(Properties.Resources.signOfFolder))
            {
               
                return false;
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
