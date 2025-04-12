namespace BrightisRendererV2.Models.UI.Events.Exceptions;

public class EventBrokerageException : Exception
{
    public EventBrokerageException()
    {
    }

    public EventBrokerageException(string message) : base(message)
    {
    }

    public EventBrokerageException(string message, Exception inner) : base(message, inner)
    {
    }
}