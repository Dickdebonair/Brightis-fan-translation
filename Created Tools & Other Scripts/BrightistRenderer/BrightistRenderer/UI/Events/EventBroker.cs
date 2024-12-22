using BrightistRenderer.Models.UI.Events;
using BrightistRenderer.Models.UI.Events.Exceptions;

namespace BrightistRenderer.UI.Events
{
    public class EventBroker
    {
        public static readonly EventBroker Instance = new();

        private readonly Dictionary<Type, List<Subscription>> _messageSubscriptions;
        private Func<Type, object> _resolverCallback;

        public EventBroker()
        {
            _messageSubscriptions = new Dictionary<Type, List<Subscription>>();
        }

        public void Subscribe<THandler, TMessage>(Action<THandler, TMessage> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscription subscription = new Subscription(handler)
            {
                HandlerType = typeof(THandler)
            };

            AddSubscription<TMessage>(subscription);
        }

        private void AddSubscription<TMessage>(Subscription subscription)
        {
            Type messageType = typeof(TMessage);

            if (!_messageSubscriptions.ContainsKey(messageType))
            {
                _messageSubscriptions[messageType] = new List<Subscription>();
            }

            bool isHandlerAlreadyRegistered = _messageSubscriptions[messageType].Any(s => s.Handler == subscription.Handler);
            if (isHandlerAlreadyRegistered)
            {
                throw new DuplicatedHandlerException("Handler was already registered");
            }

            _messageSubscriptions[messageType].Add(subscription);
        }

        public void Subscribe<THandler, TMessage>(Func<TMessage, bool> filter, Action<THandler, TMessage> handler)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscription subscription = new Subscription(handler)
            {
                Filter = filter,
                HandlerType = typeof(THandler)
            };

            AddSubscription<TMessage>(subscription);
        }

        public void Subscribe<TMessage>(Func<TMessage, bool> filter, Action<TMessage> handler)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscribe(handler);
            _messageSubscriptions[typeof(TMessage)]
                .Single(s => s.Handler == (Delegate)handler)
                .Filter = filter;
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Subscription subscription = new Subscription(handler);

            AddSubscription<TMessage>(subscription);
        }

        public void Raise(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Type messageType = message.GetType();
            bool isSomeoneInterested = _messageSubscriptions.ContainsKey(messageType) && _messageSubscriptions[messageType].Count > 0;
            if (!isSomeoneInterested)
            {
                return;
            }

            List<Subscription> subscriptions = _messageSubscriptions[messageType];

            EnsureResolveCallbackIsSetIfNeeded(subscriptions);

            foreach (Subscription subscription in subscriptions.ToArray())
            {
                RaiseForSubscription(message, subscription);
            }
        }

        private void EnsureResolveCallbackIsSetIfNeeded(List<Subscription> subscriptions)
        {
            bool hasAnyActivationSubscription = subscriptions.Any(s => s.HandlerType != null);
            bool hasResolveCallbackSet = _resolverCallback != null;
            if (hasAnyActivationSubscription && !hasResolveCallbackSet)
            {
                throw new NoResolveCallbackException("Can't activate handler, no resolve callback set.");
            }
        }

        private void RaiseForSubscription(object message, Subscription subscription)
        {
            try
            {
                bool isFilterSet = subscription.Filter != null;
                if (isFilterSet)
                {
                    bool isFilterMatched = (bool)subscription.Filter.DynamicInvoke(message);
                    if (!isFilterMatched)
                    {
                        return;
                    }
                }

                bool shallHandlerTypeBeCreated = subscription.HandlerType != null;
                if (shallHandlerTypeBeCreated)
                {
                    Type handlerType = subscription.HandlerType;
                    object handler = _resolverCallback(handlerType);

                    subscription.Handler.DynamicInvoke(handler, message);
                }
                else
                {
                    subscription.Handler.DynamicInvoke(message);
                }
            }
            catch (Exception e)
            {
                throw new EventBrokerageException("Error raising for subscription", e);
            }
        }

        public void SetResolverCallback(Func<Type, object> resolverCallback)
        {
            if (resolverCallback == null)
            {
                throw new ArgumentNullException(nameof(resolverCallback));
            }

            _resolverCallback = resolverCallback;
        }
    }
}