using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearningSystem.Models
{
    public class DisplayLectureModel
    {
        public string LectureName { get; set; }

        public Guid PrevLecture { get; set; }

        public Guid NextLecture { get; set; }

        public Guid CurrentLecture { get; set; }

        public MvcHtmlString LectureContent { get; set; }
    }
}