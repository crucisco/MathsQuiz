using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    /// <summary>
    /// DecimalProblem defines the structure of a quiz problem in the decimal quiz
    /// </summary>
    public class DecimalProblem
    {
        public DecimalProblem(double number, DecimalOperator decimalOperator, Operation operation, int decimalPlacesInNumber)
        {
            Number = number;
            OperatorIndex = decimalOperator;            
            ProblemOperation = operation;
            DecimalPlacesInNumber = decimalPlacesInNumber;
            Answer = null;
        }

        public double Number { get; set; }

        public int DecimalPlacesInNumber { get; set; }

        public DecimalOperator OperatorIndex { get; set; }

        public double Operator
        {
            get
            {
                return Math.Pow(10, DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(OperatorIndex));
            }
        }

        public Operation ProblemOperation { get; set; }
        
        public bool IsAnswered { get { return Answer.HasValue; } }

        public double? Answer { get; set; }

        public int GetExpectedDecimalPlacesInAnswer()
        {
            int places = DecimalPlacesInNumber;
            if (ProblemOperation == Operation.Multiply)
            {
                places = DecimalPlacesInNumber - DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(OperatorIndex);
            }
            else
            {
                places = DecimalPlacesInNumber + DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(OperatorIndex);
            }

            return (places < 0) ? 0 : places;
        }
    }
}
