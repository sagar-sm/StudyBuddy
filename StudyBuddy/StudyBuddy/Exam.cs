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
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace StudyBuddy
{
    public class Exam
    {
        private string _ename;
        private DateTime _examd;
        private DateTime _examt;
        private string _priority;

        public string Ename
        {
            get { return _ename; }
            set { _ename = value; }
        }

        public DateTime Edate
        {
            get { return _examd.Date; }
            set { _examd = value; }
        }

        public string DisplayDate //fuck. this is inevitable :P
        {
            get { return _examd.Date.ToLongDateString(); }
        }

        public DateTime Etime
        {
            get { return _examt; }
            set { _examt = value; }
        }

        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

    }
}

