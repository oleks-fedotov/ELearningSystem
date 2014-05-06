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
                        if (!CheckIfLecturerHasAccessToTheTopic(user, topicId))
                            return View("AccessDenied");
                        else
                        {
                            TopicForEditing t = new TopicForEditing();
                            t.ID = topicId;
                            t.CourseId = _repository.CourseTopics.Where(x => x.ID == topicId).First().CourseId;
                            ViewBag.CourseName = _repository.Courses.Where(x => x.ID == t.CourseId).First<Course>().Name;
                            ViewBag.TopicName = _repository.CourseTopics.Where(x => x.ID == topicId).First().Name;
                            t.Lectures = _repository.Lectures.Where(x => x.TopicId == topicId).ToList<Lecture>().OrderBy(x => x.OrderNumber).ToList<Lecture>();
                            t.Tests = _repository.Tests.Where(x => x.TopicId == topicId).ToList<Test>();
                            return View(t);
                        }
                    }
                    catch (Exception e)
                    {
                        return View("Error");
                    }

                }
            }
            else return View("UnauthorizedAccess");
        }

        [HttpPost]
        public ActionResult EditTopic(UserInformation user, TopicForEditing topic)
        {
            SaveReorderedLectures(topic.Lectures);
            return RedirectToAction("EditTopic", new { topicId = topic.ID });
        }

        [HttpPost]
        public ActionResult DeleteTopic(UserInformation user, Guid topicId)
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
                        if (!CheckIfLecturerHasAccessToTheTopic(user, topicId))
                            return View("AccessDenied");
                        else
                        {
                            //Change order
                            CourseTopic topic = _repository.CourseTopics.Where(x => x.ID == topicId).First();
                            List<CourseTopic> topics = _repository.CourseTopics.Where(x => x.CourseId == topic.CourseId && x.OrderNumber > topic.OrderNumber).ToList();
                            topics.ForEach(x => x.OrderNumber--);
                            //delete and save
                            _repository.DeleteTopic(topicId);
                            foreach (var item in topics)
                            {
                                _repository.SaveTopic(item);
                            }
                            return RedirectToAction("EditCourse","Course", new { ID = topic.CourseId });
                        }
                    }
                    catch (Exception e)
                    {
                        return View("Error");
                    }

                }
            }
            else return View("UnauthorizedAccess");
        }

        private void SaveReorderedLectures(List<Lecture> lectures)
        {
            foreach (var item in lectures)
            {
                try
                {
                    Lecture l = _repository.Lectures.Where(x => x.ID == item.ID).First();
                    item.LectureContent = l.LectureContent;
                    item.Homework = l.Homework;
                    item.Name = l.Name;
                    _repository.SaveLecture(item);
                }
                catch { }

            }
        }

        private bool CheckIfLecturerHasAccessToTheTopic(UserInformation user, Guid topicId)
        {
            return user.UserId == _repository.Courses.Where(x => x.ID == _repository.CourseTopics.Where(y => y.ID == topicId).FirstOrDefault().CourseId).First().LecturerId;
        }
    }
}
