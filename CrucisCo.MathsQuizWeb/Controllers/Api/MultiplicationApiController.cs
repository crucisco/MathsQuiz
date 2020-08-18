using CrucisCo.MathsQuiz.DAL;
using CrucisCo.MathsQuizEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CrucisCo.MathsQuizWeb.Controllers.Api
{
    public class MultiplicationApiController : MathsQuizBaseApiController
    {
        const string QuizMultiplesSessionKey = "QuizMultiples";
        
        [HttpPost]
        public IDictionary<string, string> GetTables()
        {
            var enumValues = Enum.GetValues(typeof(MultiplicationTable));
           var tableDictionary = new Dictionary<string, string>();

            for (var i=0; i<enumValues.Length; i++)
            {
               string value = ((int)enumValues.GetValue(i)).ToString();
                tableDictionary.Add(value, value);
            }

            
            return tableDictionary;
        }


        [HttpPost]
        public bool SetQuizMultiple([FromBody]IEnumerable<int> multiples)
        {
            var session = HttpContext.Current.Session;
            session.Clear();
            session.Add(QuizMultiplesSessionKey, multiples);

            return true;
        }

        [HttpPost]
        public MultiplicationProblem GetProblem() {
            var engine = new MultiplicationEngine((IEnumerable<int>)HttpContext.Current.Session[QuizMultiplesSessionKey]);

            return engine.CreateRandomMultiplicationProblem();
        }


        [HttpPost]
        public void SetAnswer([FromBody]MultiplicationProblem problem)
        {
            // Save to session
            AddToQuizAnswers(problem);
        }

        [HttpPost]
        public IEnumerable<MultiplicationResult> GetResults()
        {
            return EvaluateAnswers();
        }

        [HttpPost]
        public IEnumerable<MultiplicationQuizResult> GetQuizResultsFromDb()
        {
            return GetMultiplicationQuizResultsFromDb(new Models.UserStatsSearch { User = HttpContext.Current.User.Identity.Name, FromDate = DateTime.Now.AddMonths(-1), ToDate = DateTime.Now });
        }

        private void AddToQuizAnswers(MultiplicationProblem problem)
        {
            var session = HttpContext.Current.Session;
            string sessionKey = GetSessionKey();
            if (!session.Keys.Cast<string>().Contains(sessionKey))
            {
                session.Add(sessionKey, new List<MultiplicationProblem>());
            }

            var answerList = (List<MultiplicationProblem>)session[sessionKey];
            answerList.Add(problem);
        }

        private IEnumerable<MultiplicationResult> EvaluateAnswers()
        {
            var results = new List<MultiplicationResult>();
            
            // get the problems from session
            var answerList = (List<MultiplicationProblem>)HttpContext.Current.Session[GetSessionKey()];

            if (answerList != null && answerList.Count > 0)
            {
                // create an engine
                var engine = new MultiplicationEngine((IEnumerable<int>)HttpContext.Current.Session[QuizMultiplesSessionKey]);


                foreach (var answer in answerList)
                {
                    results.Add(new MultiplicationResult(answer, engine));
                }

                //Save to DB now
                SaveResultToDb(results);
            }

            return results;
        }

        private static void SaveResultToDb(List<MultiplicationResult> results)
        {
            using (var entitiesContext = new MathsQuizEntities())
            {

                entitiesContext.MultiplicationQuizResults.Add(new MultiplicationQuizResult
                {
                    Answered = results.Count(),
                    Correct = results.Count(r => r.IsCorrect),
                    QuizDate = DateTime.Now,
                    QuizUser = HttpContext.Current.User.Identity.Name,
                    TimesTable = GetMultiplesListing()
                });

                entitiesContext.SaveChanges();
            }
        }

        private string GetSessionKey()
        {
            // For now, we're just interestd in the most recent quiz
            return string.Format("QuizAnswers");
        }

        private static string GetMultiplesListing()
        {
            var multiples = (IEnumerable<int>)HttpContext.Current.Session[QuizMultiplesSessionKey];
            string listing = "";
            foreach(int multiple in multiples)
            {
                listing += multiple.ToString() + ",";
            }

            if (listing.EndsWith(","))
            {
                listing = listing.Substring(0, listing.Length - 1);
            }

            return listing;
        }
    }
}