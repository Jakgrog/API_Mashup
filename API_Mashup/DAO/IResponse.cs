namespace ApiMashup.DAO
{
    /// <summary>
    /// Stores the deserialized information from a response message
    /// retrieved in the Dao.
    /// </summary>
    public interface IResponse
    {
        string GetExceptionMessage();
    }
}