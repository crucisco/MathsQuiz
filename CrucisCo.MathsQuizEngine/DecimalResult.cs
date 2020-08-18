using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    public class DecimalResult
    {
        private readonly DecimalEngine _engine;

        public DecimalResult(DecimalProblem problem, DecimalEngine engine)
        {
            _engine = engine;
            Problem = problem;

            IsCorrect = _engine.Evaluate(Problem);
        }

        public DecimalProblem Problem { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}
