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
    public class CourseRequestsController : Controller
    {
        //
        // GET: /CoursesRequest/

        IElearningSystemRepository _repository;
        MembershipProvider _provider;

        public CourseRequestsController(IElearningSystemRepository repo, MembershipProvider membershipProvider)
        {
            _repository = repo;
            _provider = membershipProvider;
        }

        public ActionResult Index(UserInformation user)
        {
            if (user != null)
            {
                if (user.IsStudent)
                {
                    List<StudentCourseRequestModel> requests = (from x in _repository.CourseRequests
                                                                where x.StudentId == user.UserId
                                                                select new StudentCourseRequestModel()
                                                                {
                                                                    CourseId = x.CourseId,
                                                                    Id = x.ID,
                                                                    Date = x.Date,
                                                                    CourseName = _repository.Courses.Where(c => c.ID == x.CourseId).Select(s => s.Name).FirstOrDefault(),
                                                                    WasApproved = !x.IsDeclined,
                                                                }).OrderByDescending(x => x.Date).ToList<StudentCourseRequestModel>();
                    return View("StudentCourseRequests", requests);
                }
                else
                {
                    List<Guid> courseIds = _repository.Courses.Where(x => x.LecturerId == user.UserId).Select(m => m.ID).ToList<Guid>();
                    List<LecturerCourseRequestModel> requests = (from x in _repository.CourseRequests
                                                                 where courseIds.Contains(x.CourseId) &&
                                                                         x.IsDeclined == true
                                                                 select new LecturerCourseRequestModel()
                                                                 {
                                                                     CourseId = x.CourseId,
                                                                     Id = x.ID,
                                                                     StudentId = x.StudentId,
                                                                     Message = x.Message,
                                                                     Date = x.Date,
                                                                     CourseName = _repository.Courses.Where(c => c.ID == x.CourseId).Select(s => s.Name).FirstOrDefault(),
                                                                     StudentName = _repository.Students.Where(s => s.ID == x.StudentId).Select(n => n.Name + " " + n.Surname).FirstOrDefault()
                                                                 }).OrderByDescending(d => d.Date).ToList<LecturerCourseRequestModel>();
                    return View("LecturerCourseRequests", requests);
                }
            }
            else
            {
                return View("UnauthorizedAccess");
            }
        }

        [HttpPost]
        public JsonResult ApproveRequest(UserInformation user, Guid courseId, Guid studentId)
        {
            if (user != null)
            {
                if (CheckIfLecturerCanAccessTheCourse(user.UserId.Value, courseId))
                {
                    CourseRequest request = _repository.CourseRequests.Where(x => x.StudentId == studentId && x.CourseId == courseId).First();
                    request.IsDeclined = false;
                    _repository.SaveCourseRequest(request);
                    _repository.SaveStudentCourse(new StudentCourse() { StudentId = studentId, CourseId = courseId, StartDate = DateTime.Now });
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult DeleteRequest(UserInformation user, Guid id)
        {
            if (user != null)
            {
                Guid courseId = _repository.CourseRequests.Where(x => x.ID == id).Select(m => m.CourseId).FirstOrDefault();
                if (courseId != Guid.Empty && CheckIfLecturerCanAccessTheCourse(user.UserId.Value, courseId))
                {
                    _repository.DeleteCourseRequest(id);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }
        }

        private bool CheckIfLecturerCanAccessTheCourse(Guid lecturerId, Guid courseId)
        {
            return lecturerId == _repository.Courses.Where(x => x.ID == courseId).First().LecturerId;
        }
    }
}
