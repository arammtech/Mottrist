using System;
using System.Collections.Generic;
using System.Linq;

namespace Mottrist.Domain.Global
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success status, errors, and additional information such as exceptions, "not found," or "exists" scenarios.
    /// </summary>
    public partial class Result
    {
        public bool IsSuccess { get; private set; }
        public HashSet<string> Errors { get; private set; } = new HashSet<string>();

        private Result(bool isSuccess , IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            if (errors != null)
            {
                Errors = new HashSet<string>(errors);
            }
        }

        public Result() : this(false) { }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a successful operation.
        /// </summary>
        public static Result Success()
        {
            return new Result(isSuccess: true);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a failure with a single error message.
        /// </summary>
        public static Result Failure(string errorMessage)
        {
            return new Result(isSuccess: false, errors: new[] { errorMessage });
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a failure with multiple error messages.
        /// </summary>
        public static Result Failure(IEnumerable<string> errorMessages)
        {
            return new Result(isSuccess: false, errors: errorMessages);
        }



        /// <summary>
        /// Adds a single error message to the collection of errors.
        /// </summary>
        public void AddError(string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Errors.Add(errorMessage);
            }
        }

        /// <summary>
        /// Adds multiple error messages to the collection of errors.
        /// </summary>
        public void AddErrors(IEnumerable<string> errorMessages)
        {
            foreach (var error in errorMessages.Where(e => !string.IsNullOrWhiteSpace(e)))
            {
                Errors.Add(error);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the result contains any errors.
        /// </summary>
        public bool HasErrors => Errors.Any();

        /// <summary>
        /// Returns a string representation of the result for debugging purposes.
        /// </summary>
        public override string ToString()
        {
            if (IsSuccess) 
              return "Success";
            else 
              return $"Failure: {string.Join(", ", Errors)}";
        }
    }
}
