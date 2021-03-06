﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MathsQuizEntities : DbContext
    {
        public MathsQuizEntities()
            : base("name=MathsQuizEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MultiplicationQuizResult> MultiplicationQuizResults { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DecimalQuizResult> DecimalQuizResults { get; set; }
    
        public virtual ObjectResult<UserSummaryStatResult> GetSummaryStats(string quizUser, Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate)
        {
            var quizUserParameter = quizUser != null ?
                new ObjectParameter("quizUser", quizUser) :
                new ObjectParameter("quizUser", typeof(string));
    
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserSummaryStatResult>("GetSummaryStats", quizUserParameter, fromDateParameter, toDateParameter);
        }
    }
}
