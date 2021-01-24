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
        public RLC_Node<T> prevent;
        public RLC_Data<T> data;

        public RLC_Node(){}
    }

    //游程编码堆栈
    public class RLC_NodeList<T> where T:unmanaged
    {
        public RLC_Node<T> lastNode;
        public int Count{get;protected set;}
        public RLC_NodeList():base()
        {
            lastNode = null;
            Count = 0;
        }

        public RLC_Node<T> PushBack(RLC_Data<T> d)
        {
            RLC_Node<T> node = new RLC_Node<T>();
            node.prevent = lastNode;
            node.data = d;
            lastNode = node;
            Count++;
            return node;
        }
        public void PushBack(T d,int idx)
        {
            if((idx == lastNode.data.Index + lastNode.data.Length)&&lastNode.data.Data.Equals(d)&&lastNode != null)
            {
                lastNode.data.Length++;
            }
            else
            {
                RLC_Node<T> node = new RLC_Node<T>();
                node.prevent = lastNode;
                node.data.Data = d;
                node.data.Length = 1;
                node.data.Index = idx;
                lastNode = node;
                Count++;  
            }
        }
        
        public void PushBack(ref RLC_Node<T> node)
        {
            node.prevent = lastNode;
            lastNode = node;
            Count++;
        }

        public RLC_Node<T> PopBack()
        {
            RLC_Node<T> res;
            if(Count > -1)
            {
                res = lastNode;
                lastNode = res.prevent;
            }
            {
                res = null;
            }
            return res;
        }

        public RLC_Node<T> ElementAt(int idx)
        {
            RLC_Node<T> res = lastNode;
            int LoopIdx = Count -1;
            while(idx < LoopIdx)
            {
                res = lastNode.prevent;
                LoopIdx--;
            }
            return res;
        }
        
        public override string ToString()
        {
            String res = "";
            RLC_Node<T> node = lastNode;
            while(res != null)
            {
                res = node.ToString() + "\r"+ res;
                node = node.prevent;
            }
            return res;
        }
    }
}