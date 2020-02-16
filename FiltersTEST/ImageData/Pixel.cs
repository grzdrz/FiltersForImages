﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersTEST.ImageData
{
    public struct Pixel
    {
        private byte r;
        public byte R
        {
            get { return r; }
            set
            {
                if (value < 0) r = 0;
                else if (value > 255) r = 255;
                else r = value;
            }
        }

        private byte g;
        public byte G
        {
            get { return g; }
            set
            {
                if (value < 0) g = 0;
                else if (value > 255) g = 255;
                else g = value;
            }
        }

        private byte b;
        public byte B
        {
            get { return b; }
            set
            {
                if (value < 0) b = 0;
                else if (value > 255) b = 255;
                else b = value;
            }
        }

        private byte alphaChannel;
        public byte A
        {
            get { return alphaChannel; }
            set 
            {
                if (value < 0) alphaChannel = 0;
                else if (value > 255) alphaChannel = 255;
                else alphaChannel = value;
            }
        }

        public Point Position;

        public Pixel(byte r, byte g, byte b, byte a, Point position)
        {
            this.alphaChannel = 0;
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.Position = position;

            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Pixel(Color color, Point position)
        {
            this.alphaChannel = 0;
            this.r = 0;
            this.g = 0;
            this.b = 0;
            this.Position = position;

            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }
    }

    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
