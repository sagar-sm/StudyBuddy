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
    public partial class EditAssn : PhoneApplicationPage
    {
        public EditAssn()
        {
            InitializeComponent();
        }

        private int index;
        private Assignment selectedAssn;

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
                selectedAssn = GlobalVars.UserAssns[index];
                PageTitle.DataContext = selectedAssn.Ename;
                SubDateHolder.DataContext = selectedAssn.Edate.Date.ToLongDateString();
                SubTimeHolder.DataContext = String.Concat(selectedAssn.Etime.Hour.ToString(), ":", selectedAssn.Etime.Minute.ToString(), " hrs");
                DetailsHolder.DataContext = selectedAssn.Details;
                TimeSpan span = selectedAssn.Edate.Subtract(DateTime.Today);
                if (span.Days < 0)
                {
                    DayCountdown.Text = Math.Abs(span.Days).ToString();
                    DayCountdownSubtitle.Text = "day(s) overdue!";
                }
                else
                {
                    DayCountdown.Text = span.Days.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Page does not exist. Maybe you had deleted something.");
                NavigationService.GoBack();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddAssn.xaml?edit=" + index.ToString(), UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this assignment? This action can't be reverted.", "Confirm Delete",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    List<Assignment> temp = new List<Assignment>();
                    if (myIsolatedStorage.FileExists("Assignments.xml"))
                    {
                        using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Assignments.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                            temp = (List<Assignment>)serializer.Deserialize(stream1);
                        }
                        temp.RemoveAt(index);
                        GlobalVars.UserAssns = temp;
                    }

                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Assignments.xml", FileMode.Create, myIsolatedStorage))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            serializer.Serialize(xmlWriter, GlobalVars.UserAssns);
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