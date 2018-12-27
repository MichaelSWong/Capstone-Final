using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
//HACK: Why isn't it picking up the SQLClient line???
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Tests.DAL
{
    [TestClass]
    public class TournamentDALTests
    {
        const string ConnectionString = "";
        private TransactionScope tran;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            //HACK: I have the correct items here but for some reason it isn't registering the sqlclient which i have above.
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into tournament values ('testname',1,'2018-12-12 12:12:000','2018-12-12 11:11:000'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                //tournamentID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }
        /*
        [TestMethod()]
        public void BrowseTournamentTest()
        {
            //Arrange
            TournamentDAL dal = new TournamentDAL(ConnectionString);
            //Act
            List<Tournament> result = dal.BrowseTournaments();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Active);
            Assert.AreEqual("testname", result.TournamentName);
            Assert.AreEqual("2018-12-12 12:12:000", result.TournamentStartDate);
            Assert.AreEqual("2018-12-12 11:11:000", result.TournamentCreateDate);
        }

        [TestMethod()]
        public void SearchTournamentnTest()
        {
            //Arrange
            TournamentDAL dal = new TournamentDAL(ConnectionString);
            const string search = "name";
            //Act
            List<Tournament> result = dal.SearchTournaments(search);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Active);
            Assert.AreEqual("testname", result.TournamentName);
            Assert.AreEqual("2018-12-12 12:12:000", result.TournamentStartDate);
            Assert.AreEqual("2018-12-12 11:11:000", result.TournamentCreateDate);
        }
        //TODO: Figure out how to put in another test object to run against these tests.
        //[TestMethod]
        //public void BrowseOldTournamentsTests()
        //{
        //    TournamentDAL dal = new TournamentDAL(ConnectionString);
        //    const string search = "name";

        //    List<Tournament> reslut = dal.BrowseOldTournaments();

        }
    */
    }
}

