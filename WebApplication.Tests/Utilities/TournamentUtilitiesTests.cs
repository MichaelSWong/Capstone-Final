using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Web.Models;
using WebApplication.Web.Utilities;

namespace WebApplication.Tests.Utilities
{
    [TestClass]
    public class TournamentUtilitiesTests
    {
        Tournament testObj = null;
        string returnNullError = "When errored, should not return null.";
        string returnEmptyListError = "When errored, should return empty list.";
        List<List<Slot>> output = null;

        [TestInitialize]
        public void Init()
        {
            testObj = new Tournament()
            {
                PLayerCount = 4,
                Slots = new List<Slot>()
                {
                    new Slot()
                    {
                        ID = 1,
                    },
                    new Slot()
                    {
                        ID = 2,
                    },
                    new Slot()
                    {
                        ID = 3,
                    },
                    new Slot()
                    {
                        ID = 4,
                    },
                    new Slot()
                    {
                        ID = 5,
                    },
                    new Slot()
                    {
                        ID = 6,
                    },
                    new Slot()
                    {
                        ID = 7,
                    },
                },
            };

            testObj.Slots[0].NextSlot = testObj.Slots[4];
            testObj.Slots[1].NextSlot = testObj.Slots[4];
            testObj.Slots[2].NextSlot = testObj.Slots[5];
            testObj.Slots[3].NextSlot = testObj.Slots[5];

            testObj.Slots[4].NextSlot = testObj.Slots[6];
            testObj.Slots[5].NextSlot = testObj.Slots[6];
        }
        
        [TestMethod]
        public void ConstructTreeLevelsTest()
        {
            output = TournamentUtilities.ConstructTree(testObj);

            Assert.IsNotNull(output);

            Assert.AreEqual(3, output.Count);
            Assert.AreEqual(4, output[0].Count);
            Assert.AreEqual(2, output[1].Count);
            Assert.AreEqual(1, output[2].Count);

            Assert.AreEqual(1, output[0][0].ID);
            Assert.AreEqual(2, output[0][1].ID);
            Assert.AreEqual(3, output[0][2].ID);
            Assert.AreEqual(4, output[0][3].ID);

            Assert.AreEqual(5, output[1][0].ID);
            Assert.AreEqual(6, output[1][1].ID);

            Assert.AreEqual(7, output[2][0].ID);
        }
        [TestMethod]
        public void ConstructTreeTestInvalidInputHighLevel()
        {
            // Test null input
            output = TournamentUtilities.ConstructTree(null);
            Assert.IsNotNull(output, returnNullError);
            Assert.AreEqual(0, output.Count, returnEmptyListError);
        }
        [TestMethod]
        public void ConstructTreeTestInvalidInputMidLevel()
        {
            // Test null values within input
            output = TournamentUtilities.ConstructTree(new Tournament());
            Assert.IsNotNull(output, returnNullError);
            Assert.AreEqual(0, output.Count, returnEmptyListError);

            // Test empty slots list
            Tournament localTestObj = new Tournament()
            {
                Slots = new List<Slot>(),
            };
            output = TournamentUtilities.ConstructTree(localTestObj);
            Assert.IsNotNull(output, returnNullError);
            Assert.AreEqual(0, output.Count, returnEmptyListError);

            // Test invalid player count
            localTestObj.Slots.Add(new Slot());
            output = TournamentUtilities.ConstructTree(localTestObj);
            Assert.IsNotNull(output, returnNullError);
            Assert.AreEqual(0, output.Count, returnEmptyListError);
        }
    }
}
