using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CrucisCo.MathsQuizEngine
{
	public class MultiplicationEngine
	{
		private readonly Random randomizer = new Random();
		private const int MaxMultiplier = 12;
		private IEnumerable<MultiplicationTable> _quizTables;

		public MultiplicationEngine(IEnumerable<MultiplicationTable> quizTables)
		{
			_quizTables = quizTables;
		}

		public MultiplicationEngine(IEnumerable<int> quizMultiples)
		{
			var tables = new List<MultiplicationTable>();
			foreach (int quizMultiple in quizMultiples)
			{
				tables.Add((MultiplicationTable)Enum.Parse(typeof(MultiplicationTable), quizMultiple.ToString()));
			}

			_quizTables = tables;
		}

		private int RandomMultiplier
		{
			get
			{
				return randomizer.Next(0, MaxMultiplier + 1);
			}
		}

		private int RandomMultiple
		{
			get
			{
				// get a random index
				int randomIndex = randomizer.Next(0, _quizTables.Count());
				return (int)_quizTables.ToArray()[randomIndex];
			}
		}

		private IEnumerable<int> QuizMultiples
		{
			get 
			{
				return _quizTables.Select(q => (int)q);
			}
		}

		
		public MultiplicationProblem CreateRandomMultiplicationProblem()
		{
			return new MultiplicationProblem(RandomMultiple, RandomMultiplier);
		}

		public bool Evaluate(MultiplicationProblem problem)
		{
			return problem.Answer == problem.Multiple * problem.Multiplier;			
		}
	}
}
