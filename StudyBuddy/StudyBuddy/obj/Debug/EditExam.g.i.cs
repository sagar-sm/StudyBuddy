﻿#pragma checksum "C:\Users\Sagar\Documents\Expression\Blend 4\Projects\StudyBuddy\StudyBuddy\EditExam.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0C2F808975D012A8708C3FFA719516C9"
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
    
    
    public partial class EditExam : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        internal System.Windows.Controls.TextBlock ExamDateHolder;
        
        internal System.Windows.Controls.TextBlock ExamTimeHolder;
        
        internal System.Windows.Controls.Image DispImg;
        
        internal System.Windows.Controls.TextBlock CommentTbl;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/StudyBuddy;component/EditExam.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentPanel")));
            this.ExamDateHolder = ((System.Windows.Controls.TextBlock)(this.FindName("ExamDateHolder")));
            this.ExamTimeHolder = ((System.Windows.Controls.TextBlock)(this.FindName("ExamTimeHolder")));
            this.DispImg = ((System.Windows.Controls.Image)(this.FindName("DispImg")));
            this.CommentTbl = ((System.Windows.Controls.TextBlock)(this.FindName("CommentTbl")));
        }
    }
}

