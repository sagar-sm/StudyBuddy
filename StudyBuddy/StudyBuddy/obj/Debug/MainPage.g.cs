﻿#pragma checksum "C:\Users\Sagar\Documents\Expression\Blend 4\Projects\StudyBuddy\StudyBuddy\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1D311516FBD0695C56F3F23F0706B3C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.488
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace StudyBuddy {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.PanoramaItem exams;
        
        internal System.Windows.Controls.Button AddNewExamBtn;
        
        internal System.Windows.Controls.ListBox examListbx;
        
        internal Microsoft.Phone.Controls.PanoramaItem assignments;
        
        internal System.Windows.Controls.Button AddNewAssnBtn;
        
        internal System.Windows.Controls.ListBox assnListbx;
        
        internal Microsoft.Phone.Controls.PanoramaItem gradecard;
        
        internal System.Windows.Controls.Button AddGcBtn;
        
        internal System.Windows.Controls.TextBlock SGPATbl;
        
        internal System.Windows.Controls.TextBlock CGPATbl;
        
        internal System.Windows.Controls.ListBox gcListbx;
        
        internal Microsoft.Phone.Controls.PanoramaItem profile;
        
        internal System.Windows.Controls.StackPanel ProfileStack;
        
        internal System.Windows.Controls.Button AddProfileBtn;
        
        internal System.Windows.Controls.TextBlock NameTbl;
        
        internal System.Windows.Controls.TextBlock CourseTbl;
        
        internal System.Windows.Controls.TextBlock InstiTbl;
        
        internal System.Windows.Controls.TextBlock SemTbl;
        
        internal System.Windows.Controls.TextBlock WorkTbl;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/StudyBuddy;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.exams = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("exams")));
            this.AddNewExamBtn = ((System.Windows.Controls.Button)(this.FindName("AddNewExamBtn")));
            this.examListbx = ((System.Windows.Controls.ListBox)(this.FindName("examListbx")));
            this.assignments = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("assignments")));
            this.AddNewAssnBtn = ((System.Windows.Controls.Button)(this.FindName("AddNewAssnBtn")));
            this.assnListbx = ((System.Windows.Controls.ListBox)(this.FindName("assnListbx")));
            this.gradecard = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("gradecard")));
            this.AddGcBtn = ((System.Windows.Controls.Button)(this.FindName("AddGcBtn")));
            this.SGPATbl = ((System.Windows.Controls.TextBlock)(this.FindName("SGPATbl")));
            this.CGPATbl = ((System.Windows.Controls.TextBlock)(this.FindName("CGPATbl")));
            this.gcListbx = ((System.Windows.Controls.ListBox)(this.FindName("gcListbx")));
            this.profile = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("profile")));
            this.ProfileStack = ((System.Windows.Controls.StackPanel)(this.FindName("ProfileStack")));
            this.AddProfileBtn = ((System.Windows.Controls.Button)(this.FindName("AddProfileBtn")));
            this.NameTbl = ((System.Windows.Controls.TextBlock)(this.FindName("NameTbl")));
            this.CourseTbl = ((System.Windows.Controls.TextBlock)(this.FindName("CourseTbl")));
            this.InstiTbl = ((System.Windows.Controls.TextBlock)(this.FindName("InstiTbl")));
            this.SemTbl = ((System.Windows.Controls.TextBlock)(this.FindName("SemTbl")));
            this.WorkTbl = ((System.Windows.Controls.TextBlock)(this.FindName("WorkTbl")));
        }
    }
}
