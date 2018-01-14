using UnityEngine;
using UniRx;

namespace TypedMessageBroker.Examples
{
    public class Sample : MonoBehaviour
    {
        public void Start()
        {
            var broker1 = new TypedMessageBroker<IMessageType1>();
            var broker2 = new TypedMessageBroker<IMessageType2>();

            broker1.Receive<Message1Hoge>().Subscribe(x => Debug.Log($"Message1 Hoge {x.Value}"));
            broker1.Receive<Message1Fuga>().Subscribe(x => Debug.Log($"Message1 Fuga {x.Value}"));

            broker2.Receive<Message2Hoge>().Subscribe(x => Debug.Log($"Message2 Hoge {x.Value}"));
            broker2.Receive<Message2Fuga>().Subscribe(x => Debug.Log($"Message2 Fuga {x.Value}"));

            broker1.Publish(new Message1Hoge(0));
            broker1.Publish(new Message1Fuga(1));

            broker2.Publish(new Message2Hoge(0));
            broker2.Publish(new Message2Fuga(1));

            // error CS0311: The type `TypedMessageBroker.Examples.Message2Hoge' cannot be used as type parameter `T2' in the generic type or method
            // broker1.Publish(new Message2Hoge(2));
        }
    }

    public interface IMessageType1
    {
    }

    public class Message1Hoge : IMessageType1
    {
        public Message1Hoge(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class Message1Fuga : IMessageType1
    {
        public Message1Fuga(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public interface IMessageType2
    {
    }

    public class Message2Hoge : IMessageType2
    {
        public Message2Hoge(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class Message2Fuga : IMessageType2
    {
        public Message2Fuga(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}