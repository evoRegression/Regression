using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    #region Before
    public class GeneralStudent
	{
        public GeneralStudent(string type)
        {
            myType = type;
        }

        private string myType;

        public void Introduce()
        {
            if ("highSchool".Equals(myType))
            {
                Console.WriteLine("I am an high school student");
            }
            else if ("primarySchool".Equals(myType))
            {
                Console.WriteLine("I am a under primary school student");
            }
            else if ("underGraduate".Equals(myType))
            {
                Console.WriteLine("I am a under graduate student");
            }
            else
            {
                Console.WriteLine("I am a common student");
            }
        }
    }
    #endregion

    #region After
    public class ParentStudent
    {
        public virtual void Introduce()
        {
            Console.WriteLine("I am a common student");
        }
    }

    public class HighSchoolStudent : ParentStudent
    {
        public override void Introduce()
        {
            Console.WriteLine("I am a under high school student");
        }
    }
    #endregion
}
