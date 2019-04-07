using ApiMashup.Validation;

namespace ApiMashup.DAO
{
    /// <summary>
    /// Contains the deserialized information from a response message
    /// retrieved in the Dao.
    /// </summary>
    public interface IResponse
    {
        IValidation Validation { get; set; }
        string GetExceptionMessage();
    }
}