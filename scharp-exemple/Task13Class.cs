using System;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace NUnit.Tests1
{
    [TestFixture]
    public class Task13Class
    {
        IWebDriver driver = null;
        WebDriverWait wait = null;
        int implicitWaitSeconds = 20;

        [SetUp]
        public void Init()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            //driver.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromSeconds(implicitWaitSeconds));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(implicitWaitSeconds));
        }

        [Test]
        public void Task13Test()
        {
            var startUrl = "http://localhost:8080/litecart/";
            driver.Url = startUrl;
            wait.Until(d => d.Title == "Online Store | My Store");
            //driver.FindElements(By.CssSelector("nav.content ul li a"))[0].Click();

            BuyDucks(driver, wait);
            var quantity = driver.FindElement(By.CssSelector("div#cart a span.quantity")).Text;
            Assert.True(quantity != "0");

            driver.FindElement(By.CssSelector("div#cart a.link")).Click();
            
            RemoveDucks(driver, wait);
            driver.Url = startUrl;
            wait.Until(d => d.Title == "Online Store | My Store");
            Assert.True(driver.FindElement(By.CssSelector("div#cart a span.quantity")).Text == "0");
        }

        private void RemoveDucks(IWebDriver driver, WebDriverWait wait)
        {
            while (driver.FindElements(By.Name("remove_cart_item")).Count > 0)
            {
                var button = driver.FindElement(By.Name("remove_cart_item"));
                button.Click();
                wait.Until(ExpectedConditions.StalenessOf(button));
            }
        }
        private void BuyDucks(IWebDriver driver, WebDriverWait wait)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var url = driver.Url;
            for (int i = 0; i<3;i++)
            {
                var ducks = driver.FindElements(By.CssSelector("div.content ul li a.link"));
                //случайная утка
                ducks[rand.Next(0, ducks.Count - 1)].Click();
                //ждем появления кнопки добавления в корзину
                wait.Until(ExpectedConditions.ElementExists(By.Name("add_cart_product")));
                if (driver.FindElements(By.Name("options[Size]")).Count > 0)
                {
                    var opt = driver.FindElement(By.Name("options[Size]"));
                    var act = new Actions(driver);
                    act.Click(opt).SendKeys(opt, Keys.ArrowDown).SendKeys(opt, Keys.Enter).Perform();
                }
                var quantity = driver.FindElement(By.CssSelector("span.quantity"));
                var button = driver.FindElement(By.Name("add_cart_product"));
                button.Click();
                //wait.Until(ExpectedConditions.ElementExists(By.CssSelector("span.quantity")));
                //wait.Until(ExpectedConditions.StalenessOf(quantity));
                Thread.Sleep(TimeSpan.FromSeconds(3));
                driver.FindElement(By.CssSelector("nav#site-menu ul li.general-0 a")).Click();
                driver.Url = url;
            }
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
        
    }
}
