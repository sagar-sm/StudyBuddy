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
using System.Windows.Media.Imaging;

using System.Xml;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace StudyBuddy
{
    public partial class EditExam : PhoneApplicationPage
    {
        public EditExam()
        {
            InitializeComponent();
        }

        private int index;
        private Exam selectedExam;
        
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
                selectedExam = GlobalVars.UserExams[index];
                PageTitle.DataContext = selectedExam.Ename;
                ExamDateHolder.DataContext = selectedExam.Edate.Date.ToLongDateString();
                ExamTimeHolder.DataContext = String.Concat(selectedExam.Etime.Hour.ToString(), ":", selectedExam.Etime.Minute.ToString(), " hrs");
                if (selectedExam.Priority == "High Priority")
                {
                    DispImg.Source = new BitmapImage(new Uri("/Images/high.png", UriKind.Relative));
                    TimeSpan span = selectedExam.Edate.Subtract(DateTime.Today);
                    CommentTbl.Text = "This exam seems to be very important. Lets gear up ourselves and GET THIS DONE. Only " + span.Days.ToString() + " days remaining.";
                }
                else if (selectedExam.Priority == "Medium Priority")
                {
                    DispImg.Source = new BitmapImage(new Uri("/Images/attention.png", UriKind.Relative));
                    TimeSpan span = selectedExam.Edate.Subtract(DateTime.Today);
                    CommentTbl.Text = "Exam seems easy but important. Leave no loophole and crack this exam. Only " + span.Days.ToString() + " days remaining.";
                }
                else if (selectedExam.Priority == "Low Priority")
                {
                    DispImg.Source = new BitmapImage(new Uri("/Images/everythingok.png", UriKind.Relative));
                    TimeSpan span = selectedExam.Edate.Subtract(DateTime.Today);
                    CommentTbl.Text = "Seems like this exam is a piece of cake for you. Go ahead and score high. Only " + span.Days.ToString() + " days remaining.";
                }
            }
            catch
            {
                MessageBox.Show("Sorry the page u are trying to visit no longer exists. Maybe you had deleted this exam.");
                NavigationService.GoBack();
            }
        }
        

        private void EditButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddExam.xaml?edit="+index.ToString(), UriKind.Relative));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this exam? This action can't be reverted.", "Confirm Delete",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    List<Exam> temp = new List<Exam>();
                    if (myIsolatedStorage.FileExists("Exams.xml"))
                    {
                        using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Exams.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Exam>));
                            temp = (List<Exam>)serializer.Deserialize(stream1);
                        }
                        temp.RemoveAt(index);
                        GlobalVars.UserExams = temp;
                    }

                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Exams.xml", FileMode.Create, myIsolatedStorage))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Exam>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            serializer.Serialize(xmlWriter, GlobalVars.UserExams);
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