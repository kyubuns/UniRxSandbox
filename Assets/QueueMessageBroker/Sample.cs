using System;
using UnityEngine;
using UniRx;

namespace QueueMessageBroker.Examples
{
    public class Sample : MonoBehaviour
    {
        public void Start()
        {
            var broker = new QueueAsyncMessageBroker();

            broker.Subscribe<Message>(x => Observable.Timer(TimeSpan.FromSeconds(x.Value)).AsUnitObservable()).AddTo(this);

            broker.Subscribe<Message>(x =>
            {
                Debug.Log($"{Time.time} Receive Message {x.Value}");
                return Observable.ReturnUnit();
            }).AddTo(this);

            broker.Publish(new Message(3));
            broker.Publish(new Message(1));
            broker.Publish(new Message(2));
        }
    }

    public class Message
    {
        public Message(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}