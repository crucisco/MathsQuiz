using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrucisCo.MathsQuizEngine;
using System.Collections.Generic;

namespace CrucisCo.MathsQuizEngineTests
{
	[TestClass]
	public class DecimalEngineTests
	{
		#region Constructor tests
		[TestMethod]
		public void DecimalOperatorPower1IsCreatedAsExpected()
		{
			// Arrange
			var engine = new DecimalEngine(DecimalOperator.Tens, DecimalOperator.Tens);
			var p = engine.CreateRandomDecimalProblem();

			// Act

			// Assert		
			Assert.AreEqual(10, p.Operator);
		}

		[TestMethod]
		public void DecimalOperatorPowerMinus4IsCreatedAsExpected()
		{
			// Arrange
			var engine = new DecimalEngine(DecimalOperator.TenThousandths, DecimalOperator.TenThousandths);
			var p = engine.CreateRandomDecimalProblem();

			// Act

			// Assert		
			Assert.AreEqual(0.0001, p.Operator);
		}

		#endregion

		#region Answer Evaluation tests

		[TestMethod]
		public void EvaluateReturnsFalseIfAnswerNotCorrect()
		{
			// Arrange
			var engine = new DecimalEngine();
			var p = engine.CreateRandomDecimalProblem();
			var expectedAnswer = p.ProblemOperation == Operation.Multiply ? p.Number * p.Operator : p.Number / p.Operator;

			// Act
			p.Answer = Math.Round(expectedAnswer + 1, 5); // Just to make it incorrect

			// Assert		
			Assert.IsFalse(engine.Evaluate(p));
		}

		[TestMethod]
		public void EvaluateReturnsTrueIfAnswerCorrect()
		{
			// Arrange
			var engine = new DecimalEngine();
			var p = engine.CreateRandomDecimalProblem();
			var expectedAnswer = p.ProblemOperation == Operation.Multiply ? p.Number * p.Operator : p.Number / p.Operator;

			// Act
			p.Answer = Math.Round(expectedAnswer, p.GetExpectedDecimalPlacesInAnswer());

			// Assert		
			Assert.IsTrue(engine.Evaluate(p));
		}

		#endregion
	}
}
