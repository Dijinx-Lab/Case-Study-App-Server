using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Constants
{
    public class MessageConstants
    {
        //GENERIC MESSAGES
        public const string ServerError = "Internal server error";
        public const string BadRequest = "Bad request submitted and one or more validations failed";
        public const string ItemNotFound = "Item not found";
        public const string ErrorProcessingRequest = "There was an error processing your request, please try again later";
        public const string ItemDeleted = "Item deleted successfully";

        //USER
        public const string EmailOrPassIncorrent = "Email or password is incorrect";
        public const string PasswordsDontMatch = "Passwords do not match";
        public const string EmailTaken = "Email already taken";
        public const string UnauthorizedAccess = "You are unauthorized to access this endpoint";
        public const string UnknownToken = "Malformed, unknown or missing token in the Authorization header";
        public const string UnknownRefreshToken = "Malformed, unknown or missing refresh token in the Refresh header";
        public const string RefreshToken = "Authorization token is expired";
        public const string SignedOut = "Signed out";

        //TEAM
        public const string NameIsRequired = "Name is a required field";
        public const string TeamCodeIsRequired = "Team Code is a required field";

        //UPLOAD
        public const string FileIsRequired = "File is a required field";
        public const string FileSizeLimit = "Max file size limit is 10MB";
    }
}