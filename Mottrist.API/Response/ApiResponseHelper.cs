using Microsoft.AspNetCore.Mvc;

namespace Mottrist.API.Response
{
    public static class ApiResponseHelper
    {
        public static IActionResult SuccessResponse<T>(T data, string message = "Request successful")
        {
            return new ObjectResult(new ApiResponse<T>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = data,
                Message = message,
                Error = null
            })
            { StatusCode = StatusCodes.Status200OK };
        }

        public static IActionResult SuccessResponse(string message = "Operation completed successfully")
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Data = null,
                Message = message,
                Error = null
            })
            { StatusCode = StatusCodes.Status200OK };
        }

        public static IActionResult BadRequestResponse(string code, string message, params string[] details)
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Data = null,
                Message = message,
                Error = new ApiError(code, details)
            })
            { StatusCode = StatusCodes.Status400BadRequest };
        }

        public static IActionResult NotFoundResponse(string code, string message, params string[] details)
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                Data = null,
                Message = message,
                Error = new ApiError(code,details)
            })
            { StatusCode = StatusCodes.Status404NotFound };
        }

        public static IActionResult NoContentResponse(string message = "No content available")
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = true,
                StatusCode = StatusCodes.Status204NoContent,
                Data = null,
                Message = message,
                Error = null
            })
            { StatusCode = StatusCodes.Status204NoContent };
        }

        public static IActionResult CreatedResponse<T>(string routeName, object routeValues, T data, string message = "Resource created successfully")
        {
            return new CreatedAtRouteResult(routeName, routeValues, new ApiResponse<T>
            {
                Success = true,
                StatusCode = StatusCodes.Status201Created,
                Data = data,
                Message = message,
                Error = null
            });
        }

        public static IActionResult StatusCodeResponse(int statusCode, string code, string message, params string[] details)
        {
            return new ObjectResult(new ApiResponse<object>
            {
                Success = statusCode is >= 200 and < 300, // 200 => 299  #Success
                StatusCode = statusCode,
                Data = null,
                Message = message,
                Error = statusCode is >= 400 ? new ApiError(code, details) : null
            })
            { StatusCode = statusCode };
        }
    }

}
