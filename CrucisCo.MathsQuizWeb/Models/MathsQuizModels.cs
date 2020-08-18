using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrucisCo.MathsQuizWeb.Models
{
    public class UserStatsSearch
    {
        public string User { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class UserResultsSummary
    {
        public string QuizType { get; set; }
        public int QuizzesTaken { get; set; }
        public int QuizzesWithOverMinCorrectAnswers { get; set; }
        public int AverageQuestionsPerQuiz { get; set; }
        public int AverageCorrectPerQuiz { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalCorrect { get; set; }
    }

    public class MultiplicationResults
    {

    }

    public class MathsQuizModels
    {
    }
}