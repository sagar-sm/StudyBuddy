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
    public partial class AddAssn : PhoneApplicationPage
    {
        public AddAssn()
        {
            InitializeComponent();
            firstload = true;
        }

        int index;
        private Assignment ToEdit;
        bool firstload; //so that values can be changed after 1st load to avoid = on each load values will be set back to old values

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("edit", out selectedIndex))
                index = int.Parse(selectedIndex); //for an edit
            else
                index = -1; //for a new assignment
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
                        List<Assignment> temp = new List<Assignment>();
                        if (myIsolatedStorage.FileExists("Assignments.xml"))
                        {
                            using (IsolatedStorageFileStream stream1 = new IsolatedStorageFileStream("Assignments.xml", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                                temp = (List<Assignment>)serializer.Deserialize(stream1);
                            }
                            ToEdit = temp[index];
                            if (firstload)
                            {
                                assnNameTbx.DataContext = ToEdit.Ename;
                                subDate.Value = ToEdit.Edate;
                                subTime.Value = ToEdit.Etime;
                                detailsTbx.Text = ToEdit.Details;
                                firstload = false;
                            }
                        }
                    }
                }
                else
                    PageTitle.DataContext = "new assignment";
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong. You might have deleted the item on this page.");
                NavigationService.GoBack();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) //BEWARE BEFORE EDITING
        {
            if (assnNameTbx.Text == "")
            {
                MessageBox.Show("Please enter the name of the assignment");
            }
            else
            {
                if (index == -1) //if new, then save
                {
                    //assigning selected date/time to dt; 
                    DateTime d = (DateTime)subDate.Value;
                    DateTime t = (DateTime)subTime.Value;
                    TimeSpan span = d.Subtract(DateTime.Today);

                    if (span.Days < 0)
                    {
                        if (MessageBox.Show("The date you have entered has already passed. Is this assignment still pending?", "Revise your entry",
                            MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        else
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


                                    temp.Add(new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });
                                    GlobalVars.UserAssns = temp;
                                }
                                else
                                    GlobalVars.UserAssns.Add(new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });



                                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Assignments.xml", FileMode.Create, myIsolatedStorage))
                                {
                                    XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                                    {
                                        serializer.Serialize(xmlWriter, GlobalVars.UserAssns);
                                    }
                                }
                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                            }
                        }
                    }

                    else
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


                                temp.Add(new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });
                                GlobalVars.UserAssns = temp;
                            }
                            else
                                GlobalVars.UserAssns.Add(new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });



                            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Assignments.xml", FileMode.Create, myIsolatedStorage))
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(List<Assignment>));
                                using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                                {
                                    serializer.Serialize(xmlWriter, GlobalVars.UserAssns);
                                }
                            }
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                        }
                    }
                }

                else //if an edit, delete current > save update
                {
                    //assigning selected date+time to dt; 
                    DateTime d = (DateTime)subDate.Value;
                    DateTime t = (DateTime)subTime.Value;
                    TimeSpan span = d.Subtract(DateTime.Today);
                    if (span.Days < 0)
                    {
                        if (MessageBox.Show("The date you have entered has already passed. Is this assignment still pending?", "Revise your Entry",
                            MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        else
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
                                    temp.Insert(index, new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });
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
                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                            }
                        }
                    }

                    else
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
                                temp.Insert(index, new Assignment { Edate = d, Etime = t, Ename = assnNameTbx.Text, Details = detailsTbx.Text });
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
