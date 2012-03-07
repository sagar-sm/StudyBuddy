using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.ComponentModel;

using Microsoft.Phone.Shell;
using System.Xml;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace StudyBuddy
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;

            ShellTile LiveTile = ShellTile.ActiveTiles.First();
            if (LiveTile != null)
            {
                StandardTileData tile = new StandardTileData();
                tile.BackgroundImage = new Uri("/Images/tile0.png",UriKind.Relative);
                tile.BackBackgroundImage = new Uri("/Images/tile1.png", UriKind.Relative);
                tile.Title = "StudyBuddy";
                tile.BackTitle = "StudyBuddy";
                LiveTile.Update(tile);
            }
            
            this.Loaded +=new RoutedEventHandler(MainPage_Loaded);
        }

        //public System.Windows.Data.CollectionViewSource eSorter { get; set; } //to sort exam listbox view
        //public System.Windows.Data.CollectionViewSource aSorter { get; set; } //to sort assn listbox view

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
 
            //Load all Data
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Load all exams
                List<Exam> temp = new List<Exam>();
                if (myIsolatedStorage.FileExists("Exams.xml"))
                {
                    using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Exams.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {

                        XmlSerializer serializer = new XmlSerializer(typeof(List<Exam>));
                        temp = (List<Exam>)serializer.Deserialize(stream1);

                        /* use for debugging xml
                        using (StreamReader reader = new StreamReader(stream1))
                        {
                            this.tbx.Text = reader.ReadToEnd();
                        }*/
                    }

                    GlobalVars.UserExams = temp;
                    /*
                    eSorter = new System.Windows.Data.CollectionViewSource();
                    eSorter.Source = GlobalVars.UserExams;
                    eSorter.SortDescriptions.Clear();
                    eSorter.SortDescriptions.Add(new SortDescription("Edate", ListSortDirection.Descending));*/
                    examListbx.ItemsSource = GlobalVars.UserExams;

                } //else disp graphic

               //Load all assignments
                List<Assignment> temp2 = new List<Assignment>();
                if (myIsolatedStorage.FileExists("Assignments.xml"))
                {
                    using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Assignments.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {

                        XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                        temp2 = (List<Assignment>)serializer.Deserialize(stream1);

                        /* use for debugging xml
                        using (StreamReader reader = new StreamReader(stream1))
                        {
                            this.tbx.Text = reader.ReadToEnd();
                        }*/
                    }

                    GlobalVars.UserAssns = temp2;
                    /*
                    aSorter = new System.Windows.Data.CollectionViewSource();
                    aSorter.Source=GlobalVars.UserAssns;
                    aSorter.SortDescriptions.Clear();
                    aSorter.SortDescriptions.Add(new SortDescription("Edate", ListSortDirection.Descending));*/
                    assnListbx.ItemsSource = GlobalVars.UserAssns;
                    
                }
                /*else : implement. display graphic*/


                //Load Profile
                Profile current = new Profile();
                if (myIsolatedStorage.FileExists("Profile.xml"))
                {
                    using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Profile.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                        current = (Profile)serializer.Deserialize(stream1);
                    }

                    NameTbl.DataContext = current.Name;
                    CourseTbl.DataContext = String.Concat("Studying ", current.Course, " at ");
                    InstiTbl.DataContext = current.Insti;
                    SemTbl.DataContext = current.Sem;
                    WorkTbl.DataContext = String.Concat("Currently working as ",current.Work);
                } //else disp graphic

                //Load Gradecard
                List<Gradecard> tempgc = new List<Gradecard>();
              

                if (myIsolatedStorage.FileExists("Gradecards.xml"))
                {
                    using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Gradecards.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {

                        XmlSerializer serializer = new XmlSerializer(typeof(List<Gradecard>));
                        tempgc = (List<Gradecard>)serializer.Deserialize(stream1);

                    }

                    
                    
                    GlobalVars.UserGc = tempgc;
                    /*
                    aSorter = new System.Windows.Data.CollectionViewSource();
                    aSorter.Source=GlobalVars.UserGc;
                    aSorter.SortDescriptions.Clear();
                    aSorter.SortDescriptions.Add(new SortDescription("Edate", ListSortDirection.Descending));*/
                    gcListbx.ItemsSource = GlobalVars.UserGc;

                }                /*else : implement. display graphic*/

                if (myIsolatedStorage.FileExists("Point.txt"))
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Point.txt", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {
                        StreamReader rd = new StreamReader(stream);
                        string[] tmp = rd.ReadLine().Split(',');
                        GlobalVars.SGPA = Double.Parse(tmp[0]);
                        GlobalVars.CGPA = Double.Parse(tmp[1]);
                        rd.Close();
                    }
                }
                SGPATbl.DataContext = String.Concat("SGPA = ", GlobalVars.SGPA.ToString());
                CGPATbl.DataContext = String.Concat("CGPA = ", GlobalVars.CGPA.ToString());

            }
                     
        }



        private void examListbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (examListbx.SelectedIndex == -1)
                return;
            NavigationService.Navigate(new Uri("/EditExam.xaml?selectedItem=" + examListbx.SelectedIndex, UriKind.Relative));

            examListbx.SelectedIndex = -1;
        }

        private void gcListbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gcListbx.SelectedIndex == -1)
                return;
            NavigationService.Navigate(new Uri("/EditGc.xaml?selectedItem=" + gcListbx.SelectedIndex, UriKind.Relative));

            gcListbx.SelectedIndex = -1;
        }

        private void assnListbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (assnListbx.SelectedIndex == -1)
                return;
            NavigationService.Navigate(new Uri("/EditAssn.xaml?selectedItem=" + assnListbx.SelectedIndex, UriKind.Relative));

            assnListbx.SelectedIndex = -1;
        }

        private void AddNewExamBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddExam.xaml?edit=-1", UriKind.Relative));
        }

        private void AddNewAssnBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddAssn.xaml?edit=-1", UriKind.Relative));
        }

        private void AddGcBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddGc.xaml?edit=-1", UriKind.Relative));
        }

        private void AddProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EditProfile.xaml", UriKind.Relative));
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }       

        
    }
}