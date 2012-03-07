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
    public partial class EditProfile : PhoneApplicationPage
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Profile current = new Profile();
                if (myIsolatedStorage.FileExists("Profile.xml"))
                {
                    using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Profile.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                        current = (Profile)serializer.Deserialize(stream1);
                    }

                    NameTbx.Text = current.Name;
                    CourseTbx.Text = current.Course;
                    InstiTbx.Text = current.Insti;
                    SemTbx.Text = current.Sem;
                    WorkTbx.Text = current.Work;
                }
                else
                    WorkTbx.Text = "Student";
            }
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Profile updated = new Profile();
                updated.Name = NameTbx.Text;
                updated.Course = CourseTbx.Text;
                updated.Insti = InstiTbx.Text;
                updated.Sem = SemTbx.Text;
                updated.Work = WorkTbx.Text;

                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Profile.xml", FileMode.Create, myIsolatedStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    {
                        serializer.Serialize(xmlWriter, updated);
                    }
                }
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }       

    }
}