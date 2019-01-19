using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsManagementSystem.DataStructures
{
  public  class Doubly<T> : IEnumerable<T>
    {   
        //data members
        public Node Start { get; private set; }
        private Node _end;

        public Node End
        {
            get { return _end; }
        }

        public int Counter { get; private set; }
        
        //functions
        public void AddToStart(T newItem)
        {
            Node temp = new Node(newItem);
            temp.Next = Start;
            if (Start == null)
            {
                _end = temp;
            }
            if (Start != null)
            {
                Start.Previous = temp;
            }
            Start = temp;
            Counter++;
        }

        public void AddToEnd(T newItem)
        {
            if (Start == null)
            {
                AddToStart(newItem);
                return;
            }
            Node temp = new Node(newItem);
            if (Start != null)
            {
                temp.Previous = _end;
            }
            _end.Next = temp;
            _end = temp;
            Counter++;
        }

        public bool RemoveFirst()
        {
            if (Start == null)
            {
                return false;
            }
            Start = Start.Next;
            if (Start != null)
            {
                Start.Previous = null;
            }
            if (Start == null)
            {
                _end = null;
            }
            Counter--;
            return true;
        }
        
        public bool RemoveLast()
        {
            if (_end == null)
            {
                return false;
            }
            _end = _end.Previous;
            if (_end == null)
            {
                Start = _end;
            }
            if (_end != null)
            {
                _end.Next = null;
            }
            Counter--;
            return true;
        }

        public bool RemoveNode(Node nodeToRemove) // check if first/last/only two/whatever
        {
            if (nodeToRemove == Start)
            {
                return RemoveFirst();
            }

            if (nodeToRemove == _end)
            {
                return RemoveLast();
            }

            (nodeToRemove.Previous).Next = nodeToRemove.Next;
            (nodeToRemove.Next).Previous = nodeToRemove.Previous;

            Counter--;
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node temp = Start;
            while (temp != null)
            {
                yield return temp.Data;
                temp = temp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node
        {   
            //data members
            public Node Previous { get; set; }
            public Node Next { get; set; }
            public T Data;

            //c'tor
            public Node(T data)
            {
                this.Data = data;
            }
        }
    }
}
