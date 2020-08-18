using CrucisCo.MathsQuiz.DAL;
using CrucisCo.MathsQuizEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CrucisCo.MathsQuizWeb.Controllers.Api
{
    public class DecimalApiController : MathsQuizBaseApiController
    {
        const string QuizDecimalOpertorRangeSessionKey = "DecimalOperatorRange";

        [HttpPost]
        public IDictionary<string, string> GetDecimalOperators()
        {
            var decimalOperators = typeof(DecimalOperator).GetEnumValues().Cast<DecimalOperator>();
            var operatorDictionary = new Dictionary<string, string>();

            foreach(DecimalOperator dop in decimalOperators)
            {
                string name = dop.ToString();
                string value = DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(dop).ToString();
                operatorDictionary.Add(name, value);
            }

            return operatorDictionary;
        }

        [HttpPost]
        public bool SetQuizOperators([FromBody]IEnumerable<int> operators)
        {
            var session = HttpContext.Current.Session;
            session.Clear();

            var decimalOperatorRange = new List<DecimalOperator>();
            decimalOperatorRange.Add(DecimalEngine.GetDecimalOperatorEnumFromDecimalIndex(operators.Min()));
            decimalOperatorRange.Add(DecimalEngine.GetDecimalOperatorEnumFromDecimalIndex(operators.Max()));

            session.Add(QuizDecimalOpertorRangeSessionKey, decimalOperatorRange);

            return true;
        }

        [HttpPost]
        public DecimalProblem GetProblem()
        {
            var engine = CreateEngineFromSession();

            return engine.CreateRandomDecimalProblem();
        }

        [HttpPost]
        public void SetAnswer([FromBody]DecimalProblem problem)
        {
            // Save to session
            AddToQuizAnswers(problem);
        }

        [HttpPost]
        public IEnumerable<DecimalResult> GetResults()
        {
            return EvaluateAnswers();
        }

        [HttpPost]
        public IEnumerable<DecimalQuizResult> GetQuizResultsFromDb()
        {
            return GetDecimalQuizResultsFromDb(new Models.UserStatsSearch { User = HttpContext.Current.User.Identity.Name, FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now });
        }

        private void AddToQuizAnswers(DecimalProblem problem)
        {
            var session = HttpContext.Current.Session;
            string sessionKey = GetSessionKey();
            if (!session.Keys.Cast<string>().Contains(sessionKey))
            {
                session.Add(sessionKey, new List<DecimalProblem>());
            }

            var answerList = (List<DecimalProblem>)session[sessionKey];
            answerList.Add(problem);
        }

        private IEnumerable<DecimalResult> EvaluateAnswers()
        {
            var results = new List<DecimalResult>();

            // get the problems from session
            var answerList = (List<DecimalProblem>)HttpContext.Current.Session[GetSessionKey()];

            if (answerList != null && answerList.Count > 0)
            {
                // create an engine
                DecimalEngine engine = CreateEngineFromSession();

                foreach (var answer in answerList)
                {
                    results.Add(new DecimalResult(answer, engine));
                }

                //Save to DB now
                SaveResultToDb(results);
            }

            return results;
        }

        private static DecimalEngine CreateEngineFromSession()
        {
            var decimalOperatorRange = (IEnumerable<DecimalOperator>)HttpContext.Current.Session[QuizDecimalOpertorRangeSessionKey];
            int floorOp;
            int ceilingOp;
            int firstOp = DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(decimalOperatorRange.First());
            int lastOp = DecimalEngine.GetDecimalIndexFromDecimalOperatorEnum(decimalOperatorRange.Last());
            if (firstOp > lastOp)
            {
                floorOp = lastOp;
                ceilingOp = firstOp;
            }
            else
            {
                floorOp = firstOp;
                ceilingOp = lastOp;
            }

            return new DecimalEngine(DecimalEngine.GetDecimalOperatorEnumFromDecimalIndex(floorOp), DecimalEngine.GetDecimalOperatorEnumFromDecimalIndex(ceilingOp));
        }

        private static void SaveResultToDb(List<DecimalResult> results)
        {
            using (var entitiesContext = new MathsQuizEntities())
            {
                var decimalOperatorRange = (IEnumerable<DecimalOperator>)HttpContext.Current.Session[QuizDecimalOpertorRangeSessionKey];
                entitiesContext.DecimalQuizResults.Add(new DecimalQuizResult
                {
                    Answered = results.Count(),
                    Correct = results.Count(r => r.IsCorrect),
                    QuizDate = DateTime.Now,
                    QuizUser = HttpContext.Current.User.Identity.Name,
                    DecimalRange = string.Format("{0} - {1}", decimalOperatorRange.Min(), decimalOperatorRange.Max())
                });

                entitiesContext.SaveChanges();
            }
        }

        private string GetSessionKey()
        {
            // For now, we're just interestd in the most recent quiz
            return string.Format("QuizAnswers");
        }
    }
}
