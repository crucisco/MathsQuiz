using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
    /// <summary>
    /// DecimalEngine class creates and manages problems for the Decimal quiz
    /// The engine randomly chooses a number of up to 4 digits with a decimal point at any position - including before the 4 digits (i.e. 0.dddd)
    /// The engine randomly picks a multiplier or divisor of 10^n where -4 gte n lte 4.
    /// The engine randomly chooses whether to create a problem involving division or multiplication
    /// </summary>
    public class DecimalEngine
    {
        private readonly DecimalOperator _maxDecimalOperator;
        private readonly DecimalOperator _minDecimalOperator;

        private readonly Random randomizer = new Random();

        public DecimalEngine()
        {
            var operators = Enum.GetValues(typeof(DecimalOperator)).Cast<DecimalOperator>().ToList().OrderBy(v => (int)v);
            _minDecimalOperator = (DecimalOperator)Enum.Parse(typeof(DecimalOperator), operators.First().ToString());
            _maxDecimalOperator = (DecimalOperator)Enum.Parse(typeof(DecimalOperator), operators.Last().ToString());
        }

        public DecimalEngine(DecimalOperator minDecimalOperator, DecimalOperator maxDecimalOperator)
        {
            _minDecimalOperator = minDecimalOperator;
            _maxDecimalOperator = maxDecimalOperator;
        }

        private DecimalOperator RandomDecimalOperator
        {
            get
            {
                int randomOperator = randomizer.Next(GetDecimalIndexFromDecimalOperatorEnum(_minDecimalOperator), GetDecimalIndexFromDecimalOperatorEnum(_maxDecimalOperator));
                return GetDecimalOperatorEnumFromDecimalIndex(randomOperator);
            }
        }

        private int RandomDecimalPlacesInNumber
        {
            get
            {
                int randomPlaces = randomizer.Next((int)DecimalPlaces.Zero, (int)DecimalPlaces.Four);
                return randomPlaces;
            }
        }

        private double RandomDecimalNumber
        {
            get
            {
                var number = randomizer.NextDouble();
                return number * Math.Pow(10, GetDecimalIndexFromDecimalOperatorEnum(RandomDecimalOperator));
            }
        }

        private Operation RandomOperation
        {
            get
            {
                return randomizer.Next(1, 10) <= 5 ? Operation.Multiply : Operation.Divide;
            }
        }

        
        public DecimalProblem CreateRandomDecimalProblem()
        {
            int decimalPlaces = RandomDecimalPlacesInNumber;
            return new DecimalProblem(Math.Round(RandomDecimalNumber, decimalPlaces), RandomDecimalOperator, RandomOperation, decimalPlaces);
        }

        public bool Evaluate(DecimalProblem problem)
        {
            if (problem.ProblemOperation == Operation.Multiply)
            {
                return problem.Answer == Math.Round(problem.Number * problem.Operator, problem.GetExpectedDecimalPlacesInAnswer());
            }
            else
            {
                return problem.Answer == Math.Round(problem.Number / problem.Operator, problem.GetExpectedDecimalPlacesInAnswer());
            }
        }

        public static int GetDecimalIndexFromDecimalOperatorEnum(DecimalOperator operatorIndex)
        {
            string operatorIndexValue = typeof(DecimalOperator).GetMember(operatorIndex.ToString()).First().GetCustomAttributesData().First().NamedArguments.First().TypedValue.Value.ToString();
            return int.Parse(operatorIndexValue);
        }

        public static DecimalOperator GetDecimalOperatorEnumFromDecimalIndex(int decimalIndex)
        {
            return typeof(DecimalOperator).GetEnumValues().Cast<DecimalOperator>().First(o => GetDecimalIndexFromDecimalOperatorEnum(o) == decimalIndex);
        }
    }
}
