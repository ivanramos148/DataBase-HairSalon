using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
      [HttpGet("/clients")]
      public ActionResult Index()
      {
        List<Client> allClients = Client.GetAll();
        return View(allClients);
      }


      [HttpGet("/stylists/{id}/clients/new")]
      public ActionResult CreateForm(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        string thisName = thisStylist.GetStylistName();
        int thisId = thisStylist.GetId();
        return View(thisStylist);
      }

      [HttpPost("/stylists/{id}/clients/new")]
      public ActionResult Create(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        string thisName = thisStylist.GetStylistName();
        int thisId = thisStylist.GetId();


        string newClientName = Request.Form["new-client-name"];
        int newStylistId = thisStylist.GetId();
        Client newClient = new Client(newClientName, newStylistId);
        newClient.Save();

        return View("~/Views/Stylists/Profile.cshtml", thisStylist);
      }

      [HttpPost("/clients/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Client thisClient = Client.Find(id);
        Stylist thisStylist = thisClient.FindStylist(thisClient.GetStylistId());
        thisClient.DeleteClient();

        return View(thisStylist);
      }

    }
}
