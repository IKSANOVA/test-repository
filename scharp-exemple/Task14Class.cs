using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System;

namespace NUnit.Tests1
{
    [TestFixture]
    public class Task14Class
    {
        IWebDriver driver = null;
        WebDriverWait wait = null;

        [SetUp]
        public void Init()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 20));
        }

        [Test]
        public void Task14Test()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            IWebElement element2 = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "My Store");
            driver.FindElement(By.LinkText("Countries")).Click();
            driver.FindElement(By.LinkText("Add New Country")).Click();

            driver.FindElements(By.Name("status"))[0].Click();
            driver.FindElement(By.Name("iso_code_2")).SendKeys("111111111");

            var wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[0].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            var curHandler = driver.CurrentWindowHandle;
            var otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("iso_code_3")).SendKeys("333333");

            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[1].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("name")).SendKeys("Test Country");
            driver.FindElement(By.Name("domestic_name")).SendKeys("Testing");
            driver.FindElement(By.Name("tax_id_format")).SendKeys("123");
            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[2].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("address_format")).SendKeys("Address format");
            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[3].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("postcode_format")).SendKeys("141410");
            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[4].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("currency_code")).SendKeys("1111");
            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[5].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("phone_code")).SendKeys("1111");
            wCount = driver.WindowHandles.ToArray();
            driver.FindElements(By.ClassName("fa-external-link"))[6].Click();
            wait.Until(d => d.WindowHandles.Count > wCount.Count());
            Thread.Sleep(TimeSpan.FromSeconds(10));

            curHandler = driver.CurrentWindowHandle;
            otherWindow = driver.WindowHandles.Where(h => !wCount.Contains(h)).First();
            driver.SwitchTo().Window(otherWindow);
            driver.Close();
            driver.SwitchTo().Window(curHandler);

            driver.FindElement(By.Name("zone[code]")).SendKeys("1111");
            driver.FindElement(By.Name("zone[name]")).SendKeys("Наименование");
            driver.FindElement(By.Name("save")).Click();
            Assert.Pass("Test passed!");
        }

        private bool IsHeaderPresent(IWebDriver driver)
        {
            var element = driver.FindElement(By.TagName("h1"));
            return element != null;
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
        
    }
}
