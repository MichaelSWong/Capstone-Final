using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using System.Data.SqlClient;
using System.IO;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class SlotDALTests
    {
        const string CONN_STRING = @"Data Source =.\SQLEXPRESS;Initial Catalog = DemoDB_TEST; Integrated Security = True";
        private TransactionScope trans = null;
        private ISlotDAL slotDAL = null;
        private IUserDAL userDAL = null;

        [TestInitialize]
        public void Init()
        {
            string sqlScript = File.ReadAllText(@"../../../../schema_TEST.sql");
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlScript, conn);
                cmd.ExecuteNonQuery();
            }

            trans = new TransactionScope();
            slotDAL = new SlotDAL(CONN_STRING);
            userDAL = new UserSqlDAL(CONN_STRING);
        }

        [TestCleanup]
        public void CleanUp()
        {
            trans.Dispose();
        }

        [TestMethod]
        public void CreateSlot()
        {
            Slot newSlot = new Slot()
            {
                TournamentID = 1,
                Player = new User()
                {
                    Id = 5,
                },
                NextSlot = new Slot()
                {
                    ID = 5,
                },
            };
            int rowsAffected = slotDAL.CreateSlot(newSlot);
            Assert.AreNotEqual(0, rowsAffected);
            Assert.IsNotNull(slotDAL.GetSlot(8).Player);
            Assert.IsNotNull(slotDAL.GetSlot(8).NextSlot);
            Assert.AreEqual(5, slotDAL.GetSlot(8).Player.Id);
            Assert.AreEqual(5, slotDAL.GetSlot(8).NextSlot.ID);
        }
        [TestMethod]
        public void CreateSlotInvalidInput()
        {
            int rowsAffected = slotDAL.CreateSlot(null);
            Assert.AreEqual(0, rowsAffected);

            Slot newSlot = new Slot();
            rowsAffected = slotDAL.CreateSlot(newSlot);
            Assert.AreEqual(0, rowsAffected);
        }

        [TestMethod]
        public void GetSlot()
        {
            Slot output = slotDAL.GetSlot(1);
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output.ID);

            output = slotDAL.GetSlot(3);
            Assert.IsNotNull(output);
            Assert.AreEqual(3, output.ID);

            output = slotDAL.GetSlot(7);
            Assert.IsNotNull(output);
            Assert.AreEqual(7, output.ID);
        }
        [TestMethod]
        public void GetSlotInvalidInput()
        {
            Slot output = slotDAL.GetSlot(-1);
            Assert.IsNotNull(output, "Returning slot should not be null, even if there was an error.");
            Assert.AreEqual(-1, output.ID, "Returning slot should have an ID of -1 to show an error.");

            output = slotDAL.GetSlot(10000);
            Assert.IsNotNull(output, "Returning slot should not be null, even if there was an error.");
            Assert.AreEqual(-1, output.ID, "Returning slot should have an ID of -1 to show an error.");
        }

        [TestMethod]
        public void GetSlots()
        {
            List<Slot> output = slotDAL.GetSlots();
            Assert.IsNotNull(output);
            Assert.AreEqual(7, output.Count);
        }
        [TestMethod]
        public void GetSlotsTourneyID()
        {
            List<Slot> output = slotDAL.GetSlots(1);
            Assert.IsNotNull(output);
            Assert.AreEqual(7, output.Count);
        }
        [TestMethod]
        public void GetSlotsInvalidInput()
        {
            List<Slot> output = slotDAL.GetSlots(-1);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Count, "Should return an empty list if errored.");

            output = slotDAL.GetSlots(10000);
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Count, "Should return an empty list if errored.");
        }
    }
}