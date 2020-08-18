using CrucisCo.MathsQuiz.DAL;
using CrucisCo.MathsQuizWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrucisCo.MathsQuizWeb.Controllers.Api
{
    public class MathsQuizBaseApiController : ApiController
    {

        [HttpPost]
        public IEnumerable<UserResultsSummary> GetUserResultsSummary(UserStatsSearch search)
        {
            var results = new List<UserResultsSummary>();
            using (var entitiesContext = new MathsQuizEntities())
            {
                var stats = entitiesContext.GetSummaryStats(search.User, search.FromDate, search.ToDate).ToList();
                               
                stats.ForEach(s =>
                    results.Add(
                        new UserResultsSummary {
                            QuizType = s.QuizType,
                            AverageCorrectPerQuiz = s.AvgCorrectPerQuiz ?? 0,
                            AverageQuestionsPerQuiz = s.AvgCorrectPerQuiz ?? 0,
                            QuizzesTaken = s.QuizzesTaken ?? 0,
                            QuizzesWithOverMinCorrectAnswers = s.QuizzesOverMinAnswers ?? 0,
                            TotalCorrect  = s.TotalCorrect ?? 0,
                            TotalQuestions = s.TotalQuestionsAnswered ?? 0
                        }));
            }

            return results;
        }

        protected IEnumerable<MultiplicationQuizResult> GetMultiplicationQuizResultsFromDb(UserStatsSearch search)
        {

            IEnumerable<MultiplicationQuizResult> results = new List<MultiplicationQuizResult>();
            using (var entitiesContext = new MathsQuizEntities())
            {
                results = entitiesContext.MultiplicationQuizResults
                    .Where(r => r.QuizUser.Equals(search.User))
                    .Where(r => r.QuizDate > search.FromDate)
                    .Where(r => r.QuizDate <= search.ToDate)
                    .OrderBy(r => r.QuizDate);

                results = results.ToList();
            }

            return results;
        }

        protected IEnumerable<DecimalQuizResult> GetDecimalQuizResultsFromDb(UserStatsSearch search)
        {

            IEnumerable<DecimalQuizResult> results = new List<DecimalQuizResult>();
            using (var entitiesContext = new MathsQuizEntities())
            {
                results = entitiesContext.DecimalQuizResults
                    .Where(r => r.QuizUser.Equals(search.User))
                    .Where(r => r.QuizDate > search.FromDate)
                    .Where(r => r.QuizDate <= search.ToDate)
                    .OrderBy(r => r.QuizDate);

                results = results.ToList();
            }

            return results;
        }
    }
}
