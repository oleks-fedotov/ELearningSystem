using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearningSystem.Models
{
    public class LecturerCourseRequestModel
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public Guid StudentId { get; set; }

        public string StudentName { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }

    public class StudentCourseRequestModel
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public DateTime Date { get; set; }

        public bool WasApproved { get; set; }
    }
}