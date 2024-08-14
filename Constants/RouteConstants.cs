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

        //ADMIN
        public const string AdminRoute = $"{BaseV1Route}{AdminSubRoute}";
        public const string TeamRoute = $"{BaseV1Route}{AdminSubRoute}/team";
        public const string UploadRoute = $"{BaseV1Route}{AdminSubRoute}/upload";
        public const string FigureRoute = $"{BaseV1Route}{AdminSubRoute}/figure";
        public const string ChallengeRoute = $"{BaseV1Route}{AdminSubRoute}/challenge";
        public const string CaseStudyRoute = $"{BaseV1Route}{AdminSubRoute}/case-study";
        public const string OutcomeRoute = $"{BaseV1Route}{AdminSubRoute}/outcome";
        public const string LeadershipStrategyRoute = $"{BaseV1Route}{AdminSubRoute}/leadership-strategy";
        public const string CaseStudyFigureRoute = $"{BaseV1Route}{AdminSubRoute}/case-study/figure";
    }
}