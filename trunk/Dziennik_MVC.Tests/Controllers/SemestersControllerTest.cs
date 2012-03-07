using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Dziennik_MVC.Models.Data.Abstract;
using Dziennik_MVC.Areas.Admin.Controllers;
using Moq;
using Dziennik_MVC.Models.Entities;
using PagedList;


namespace Dziennik_MVC.Tests.Controllers
{
    [TestFixture]
    public class SemestersControllerTest
    {
        [Test]
        public void Can_View_All_Semesters()
        {
            //Arrange
            var mock = new Mock<ISemestersRepository>();
            mock.Setup(m => m.GetAllSemesters).Returns(
                new Semesters[] {
                    new Semesters {SemesterID=1, Type="letni", Year = "2006/2007"},
                    new Semesters {SemesterID=2, Type="zimowy", Year = "2006/2007"},
                    new Semesters {SemesterID=3, Type="letni", Year = "2007/2008"},
                    new Semesters {SemesterID=4, Type="zimowy", Year = "2007/2008"},
                    new Semesters {SemesterID=5, Type="letni", Year = "2008/2009"}
                }.AsQueryable());

            SemestersController target = new SemestersController(mock.Object);
            

            // Action
            Semesters[] result = ((PagedList.PagedList<Semesters>)target.List(null,null).Model).ToArray<Semesters>(); // Tylko pageSize = 10

            // Assert
            Assert.AreEqual(5,result.Count());
            Assert.IsTrue(result[0].SemesterID == 1 && result[0].Type == "letni" && result[0].Year == "2006/2007");
            Assert.IsTrue(result[1].SemesterID == 2 && result[1].Type == "zimowy" && result[1].Year == "2006/2007");
            Assert.IsTrue(result[2].SemesterID == 3 && result[2].Type == "letni" && result[2].Year == "2007/2008");
            Assert.IsTrue(result[3].SemesterID == 4 && result[3].Type == "zimowy" && result[3].Year == "2007/2008");
            Assert.IsTrue(result[4].SemesterID == 5 && result[4].Type == "letni" && result[4].Year == "2008/2009");
            
        }

        [Test]
        public void Can_Sort_And_Paginate()
        {
            //Arrange
            var mock = new Mock<ISemestersRepository>();
            mock.Setup(m => m.GetAllSemesters).Returns(
                new Semesters[] {
                    new Semesters {SemesterID=1, Type="letni", Year = "2006/2007"},
                    new Semesters {SemesterID=2, Type="zimowy", Year = "2006/2007"},
                    new Semesters {SemesterID=3, Type="letni", Year = "2007/2008"},
                    new Semesters {SemesterID=4, Type="zimowy", Year = "2007/2008"},
                    new Semesters {SemesterID=5, Type="letni", Year = "2008/2009"}
                }.AsQueryable());

            SemestersController target = new SemestersController(mock.Object);


            // Action
            Semesters[] result = ((PagedList.PagedList<Semesters>)target.List("Type asc", 1).Model).ToArray<Semesters>(); // Tylko pageSize = 10

            // Assert
            Assert.AreEqual(5, result.Count());
            Assert.IsTrue(result[0].SemesterID == 1 && result[0].Type == "letni" && result[0].Year == "2006/2007");
            Assert.IsTrue(result[1].SemesterID == 3 && result[1].Type == "letni" && result[1].Year == "2007/2008");
            Assert.IsTrue(result[2].SemesterID == 5 && result[2].Type == "letni" && result[2].Year == "2008/2009");
            Assert.IsTrue(result[3].SemesterID == 2 && result[3].Type == "zimowy" && result[3].Year == "2006/2007");
            Assert.IsTrue(result[4].SemesterID == 4 && result[4].Type == "zimowy" && result[4].Year == "2007/2008");
        }

        [Test]
        public void Can_Add_Semesters()
        {
            // Arrange  
            var mock = new Mock<ISemestersRepository>();            

            SemestersController target = new SemestersController(mock.Object);
            Semesters semester = new Semesters{ SemesterID=1, Type="letni", Year="2007/2008"};
            // Action
            
            ActionResult result = target.Add(semester);

            // Assert
            mock.Verify(m => m.AddSemester(semester),Times.Once());
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
            Assert.AreEqual("Pomyślnie dodano nowy semestr!", target.TempData["message"]);
        }

        [Test]
        public void Can_Edit_Semester()
        {

            // Arrange 
            Mock<ISemestersRepository> mock = new Mock<ISemestersRepository>();

            // Arrange 
            SemestersController target = new SemestersController(mock.Object);

            // Arrange 
            Semesters semester = new Semesters { SemesterID = 1, Type = "letni", Year = "2007/2008" };

            // Act 
            ActionResult result = target.Edit(semester);

            // Assert
            mock.Verify(m => m.EditSemester(semester));

            // Assert 
            Assert.AreEqual("Zauktalizowano semestr!", target.TempData["message"]);
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        }

        [Test]
        public void Can_Remove_Semester()
        {
            // Arrange 
            Mock<ISemestersRepository> mock = new Mock<ISemestersRepository>();
            Semesters semester = new Semesters { SemesterID = 2, Type = "letni", Year = "2007/2008"};
            mock.Setup(m => m.GetSemesterByID(2)).Returns(semester);
            mock.Setup(m => m.DeleteSemester(semester));
            // Arrange
            SemestersController target = new SemestersController(mock.Object);
            
            // Act
            var result = target.DeleteConfirmed(2);
            
            // Assert - check the method result type
            mock.Verify(m => m.DeleteSemester(semester));
            Assert.AreEqual("Usunięto semestr!", target.TempData["message"]);           
            Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
        } 
    }
}
