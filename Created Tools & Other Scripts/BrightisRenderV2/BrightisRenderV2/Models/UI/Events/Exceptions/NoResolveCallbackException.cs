namespace BrightisRendererV2.Models.UI.Events.Exceptions;

public class NoResolveCallbackException : EventBrokerageException
{
    public NoResolveCallbackException()
    {
    }

    public NoResolveCallbackException(string message) : base(message)
    {
    }

    public NoResolveCallbackException(string message, Exception inner) : base(message, inner)
    {
    }
}