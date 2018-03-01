using System.Collections.Generic;
using System;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    private string _stylistName;

    public Dictionary<int, string> _Stylers = new Dictionary<int, string>{};

    public Stylist(string stylistName, int Id = 0)
    {
      _id = Id;
      _stylistName = stylistName;
    }

    

    public void SetStylistName(string stylistName)

    {
      _stylistName = stylistName;
    }

    public string GetStylistName()
    {
      return _stylistName;
    }

    public void SetId(int id)
    {
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allStylists;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (stylistName) VALUES (@stylistName);";

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;
      cmd.Parameters.Add(stylistName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void ToDictionary(Stylist stylist)
    {
      _Stylers.Add(stylist.GetId(), stylist.GetStylistName());
    }

    public string GetStylers(int key)
    {
      return _Stylers[key];
    }

    public static void DeleteAll()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd1 = conn.CreateCommand() as MySqlCommand;
     cmd1.CommandText = @"TRUNCATE TABLE stylists;";
     cmd1.ExecuteNonQuery();

     var cmd2 = conn.CreateCommand() as MySqlCommand;
     cmd2.CommandText = @"TRUNCATE TABLE clients;";
     cmd2.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool stylistNameEquality = (this.GetStylistName() == newStylist.GetStylistName());
        return (idEquality && stylistNameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public static Stylist Find(int id)
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

    public List<Client> GetClients()
    {
      List<Client> allStylistClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @searchStylistId;";

      MySqlParameter searchStylistId = new MySqlParameter();
      searchStylistId.ParameterName = "@searchStylistId";
      searchStylistId.Value = this._id;
      cmd.Parameters.Add(searchStylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, stylistId, clientId);
        allStylistClients.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allStylistClients;
    }

    public void DeleteStylist()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void DeleteAllClients()
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @searchId;";

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

    // public void DeleteClient()
    // {
    //  MySqlConnection conn = DB.Connection();
    //  conn.Open();
    //
    //  var cmd = conn.CreateCommand() as MySqlCommand;
    //  cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @searchId;";
    //
    //  MySqlParameter searchId = new MySqlParameter();
    //  searchId.ParameterName = "@searchId";
    //  searchId.Value = this._id;
    //  cmd.Parameters.Add(searchId);
    //
    //  cmd.ExecuteNonQuery();
    //
    //  conn.Close();
    //  if (conn != null)
    //  {
    //    conn.Dispose();
    //  }
    // }
  }
}
