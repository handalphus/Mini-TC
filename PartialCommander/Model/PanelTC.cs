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
        private string[][] currentDirectoryContent;
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
                currentPath = value;
                    
            }
        }
        public string[] Drives
        {
            get
            {
                drives = null;
                drives = Directory.GetLogicalDrives();
                return drives;

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
                        string pureDirectory = value.Substring(3);
                    if (IsLocationAccessible(pureDirectory))
                    {
                        currentPath = value.Substring(3);
                        if (!currentPath.EndsWith(@"\"))
                        {
                            currentPath += @"\";
                        }
                        currentItem = pureDirectory;
                    }
                    

                    }
                    else if (value
                        == Properties.Resources.goToParentFolder)
                    {
                        //usuwam końcowy \ a potem przechodzę do nadfolderu
                        currentPath = currentPath.Substring(0, currentPath.LastIndexOf(@"\"));
                        currentPath = currentPath.Substring(0, currentPath.LastIndexOf(@"\") + 1);
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

                string[][] currDirCont = new string[2][];
                try
                {
                    currDirCont[0] = Directory.GetDirectories(CurrentPath);
                    currDirCont[1] = Directory.GetFiles(CurrentPath);
                    int nOfDirs = currDirCont[0].Length;
                    int nOfFiles = currDirCont[1].Length;
                    string[] tree;
                    if (CurrentPath == CurrentDrive)
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
                    currentDirectoryContent = currDirCont;
                    return tree;
                }
                catch (Exception)
                {
                    int nOfDirs = currentDirectoryContent[0].Length;
                    int nOfFiles = currentDirectoryContent[1].Length;
                    string[] tree;
                    if (CurrentPath == CurrentDrive)
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
                        tree[tree.Length - (nOfDirs + nOfFiles) + i] = $@"{Properties.Resources.signOfFolder}{currentDirectoryContent[0][i]}";
                    }
                    for (int i = 0; i < nOfFiles; i++)
                    {
                        tree[tree.Length - nOfFiles + i] = currentDirectoryContent[1][i];
                    }
                    currentDirectoryContent = currDirCont;
                    return tree;

                }
                
                
            }
        }
        #endregion
       
        public bool Copy(int numberOfFileInCurrentLocation, PanelTC panel)
        {
            string desiredLocation = panel.CurrentPath;
            
            string nameOfFile = Path.GetFileName(currentDirectoryContent[1][numberOfFileInCurrentLocation]);
            if (File.Exists(desiredLocation+nameOfFile))
            {
                return false;
            }
            else
            {
                File.Copy(CurrentPath+nameOfFile, desiredLocation + nameOfFile);
                return true;
            }
        }
        public bool Copy(string pathToFile, PanelTC panel)
        {
            string desiredLocation = panel.CurrentPath;

            string nameOfFile = Path.GetFileName(pathToFile);
            if (File.Exists(desiredLocation + nameOfFile))
            {
                return false;
            }
            else
            {
                File.Copy(CurrentPath + nameOfFile, desiredLocation + nameOfFile);
                return true;
            }
        }
        #region additional methods
        public bool IsLocationAccessible(string path)
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
        #endregion
    }

}
