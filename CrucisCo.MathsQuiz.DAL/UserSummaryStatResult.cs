//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrucisCo.MathsQuiz.DAL
{
    using System;
    
    public partial class UserSummaryStatResult
    {
        public string QuizType { get; set; }
        public Nullable<int> QuizzesTaken { get; set; }
        public Nullable<int> QuizzesOverMinAnswers { get; set; }
        public Nullable<int> AvgAnswersPerQuiz { get; set; }
        public Nullable<int> AvgCorrectPerQuiz { get; set; }
        public Nullable<int> TotalQuestionsAnswered { get; set; }
        public Nullable<int> TotalCorrect { get; set; }
    }
}