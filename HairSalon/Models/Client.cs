using System.Collections.Generic;
using System;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private int _stylist_id;
    private string _clientName;

    public Client(string clientName, int stylistId, int Id = 0)
    {
      _id = Id;
      _stylist_id = stylistId;
      _clientName = clientName;
    }


    public void SetClientName(string clientName)
    {
      _clientName = clientName;
    }

    public string GetClientName()
    {
      return _clientName;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylist_id;
    }



    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (clientName, stylist_id) VALUES (@clientName, @stylistId);";

     MySqlParameter clientName = new MySqlParameter();
     clientName.ParameterName = "@clientName";
     clientName.Value = this._clientName;
     cmd.Parameters.Add(clientName);

     MySqlParameter stylistId = new MySqlParameter();
     stylistId.ParameterName = "@stylistId";
     stylistId.Value = this._stylist_id;
     cmd.Parameters.Add(stylistId);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"TRUNCATE TABLE clients;";
     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
    }



    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool clientNameEquality = (this.GetClientName() == newClient.GetClientName());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && clientNameEquality && stylistIdEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static Client Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int clientId = 0;
     string clientName = "";
     int stylistId = 0;

     while (rdr.Read())
     {
       clientId = rdr.GetInt32(0);
       clientName = rdr.GetString(1);
       stylistId = rdr.GetInt32(2);
     }

     Client foundClient = new Client(clientName, stylistId, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundClient;
    }

    public Stylist FindStylist(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistId);

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }

      return foundStylist;
    }

    public string GetName(Client clientele)
    {
      int clientSearchId = clientele.GetStylistId();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = clientSearchId;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist foundStylist = new Stylist(stylistName, stylistId);

       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }

      string namedStylist = foundStylist.GetStylistName();
      return namedStylist;
    }
    public void DeleteClient()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = this._id;
     cmd.Parameters.Add(searchId);

     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
      }
      }
     public static List<Client> GetAll()
     {
       List<Client> allClients = new List<Client> {};
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM clients;";
       var rdr = cmd.ExecuteReader() as MySqlDataReader;
       while(rdr.Read())
       {
         int id = rdr.GetInt32(0);
         string clientName = rdr.GetString(1);
         int stylistId = rdr.GetInt32(2);
         Client newClient = new Client(clientName, stylistId, id);
         allClients.Add(newClient);
       }
       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
       return allClients;
     }
    }
  }
