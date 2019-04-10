using System;

namespace ApiMashup.Validation
{
    /// <summary>
    /// Validation exception that is thrown if the validation fails
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message, params object[] args)
            : base(String.Format(message, args))
        {
        }
    }

    /// <summary>
    /// Abstract Base class for validation classes. 
    /// Implements the Validate function that checks
    /// the valid state and throws the validationException
    /// together with the error message if false.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValidationBase<T> : IValidation where T : class
    {
        protected T Context { get; private set; }
        protected ValidationBase(T context)
        {
            Context = context ?? throw new ArgumentNullException("context");
        }
        public void Validate()
        {
            if (!IsValid)
            {
                throw new ValidationException(Message);
            }
        }
        public abstract bool IsValid { get; }
        public abstract string Message { get; }
    }
}