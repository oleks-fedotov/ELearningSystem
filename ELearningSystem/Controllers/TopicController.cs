using Domain;
using Domain.Abstract;
using ELearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
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

        [HttpGet]
        public ActionResult EditTopic(UserInformation user, Guid topicId)
        {
            if (user != null)
            {
                if (user.IsStudent)
                {
                    return View("Students limitations");
                }
                else
                {
                    try
                    {
                        TopicForEditing t = new TopicForEditing();
                        t.ID = topicId;
                        t.CourseId = _repository.CourseTopics.Where(x => x.ID == topicId).First().CourseId;
                        ViewBag.CourseName = _repository.Courses.Where(x => x.ID == t.CourseId);
                        ViewBag.TopicName = _repository.CourseTopics.Where(x => x.ID == topicId).First().Name;
                        t.Lectures = _repository.Lectures.Where(x => x.TopicId == topicId).ToList<Lecture>();
                        t.Tests = _repository.Tests.Where(x => x.TopicId == topicId).ToList<Test>();
                        return View(t);
                    }
                    catch(Exception e)
                    {
                        return View("Error");
                    }
                    
                }
            }
            else return View("UnauthorizedAccess");
        }

    }
}
