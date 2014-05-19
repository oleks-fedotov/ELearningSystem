using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EFElearningSystemRepository : IElearningSystemRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<Student> Students
        {
            get
            {
                return _context.Students;
            }
        }

        public void SaveStudent(Student student)
        {
            if (student.ID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                //student.ID = Guid.NewGuid();
                _context.Students.Add(student);
            }
            else
            {
                Student dbEntry = _context.Students.Find(student.ID);
                if (dbEntry != null)
                {
                    dbEntry.Email = student.Email;
                    dbEntry.Information = student.Information;
                    dbEntry.Interests = student.Interests;
                    dbEntry.Name = student.Name;
                    dbEntry.Surname = student.Surname;
                    dbEntry.Uni = student.Uni;
                }
            }
            _context.SaveChanges();
        }

        public IQueryable<Answer> Answers
        {
            get
            {
                return _context.Answers;
            }
        }

        public IQueryable<Course> Courses
        {
            get
            {
                return _context.Courses;
            }
        }

        public void SaveCourse(Course course)
        {
            if (course.ID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                course.CreationDate = DateTime.Now;
                _context.Courses.Add(course);
            }
            else
            {
                Course dbEntry = _context.Courses.Find(course.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = course.Name;
                    dbEntry.RequiredSkills = course.RequiredSkills;
                    dbEntry.EndDate = course.EndDate;
                    dbEntry.Category = course.Category;
                    dbEntry.ComplexityLevel = course.ComplexityLevel;
                    dbEntry.CourseContent = course.CourseContent;
                    dbEntry.CourseType = course.CourseType;
                    dbEntry.Description = course.Description;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteCourse(Guid courseId)
        {
            Course courseForRemoving = _context.Courses.Find(courseId);
            if (courseForRemoving != null)
            {
                _context.Courses.Remove(courseForRemoving);
                _context.SaveChanges();
            }
        }

        public IQueryable<CourseCategory> CourseCategories
        {
            get
            {
                return _context.CourseCategories;
            }
        }

        public IQueryable<CourseRequest> CourseRequests
        {
            get
            {
                return _context.CourseRequests;
            }
        }

        public void SaveCourseRequest(CourseRequest request)
        {
            if (request.ID == Guid.Empty)
            {
                _context.CourseRequests.Add(request);
            }
            else
            {
                CourseRequest dbEntry = _context.CourseRequests.Find(request.ID);
                if (dbEntry != null)
                {
                    dbEntry.CourseId = request.CourseId;
                    dbEntry.IsDeclined = request.IsDeclined;
                    dbEntry.StudentId = request.StudentId;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteCourseRequest(Guid id)
        {
            CourseRequest requestForRemoving = _context.CourseRequests.Find(id);
            if (requestForRemoving != null)
            {
                _context.CourseRequests.Remove(requestForRemoving);
                _context.SaveChanges();
            }
        }

        public IQueryable<CourseTopic> CourseTopics
        {
            get
            {
                return _context.CourseTopics;
            }
        }

        public void SaveTopic(CourseTopic topic)
        {
            if (topic.ID == Guid.Empty)
            {
                _context.CourseTopics.Add(topic);
            }
            else
            {
                CourseTopic dbEntry = _context.CourseTopics.Find(topic.ID);
                if (dbEntry != null)
                {
                    dbEntry.Name = topic.Name;
                    dbEntry.OrderNumber = topic.OrderNumber;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteTopic(Guid topicId)
        {
            CourseTopic topicForRemoving = _context.CourseTopics.Find(topicId);
            if (topicForRemoving != null)
            {
                _context.CourseTopics.Remove(topicForRemoving);
                _context.SaveChanges();
            }
        }

        public IQueryable<CourseType> CourseTypes
        {
            get
            {
                return _context.CourseTypes;
            }
        }

        public IQueryable<GivenAnswer> GivenAnswers
        {
            get
            {
                return _context.GivenAnswers;
            }
        }

        public IQueryable<Homework> Homeworks
        {
            get
            {
                return _context.Homeworks;
            }
        }

        public IQueryable<HomeworkContent> HomeworkContents
        {
            get
            {
                return _context.HomeworkContents;
            }
        }

        public IQueryable<Lecture> Lectures
        {
            get
            {
                return _context.Lectures;
            }
        }

        public void SaveLecture(Lecture lecture)
        {
            if (lecture.ID == Guid.Empty)
                _context.Lectures.Add(lecture);
            else
            {
                Lecture dbEntry = _context.Lectures.Find(lecture.ID);
                if (dbEntry != null)
                {
                    dbEntry.Homework = lecture.Homework;
                    dbEntry.LectureContent = lecture.LectureContent;
                    dbEntry.Name = lecture.Name;
                    dbEntry.OrderNumber = lecture.OrderNumber;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteLecture(Guid lectureId)
        {
            Lecture lectureForRemoving = _context.Lectures.Find(lectureId);
            if (lectureForRemoving != null)
            {
                _context.Lectures.Remove(lectureForRemoving);
                _context.SaveChanges();
            }
        }

        public IQueryable<LectureContent> LectureContents
        {
            get
            {
                return _context.LectureContents;
            }
        }

        public IQueryable<Lecturer> Lecturers
        {
            get
            {
                return _context.Lecturers;
            }
        }

        public void SaveLecturer(Lecturer lecturer)
        {
            if (lecturer.ID.ToString() == "00000000-0000-0000-0000-000000000000")
                _context.Lecturers.Add(lecturer);
            else
            {
                Lecturer dbEntry = _context.Lecturers.Find(lecturer.ID);
                if (dbEntry != null)
                {
                    dbEntry.Email = lecturer.Email;
                    dbEntry.Information = lecturer.Information;
                    dbEntry.Interests = lecturer.Interests;
                    dbEntry.IsAcademic = lecturer.IsAcademic;
                    dbEntry.Name = lecturer.Name;
                    dbEntry.Surname = lecturer.Surname;
                }
            }
            _context.SaveChanges();
        }

        public IQueryable<Question> Questions
        {
            get
            {
                return _context.Questions;
            }
        }

        public IQueryable<QuestionResult> QuestionResults
        {
            get
            {
                return _context.QuestionResults;
            }
        }

        public IQueryable<StudentCourse> StudentCourses
        {
            get
            {
                return _context.StudentCourses;
            }
        }

        public void SaveStudentCourse(StudentCourse studentCourse)
        {
            if (studentCourse.ID == Guid.Empty)
            {
                studentCourse.StartDate = DateTime.Now;
                studentCourse.Mark = 0;
                _context.StudentCourses.Add(studentCourse);
            }
            else
            {
                StudentCourse dbEntry = _context.StudentCourses.Find(studentCourse.ID);
                dbEntry.EndDate = studentCourse.EndDate;
                dbEntry.Mark = studentCourse.Mark;
            }
            _context.SaveChanges();
        }

        public void DeleteStudentCourse(Guid id)
        {
            StudentCourse dbEntry = _context.StudentCourses.Find(id);
            if (dbEntry != null)
            {
                _context.StudentCourses.Remove(dbEntry);
                _context.SaveChanges();
            }
        }

        public IQueryable<Test> Tests
        {
            get
            {
                return _context.Tests;
            }
        }

        public IQueryable<TestResult> TestResults
        {
            get
            {
                return _context.TestResults;
            }
        }

        public IQueryable<WatchedLecture> WatchedLectures
        {
            get
            {
                return _context.WatchedLectures;
            }
        }

        public void SaveWatchedLecture(WatchedLecture watchedLecture)
        {
            if (watchedLecture.ID == Guid.Empty)
            {
                _context.WatchedLectures.Add(watchedLecture);
            }
            _context.SaveChanges();
        }

        public void DeleteWatchedLecture(Guid id)
        {
            WatchedLecture dbEntry = _context.WatchedLectures.Find(id);
            if (dbEntry != null)
            {
                _context.WatchedLectures.Remove(dbEntry);
                _context.SaveChanges();
            }
        }
    }
}
