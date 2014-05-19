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

        [HttpPost]
        public ActionResult DeleteLecture(UserInformation user, Guid lectureId)
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
                            //Change order
                            Lecture lecture = _repository.Lectures.Where(x => x.ID == lectureId).First();
                            List<Lecture> lectures = _repository.Lectures.Where(x => x.TopicId == lecture.TopicId && x.OrderNumber > lecture.OrderNumber).ToList();
                            lectures.ForEach(x => x.OrderNumber--);
                            //delete and save
                            _repository.DeleteLecture(lectureId);
                            foreach (var item in lectures)
                            {
                                _repository.SaveLecture(item);
                            }
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

        [HttpPost]
        public MvcHtmlString ShowLectureContent(UserInformation user, Guid lectureId)
        {
            if (user != null)
            {

                if (isCoursePublic(lectureId))
                {
                    if (lectureId == Guid.Empty)
                        return MvcHtmlString.Create("<h3>There is no more lectures to show</h3>");
                    else
                        return GetLectureContent(user, lectureId);
                }
                else
                {
                    if (CheckIfStudentIsSubscribedForCourse(user.UserId.Value, lectureId))
                    {
                        if (lectureId == Guid.Empty)
                            return MvcHtmlString.Create("<h3>There is no more lectures to show</h3>");
                        else
                            return GetLectureContent(user, lectureId);  
                    }
                    else
                    {
                        return MvcHtmlString.Create("<h3>You should be subscribed for this course to see the lecture content.</h3>");
                    }
                }
            }
            else
            {
                return MvcHtmlString.Create("<h3>You should be authorized to see the lecture content.</h3>");
            }
        }

        [HttpPost]
        public MvcHtmlString ShowNextLecture(UserInformation user, Guid lectureId)
        {
            if (user != null)
            {
                _repository.SaveWatchedLecture(new WatchedLecture() { StudentId = user.UserId.Value, CourseId = GetCourseIdByLectureId(lectureId), LectureId = lectureId });
            }
            return ShowLectureContent(user, GetNextLectureId(lectureId));
        }

        [HttpPost]
        public MvcHtmlString ShowPrevLecture(UserInformation user, Guid lectureId)
        {
            return ShowLectureContent(user, GetPrevLectureId(lectureId));
        }

        [HttpPost]
        public Guid AjaxGetNextLectureId(Guid lectureId)
        {
            return GetNextLectureId(lectureId);
        }

        [HttpPost]
        public Guid AjaxGetPrevLectureId(Guid lectureId)
        {
            return GetPrevLectureId(lectureId);
        }

        public MvcHtmlString GetLectureContent(UserInformation user, Guid lectureId)
        {
            var lect = _repository.Lectures.Where(x => x.ID == lectureId).Select(c => c.LectureContent);
            if (lect.Count() > 0)
            {
                return MvcHtmlString.Create(lect.First());
            }
            else
            {
                return MvcHtmlString.Create("<h3>There is no more lectures to show.</h3>");
            }
        }

        private Guid GetNextLectureId(Guid lectureId)
        {
            Lecture l = _repository.Lectures.Where(x => x.ID == lectureId).First();
            var nextLectures = _repository.Lectures.Where(x => x.OrderNumber > l.OrderNumber).OrderBy(k => k.OrderNumber);
            return nextLectures.Count() > 0 ? nextLectures.First().ID : Guid.Empty;
        }

        private Guid GetPrevLectureId(Guid lectureId)
        {
            Lecture l = _repository.Lectures.Where(x => x.ID == lectureId).First();
            var nextLectures = _repository.Lectures.Where(x => x.OrderNumber < l.OrderNumber).OrderBy(k => k.OrderNumber);
            return nextLectures.Count() > 0 ? nextLectures.Last().ID : Guid.Empty;
        }

        private Guid GetCourseIdByLectureId(Guid lectureId)
        {
            return _repository.CourseTopics.Where(x => x.ID == _repository.Lectures.Where(t => t.ID == lectureId).Select(m => m.TopicId).FirstOrDefault()).Select(s => s.CourseId).First();
        }

        private bool isCoursePublic(Guid lectureId)
        {
            return _repository.CourseTypes.Where(z => z.ID == (_repository.Courses.Where(x => x.ID ==
                _repository.CourseTopics.Where(t => t.ID == _repository.Lectures.Where(l => l.ID == lectureId).
                    FirstOrDefault().TopicId).FirstOrDefault().CourseId).FirstOrDefault().CourseTypeId)).First().TypeName.ToLower() == "public";
        }

        private bool CheckIfStudentIsSubscribedForCourse(Guid studentId, Guid lectureId)
        {
            return _repository.StudentCourses.Where(x => x.StudentId == studentId &&
                x.CourseId == _repository.CourseTopics.Where(m => m.ID == _repository.Lectures.Where(l => l.ID == lectureId).FirstOrDefault().TopicId).FirstOrDefault().CourseId)
                .Count() > 0;
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
