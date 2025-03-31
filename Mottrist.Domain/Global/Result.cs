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
        public bool IsException { get; private set; }
        public bool IsNotFound { get; private set; }
        public bool IsExist { get; private set; } // New property to indicate duplicate resource

        private Result(bool isSuccess, bool isException = false, bool isNotFound = false, bool isExist = false, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            IsException = isException;
            IsNotFound = isNotFound;
            IsExist = isExist;

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
        public static Result Failure(string errorMessage, bool isException = false)
        {
            return new Result(isSuccess: false, isException: isException, errors: new[] { errorMessage });
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a failure with multiple error messages.
        /// </summary>
        public static Result Failure(IEnumerable<string> errorMessages, bool isException = false)
        {
            return new Result(isSuccess: false, isException: isException, errors: errorMessages);
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a "not found" error.
        /// </summary>
        public static Result NotFound(string errorMessage)
        {
            return new Result(isSuccess: false, isNotFound: true, errors: new[] { errorMessage });
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> instance representing a resource that already exists.
        /// </summary>
        public static Result Exist(string errorMessage)
        {
            return new Result(isSuccess: false, isExist: true, errors: new[] { errorMessage });
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
            if (IsSuccess) return "Success";
            if (IsNotFound) return $"Not Found: {string.Join(", ", Errors)}";
            if (IsExist) return $"Already Exists: {string.Join(", ", Errors)}";
            return IsException
                ? $"Exception: {string.Join(", ", Errors)}"
                : $"Failure: {string.Join(", ", Errors)}";
        }
    }
}
