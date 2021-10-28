using System;
using System.Collections.Generic;
using Lab1;
using Xunit;

namespace TempLab1.Tests
{
    public class MyDictonaryTests
    {
        [Fact]
        public void Add_KeyValuePair_ShouldAddNewElement()
        {
            //arrange
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Banana");
            
            //act
            myDict.Add(data);
            
            //assert
            Assert.True(myDict[data.Key] == data.Value, "Element was not added");
        }
        
        [Fact]
        public void Add_WithTwoParams_ShouldAddNewElement()
        {
            //arrange
            MyDict<int, string> myDict = new MyDict<int, string>();
            int key = 1;
            string value = "Banana";
            
            //act
            myDict.Add(key,value);    
            
            //assert
            Assert.True(myDict[key] == value, "Element was not added");

        }

        [Fact]
        public void Clear_ShouldClearDictionary()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Banana");
            
            //act
            myDict.Add(data);
            myDict.Clear();
            
            //assert
            Assert.False(myDict.Contains(data));
        }
        [Fact]
        public void Contains_ElementExists_ShouldReturnTrue()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Banana");
            
            //act
            myDict.Add(data);
            
            //assert
            Assert.True(myDict.Contains(data));
        }
        
        [Fact]
        public void CopyTo_ShouldCopyTwoElementsToDictionaryByIndex1()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            MyDict<int, string> myDictCopied = new MyDict<int, string>();
            KeyValuePair<int, string> data1 = new KeyValuePair<int, string>(1, "Banana");
            KeyValuePair<int, string> data2 = new KeyValuePair<int, string>(2, "Apple");
            KeyValuePair<int, string> data3 = new KeyValuePair<int, string>(3, "Orange");
            KeyValuePair<int, string> data4 = new KeyValuePair<int, string>(4, "Melon");
            KeyValuePair<int, string>[] data23Arr = {data2, data3};
            
            //act
            myDict.Add(data1);
            myDict.Add(data2);
            myDict.Add(data3);
            myDict.Add(data4);
            
            myDictCopied.Add(data1);
            myDictCopied.Add(data4);
            myDictCopied.CopyTo(data23Arr, 1);
            
            //assert
            Assert.Equal(myDict, myDictCopied);
        }
        
        [Fact]
        public void Remove_TakesKeyValuePair_ShouldRemoveElement()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);
            myDict.Remove(data);
            
            //assert
            Assert.False(myDict.Contains(data));
        }

        [Fact]
        public void CountProperty_DictionaryContainsOneElement_ShouldReturn1()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);
            
            //assert
            Assert.Equal(myDict.Count, 1);
        }
        
        [Fact]
        public void ContainsKey_ElementExists_ShouldReturnTrue()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);
            
            //assert
            Assert.True(myDict.ContainsKey(data.Key));
        }
        
        [Fact]
        public void Remove_TakesKey_ShouldRemoveElement()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);
            myDict.Remove(data.Key);
            
            //assert
            Assert.False(myDict.Contains(data));
        }

        [Fact]
        public void TryGetValue_TakesKeyAndOutValue_ShouldReturnTrue()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);
            myDict.TryGetValue(data.Key, out string value);

            //assert
            Assert.Equal(data.Value, value);
        }

        [Fact]
        public void TryGetValue_TakesKeyAndOutValue_ThrowsArgumentException()
        {
            //arrange
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            int wrongKey = data.Key + 1;
            
            //act
            myDict.Add(data);

            //assert
            Assert.Throws<ArgumentException>(() => myDict.TryGetValue(wrongKey, out string value));
        }

        [Fact]
        public void IndexerGet_TakesKey_ShouldReturnValue()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict.Add(data);

            //assert
            Assert.Equal(myDict[data.Key],data.Value);
        }
        
        [Fact]
        public void IndexerGet_TakesKey_ThrowsIndexOutOfRangeException()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            
            //act
            
            //assert
            Assert.Throws<IndexOutOfRangeException>(() => myDict[1]);
        }

        [Fact]
        public void IndexerSet_TakesKey_ElementDoesntExist_AddsNewKeyValuePair()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            
            //act
            myDict[data.Key] = data.Value;

            //assert
            Assert.Equal(myDict[data.Key],data.Value);
        }

        [Fact]
        public void KeysProperty_DictionaryContainsOneElement_ShouldReturnArrayWithOneKey()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            int[] resultArray = {data.Key};
            
            //act
            myDict.Add(data);

            //assert
            Assert.Equal(myDict.Keys, resultArray);
        }
        
        [Fact]
        public void ValuesProperty_DictionaryContainsOneElement_ShouldReturnArrayWithOneValue()
        {
            //arrange 
            MyDict<int, string> myDict = new MyDict<int, string>();
            KeyValuePair<int, string> data = new KeyValuePair<int, string>(1, "Orange");
            string[] resultArray = {data.Value};
            
            //act
            myDict.Add(data);

            //assert
            Assert.Equal(myDict.Values, resultArray);
        }
    }
}
