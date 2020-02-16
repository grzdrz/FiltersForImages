using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using FiltersTEST;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OnePixelImageGrayShadesFilterTest()
        {
            Pixel[,] pixels = new Pixel[1, 1]
            {
                { new Pixel(Color.FromArgb(11, 100, 100, 100)) }
            };

            Filter filter = new GrayShadesFilter();
            Bitmap bm = filter.ApplyFilter(pixels);

            Assert.IsNotNull(bm, "Пустое изображение");
            Assert.AreEqual(bm.Height * bm.Width, pixels.Length, "Не совпадают размеры изображений");
            Assert.AreEqual(bm.GetPixel(0, 0).A, 11, "Не совпадают альфа каналы пикселя изображений");
        }

        [TestMethod]
        public void TwoPixelImageGrayShadesFilterTest()
        {
            Pixel[,] pixels = new Pixel[2, 1]
            {
                { new Pixel(Color.FromArgb(11, 100, 100, 100)) },
                { new Pixel(Color.FromArgb(21, 100, 100, 100)) }
            };

            Filter filter = new GrayShadesFilter();
            Bitmap bm = filter.ApplyFilter(pixels);

            ICollection expectedAlphaChannelOfPixelsCollection = new byte[,]
            {
                { bm.GetPixel(0, 0).A },
                { bm.GetPixel(1, 0).A }
            };
            ICollection actualAlphaChannelOfPixelsCollection = new byte[,]
            {
                { 11 },
                { 21 }
            };


            Assert.IsNotNull(bm, "Пустое изображение");
            Assert.AreEqual(bm.Height * bm.Width, pixels.Length, "Не совпадают размеры изображений");
            CollectionAssert.AreEqual(
                expectedAlphaChannelOfPixelsCollection,
                actualAlphaChannelOfPixelsCollection, 
                "Не совпадают альфа каналы пикселя изображений");
        }

        [TestMethod]
        public void FourPixelImageGrayShadesFilterTest()
        {
            Pixel[,] pixels = new Pixel[2, 2]
            {
                { new Pixel(Color.FromArgb(11, 100, 100, 100)), new Pixel(Color.FromArgb(12, 100, 100, 100)) },
                { new Pixel(Color.FromArgb(21, 100, 100, 100)), new Pixel(Color.FromArgb(22, 100, 100, 100)) }
            };

            Filter filter = new GrayShadesFilter();
            Bitmap bm = filter.ApplyFilter(pixels);

            ICollection expectedAlphaChannelOfPixelsCollection = new byte[,]
            {
                { bm.GetPixel(0, 0).A, bm.GetPixel(0, 1).A },
                { bm.GetPixel(1, 0).A, bm.GetPixel(1, 1).A }
            };
            ICollection actualAlphaChannelOfPixelsCollection = new byte[,]
            {
                { 11, 12 },
                { 21, 22 }
            };


            Assert.IsNotNull(bm, "Пустое изображение");
            Assert.AreEqual(bm.Height * bm.Width, pixels.Length, "Не совпадают размеры изображений");
            CollectionAssert.AreEqual(
                expectedAlphaChannelOfPixelsCollection,
                actualAlphaChannelOfPixelsCollection,
                "Не совпадают альфа каналы пикселя изображений");
        }
    }
}
