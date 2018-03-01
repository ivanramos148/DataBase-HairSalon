using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ivan_ramos_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

    [TestMethod]
    public void Save_savesItAllToTheDatabase_ClientList()
    {
      //Arrange
      string clientName = "Clientname";
      Client newClient = new Client(clientName, 1);

      //Act
      newClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{newClient};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void GetAll_getsItAllFromData_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAll_returnsTheClient_ClientList()
    {
      //Arrange
      string nameOne = "Clientname";
      string nameTwo = "person";
      Client client1 = new Client(nameOne, 1);
      Client client2 = new Client(nameTwo, 1);
      List<Client> newList = new List<Client> {client1, client2};

      //Act
      client1.Save();
      client2.Save();
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_savesIdToDatabase_Id()
    {
      //Arrange
      string name = "Clientname";
      Client newClient = new Client(name, 1);
      newClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = newClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_findsTheClientInDatabase_Client()
    {
      //Arrange
      Client newClient = new Client("Clientname", 1);
      newClient.Save();

      //Act
      Client foundClient = Client.Find(newClient.GetId());

      //Assert
      Assert.AreEqual(true, newClient.Equals(foundClient));
    }

    [TestMethod]
    public void FindStylist_RetrieveTheStylistName_Stylist()
    {
      //Arrange
      Client newClient = new Client("Clientname", 1);
      newClient.Save();
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();

      //Act
      Stylist foundStylist = newClient.FindStylist(newClient.GetStylistId());

      //Assert
      Assert.AreEqual("Ivan", foundStylist.GetStylistName());
    }

    [TestMethod]
    public void Equals_databaseReturnsTrueIfNamesAreTheSame_Client()
    {
      //Arrange
      Client firstClient = new Client("Clientname", 1);
      Client secondClient = new Client("Clientname", 1);

      //Act
      firstClient.Save();
      secondClient.Save();

      //Assert
      Assert.AreEqual(true, firstClient.GetClientName().Equals(secondClient.GetClientName()));
    }

    [TestMethod]
    public void DeleteClient_removesClientFromDatabase_Client()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();
      Client newClient = new Client("Clientname", 1);
      newClient.Save();
      Client newClient2 = new Client("Dalia", 1);
      newClient2.Save();

      //Act
      newClient.DeleteClient();
      List<Client> resultList = newStylist.GetClients();

      //Assert
      Assert.AreEqual(1, resultList.Count);

    }
    [TestMethod]
    public void GetName_ReturnStylistNameForOneElementOfList_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ivan");
      newStylist.Save();
      Client newClient = new Client("Clientname", 1);
      newClient.Save();
      Client newClient2 = new Client("Dalia", 1);
      newClient2.Save();
      //Act
      string foundStylist = newClient.GetName(newClient);
      Console.WriteLine(foundStylist);
      //Assert
      Assert.AreEqual("Ivan", foundStylist);
    }
    
  }
}
