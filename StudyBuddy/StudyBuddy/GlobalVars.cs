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

namespace StudyBuddy
{
    public static class GlobalVars
    {
        public static List<string> Priorities = new List<string> { "High Priority", "Medium Priority", "Low Priority" };
        public static List<Exam> UserExams = new List<Exam>();
        public static List<Assignment> UserAssns = new List<Assignment>();
        public static List<Gradecard> UserGc = new List<Gradecard>();
        public static double SGPA = new double();
        public static double CGPA = new double();
    }
}
