using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IElearningSystemRepository
    {
        IQueryable<Student> Students { get; }

        void SaveStudent(Student student);

        IQueryable<Answer> Answers { get; }

        IQueryable<Course> Courses { get; }

        void SaveCourse(Course course);

        IQueryable<CourseCategory> CourseCategories { get; }

        IQueryable<CourseRequest> CourseRequests { get; }

        IQueryable<CourseTopic> CourseTopics { get; }

        void SaveTopic(CourseTopic topic);

        IQueryable<CourseType> CourseTypes { get; }

        IQueryable<GivenAnswer> GivenAnswers { get; }

        IQueryable<Homework> Homeworks { get; }

        IQueryable<HomeworkContent> HomeworkContents { get; }

        IQueryable<Lecture> Lectures { get; }

        IQueryable<LectureContent> LectureContents { get; }

        IQueryable<Lecturer> Lecturers { get; }

        void SaveLecturer(Lecturer lecturer);

        IQueryable<Question> Questions { get; }

        IQueryable<QuestionResult> QuestionResults { get; }

        IQueryable<StudentCourse> StudentCourses { get; }

        IQueryable<Test> Tests { get; }

        IQueryable<TestResult> TestResults { get; }
    }
}
