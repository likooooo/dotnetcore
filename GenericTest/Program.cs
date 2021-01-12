using System;
//using System.Collections.Generic;
using System.Collections.Generic;

namespace GenericTest
{
//     class Pixel:IEquatable<Pixel>
//     {
//         public int[] data;
//         public bool Equals(Pixel p)
//             =>data.Equals(p.data);
//     }

//     class PixelCollection:ICollection<Pixel>
//     {
//         protected List<Pixel> _pixels;
        
//         public Pixel this[int i] =>_pixels[i];

//         public PixelCollection(int width,int height)
//         {
//             _pixels = new List<Pixel>(width*height);
//         }
//         public void Add(Pixel p)
//         {
//             if(!_pixels.Contains(p))
//             {
//                 _pixels.Add(p);
//             }
//             else
//             {
//                 Console.WriteLine($"{p.ToString()} already in this collection!");
//             }
//         } 
//         public void Clear(){_pixels.Clear();}
        
//         public bool Contains(Pixel p) => _pixels.Contains(p);

//         public void CopyTo(Pixel[] arry, int idx){_pixels.CopyTo(arry,idx);}
        
//         public bool Remove(Pixel p)=> _pixels.Remove(p);
        
//         public int Count
//         {
//             get => _pixels.Count;
//         }
       
//         public bool IsReadOnly
//         {
//             get => false;
//         }

//         public IEnumerator<Pixel> GetEnumerator()
//         {
//             return new PixelEnumerator(this);
//         }

//         IEnumerator IEnumerable.GetEnumerator()
//         {
//             return GetEnumerator();
//         }
//     }

//     class PixelEnumerator:IEnumerator<Pixel>
//     {
//         private PixelCollection _collecton;
//         private int currentIdx;

//         public PixelEnumerator( PixelCollection collecton)
//         {
//             _collecton = collecton;
//             currentIdx = -1;
//             Current = default(Pixel);
//         }

//         public bool MoveNext() => ++currentIdx < _collection.Count;

//         public void Reset()
//         {
//             currentIdx = -1;
//         }

//         public void Dispose(){}
        
//         public Pixel Current
//         {
//             get =>_collecton[currentIdx];
//         }

//         object IEnumerator.Current
//         {
//             get => (object)_collecton[currentIdx];
//         }
//     }

    class Program
    {
        struct Point<T>
        {
            public T x,y;
        }
        static void Main(string[] args)
        {
            Point<int> p0 = new Point<int>();
            p0.x =10;
            Console.WriteLine("Hello World!");
        }
    }
}
