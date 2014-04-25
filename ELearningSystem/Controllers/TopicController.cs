using Domain;
using Domain.Abstract;
using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ELearningSystem.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /Topic/
        IElearningSystemRepository _repository;
        MembershipProvider _provider;

        public TopicController(IElearningSystemRepository repo, MembershipProvider membershipProvider)
        {
            _repository = repo;
            _provider = membershipProvider;
        }

        [HttpPost]
        public ActionResult Index(Guid topicId)
        {
            CourseTopic t = _repository.CourseTopics.Where(x => x.ID == topicId).First();
            Topic topic = new Topic() { CourseId = t.CourseId, ID = t.ID, TopicName = t.Name };
            topic.CourseName = _repository.Courses.Where(x => x.ID == topic.CourseId).Select(x => x.Name).First();
            List<Lecture> lectures = _repository.Lectures.Where(x => x.TopicId == topicId).ToList<Lecture>();
            List<Test> tests = _repository.Tests.Where(x => x.TopicId == topicId).ToList<Test>();
            topic.Lectures = lectures;
            topic.Tests = tests;
            return View(topic);
        }

    }
}
