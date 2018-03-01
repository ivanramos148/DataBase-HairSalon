using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistsControllerTest
  {
    [TestMethod]
    public void CreateForm_ReturnsTheHTMLIfItsTrue_True()
    {
      //Arrange
      StylistsController controller = new StylistsController();

      //Act
      IActionResult createFormView = controller.CreateForm();
      ViewResult result = createFormView as ViewResult;

      //Assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
      
    }
  }
}
