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
        public string CourseName { get; set; }

        public Guid CourseId { get; set; }

        public string TopicName { get; set; }

        public Guid ID { get; set; }

        public List<Lecture> Lectures { get; set; }

        public List<Test> Tests { get; set; }
    }
}