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

    }
}