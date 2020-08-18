using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using CrucisCo.MathsQuiz.DAL;
using CrucisCo.MathsQuizWeb.Models;

namespace CrucisCo.MathsQuizWeb.Controllers.Api
{
    public class UserStatsApiController : MathsQuizBaseApiController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? (new HttpContextWrapper(HttpContext.Current)).GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public IEnumerable<string> GetUsers()
        {
            return UserManager.Users.Select(u => u.UserName);
        }


        [HttpPost]
        public IEnumerable<MultiplicationQuizResult> GetMultiplicationQuizResults(UserStatsSearch search)
        {
            return GetMultiplicationQuizResultsFromDb(search);
        }

        [HttpPost]
        public IEnumerable<DecimalQuizResult> GetDecimalQuizResults(UserStatsSearch search)
        {
            return GetDecimalQuizResultsFromDb(search);
        }
    }
}
