using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    #region Before
    public interface I_Study
    {
        void createPaper();

        void createThesis();
    }

    public class PostGraduateStudent : IPaperable
    {
        public void createPaper()
        {
            // create a paper
        }
    }

    public class UnderGraduateStudent : IThesisable
    {
        public void createThesis()
        {
            // create a thesis
        }
	}
    #endregion

    #region After
    public interface IPaperable
    {
        void createPaper();
    }

    public interface IThesisable
    {
        void createThesis();
    }
    #endregion
}
