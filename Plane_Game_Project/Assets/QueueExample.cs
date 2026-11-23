using UnityEngine;
using System.Collections; // this is needed for  Queue universalQ = new Queue();
using System.Collections.Generic;// this is neccessary for using queues, otherwise it will be error in console. This is needed for Queue<T>
public class QueueExample : MonoBehaviour
{
    [ContextMenu("Output")] // calls the function below from inspector
    void OutputVoid()
    {
        // to create Queue<T> - using System.Collections.Generic; - this is needed

        Queue<string> ExampleQ = new Queue<string>(); // this line creates new queue, this is modern way to create a queue in modern C#

        ExampleQ.Enqueue("test"); // this adds new item to the end of the queue. Enqueue is a key word that used to add new item to a q
        ExampleQ.Enqueue("example"); // this adds item to the end of the queue

        // now the queue has 2 items

        Debug.Log(ExampleQ.Dequeue()); // outputs "test" - the first item in the q. Dequeue is a key word used to get&remove the first item in a queue

        // now queue has one item left : "example"

        Debug.Log(ExampleQ.Peek()); // Peek is a key word, it reads only the first item in the queue, but unlike Dequeue(), Peek() doesnt remove the item from a queue
        Debug.Log(ExampleQ.Dequeue()); // outputs "example" again because its still in the queue

        Queue universalQ = new Queue(); // using System.Collections;  - this is required at the top of the script otherwise it will cause error

        universalQ.Enqueue("example of a text");
        universalQ.Enqueue(123);

        // universalQ doesnt have set data type, this suggest it can have items with different data types.

        
    }

}
