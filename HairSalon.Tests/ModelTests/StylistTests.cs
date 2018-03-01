using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ivan_ramos_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      string stylistName = "Ivan";
      Stylist newStylist = new Stylist(stylistName);

      //Act
      newStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{newStylist};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfStylistNamesAreTheSame_Stylist()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Ivan");
      Stylist secondStylist = new Stylist("Ivan");

      //Act
      firstStylist.Save();
      secondStylist.Save();

      //Assert
      Assert.AreEqual(true, firstStylist.GetStylistName().Equals(secondStylist.GetStylistName()));
    }

    [TestMethod]
    public void GetAll_ReturnsStylists_StylistList()
    {
      //Arrange
      string nameOne = "Ivan";
      string nameTwo = "Name";
      Stylist stylist1 = new Stylist(nameOne);
      Stylist stylist2 = new Stylist(nameTwo);
      List<Stylist> newList = new List<Stylist> {stylist1, stylist2};

      //Act
      stylist1.Save();
      stylist2.Save();
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Ivan";
      Stylist newStylist = new Stylist(name);
      newStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = newStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(newStylist.GetId());

      //Assert
      Assert.AreEqual(true, newStylist.Equals(foundStylist));
    }

    [TestMethod]
    public void GetClients_ReturnsListofClientsByStylist_ClientList()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();
      Client newClient = new Client("Dalia", newStylist.GetId());
      newClient.Save();
      Client newClient2 = new Client("Person", newStylist.GetId());
      newClient2.Save();
      List<Client> testClients = new List<Client> {newClient, newClient2};

      //Act
      List<Client> resultClients = newStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testClients, resultClients);
    }

    [TestMethod]
    public void ToDictionary_DictioarySaves_Dictionary()
    {
      //Arrange
      Stylist Ivan = new Stylist("Ivan");
      Ivan.Save();

      //Act
      Ivan.ToDictionary(Ivan);
      string IvanName = Ivan.GetStylers(Ivan.GetId());

      //Assert
      Assert.AreEqual("Ivan", IvanName);
    }

    [TestMethod]
    public void DeleteStylist_RemovesOneStyistFromDatabase_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();
      Stylist newStylist2 = new Stylist("Name");
      newStylist2.Save();
      List<Stylist> testList = new List<Stylist>{newStylist2};

      //Act
      newStylist.DeleteStylist();
      List<Stylist> resultList = Stylist.GetAll();

      //Assert
      Assert.AreEqual(testList.Count, resultList.Count);
    }

    [TestMethod]
    public void DeleteAllClients_RemovesAllClientsFromStylist_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();
      Client newClient = new Client("Dalia", newStylist.GetId());
      newClient.Save();
      Client newClient2 = new Client("Person", newStylist.GetId());
      newClient2.Save();
      List<Client> testClients = new List<Client> {};

      //Act
      newStylist.DeleteAllClients();
      List<Client> resultClients = newStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testClients, resultClients);

    }
  }
}
