using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrucisCo.MathsQuizEngine;
using System.Collections.Generic;

namespace CrucisCo.MathsQuizEngineTests
{
	[TestClass]
	public class MultiplicationEngineTests
	{
        #region Single MultiplicationTable constructor tests
        [TestMethod]
        public void EnumConstructorSingleTableReturnsSelectedQuizMultiple()
        {
            // Arrange
            const MultiplicationTable testMultiple = MultiplicationTable.Five;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();

            // Act
            int multiple = p.Multiple;

            // Assert		
            Assert.AreEqual((int)testMultiple, multiple);
            Assert.IsFalse(p.IsAnswered);
        }


        [TestMethod]
        public void EnumConstructorSingleTableEvaluateReturnsTrueWhenAnswerIsCorrect()
        {
            // Arrange
            const MultiplicationTable testMultiple = MultiplicationTable.Five;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = p.Multiple * p.Multiplier;

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EnumConstructorSingleTableEvaluateReturnsFalseWhenAnswerIsNotCorrect()
        {
            // Arrange
            const MultiplicationTable testMultiple = MultiplicationTable.Five;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = 33; // just a number that is not a multiple of testMultiple

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsFalse(result);
        }
        #endregion

        #region Single int constructor tests
        [TestMethod]
        public void IntConstructorSingleTableReturnsSelectedQuizMultiple()
        {
            // Arrange
            const int testMultiple = 5;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();

            // Act
            int multiple = p.Multiple;

            // Assert		
            Assert.AreEqual((int)testMultiple, multiple);
            Assert.IsFalse(p.IsAnswered);
        }


        [TestMethod]
        public void IntConstructorSingleTableEvaluateReturnsTrueWhenAnswerIsCorrect()
        {
            // Arrange
            const int testMultiple = 5;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = p.Multiple * p.Multiplier;

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IntConstructorSingleTableEvaluateReturnsFalseWhenAnswerIsNotCorrect()
        {
            // Arrange
            const int testMultiple = 5;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = 33; // just a number that is not a multiple of testMultiple

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsFalse(result);
        }
        #endregion

        #region Multiple int constructor tests
        [TestMethod]
        public void IntConstructorMultipleTableReturnsSelectedQuizMultiple()
        {
            // Arrange
            int[] testMultiples = new[] { 2, 4, 8 };
            var engine = new MultiplicationEngine(testMultiples);
            var p = engine.CreateRandomMultiplicationProblem();

            // Act
            int multiple = p.Multiple;

            // Assert
            Assert.IsTrue(new List<int>(testMultiples).Contains(multiple));
            Assert.IsFalse(p.IsAnswered);
        }


        [TestMethod]
        public void IntConstructorMultipleTableEvaluateReturnsTrueWhenAnswerIsCorrect()
        {
            // Arrange
            const int testMultiple = 5;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = p.Multiple * p.Multiplier;

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IntConstructorMultipleTableEvaluateReturnsFalseWhenAnswerIsNotCorrect()
        {
            // Arrange
            const int testMultiple = 5;
            var engine = new MultiplicationEngine(new[] { testMultiple });
            var p = engine.CreateRandomMultiplicationProblem();
            p.Answer = 33; // just a number that is not a multiple of testMultiple

            // Act
            bool result = engine.Evaluate(p);

            // Assert		
            Assert.IsTrue(p.IsAnswered);
            Assert.IsFalse(result);
        }
        #endregion
    }
}
