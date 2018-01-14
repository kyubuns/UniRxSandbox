using System;
using UniRx;

namespace TypedMessageBroker
{
    public interface ITypedMessagePublisher<T>
    {
        /// <summary>
        /// Send Message to all receiver.
        /// </summary>
        void Publish<T2>(T2 message) where T2 : T;
    }

    public interface ITypedMessageReceiver<T>
    {
        /// <summary>
        /// Subscribe typed message.
        /// </summary>
        IObservable<T2> Receive<T2>() where T2 : T;
    }

    public interface ITypedMessageBroker<T> : ITypedMessagePublisher<T>, ITypedMessageReceiver<T>
    {
    }

    public class TypedMessageBroker<T> : ITypedMessageBroker<T>, IDisposable
    {
        private readonly MessageBroker messageBroker = new MessageBroker();

        public void Publish<T2>(T2 message) where T2 : T
        {
            messageBroker.Publish(message);
        }

        public IObservable<T2> Receive<T2>() where T2 : T
        {
            return messageBroker.Receive<T2>();
        }

        public void Dispose()
        {
            messageBroker.Dispose();
        }
    }
}