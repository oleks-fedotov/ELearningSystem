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
                        return View("EditLecture", new LectureForEditing() { TopicId = topicId });
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
                            int newOrdNumber = _repository.Lectures.Where(x => x.TopicId == lecture.TopicId).Count() + 1;
                            Lecture lect = new Lecture()
                            {
                                ID = lecture.ID,
                                Name = lecture.Name,
                                TopicId = lecture.TopicId,
                                OrderNumber = newOrdNumber,
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
                        //Check if user can edit this lecture
                        return View();
                    }
                    catch (Exception e)
                    {
                        return View("Error");
                    }

                }
            }
            else return View("UnauthorizedAccess");
        }

    }
}
