using System;
using System.Collections.Generic;
using ImageProcess.ImageEntry.Bmp;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.RLC
{
    //游程编码节点数据结构
    public struct RLC_Data
    {
        public IntPtr Start;
        public int Data;
        public int Length;

        public override string ToString() => $"({Data},{Length})";
        public byte[] CopyTo()
        {
            byte[] res = new byte[Length * 4];
            Marshal.Copy(Start,res,0,res.Length);
            return res;
        }
    }

    //游程编码节点
    public class RLC_Node
    {
        public RLC_Node prevent;
        public RLC_Data data;

        public RLC_Node(){}
    }

    //游程编码堆栈
    public class RLC_NodeList
    {
        public RLC_Node lastNode;
        public int Count{get;protected set;}
        protected RLC_NodeList():base()
        {
            lastNode = null;
            Count = 0;
        }

        public RLC_Node PushBack(int data,int length) => PushBack
        (
            new RLC_Data
            {
                Data  = data,
                Length = length
            }
        );
        
        public RLC_Node PushBack(RLC_Data d)
        {
            RLC_Node node = new RLC_Node();
            node.prevent = lastNode;
            node.data = d;
            lastNode = node;
            Count++;
            return node;
        }
        
        public void PushBack(ref RLC_Node node)
        {
            node.prevent = lastNode;
            lastNode = node;
            Count++;
        }

        public RLC_Node PopBack()
        {
            RLC_Node res;
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

        public RLC_Node ElementAt(int idx)
        {
            RLC_Node res = lastNode;
            int LoopIdx = Count -1;
            while(idx < LoopIdx)
            {
                res = lastNode.prevent;
                LoopIdx--;
            }
            return res;
        }

        public static RLC_NodeList CreateNodeList()
        {
            RLC_NodeList res = new RLC_NodeList();
            return res;
        }
        
        public override string ToString()
        {
            String res = "";
            RLC_Node node = lastNode;
            while(res != null)
            {
                res = node.ToString() + "\r"+ res;
                node = node.prevent;
            }
            return res;
        }
    }
}