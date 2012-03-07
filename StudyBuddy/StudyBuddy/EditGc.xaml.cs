using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using System.Xml;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace StudyBuddy
{
    public partial class EditGc : PhoneApplicationPage
    {
        public EditGc()
        {
            InitializeComponent();
        }
        private int index;
        private Gradecard selectedGc;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                index = int.Parse(selectedIndex);
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedGc = GlobalVars.UserGc[index];
                PageTitle.DataContext = selectedGc.Course;
                CreditsHolder.DataContext = String.Concat("Credits Registered: ", selectedGc.Credits.ToString());
                GradeHolder.DataContext = selectedGc.Grade.ToString();
            }
            catch 
            {
                MessageBox.Show("Sorry, the page you are trying to access has expired. Maybe you had deleted something.");
                NavigationService.GoBack();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddGc.xaml?edit=" + index.ToString(), UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to drop the course? This action can't be reverted.", "Confirm Drop",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    List<Gradecard> temp = new List<Gradecard>();
                    if (myIsolatedStorage.FileExists("Gradecards.xml"))
                    {
                        using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Gradecards.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Gradecard>));
                            temp = (List<Gradecard>)serializer.Deserialize(stream1);
                        }
                        temp.RemoveAt(index);
                        GlobalVars.UserGc = temp;
                    }

                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Gradecards.xml", FileMode.Create, myIsolatedStorage))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Gradecard>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            serializer.Serialize(xmlWriter, GlobalVars.UserGc);
                        }
                    }
                    index = -1;
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }       


    }
}