using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Constants
{
    public class RouteConstants
    {
        public const string BaseV1Route = "api/v1";
        public const string AdminSubRoute = "/admin";
        public const string UserSubRoute = "/user";

        //ADMIN
        public const string AdminRoute = $"{BaseV1Route}{AdminSubRoute}";
        public const string TeamRoute = $"{BaseV1Route}{AdminSubRoute}/team";
        public const string UploadRoute = $"{BaseV1Route}{AdminSubRoute}/upload";
        public const string FigureRoute = $"{BaseV1Route}{AdminSubRoute}/figure";
        public const string ChallengeRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/challenge";
        public const string CaseStudyRoute = $"{BaseV1Route}{AdminSubRoute}/case-study";
        public const string OutcomeRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/outcome";
        public const string LeadershipStrategyRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/leadership-strategy";
        public const string CaseStudyFigureRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/figure";
        public const string QuestionRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/question";

        //USER
        public const string UserTeamRoute = $"{BaseV1Route}{UserSubRoute}/team";
        public const string TimingRoute = $"{BaseV1Route}{UserSubRoute}/timing";
        public const string UserCaseStudyRoute = $"{BaseV1Route}{UserSubRoute}/case-study";
        public const string UserQuestionRoute = $"{BaseV1Route}{UserSubRoute}/question";
    }
}