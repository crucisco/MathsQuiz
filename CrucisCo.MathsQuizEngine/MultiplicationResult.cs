using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    public class MultiplicationResult
    {
        private readonly MultiplicationEngine _engine;
        
        public MultiplicationResult(MultiplicationProblem problem, MultiplicationEngine engine)
        {
            _engine = engine;
            Problem = problem;

            IsCorrect = _engine.Evaluate(Problem);
        }

        public MultiplicationProblem Problem { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}
