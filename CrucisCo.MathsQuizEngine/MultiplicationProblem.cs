using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    /// <summary>
    /// MultiplicationProblem defines the structure of a quiz problem
    /// </summary>
    public class MultiplicationProblem
    {
        public MultiplicationProblem(int multiple, int multiplier)
        {
            Multiple = multiple;
            Multiplier = multiplier;
            Answer = null;
        }
        public int Multiple { get; private set; }
        public int Multiplier { get; private set; }
        
        public bool IsAnswered { get { return Answer.HasValue; } }

        public int? Answer { get; set; }
    }
}
