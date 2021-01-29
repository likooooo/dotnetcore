using System;
using System.Collections.Generic;
using ImageProcess.ImageEntry.Bmp;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.RLC
{
    //游程编码节点数据结构
    public struct RLC_Data<T> where T:unmanaged
    {
        public T Data;
        public int Index;
        public int Length;

        public override string ToString() => $"({Index},{Data},{Length})";
    }

    //游程编码节点
    public class RLC_Node<T> where T:unmanaged
    {
        public RLC_Node<T> next;
        public RLC_Data<T> data;

        public RLC_Node(){}
    }

    //游程编码堆栈
    public class RLC_NodeList<T> where T:unmanaged
    {
        public RLC_Node<T> head;
        public RLC_Node<T> endNode;
        public int Count{get;protected set;}
        public bool IsEmpty{get=>Count==0;}
        public RLC_Node<T> this[int idx]{get=>ElementAt(idx);}
       
        public RLC_NodeList():base()
        {
            head = null;
            endNode = null;
            Count = 0;
        }

        public RLC_Node<T> ElementAt(int idx)
        {
            if(idx >= Count)
            {
                throw new Exception("Index out of range");
            }
            RLC_Node<T> p = head;
            while(idx > -1)
            {
                p = p.next;
                idx --;
            }
            return p;
        }

        public void Clear()
        {
            head = null;
            endNode = null;
        } 

        public void Append(RLC_Node<T> data)
        {
            if(Count == 0)
            {
                head = data;
                endNode = data;
                Count++;
                return;
            }
            endNode.next = data;
            endNode = data;
            Count++;
        }
        
        public void PushBack(T d,int idx)
        {
            if((idx == endNode.data.Index + endNode.data.Length)&&endNode.data.Data.Equals(d))
            {
                endNode.data.Length++;
            }
            else
            {
                RLC_Node<T> node = new RLC_Node<T>();
                node.data.Data = d;
                node.data.Length = 1;
                node.data.Index = idx;
                endNode.next = node;
                endNode = node;
                Count++;  
            }
        }
        

        public override string ToString()
        {
            String res = "";
            RLC_Node<T> node = head;
            while(res != null)
            {
                res = node.ToString() + "\r"+ res;
                node = node.next;
            }
            return res;
        }
    }
}