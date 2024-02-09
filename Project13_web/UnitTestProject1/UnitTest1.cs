using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project13_web.Controllers;
using Project13_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Index_ReturnsViewResult_WithSearchString()
        {
            // Arrange
            var controller = new HomeController();
            string searchString = "Harry Potter"; // เพิ่มค่าสตริงสำหรับการค้นหา

            // Act
            var result = controller.Index(searchString);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_ReturnsViewResult_WithoutSearchString()
        {
            // Arrange
            var controller = new HomeController();
            string searchString = null; // ไม่ระบุค่าสตริงสำหรับการค้นหา

            // Act
            var result = controller.Index(searchString);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cartoon_ReturnsViewResult_WithSearchString()
        {
            // Arrange
            var controller = new HomeController();
            string searchString = "Naruto"; // ค้นหาหนังสือ "Naruto" ในหมวดหมู่การ์ตูน

            // Act
            var result = controller.Cartoon(searchString);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.Model);

            var model = (IEnumerable<Book>)viewResult.Model;
            Assert.IsTrue(model.All(b => b.Category == "การ์ตูน"));
            Assert.IsTrue(model.All(b => b.Book_Name.Contains(searchString)));
        }

    }
}
