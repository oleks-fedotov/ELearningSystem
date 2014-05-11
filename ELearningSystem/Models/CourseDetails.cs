using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace ELearningSystem.Models
{
    public class CourseDetails
    {
        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public List<Topic> Topics { get; set; }
    }

    public class Topic
    {
        public Guid CourseId { get; set; }

        public string TopicName { get; set; }

        public decimal OrderNumber { get; set; }

        public Guid ID { get; set; }

        public int LecturesCount { get; set; }

        public int TestsCount { get; set; }

        //public List<Lecture> Lectures { get; set; }

        //public List<Test> Tests { get; set; }
    }

    public class TopicForEditing
    {
        public Guid CourseId { get; set; }

        public Guid ID { get; set; }

        public decimal OrderNumber { get; set; }

        public List<Lecture> Lectures { get; set; }

        public List<Test> Tests { get; set; }
    }

    public class CourseSummaryModel
    {
        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public string LecturerName { get; set; }

        public Guid LecturerId { get; set; }

        public int TopicsQuantity { get; set; }

        public string Description { get; set; }
    }

    public class CourseDetailsModel
    {
        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public Dictionary<string, List<string>> Details { get; set; }
    }
}