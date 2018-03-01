using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
      [HttpGet("/stylists/new")]
      public ActionResult CreateForm()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpPost("/stylists/new")]
      public ActionResult Create()
      {
        string newName = Request.Form["new-name"];
        Stylist newStylist = new Stylist(newName);
        newStylist.Save();
        newStylist.ToDictionary(newStylist);

        List<Stylist> allStylists = Stylist.GetAll();
        return RedirectToAction("Index", "Home");
      }


      [HttpGet("/stylists/{id}/profile")]
      public ActionResult Profile(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        return View(thisStylist);
      }


      [HttpPost("/stylists/{id}/delete/stylist")]
      public ActionResult Delete(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        string deletedStylist = thisStylist.GetStylistName();
        thisStylist.DeleteAllClients();
        thisStylist.DeleteStylist();
        return View(deletedStylist);
      }

    }
}
