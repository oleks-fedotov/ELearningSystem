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
    public class LecturerController : Controller
    {
        //
        // GET: /Lecturer/

        IElearningSystemRepository _repository;
        MembershipProvider _provider;

        public LecturerController(IElearningSystemRepository repo, MembershipProvider membershipProvider)
        {
            _repository = repo;
            _provider = membershipProvider;
        }

        public ActionResult Index()
        {
            List<LecturerModel> lecturers = _repository.Lecturers.Select(x => new LecturerModel()
                {
                    AcademicStatus = x.IsAcademic,
                    Information = x.Information,
                    Interests = x.Interests,
                    Name = x.Name,
                    Surname = x.Surname
                }).ToList<LecturerModel>();
            return View("AllLecturers", lecturers);
        }

    }
}
