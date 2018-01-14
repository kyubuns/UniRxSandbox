using System;
using System.Collections.Generic;
using UniRx;

namespace QueueMessageBroker
{
    public class QueueAsyncMessageBroker
    {
        private bool running;
        private readonly AsyncMessageBroker broker = new AsyncMessageBroker();
        private readonly List<Action> queue = new List<Action>();

        public void Publish<T>(T message)
        {
            queue.Add(() =>
            {
                running = true;
                broker.PublishAsync(message).Subscribe(_ =>
                {
                    running = false;
                    Next();
                });
            });
            Next();
        }

        private void Next()
        {
            if (running || queue.Count == 0) return;

            var e = queue[0];
            queue.RemoveAt(0);
            e();
        }

        public IDisposable Subscribe<T>(Func<T, IObservable<Unit>> asyncMessageReceiver)
        {
            return broker.Subscribe(asyncMessageReceiver);
        }
    }
}