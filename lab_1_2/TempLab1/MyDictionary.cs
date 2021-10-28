using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    internal class Node<TKey, TVal>
    {
        internal KeyValuePair<TKey, TVal> data;
        internal Node<TKey,TVal> prev;
        internal Node<TKey,TVal> next;

        public Node(TKey key, TVal val)
        {
            data = new KeyValuePair<TKey, TVal>(key, val);
        }

        public Node(KeyValuePair<TKey, TVal> data)
        {
            this.data = data;
        }

    }
    
    public class MyDictEventArgs : EventArgs
    {
        public string Message{get;}

        public MyDictEventArgs(string mes)
        {
            Message = mes;
        }
    }

    public class MyDict<TKey, TVal> : IDictionary<TKey, TVal>
    {

        public delegate void DictHandler(object sender, MyDictEventArgs args);
        public event DictHandler OnAddHandler ;
        
        public MyDict(){}
        public MyDict(DictHandler onAddHandler)
        {
            OnAddHandler += onAddHandler;
        }

        internal Node<TKey, TVal> head;

        private Node<TKey, TVal> GetLastNode()
        {
            Node<TKey, TVal> node = head;
            
            while (node.next != null) {  
                node = node.next;  
            }  
            return node;
        }
        public IEnumerator<KeyValuePair<TKey, TVal>> GetEnumerator()
        {
            Node<TKey, TVal> node = head;
            
            while (node != null)
            {
                yield return node.data;
                node = node.next;  
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TVal> item)
        {
            Node<TKey, TVal> newNode = new Node<TKey, TVal>(item);
            if (this.head == null)
            {
                this.head = newNode;
                if (OnAddHandler != null)
                {
                    OnAddHandler(this, new MyDictEventArgs(item.Value.ToString()));
                }
                return;
            }

            Node<TKey, TVal> lastNode = GetLastNode();
            lastNode.next = newNode;
            if (OnAddHandler != null)
            {
                OnAddHandler(this, new MyDictEventArgs(item.Value.ToString()));
            }
        }

        public void Clear()
        {
            this.head = null;
        }

        public bool Contains(KeyValuePair<TKey, TVal> item)
        {
            Node<TKey, TVal> node = head;
            while (node != null)
            {
                if (node.data.Equals(item))
                {
                    return true;
                }

                node = node.next;
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TVal>[] array, int arrayIndex)
        {
            Node<TKey, TVal> node;
            bool finished = false;
            if (head == null)
            {
                if (arrayIndex != 0)
                {
                    throw new ArgumentException("arrayIndex out of range");
                }

                head = new Node<TKey, TVal>(array[0]);
                node = head;
                
                var firstNode = node;
                var prevNode = node;
                Node<TKey, TVal> curNode = new Node<TKey, TVal>(node.data);
                
                for (int i = 1; i < array.Length; i++)
                {
                    var pair = array[i];
                    Node<TKey, TVal> newNode = new Node<TKey, TVal>(pair);
                    newNode.prev = prevNode;
                    prevNode.next = newNode;
                    prevNode = newNode;
                    curNode = newNode;
                }

                curNode.next = null;
                finished = true;
            }
            
            else if (arrayIndex == 0)
            {
                var oldHead = head;
                head = new Node<TKey, TVal>(array[0]);
                node = head;
                
                var firstNode = node;
                var prevNode = node;
                Node<TKey, TVal> curNode = new Node<TKey, TVal>(node.data);

                for (int i = 1; i < array.Length; i++)
                {
                    var pair = array[i];
                    Node<TKey, TVal> newNode = new Node<TKey, TVal>(pair);
                    newNode.prev = prevNode;
                    prevNode.next = newNode;
                    prevNode = newNode;
                    curNode = newNode;
                }

                curNode.next = oldHead;
                oldHead.prev = curNode;
                finished = true;
            }
            else
            {
                int counter = 0;
                node = head;
                while (node != null)
                {
                    counter++;
                    if (counter == arrayIndex)
                    {
                        var firstNode = node;
                        var lastNode = node.next;
                        var prevNode = node;
                        Node<TKey, TVal> curNode = new Node<TKey, TVal>(node.data);
                        foreach (var pair in array)
                        {
                            Node<TKey, TVal> newNode = new Node<TKey, TVal>(pair);
                            newNode.prev = prevNode;
                            prevNode.next = newNode;
                            prevNode = newNode;
                            curNode = newNode;
                        }

                        curNode.next = lastNode;
                        lastNode.prev = curNode;
                        finished = true;
                    }
                    node = node.next;
                }

            }

            if (!finished)
            {
                throw new ArgumentException("arrayIndex out of range");
            }
        }

        private void PrintFunc()
        {
            var node = head;
            var counter = 0;
            while (node != null)
            {
                ++counter;
                if (node.prev == null)
                {
                    Console.WriteLine($"Elem {counter}({node.data.Key}): prev: null, next: {node.next.data.Key}");
                }
                else if (node.next == null)
                {
                    Console.WriteLine($"Elem {counter}({node.data.Key}): prev: {node.prev.data.Key}, next: null");
                }
                else
                {
                    Console.WriteLine($"Elem {counter}({node.data.Key}): prev: {node.prev.data.Key}, next: {node.next.data.Key}");
                }
                node = node.next;
            }
        }
        

        public bool Remove(KeyValuePair<TKey, TVal> item)
        {
            var node = head;
            while (node != null && node.data.Equals(item) == false) {  
                node = node.next;  
            }
            
            var del = node;
            if (head == null || del == null)
            {
                return false;
            }
 
            if (head == del)
            {
                head = del.next;
            }

            if (del.next != null)
            {
                del.next.prev = del.prev;
            }
 

            if (del.prev != null)
            {
                del.prev.next = del.next;
            }
            
            if (del.next == null)
            {
                node = head;
                if (node != null)
                {
                    while (node.next.next != null)
                    {
                        node = node.next;
                    }

                    node.next = null;
                }
            }
 
            return true;
        }
        

        public int Count
        {
            get
            {
                int counter = 0;
                Node<TKey, TVal> node = head;
                while (node != null)
                {
                    counter++;
                    node = node.next;
                }

                return counter;
            }
        }

        public bool IsReadOnly { get; }
        
        public void Add(TKey key, TVal value)
        {
            Node<TKey, TVal> newNode = new Node<TKey, TVal>(key, value);
            if (this.head == null)
            {
                this.head = newNode;
                return;
            }

            Node<TKey, TVal> lastNode = GetLastNode();
            lastNode.next = newNode;
            newNode.prev = lastNode;

            if (OnAddHandler != null)
            {
                OnAddHandler(this, new MyDictEventArgs(value.ToString()));
            }
        }

        public bool ContainsKey(TKey key)
        {
            Node<TKey, TVal> node = head;
            while (node != null)
            {
                if (node.data.Key.Equals(key))
                {
                    return true;
                }

                node = node.next;
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            var node = head;
            while (node != null && node.data.Key.Equals(key) == false) {  
                node = node.next;  
            }

            if (node == null)
            {
                return false;
            }
            
            var del = node;
            if (head == null || del == null)
            {
                return false;
            }
 
            if (head == del)
            {
                head = del.next;
            }

            if (del.next != null)
            {
                del.next.prev = del.prev;
            }
 

            if (del.prev != null)
            {
                del.prev.next = del.next;
            }

            return true;
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            Node<TKey, TVal> node = head;
            while (node != null)
            {
                if (node.data.Key.Equals(key))
                {
                    value = node.data.Value;
                    return true;
                }

                node = node.next;
            }

            throw new ArgumentException("Invalid key");
        }

        public TVal this[TKey key]
        {
            get
            {
                Node<TKey, TVal> node = head;
                while (node != null)
                {
                    if (node.data.Key.Equals(key))
                    {
                        return node.data.Value;
                    }

                    node = node.next;
                }

                throw new IndexOutOfRangeException("Invalid key");
            }
            set
            {
                Node<TKey, TVal> node = head;
                while (node != null)
                {
                    if (node.data.Key.Equals(key))
                    {
                        node.data = new KeyValuePair<TKey,TVal>(key, value);
                        return;
                    }
                    
                    node = node.next;
                }

                if (head != null)
                {
                    node = GetLastNode();
                    node.next = new Node<TKey, TVal>(key, value);
                    Node<TKey, TVal> lastNode = node.next;
                    lastNode.prev = node;
                }
                else
                {
                    head = new Node<TKey, TVal>(key, value);
                }
                
                
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];
                var node = head;
                var counter = 0;
                while (node != null)
                {
                    keys[counter] = node.data.Key;
                    counter++;
                    node = node.next;
                }

                return keys;
            }

        }

        public ICollection<TVal> Values
        {
            get
            {
                TVal[] vals = new TVal[Count];
                var node = head;
                var counter = 0;
                while (node != null)
                {
                    vals[counter] = node.data.Value;
                    counter++;
                    node = node.next;
                }

                return vals;
            }
        }
    }

}

