using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Dtos.Base;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Helpers
{
    public static class ResponseUtility
    {
        public static ObjectResult ReturnOk(dynamic data)
        {
            return new ObjectResult(new BaseDto
            {
                Status = true,
                Data = data,
            })
            {
                StatusCode = 200
            };
        }
        public static ObjectResult ReturnOk(dynamic? data, string message)
        {
            return new ObjectResult(new BaseDto
            {
                Status = true,
                Data = data,
                Message = message,
            })
            {
                StatusCode = 200
            };
        }
        public static ObjectResult ReturnServerError(string? error)
        {
            Console.WriteLine("INTERNAL SERVER ERROR: " + error);
            return new ObjectResult(new BaseDto
            {
                Status = false,
                Message = error ?? MessageConstants.ServerError,
            })
            {
                StatusCode = 500
            };
        }

        public static ObjectResult ReturnBadRequest(string? error)
        {
            return new ObjectResult(new BaseDto
            {
                Status = false,
                Message = error ?? MessageConstants.BadRequest,
            })
            {
                StatusCode = 400
            };
        }

        public static ObjectResult ReturnUnauthorized(string? error)
        {
            return new ObjectResult(new BaseDto
            {
                Status = false,
                Message = error ?? MessageConstants.UnauthorizedAccess,
            })
            {
                StatusCode = 401
            };
        }
    }
}
