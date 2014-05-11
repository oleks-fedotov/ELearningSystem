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
                IQueryable<CourseSummaryModel> mycourses = from x in _repository.Courses
                                                           where (from k in _repository.StudentCourses where k.StudentId == user.UserId select k.CourseId).Contains(x.ID)
                                                           select new CourseSummaryModel()
                                                           {
                                                               CourseId = x.ID,
                                                               CourseName = x.Name,
                                                               LecturerName = (from l in _repository.Lecturers where l.ID == x.LecturerId select l.Name + " " + l.Surname).FirstOrDefault(),
                                                               Description = x.Description,
                                                           };

                return View("StudentCourses", mycourses);
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
        public ActionResult CreateCourse(UserInformation user, Course course)
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
                    catch (Exception e)
                    {
                        return View("Error");
                    }

                }
            }
            else return View("UnauthorizedAccess");

        }

        [HttpGet]
        public ActionResult EditCourse(UserInformation user, Guid ID)
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
                        if (!CheckIfLecturerCanAccessTheCourse(user, ID))
                            return View("AccessDenied");
                        else
                        {
                            CourseDetails details = new CourseDetails();
                            var course = _repository.Courses.Where(x => x.ID == ID).ToList();
                            if (course.Count() != 0)
                            {
                                details.CourseId = course.First().ID;
                                details.CourseName = course.First().Name;
                                details.Topics = (from x in _repository.CourseTopics where x.CourseId == details.CourseId select new Topic { TopicName = x.Name, ID = x.ID, CourseId = x.CourseId, OrderNumber = x.OrderNumber }).OrderBy(x => x.OrderNumber).ToList<Topic>();
                                for (int i = 0; i < details.Topics.Count; i++)
                                {
                                    Guid topicId = details.Topics[i].ID;
                                    details.Topics[i].LecturesCount = _repository.Lectures.Where(x => x.TopicId == topicId).Count();
                                    details.Topics[i].TestsCount = _repository.Tests.Where(x => x.TopicId == topicId).Count();
                                }
                                //details.Topics = _repository.CourseTopics.Where(x => x.CourseId == details.CourseId);
                            }

                            return View(details);
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
        public ActionResult EditCourse(UserInformation user, CourseDetails details)
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
                        foreach (var item in details.Topics)
                        {
                            _repository.SaveTopic(new CourseTopic() { ID = item.ID, CourseId = item.CourseId, Name = item.TopicName, OrderNumber = item.OrderNumber });
                        }
                        return RedirectToAction("EditCourse", new { ID = details.CourseId });
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
        public ActionResult DeleteCourse(UserInformation user, Guid courseId)
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
                        if (!CheckIfLecturerCanAccessTheCourse(user, courseId))
                            return View("AccessDenied");
                        else
                        {
                            _repository.DeleteCourse(courseId);
                            return RedirectToAction("Index");
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
        public ActionResult AllCourses(int page = 0)
        {

            List<CourseSummaryModel> courses = (from x in _repository.Courses
                                                select new CourseSummaryModel
                                                {
                                                    LecturerId = x.LecturerId,
                                                    CourseId = x.ID,
                                                    CourseName = x.Name,
                                                    Description = x.Description.Substring(0, 150)
                                                }).ToList<CourseSummaryModel>();
            for (int i = 0; i < courses.Count; i++)
            {
                Guid lectId = courses[i].LecturerId;
                Guid courseId = courses[i].CourseId;
                courses[i].LecturerName = _repository.Lecturers.Where(x => x.ID == lectId).Select(x => x.Name).First();
                courses[i].TopicsQuantity = _repository.CourseTopics.Where(x => x.CourseId == courseId).Count();
            }
            return View(courses);
        }

        [HttpGet]
        public ActionResult CourseDetails(UserInformation user, Guid courseId)
        {
            CourseDetailsModel model = new CourseDetailsModel()
            {
                CourseId = courseId,
                CourseName = _repository.Courses.Where(x => x.ID == courseId).First().Name,
                Details = new Dictionary<string, List<string>>()
            };
            foreach (var topic in _repository.CourseTopics.Where(x => x.CourseId == courseId).OrderBy(x => x.OrderNumber).ToList())
            {
                KeyValuePair<string, List<string>> item = new KeyValuePair<string, List<string>>(topic.Name, new List<string>());
                foreach (var lecture in _repository.Lectures.Where(x => x.TopicId == topic.ID).OrderBy(x => x.OrderNumber))
                {
                    item.Value.Add(lecture.Name);
                }
                model.Details.Add(item.Key, item.Value);
            }
            if (user != null)
            {
                if (user.IsStudent == true)
                {
                    ViewBag.CanSubscribe = _repository.CourseRequests.Where(x => x.CourseId == courseId && x.StudentId == user.UserId).Count() == 0;
                    ViewBag.HasAlreadySentRequest = !ViewBag.CanSubscribe;
                }
                else
                    ViewBag.CanSubscribe = false;
            }
            else
            {
                ViewBag.CanSubscribe = false;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SubscribeCourse(UserInformation user, Guid courseId, string message)
        {
            if (user != null)
            {
                if (!user.IsStudent)
                {
                    return View("Students limitations");
                }
                else
                {
                    try
                    {
                        bool isCoursePublic = _repository.CourseTypes.Where(x => x.ID == _repository.Courses.Where(y => y.ID == courseId).FirstOrDefault().CourseTypeId).Select(k => k.TypeName).First() == "public";

                        _repository.SaveCourseRequest(new CourseRequest()
                        {
                            IsDeclined = false,
                            StudentId = user.UserId.Value,
                            CourseId = courseId,
                            Message = message,
                            Date = DateTime.Now
                        });
                        if (isCoursePublic)
                        {
                            _repository.SaveStudentCourse(new StudentCourse()
                            {
                                CourseId = courseId,
                                StudentId = user.UserId.Value
                            });

                        }
                        return Json(true);
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
            }
            return View("UnauthorizedAccess");
        }

        private bool CheckIfLecturerCanAccessTheCourse(UserInformation user, Guid courseId)
        {
            return user.UserId == _repository.Courses.Where(x => x.ID == courseId).First().LecturerId;
        }

        //isn't used
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
