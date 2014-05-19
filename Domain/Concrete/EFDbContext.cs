using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseCategory> CourseCategories { get; set; }

        public DbSet<CourseRequest> CourseRequests { get; set; }

        public DbSet<CourseTopic> CourseTopics { get; set; }

        public DbSet<CourseType> CourseTypes { get; set; }

        public DbSet<GivenAnswer> GivenAnswers { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<HomeworkContent> HomeworkContents { get; set; }

        public DbSet<Lecture> Lectures{ get; set; }

        public DbSet<LectureContent> LectureContents { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionResult> QuestionResults { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<WatchedLecture> WatchedLectures { get; set; }
    }
}