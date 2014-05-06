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
    public class LectureController : Controller
    {
        //
        // GET: /Lecture/

        IElearningSystemRepository _repository;
        MembershipProvider _provider;

        public LectureController(IElearningSystemRepository repo, MembershipProvider membershipProvider)
        {
            _repository = repo;
            _provider = membershipProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateLecture(UserInformation user, Guid topicId)
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
                            return View("EditLecture", new LectureForEditing() { TopicId = topicId, OrderNumber = (_repository.Lectures.Where(x => x.TopicId == topicId).Count() + 1) });
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

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveLecture(UserInformation user, LectureForEditing lecture)
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
                        if (!ModelState.IsValid) return View("EditLecture", lecture);
                        else
                        {
                            Lecture lect = new Lecture()
                            {
                                ID = lecture.ID,
                                Name = lecture.Name,
                                TopicId = lecture.TopicId,
                                OrderNumber = lecture.OrderNumber,
                                Homework = lecture.Homework,
                                LectureContent = lecture.LectureContent
                            };
                            _repository.SaveLecture(lect);

                            return RedirectToAction("EditTopic", "Topic", new { topicId = lecture.TopicId });
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

        [HttpGet]
        public ActionResult EditLecture(UserInformation user, Guid lectureId)
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
                        if (!CheckIfLecturerHasAccess(user, lectureId))
                            return View("AccessDenied");
                        else
                        {
                            //Check if user can edit this lecture
                            Lecture l = _repository.Lectures.Where(x => x.ID == lectureId).First();
                            LectureForEditing lect = new LectureForEditing()
                            {
                                ID = lectureId,
                                Homework = l.Homework,
                                LectureContent = l.LectureContent,
                                OrderNumber = l.OrderNumber,
                                TopicId = l.TopicId,
                                Name = l.Name
                            };
                            return View(lect);
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

        private bool CheckIfLecturerHasAccess(UserInformation user, Guid lectureId)
        {
            return user.UserId == _repository.Courses.Where(x => x.ID == _repository.CourseTopics.Where(y => y.ID == _repository.Lectures.Where(z => z.ID == lectureId).FirstOrDefault().TopicId).FirstOrDefault().CourseId).First().LecturerId;
        }

        private bool CheckIfLecturerHasAccessToTheTopic(UserInformation user, Guid topicId)
        {
            return user.UserId == _repository.Courses.Where(x => x.ID == _repository.CourseTopics.Where(y => y.ID == topicId).FirstOrDefault().CourseId).First().LecturerId;
        }

    }
}
