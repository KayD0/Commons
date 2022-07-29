using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Commons.DbAccesorContext.Model
{
    [Table(nameof(Student))]
    public class Student
    {
        [Key]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Height { get; set; }
    }
}
