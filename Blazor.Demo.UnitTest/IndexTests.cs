using Blazor.Demo.Pages;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Blazor.Demo.UnitTest
{
    public class IndexTests
    {
        private Bunit.TestContext ctx;
        private Mock<IAuthetificationService> authetificationService;

        [SetUp]
        public void Setup()
        {
            ctx = new Bunit.TestContext();
            authetificationService = new Mock<IAuthetificationService>();
        }

        [TearDown]
        public void Teardown()
        {
            ctx.Dispose();
        }

        [Test]
        public void Index_Hellworld_Exist()
        {
            //Arr
            ctx.Services.AddScoped(x => authetificationService.Object);

            var component = ctx.RenderComponent<Index>();
            //Assert
            Assert.IsTrue(component.Markup.Contains("<h1>Hello, world!</h1>"));
        }


        [Test]
        public void Index_SubmitBtn_Exist()
        {
            //Arr
            ctx.Services.AddScoped(x => authetificationService.Object);

            var component = ctx.RenderComponent<Index>();

            //Act
            var submitBtn = component.FindAll("button").FirstOrDefault(b => b.OuterHtml.Contains("Submit"));

            //Assert
            Assert.IsNotNull(submitBtn);

        }



        [Test]
        public void AuthetificationService_IsValidLogin_CalledOnce()
        {
            //Arrange
            ctx.Services.AddScoped(x => authetificationService.Object);

            var component = ctx.RenderComponent<Index>();

            var submitBtn = component.FindAll("button").FirstOrDefault(b => b.OuterHtml.Contains("Submit"));

            //Act

            submitBtn.Click();

            //Assert

            authetificationService.Verify(l => l.IsValidLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AuthetificationService_InValidLogin_Retuns_MessageError()
        {
            //Arr
            ctx.Services.AddScoped(x => authetificationService.Object);

            var component = ctx.RenderComponent<Index>();

            var buttons = component.FindAll("button");
            var submit = buttons.FirstOrDefault(b => b.OuterHtml.Contains("Submit"));
                      

            authetificationService.Setup(l => l.IsValidLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            
            //Acc
            
            submit.Click();
            authetificationService.Verify(l => l.IsValidLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            var alert = component.Find("div.alert");

            //Assert
            Assert.AreEqual("Email/Password Invalid", alert.InnerHtml);
        }


        [Test]
        public void AuthetificationService_ValidLogin_MessageError_IsEmpty()
        {
            //Arr
            ctx.Services.AddScoped(x => authetificationService.Object);

            var component = ctx.RenderComponent<Index>();

            var buttons = component.FindAll("button");
            var submit = buttons.FirstOrDefault(b => b.OuterHtml.Contains("Submit"));


            authetificationService.Setup(l => l.IsValidLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //Acc

            submit.Click();
            authetificationService.Verify(l => l.IsValidLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            var alert = component.Find("div.alert");

            //Assert
            Assert.AreEqual(string.Empty, alert.InnerHtml);
        }
    }
}