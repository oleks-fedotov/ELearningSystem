//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.GeneratedFromDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Courses
    {
        public Courses()
        {
            this.CourseRequests = new HashSet<CourseRequests>();
            this.CourseTopics = new HashSet<CourseTopics>();
            this.StudentCourse = new HashSet<StudentCourse>();
        }
    
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public System.Guid CategoryId { get; set; }
        public decimal ComplexityLevel { get; set; }
        public System.Guid CourseTypeId { get; set; }
        public System.Guid LecturerId { get; set; }
        public string RequiredSkills { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual CourseCategories CourseCategories { get; set; }
        public virtual ICollection<CourseRequests> CourseRequests { get; set; }
        public virtual CourseTypes CourseTypes { get; set; }
        public virtual Lecturers Lecturers { get; set; }
        public virtual ICollection<CourseTopics> CourseTopics { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
