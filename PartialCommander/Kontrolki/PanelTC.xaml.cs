using PartialCommander.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace PartialCommander.Kontrolki
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class KontrolkaPanelTC : UserControl
    {


        public KontrolkaPanelTC()
        {

            InitializeComponent();
           

        }
        #region custom events
        //podwójne klikniecie na element listy:

        //rejestracja zdarzenia, żeby można zbindować 
        public static readonly RoutedEvent ListItemDoubleClickedEvent = 
            EventManager.RegisterRoutedEvent("ListItemDoubleClicked", 
            RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), 
            typeof(KontrolkaPanelTC));
        //defincja zdarzenia 
        public event RoutedEventHandler ListItemDoubleClicked
        {
            add { AddHandler(ListItemDoubleClickedEvent, value); }
            remove { RemoveHandler(ListItemDoubleClickedEvent, value); }
        }
        //metoda wywołuje zdarzenie i tworzy argument przez nie przekazywany
        void RaiseListItemDoubleClicked()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(KontrolkaPanelTC.ListItemDoubleClickedEvent);
            RaiseEvent(newEventArgs);
        }

        //rozwinięcie dysków

        //rejestracja zdarzenia, żeby można zbindować 
        public static readonly RoutedEvent DropdownOpenedEvent =
            EventManager.RegisterRoutedEvent("DropdownOpened",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(KontrolkaPanelTC));
        //defincja zdarzenia 
        public event RoutedEventHandler DropdownOpened
        {
            add { AddHandler(ListItemDoubleClickedEvent, value); }
            remove { RemoveHandler(ListItemDoubleClickedEvent, value); }
        }
        //metoda wywołuje zdarzenie i tworzy argument przez nie przekazywany
        void RaiseDropdownOpened()
        {
            RoutedEventArgs newEventArgs =
                new RoutedEventArgs(KontrolkaPanelTC.DropdownOpenedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion

        #region properties
        public static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(KontrolkaPanelTC),new FrameworkPropertyMetadata(null));
        //public PanelTC CurrentPanel
        //{
        //    get => (PanelTC)GetValue(PanelProperty);
        //    set
        //    {
        //        //currentPath.Text = value.CurrentPath;


        //        //int nOfDirs = value.CurrentDirectoryContent[0].Length;
        //        //int nOfFiles = value.CurrentDirectoryContent[1].Length;
        //        //string[] tree;
        //        //if (value.CurrentPath == value.CurrentDrive)
        //        //{
        //        //    tree = new string[nOfDirs + nOfFiles];
        //        //}
        //        //else
        //        //{
        //        //    tree = new string[nOfDirs + nOfFiles + 1];
        //        //    tree[0] = Properties.Resources.goToParentFolder;
        //        //}

        //        //for (int i = 0; i < nOfDirs; i++)
        //        //{
        //        //    tree[tree.Length - (nOfDirs + nOfFiles) + i] = $"{Properties.Resources.signOfFolder}{value.CurrentDirectoryContent[0][i]}";
        //        //}
        //        //for (int i = 0; i < nOfFiles; i++)
        //        //{
        //        //    tree[tree.Length - nOfFiles + i] = value.CurrentDirectoryContent[1][i];
        //        //}
        //        //currentContent.ItemsSource = tree;
        //        SetValue(PanelProperty, value);
                

        //    }
        //}
        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }


        public string[] DirectoryContent
        {
            get { return (string[])GetValue(DirectoryContentProperty); }
            set { SetValue(DirectoryContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectoryContentProperty =
            DependencyProperty.Register("DirectoryContent", typeof(string[]), typeof(KontrolkaPanelTC), new PropertyMetadata(null));



        public ObservableCollection<string> Drives
        {
            get { return (ObservableCollection<string>)GetValue(DrivesProperty); }
            set { SetValue(DrivesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Drives.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrivesProperty =
            DependencyProperty.Register("Drives", typeof(ObservableCollection<string>), typeof(KontrolkaPanelTC), new PropertyMetadata(null));


        public string CurrentDrive
        {
            get { return (string)GetValue(CurrentDriveProperty); }
            set { SetValue(CurrentDriveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDrive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDriveProperty =
            DependencyProperty.Register("CurrentDrive", typeof(string), typeof(KontrolkaPanelTC), new PropertyMetadata(null));


        public string HighlightedElement
        {
            get { return (string)GetValue(HighlightedElementProperty); }
            set { SetValue(HighlightedElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightedElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightedElementProperty =
            DependencyProperty.Register("HighlightedElement", typeof(string), typeof(KontrolkaPanelTC), new PropertyMetadata(null));



        public string ChosenElementFromList
        {
            get { return (string)GetValue(ChosenElementFromListProperty); }
            set { SetValue(ChosenElementFromListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChosenElementFromList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChosenElementFromListProperty =
            DependencyProperty.Register("ChosenElementFromList", typeof(string), typeof(KontrolkaPanelTC), new PropertyMetadata(null));




        #endregion

        #region user control events
        private void currentContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RaiseListItemDoubleClicked();
        }

        private void drives_DropDownOpened(object sender, EventArgs e)
        {
            RaiseDropdownOpened();
        }
        #endregion


    }

}
