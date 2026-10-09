namespace Soccer.BLL.Infrastructure;

public class ValidationException(string message, string prop) : Exception(message)
{
    public string Property { get; protected set; } = prop;
}