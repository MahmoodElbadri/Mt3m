namespace Restaurant.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string resourceType, string resourceIdentifier):base($"The {resourceType} with the id {resourceIdentifier} was not found")
    {
        
    }
    public NotFoundException(string message) : base(message) { }
}
