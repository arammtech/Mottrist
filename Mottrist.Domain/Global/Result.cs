using System;
using System.Collections.Generic;

namespace Mottrist.Domain.Global
{
    public partial class Result
    {
        public bool IsSuccess { get; private set; }
        
        public HashSet<string> Errors { get; private set; } = new HashSet<string>();
        public bool IsExpection { get; private set; } 
        private Result( bool isSuccess,  bool isExpection = false, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            IsExpection = isExpection;
            if (errors != null)
            {
                Errors = new HashSet<string>(errors);
            }
        }
        public Result()
        {
            IsSuccess = false;
            IsExpection = false;
        }
        // Success factory method
        public static Result Success()
        {
            return new Result(true);
        }

        // Failure factory method for single error
        public static Result Failure(string errorMessage, bool isExpcetion = false)
        {
            return new Result(isExpcetion, false, new[] { errorMessage });
        }

        // Failure factory method for multiple errors
        public static Result Failure( IEnumerable<string> errorMessages, bool isExpcetion = false)
        {
            return new Result(isExpcetion,false, errorMessages);
        }

        // Add a single error
        public void AddError(string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Errors.Add(errorMessage);
            }
        }

        // Add multiple errors
        public void AddErrors(IEnumerable<string> errorMessages)
        {
            foreach (var error in errorMessages)
            {
                AddError(error);
            }
        }

        public bool HasErrors => Errors.Any();

        public override string ToString()
        {
            return IsSuccess ? "Success" : $"Failure: {string.Join(", ", Errors)}";
        }
    }
}
