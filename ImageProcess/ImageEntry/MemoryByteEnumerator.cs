// using System;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;

// namespace ImageProcess.Core
// {
//     public class MemoryByteList:IEnumerable<byte>
//     {
//         UnmanagedMemory _list;
//         public MemoryByteList(UnmanagedMemory mem)
//         {
//             _list = mem;
//         }

//         public IEnumerator<byte> GetEnumerator()
//         {
//             return new MemoryByteEnumerator();
//         }

//         private IEnumerator GetEnumerator1()
//         {
//             return this.GetEnumerator();
//         }

//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             return GetEnumerator1();
//         }
//     }
    
//     public class MemoryByteEnumerator:IEnumerable<byte>
//     {
//         private UnmanagedMemory _collection;
//         private int curIndex;
//         private byte curVal;

//         public MemoryByteEnumerator(UnmanagedMemory collection)
//         {
//             _collection = collection;
//             curIndex = -1;
//             curBox = default(byte);
//         }

//         public bool MoveNext()
//         {
//             //Avoids going beyond the end of the collection.
//             if (++curIndex >= _collection.Count)
//             {
//                 return false;
//             }
//             else
//             {
//                 // Set current box to next item in collection.
//                 curBox = _collection[curIndex];
//             }
//             return true;
//         }

//         public void Reset() { curIndex = -1; }

//         void IDisposable.Dispose() { _collection.Dispose();}

//         public byte Current
//         {
//             get { return curBox; }
//         }

//         object IEnumerator.Current
//         {
//             get { return Current; }
//         }
//     }
// }