using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    #region Before
    public class S_Service
	{
        public void InsertStudent(Student student)
        {
            // insert student
        }

        public void UpdateStudent(Student student)
        {
            // update student
        }

        public void DeleteStudent(Student student)
        {
            // delete student
        }

        public void InsertCourse(Course course)
        {
            // insert course
        }

        public void UpdateCourse(Course course)
        {
            // update course
        }

        public void DeleteCourse(Course course)
        {
            // delete course
        }
    }
    #endregion

    #region After
    public class StudentService
    {
        public void InsertStudent(Student student)
        {
            // insert student
        }

        public void UpdateStudent(Student student)
        {
            // update student
        }

        public void DeleteStudent(Student student)
        {
            // delete student
        }
    }

    public class CourseService
    {
        public void InsertCourse(Course course)
        {
            // insert course
        }

        public void UpdateCourse(Course course)
        {
            // update course
        }

        public void DeleteCourse(Course course)
        {
            // delete course
        }
    }
    #endregion

    public class Student
    {

    }

    public class Course
    {

    }
}
