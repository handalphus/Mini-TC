using PartialCommander.Model;
using PartialCommander.VewModel.BaseClass;
using PartialCommander.ViewModel.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PartialCommander.ViewModel
{

    internal class MiniTCVM : ViewModelBase
    {

        //instancja modelu
        private MiniTC miniTC = new MiniTC();
        public PanelTC Panel1
        {
            get => miniTC.panels[0];
            set
            {
                miniTC.panels[0] = value;
            }
        }
        public PanelTC Panel2
        {
            get => miniTC.panels[1];
            set
            {
                miniTC.panels[1] = value;
            }
        }


        //konstruktor
        public MiniTCVM()
        {
            miniTC.InitializePanels(Properties.Resources.defaultPath, Properties.Resources.defaultPath);
            CurrentPath1 = Properties.Resources.defaultPath;
            CurrentPath2 = Properties.Resources.defaultPath;
            CurrentDrive1 = Properties.Resources.defaultPath;
            CurrentDrive2 = Properties.Resources.defaultPath;
        }
     
       
        private string currentItem1;
        private string currentItem2;
        private string highlightedPath1;
        private string highlightedPath2;
        private string highlightedPath;
        private PanelTC activePanel;
        #region stałe napisowe:
        public string CopyCaption { get => Properties.Resources.copy; }
        public string DriveCaption { get => Properties.Resources.drive; }
        public string PathCaption { get => Properties.Resources.path; }
       
        #endregion
        #region interfejs publiczny

        #region panel1
        public string HighlightedPath1
        {

           

            set
            {
                highlightedPath = value;
                activePanel = miniTC.panels[0];
                
            }
        }

        public string CurrentPath1
        {
            get => miniTC.panels[0].CurrentPath;
            set
            {
                miniTC.ChangePanel(0, value);
                onPropertyChanged(nameof(CurrentPath1), nameof(Content1));
            }
        }

        public string[] Content1
        {
            get
            {
                return miniTC.panels[0].CurrentDirectoryContent;
            }
        }


        public ObservableCollection<string> Drives1
        {
            get
            {
                ObservableCollection<string> drives1 = new ObservableCollection<string>();
                foreach (var item in miniTC.panels[0].Drives)
                {
                    drives1.Add(item);
                }
                return drives1;
            }

        }


        public string CurrentDrive1
        {
            get
            {
                return miniTC.panels[0].CurrentDrive;
            }
            set
            {
                miniTC.panels[0].CurrentDrive = value;
                onPropertyChanged(nameof(CurrentPath1), nameof(Content1));
            }
        }
        #endregion

        #region panel2

        public string HighlightedPath2
        {

           

            set
            {
                highlightedPath = value;
                //po zaznaczeniu ścieżki na 2 panelu, 2 panel jest aktywny, poprzednia ścieżka jest nullem
                activePanel = miniTC.panels[1];
               
            }
        }

        public string CurrentPath2
        {
            get => miniTC.panels[1].CurrentPath;
            set
            {
                miniTC.ChangePanel(1, value);
                onPropertyChanged(nameof(CurrentPath2), nameof(Content2));
            }
        }

        public string[] Content2
        {
            get
            {
                return miniTC.panels[1].CurrentDirectoryContent;
            }
        }


        public ObservableCollection<string> Drives2
        {
            get
            {
                ObservableCollection<string> drives2 = new ObservableCollection<string>();
                foreach (var item in miniTC.panels[1].Drives)
                {
                    drives2.Add(item);
                }
                return drives2;
            }

        }


        public string CurrentDrive2
        {
            get
            {
                return miniTC.panels[1].CurrentDrive;
            }
            set
            {
                miniTC.panels[1].CurrentDrive = value;
                onPropertyChanged(nameof(CurrentPath2), nameof(Content2));
            }
        }

        #endregion
        #endregion

        #region commands
        private ICommand _getDrives1 = null;
        public ICommand GetDrives1
        {
            get
            {
                if (_getDrives1 == null)
                {
                    _getDrives1 = new RelayCommand(arg =>
                    {

                        onPropertyChanged(nameof(Drives1));

                    }, arg => true);
                }
                return _getDrives1;
            }
        }
        private ICommand _getDrives2 = null;
        public ICommand GetDrives2
        {
            get
            {
                if (_getDrives2 == null)
                {
                    _getDrives2 = new RelayCommand(arg =>
                    {

                        onPropertyChanged(nameof(Drives2));

                    }, arg => true);
                }
                return _getDrives2;
            }
        }

        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(arg =>
                    {
                        if (activePanel == miniTC.panels[0])
                        {
                            activePanel.Copy(highlightedPath,CurrentPath2);
                            onPropertyChanged(nameof(Content2));
                        }
                        else
                        {
                            activePanel.Copy(highlightedPath, CurrentPath1);
                            onPropertyChanged(nameof(Content1));
                        }
                    },
                    arg => {
                        if (!string.IsNullOrEmpty(highlightedPath))
                        {
                            if (activePanel == miniTC.panels[0])
                            {
                                return activePanel.IsCopyingPossible(highlightedPath);
                            }
                            else
                            {
                                return activePanel.IsCopyingPossible(highlightedPath);
                            }
                        }
                        else return false;
                    }
                    ) ;
                }
                return _copy;
            }
        }

        private ICommand _changeDirectory1 = null;
        public ICommand ChangeDirectory1
        {
            get
            {
                if (_changeDirectory1 == null)
                {
                    _changeDirectory1 = new RelayCommand(arg =>
                    {

                        CurrentPath1 = highlightedPath;


                    }, arg => true);
                }
                return _changeDirectory1;
            }
        }
        private ICommand _changeDirectory2 = null;
        public ICommand ChangeDirectory2
        {
            get
            {
                if (_changeDirectory2 == null)
                {
                    _changeDirectory2 = new RelayCommand(arg =>
                    {

                        CurrentPath2 = highlightedPath;


                    }, arg => true);
                }
                return _changeDirectory2;
            }
        }

        public void RefreshDrives()
        {
            ObservableCollection<string> drives1 = new ObservableCollection<string>();
            foreach (var item in miniTC.panels[1].Drives)
            {
                drives1.Add(item);
            }
        }
        #endregion
    }
}
