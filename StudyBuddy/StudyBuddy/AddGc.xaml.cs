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
    public partial class AddGc : PhoneApplicationPage
    {
        public AddGc()
        {
            InitializeComponent();
        }
        int index;
        private Gradecard ToEdit;
        bool firstload; //so that values can be changed after 1st load to avoid = on each load values will be set back to old values

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("edit", out selectedIndex))
                index = int.Parse(selectedIndex); //for an edit
            else
                index = -1; //for a new course
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                if (index != -1)
                {
                    PageTitle.DataContext = "edit course";
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

                            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Point.txt", FileMode.Open, FileAccess.Read, myIsolatedStorage))
                            {
                                StreamReader rd = new StreamReader(stream);
                                string[] tmp = rd.ReadLine().Split(',');
                                GlobalVars.SGPA = Double.Parse(tmp[0]);
                                GlobalVars.CGPA = Double.Parse(tmp[1]);
                            }
                            ToEdit = temp[index];

                            if (firstload)
                            {
                                CourseAddTbx.Text = ToEdit.Course;
                                CreditsTbx.Text = ToEdit.Credits.ToString();
                                GradeTbx.Text = ToEdit.Grade;
                                UpdSPGATbx.Text = GlobalVars.SGPA.ToString();
                                UpdCPGATbx.Text = GlobalVars.CGPA.ToString();
                                firstload = false;
                            }
                        }
                    }
                }
                else
                    PageTitle.DataContext = "new course";
            }
            catch
            {
                MessageBox.Show("Oops! Something went wrong. You might have deleted the item on this page.");
                NavigationService.GoBack();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (CourseAddTbx.Text == "" || GradeTbx.Text=="" || CreditsTbx.Text == "")
            {
                MessageBox.Show("Course/Grade/Credits cannot be blank.");
            }
            else
            {
                if (index == -1) //if new, then save
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


                            temp.Add(new Gradecard { Course = CourseAddTbx.Text, Credits = Double.Parse(CreditsTbx.Text), Grade = GradeTbx.Text });
                            GlobalVars.UserGc = temp;
                        }
                        else
                            GlobalVars.UserGc.Add(new Gradecard { Course = CourseAddTbx.Text, Credits = Double.Parse(CreditsTbx.Text), Grade = GradeTbx.Text });


                        using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Gradecards.xml", FileMode.Create, myIsolatedStorage))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Gradecard>));
                            using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                            {
                                serializer.Serialize(xmlWriter, GlobalVars.UserGc);
                            }
                        }

                        
                        if (UpdCPGATbx.Text != "" && UpdSPGATbx.Text != "")
                        {
                            GlobalVars.SGPA = Double.Parse(UpdSPGATbx.Text);
                            GlobalVars.CGPA = Double.Parse(UpdCPGATbx.Text);
                        }
                        else if (UpdCPGATbx.Text != "" && UpdSPGATbx.Text=="")
                        {
                            GlobalVars.CGPA = Double.Parse(UpdCPGATbx.Text);
                            GlobalVars.SGPA = 0;
                        }
                        else if (UpdSPGATbx.Text != "" && UpdCPGATbx.Text == "")
                        {
                            GlobalVars.SGPA = Double.Parse(UpdSPGATbx.Text);
                            GlobalVars.CGPA = 0;
                        }
                        else 
                        {
                            GlobalVars.SGPA = 0;
                            GlobalVars.CGPA = 0;
                        }
                        
                        if (GlobalVars.CGPA != 0 || GlobalVars.SGPA != 0)
                            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Point.txt", FileMode.Create, myIsolatedStorage))
                            {
                                StreamWriter wr = new StreamWriter(stream);
                                wr.Write(GlobalVars.SGPA.ToString() + "," + GlobalVars.CGPA.ToString());
                                wr.Close();
                            }

                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                }
                else //if an edit, delete current > save update
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
                            temp.Insert(index, new Gradecard { Course = CourseAddTbx.Text, Credits = Double.Parse(CreditsTbx.Text), Grade = GradeTbx.Text });
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

                        if (UpdCPGATbx.Text != "" && UpdSPGATbx.Text != "")
                        {
                            GlobalVars.SGPA = Double.Parse(UpdSPGATbx.Text);
                            GlobalVars.CGPA = Double.Parse(UpdCPGATbx.Text);
                        }
                        else if (UpdCPGATbx.Text != "" && UpdSPGATbx.Text == "")
                        {
                            GlobalVars.CGPA = Double.Parse(UpdCPGATbx.Text);
                            GlobalVars.SGPA = 0;
                        }
                        else if (UpdSPGATbx.Text != "" && UpdCPGATbx.Text == "")
                        {
                            GlobalVars.SGPA = Double.Parse(UpdSPGATbx.Text);
                            GlobalVars.CGPA = 0;
                        }
                        else
                        {
                            GlobalVars.SGPA = 0;
                            GlobalVars.CGPA = 0;
                        }

                        if (GlobalVars.CGPA != 0 || GlobalVars.SGPA != 0)
                            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Point.txt", FileMode.Create, myIsolatedStorage))
                            {
                                StreamWriter wr = new StreamWriter(stream);
                                wr.Write(GlobalVars.SGPA.ToString() + "," + GlobalVars.CGPA.ToString());
                                wr.Close();
                            }

                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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