using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SoftwareTesting
{
    [TestClass]
    public class Task11
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestInitialize]
        public void Init()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        [TestMethod]
        public void Test11()
        {
            var startUrl = "http://localhost/litecart/";
            driver.Url = startUrl;
            wait.Until(d => d.Title == "Online Store | My Store");
            driver.FindElement(By.LinkText("New customers click here")).Click();
            wait.Until(d => d.Title == "Create Account | My Store");

            var rString = RandomString();
            var name = "Name" + rString;
            var lastname = "Lastname" + rString;
            var email = "Email" + rString + "@vse.vsad";
            var pass = "testpass";

            driver.FindElement(By.Name("tax_id")).SendKeys("testtaxid");
            driver.FindElement(By.Name("company")).SendKeys("testcompany");
            driver.FindElement(By.Name("firstname")).SendKeys(name);
            driver.FindElement(By.Name("lastname")).SendKeys(lastname);
            driver.FindElement(By.Name("address1")).SendKeys("Neverstreet, 777 building");
            driver.FindElement(By.Name("postcode")).SendKeys("123456");
            driver.FindElement(By.Name("city")).SendKeys("Neverland");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+7 111 22 33");
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(pass);
            driver.FindElement(By.Name("create_account")).Click();

            wait.Until(d => d.Title == "Online Store | My Store");
            driver.FindElement(By.LinkText("Logout")).Click();
            wait.Until(d => d.Title == "Online Store | My Store");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(pass);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "Online Store | My Store");
        }

        [TestCleanup]
        public void Stop()
        {
            driver.Quit();
            driver.Dispose();
        }

        private string RandomString()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < 3; i++)
                result.Append(rand.Next(0, 10).ToString());
            return result.ToString();
        }
    }
}
