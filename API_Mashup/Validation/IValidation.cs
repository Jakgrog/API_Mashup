namespace ApiMashup.Validation
{
    /// <summary>
    /// Validation interface. Contains a bool properties that states
    /// whether the data is valid, a validate function that validates
    /// the data and the response message if the data is not valid.
    /// </summary>
    public interface IValidation
    {
        bool IsValid { get; } // True when valid
        void Validate(); // Throws an exception when not valid
        string Message { get; } // The message when object is not valid
    }
}
