using System;

namespace SOLID
{
    #region Before
    public class StudentRepository
    {
        private readonly IConnection connection;

        public StudentRepository(IConnection connection)
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
        private readonly IConnection connection;

        public TeacherRepository(IConnection connection)
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

    public class MySqlConnection : IConnection
    {
        public void Insert() { }

        public void Delete() { }

        public void Update() { }
    }

    public class PlSqlConnection : IConnection
    {
        public void Delete() { }

        public void Insert() { }

        public void Update() { }
    }

    public class EmptyConnectionMock : IConnection
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
