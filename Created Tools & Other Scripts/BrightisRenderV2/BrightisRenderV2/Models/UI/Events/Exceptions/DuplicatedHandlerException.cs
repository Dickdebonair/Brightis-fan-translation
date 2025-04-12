namespace BrightisRendererV2.Models.UI.Events.Exceptions;

public class DuplicatedHandlerException : EventBrokerageException
{
    public DuplicatedHandlerException()
    {
    }

    public DuplicatedHandlerException(string message) : base(message)
    {
    }

    public DuplicatedHandlerException(string message, Exception inner) : base(message, inner)
    {
    }
}