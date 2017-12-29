using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageTransmitter;

namespace ImageTransmitterTest
{
    [TestClass]
    public class UnitTestImageTransmitter
    {
        [TestMethod]
        public void TestInitial()
        {
            System.Drawing.Bitmap bitmap1 = Properties.Resources.coffee1;
            System.Drawing.Bitmap bitmap2 = Properties.Resources.coffee2;
            bool creation = false;
            const int c_count = 50;

            using (PDImageTransmitter imageTransmitter = new PDImageTransmitter(bitmap1, bitmap2, 0, c_count))
            {
                creation = imageTransmitter.isValid;

                if (creation)
                {
                    for (int i = 0; i < c_count; i++)
                    {
                        System.Drawing.Bitmap bitmap = imageTransmitter.GetTransmiteBitmap(i);
                    }
                }
            }

            Assert.IsTrue(creation);
        }
    }
}
