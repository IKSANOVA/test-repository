using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;

namespace NUnit.Tests1.Task19
{
    [TestClass]
    public class Task19Class
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestInitialize]
        public void Init()
        {
            driver = new ChromeDriver();
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
        }

        [TestMethod]
        public void Task19_Test()
        {
            HomePage homePage = PageFactory.InitElements<HomePage>(driver);
            homePage.wait = wait;
            homePage.GoHome();
            Assert.IsTrue(homePage.GetQuantity() == 0);
            homePage.BuyDucks(3);
            homePage.RemoveDucks();
        }

        [TestCleanup]
        public void Stop()
        {
            driver.Quit();
            driver.Dispose();
        }
        
    }
}
