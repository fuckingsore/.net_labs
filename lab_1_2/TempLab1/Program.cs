using System;
using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        public static void DisplayMessage(object sender, MyDictEventArgs args)
        {
            Console.WriteLine($"Item added with value - {args.Message}");
        }
        static void Main(string[] args)
        {
            var myDict = new MyDict<int, string>();
            myDict.OnAddHandler += DisplayMessage;
            var firstElem = new KeyValuePair<int, string>(1, "Banana");
            myDict.Add(firstElem);
            myDict.Add(2, "Apple");
            
            Console.WriteLine();
            
            Console.WriteLine("Iterating through dictionary:");
            foreach (var pair in myDict)
            {
                Console.WriteLine($"Key {pair.Key}, Value {pair.Value}");
            }
            
            Console.WriteLine();
            
            Console.WriteLine($"Does collection contains `1, Banana`: {myDict.Contains(firstElem)}");
            Console.WriteLine($"Does collection contains `3, Orange`: {myDict.Contains(new KeyValuePair<int, string>(1, "Eric"))}");
            
            Console.WriteLine();

            Console.WriteLine("Copying to dict from array");
            myDict.CopyTo(
                new KeyValuePair<int, string>[]
                    {new KeyValuePair<int, string>(3, "Orange"), new KeyValuePair<int, string>(4, "Melon")}, 1);
            
            
            Console.WriteLine();
            
            Console.WriteLine("Iterating through dictionary:");
            foreach (var pair in myDict)
            {
                Console.WriteLine($"Key {pair.Key}, Value {pair.Value}");
            }
            
            Console.WriteLine("Removing `1, Banana`...");

            myDict.Remove(firstElem);
            
            Console.WriteLine();
            
            Console.WriteLine("Iterating through dictionary:");
            foreach (var pair in myDict)
            {
                Console.WriteLine($"Key {pair.Key}, Value {pair.Value}");
            }
            
            Console.WriteLine();
            
            Console.WriteLine($"Amount of elements in the dict {myDict.Count}");
            
            Console.WriteLine();
            
            Console.WriteLine($"Does dict countain key `1`: {myDict.ContainsKey(1)}");
            Console.WriteLine($"Does dict countain key `2`: {myDict.ContainsKey(2)}");
            
            Console.WriteLine();
            
            Console.WriteLine("Removing item by key `2`");
            myDict.Remove(2);

            Console.WriteLine();
            
            Console.WriteLine("Iterating through dictionary:");
            foreach (var pair in myDict)
            {
                Console.WriteLine($"Key {pair.Key}, Value {pair.Value}");
            }
            
            Console.WriteLine();
            
            Console.WriteLine("Trying get value with key 3");
            myDict.TryGetValue(3, out string value);
            Console.WriteLine($"Value: {value}");
            
            Console.WriteLine();
            
            Console.WriteLine("Accessing data with indexers:");
            Console.WriteLine($"Value of key 3: {myDict[3]}");
            
            Console.WriteLine();
            
            Console.WriteLine("Setting data with indexers - let`s set value of key 3 to Orange");
            myDict[3] = "Orange";
            Console.WriteLine($"Value of key 3: {myDict[3]}");
            
            Console.WriteLine();
            
            Console.WriteLine("Iterating through keys:");
            foreach (var key in myDict.Keys)
            {
                Console.WriteLine($"Key - {key}");
            }
            
            Console.WriteLine();
            
            Console.WriteLine("Iterating through values:");
            foreach (var val in myDict.Values)
            {
                Console.WriteLine($"Value - {val}");
            }
        }
    }
}
