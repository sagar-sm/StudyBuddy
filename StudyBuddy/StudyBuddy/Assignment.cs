using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StudyBuddy
{
    public class Assignment
    {
        private string _ename;
        private DateTime _subd;
        private DateTime _subt;
        private string _details;

        public string Ename
        {
            get { return _ename; }
            set { _ename = value; }
        }

        public DateTime Edate
        {
            get { return _subd.Date; }
            set { _subd = value; }
        }

        public string DisplayDate //this is inevitable :P
        {
            get { return _subd.Date.ToLongDateString(); }
        }

        public DateTime Etime
        {
            get { return _subt; }
            set { _subt = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

    }
}
