using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhalesFargo;

namespace UnitTestProject1
{
    [TestClass]
    public class TestBioCommand
    {
        public string BioThing(string name)
        {
            string bioString = "";
            if (name.ToLower().Equals("jarvan"))
            {
                name = "Jarvan IV";
            }
            else if (name.ToLower().Equals("nunu&willump") || name.ToLower().Equals("nunu & willump"))
            {
                name = "Nunu";
            }
            try
            {
                bioString = File.ReadAllText("C:\\\\Users\\Blake\\Documents\\GitHub\\Discord-Bot\\src\\ChampBios\\" + (name + ".txt"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bioString = "Unable to find that bio.";
            }
            return bioString;
        }
        [TestMethod]
        public void AnnieBioPull()
        {
            Assert.AreNotEqual(BioThing("Annie"), "Unable to find that bio.");
        }

        [TestMethod]
        public void LowercaseAnnieBioPull()
        {
            Assert.AreNotEqual(BioThing("annie"), "Unable to find that bio.");
        }

        [TestMethod]
        public void BraumBioPull()
        {
            Assert.AreNotEqual(BioThing("Braum"), "Unable to find that bio.");
        }

        [TestMethod]
        public void XayahBioPull()
        {
            Assert.AreNotEqual(BioThing("Xayah"), "Unable to find that bio.");
        }

        [TestMethod]
        public void FalseBioPull()
        {
            Assert.AreEqual(BioThing("Test"), "Unable to find that bio.");
        }

        [TestMethod]
        public void JarvanPartialBioPull()
        {
            Assert.AreNotEqual(BioThing("Jarvan"), "Unable to find that bio.");
        }

        [TestMethod]
        public void JarvanFullBioPull()
        {
            Assert.AreNotEqual(BioThing("Jarvan IV"), "Unable to find that bio.");
        }

        [TestMethod]
        public void NunuPartialBioPull()
        {
            Assert.AreNotEqual(BioThing("Nunu"), "Unable to find that bio.");
        }

        [TestMethod]
        public void NunuFullBioPull()
        {
            string expected = BioThing("Nunu & Willump");
            Assert.AreNotEqual(expected, "Unable to find that bio.");
        }

        [TestMethod]
        public void NunuFullBioAltPull()
        {
            string expected = BioThing("Nunu & Willump");
            Assert.AreNotEqual(expected, "Unable to find that bio.");
        }
    }
}