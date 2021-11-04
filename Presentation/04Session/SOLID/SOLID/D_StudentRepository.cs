using System;

namespace SOLID
{
    #region Before
    public class StudentRepository
    {
        private readonly MySqlConnection connection;

        public StudentRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public void Save()
        {
            connection.Insert();
        }

        public void Delete()
        {
            connection.Delete();
        }
    }

    public class TeacherRepository
    {
        private readonly PlSqlConnection connection;

        public TeacherRepository(PlSqlConnection connection)
        {
            this.connection = connection;
        }

        public void Save()
        {
            connection.Insert();
        }

        public void Update()
        {
            connection.Update();
        }
    }

    public class MySqlConnection
    {
        public void Insert() { }

        public void Delete() { }

        public void Update() { }
    }

    public class PlSqlConnection
    {
        public void Delete() { }

        public void Insert() { }

        public void Update() { }
    }

    public class EmptyConnectionMock
    {
        public void Delete()
        {
            return;
        }

        public void Insert()
        {
            return;
        }

        public void Update()
        {
            return;
        }
    }
    #endregion

    #region After
    public interface IConnection
    {
        void Insert();
        void Update();
        void Delete();
    }
    #endregion


}
