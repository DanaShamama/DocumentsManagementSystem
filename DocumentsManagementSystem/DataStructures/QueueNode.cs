using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentsManagementSystem;

namespace DocumentsManagementSystem.DataStructures
{
    public class QueueNode<T>
    {
        //data members
        public Doubly<T> _QueueDoubly;
        public int Counter { get; private set; }
        
        //c'tor
        public QueueNode()
        {
            _QueueDoubly = new Doubly<T>();
            Counter = 0;
        }

        //functions
        public bool EnQueue(T TD, out Doubly<T>.Node refNode) // can be full??? // when does fail?
        {
            _QueueDoubly.AddToEnd(TD);
            refNode = _QueueDoubly.End;
            Counter++;
            return true;
        }

        public bool DeQueue() // is out needed??? what if there is nothing???
        {
            if (_QueueDoubly.Start != null)
            {
                _QueueDoubly.RemoveFirst();
                Counter--;
                return true;
            }
            return false;
        }
    }
}
