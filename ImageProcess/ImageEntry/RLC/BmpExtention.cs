using System;
using System.Collections.Generic;
using ImageProcess.ImageEntry.Bmp;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.RLC
{
    public static class BmpExtention
    {
        public static unsafe BmpGray8 RLCListToImage(RLC_NodeList<byte> rlc,int width,int height)
        {
            BmpGray8 gray = new BmpGray8(width,height);
            gray.InitMemory();
            IOperatorSet<byte>.Fill(gray,0);
            RLC_Node<byte> node = rlc.head;
            int loopCount;
            byte* p = (byte*)gray.Scan0.ToPointer();
            while(node != null)
            {
                loopCount = node.data.Length;
                while(--loopCount > -1)
                {
                    *(p + node.data.Index + loopCount) = 0xff;
                }
                node = node.next;
            }
            return gray;
        }
    }
}