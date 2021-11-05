using System;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Open/Closed
            GeneralStudent generalStudent = new GeneralStudent("highSchool");
            generalStudent.Introduce();
            #endregion

            #region Liskov Substitution
            AdminDataFileUser accessDataFile = new AdminDataFileUser();
            //accessDataFile.FilePath = @"c:\temp\a.txt";
            accessDataFile.ReadFile();
            accessDataFile.WriteFile();

            RegularDataFileUser accessDataFileR = new RegularDataFileUser();
            //accessDataFileR.FilePath = @"c:\temp\a.txt";
            accessDataFileR.ReadFile();
            //accessDataFileR.WriteFile(); // throw new NotImplementedException
            #endregion

            #region Dependency Injection
            IConnection studentConnection = new MySqlConnection();
            IConnection teacherConnection = new PlSqlConnection();

            StudentRepository studentRepository = new StudentRepository(studentConnection);
            studentRepository.Save();
            studentRepository.Delete();

            TeacherRepository teacherRepository = new TeacherRepository(teacherConnection);
            teacherRepository.Save();
            teacherRepository.Update();
            #endregion
        }
    }
}
