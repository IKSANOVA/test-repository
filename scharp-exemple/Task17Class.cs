using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;

namespace SoftwareTesting
{
    [TestClass]
    public class Task17Class
    {
        private EventFiringWebDriver driver;
        private WebDriverWait wait;
        string catalogUrl = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";

        [TestInitialize]
        public void Init()
        {
            driver = new EventFiringWebDriver(new ChromeDriver());
            driver.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
            driver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
            driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void Task17Test()
        {
            driver.Url = "http://localhost/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            IWebElement element2 = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("password123");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "My Store");

            driver.Url = catalogUrl;
            wait.Until(d => d.Title == "Catalog | My Store");

            var count = driver.FindElements(By.CssSelector("tr.row [href*=edit]:not([title=Edit])")).Count;

            for (int i = 0; i < count; i++)
            {
                driver.FindElements(By.CssSelector("tr.row [href*=edit]:not([title=Edit])"))[i].Click();
                wait.Until(d => d.Title.Contains("Edit Product"));
                foreach (var entry in driver.Manage().Logs.GetLog(LogType.Browser))
                    Console.WriteLine("{0}:{1} - {2}", entry.Timestamp, entry.Level, entry.Message);
                driver.Url = catalogUrl;
                wait.Until(d => d.Title == "Catalog | My Store");
            }
        }

        private bool IsHeaderPresent(IWebDriver driver)
        {
            var element = driver.FindElement(By.TagName("h1"));
            return element != null;
        }

        [TestCleanup]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}