using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.DataUtil.UtilJsonModel
{
    public class Students
    {
        public List<Student> StudentList { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
    }

    
}
