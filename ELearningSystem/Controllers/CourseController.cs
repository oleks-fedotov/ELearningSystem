using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ELearningSystem.Models;
using Domain;

namespace ELearningSystem.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        IElearningSystemRepository _repository;
        MembershipProvider _provider;

        public CourseController(IElearningSystemRepository repo, MembershipProvider membershipProvider)
        {
            _repository = repo;
            _provider = membershipProvider;
        }

        public ActionResult Index(UserInformation user)
        {
            if (user == null) return View("UnauthorizedAccess");
            else if (user.IsStudent)
            {
                //Get courses for this studentId
                //Give them to the view(StudentCourses)
                object courses = null;
                return View("StudentCourses", courses);
            }
            else
            {
                //Get courses for this lecturerId
                //Give them to the view(LecturerView)
                IQueryable<Course> courses = _repository.Courses.Where(x => x.LecturerId == user.UserId);
                return View("LecturerCourses", courses);
            }
        }

        public ActionResult CreateCourse(UserInformation user)
        {
            if (user == null) return View("UnauthorizedAccess");
            else if (user.IsStudent) return View("StudentCourseCreation");
            else
            {
                SelectList categories = new SelectList(_repository.CourseCategories, "ID", "Name");
                SelectList courseTypes = new SelectList(_repository.CourseTypes, "ID", "TypeName");
                ViewBag.Categories = categories;
                ViewBag.CourseTypes = courseTypes;
                Lecturer creator = _repository.Lecturers.Where<Lecturer>(x => x.ID == user.UserId).FirstOrDefault<Lecturer>();
                return View("CreateCourse", new Course() { LecturerId = user.UserId.GetValueOrDefault() });
            }
        }

        [HttpPost]
        public ActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveCourse(course);
                return View("CreationSucceed");
            }
            else
            {
                SelectList categories = new SelectList(_repository.CourseCategories, "ID", "Name");
                SelectList courseTypes = new SelectList(_repository.CourseTypes, "ID", "TypeName");
                ViewBag.Categories = categories;
                ViewBag.CourseTypes = courseTypes;
                return View("CreateCourse", course);
            }
        }

        [HttpPost]
        public ActionResult EditCourse(Guid ID)
        {
            CourseDetails details = new CourseDetails();
            var course = _repository.Courses.Where(x => x.ID == ID).ToList();
            if (course.Count() != 0)
            {
                details.CourseId = course.First().ID;
                details.CourseName = course.First().Name;
                details.Topics = (from x in _repository.CourseTopics where x.CourseId == details.CourseId select new Topic { TopicName = x.Name, ID = x.ID, CourseId = x.CourseId }).ToList<Topic>();
                for (int i = 0; i < details.Topics.Count; i++)
                {
                    Guid topicId = details.Topics[i].ID;
                    details.Topics[i].Lectures = _repository.Lectures.Where(x => x.TopicId == topicId).ToList();
                    details.Topics[i].Tests = _repository.Tests.Where(x => x.TopicId == topicId).ToList();
                }
                //details.Topics = _repository.CourseTopics.Where(x => x.CourseId == details.CourseId);
            }

            return View(details);
        }

        [HttpPost]
        public RedirectToRouteResult CreateTopic(Guid courseId, string topicName)
        {
            CourseTopic topic = new CourseTopic() { CourseId = courseId, Name = topicName };
            topic.OrderNumber = _repository.CourseTopics.Where(x => x.CourseId == courseId).Count() + 1;
            _repository.SaveTopic(topic);
            Guid newId = _repository.CourseTopics.Where(x => x.CourseId == courseId).OrderBy(x => x.OrderNumber).ToList().Last().ID;
            return RedirectToAction("Index", "Topic", new { topicId = newId });
        }
    }
}
