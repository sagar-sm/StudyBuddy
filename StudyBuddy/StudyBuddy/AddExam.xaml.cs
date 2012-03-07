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
    public partial class AddExam : PhoneApplicationPage
    {
        public AddExam()
        {
            InitializeComponent();
            priorityPicker.ItemsSource = GlobalVars.Priorities;
            firstload = true;
        }

        int index;
        private Exam ToEdit;
        bool firstload; //so that values can be changed after 1st load to avoid = on each load values will be set back to old values

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("edit", out selectedIndex))
                index = int.Parse(selectedIndex); //for an edit
            else
                index = -1; //for a new exam
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                if (index != -1)
                {
                    PageTitle.DataContext = "edit details";
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
                            ToEdit = temp[index];
                            if (firstload)
                            {
                                examNameTbx.DataContext = ToEdit.Ename;
                                examDate.Value = ToEdit.Edate;
                                examTime.Value = ToEdit.Etime;
                                priorityPicker.SelectedIndex = GlobalVars.Priorities.IndexOf(ToEdit.Priority);
                                firstload = false;
                            }
                        }
                    }
                }
                else
                    PageTitle.DataContext = "add new exam";
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong. You might have deleted the item that was on this page.");
                NavigationService.GoBack();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (examNameTbx.Text == "")
            {
                MessageBox.Show("Please enter a name for the exam.");
            }
            else
            {
                if (index == -1) //if new, then save
                {

                    //assigning selected date/time to dt; 
                    DateTime d = (DateTime)examDate.Value;
                    DateTime t = (DateTime)examTime.Value;
                    TimeSpan span = d.Subtract(DateTime.Today);
                    if (span.Days < 0)
                    {
                        if (MessageBox.Show("The date you have entered has already passed. Would you like to add this exam as a graded course instead?", "Revise your Entry",
                            MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            NavigationService.Navigate(new Uri("/AddGc.xaml?edit=-1", UriKind.Relative));
                    }

                    else
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


                                temp.Add(new Exam { Edate = d, Etime = t, Ename = examNameTbx.Text, Priority = priorityPicker.SelectedItem.ToString() });
                                GlobalVars.UserExams = temp;
                            }
                            else
                                GlobalVars.UserExams.Add(new Exam { Edate = d, Etime = t, Ename = examNameTbx.Text, Priority = priorityPicker.SelectedItem.ToString() });


                            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Exams.xml", FileMode.Create, myIsolatedStorage))
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(List<Exam>));
                                using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                                {
                                    serializer.Serialize(xmlWriter, GlobalVars.UserExams);
                                }
                            }

                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                        }
                    }
                }
                else //if an edit, delete current > save update
                {

                    //assigning selected date+time to dt; 
                    DateTime d = (DateTime)examDate.Value;
                    DateTime t = (DateTime)examTime.Value;
                    TimeSpan span = d.Subtract(DateTime.Today);
                    if (span.Days < 0)
                    {
                        if (MessageBox.Show("The date you have entered has already passed. Would you like to add this exam as a graded course instead?", "Revise your Entry",
                            MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            NavigationService.Navigate(new Uri("/AddGc.xaml?edit=-1", UriKind.Relative));
                    }
                    else
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
                                temp.Insert(index, new Exam { Edate = d, Etime = t, Ename = examNameTbx.Text, Priority = priorityPicker.SelectedItem.ToString() });
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
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        }
                    }
                }
            }

        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }       

    }
}