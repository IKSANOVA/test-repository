using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace NUnit.Tests1.Task19
{
    public class HomePage
    {
        public IWebDriver driver { get; set; }
        public WebDriverWait wait { get; set; }
        public string url = "http://localhost:8080/litecart/";

        [FindsBy(How = How.CssSelector, Using = "div#cart a span.quantity")]
        IWebElement quantityElement;

        [FindsBy(How = How.CssSelector, Using = "div#cart a.link")]
        IWebElement cartLink;

        [FindsBy(How = How.CssSelector, Using = "div.content ul li a.link")]
        IList<IWebElement> duckProduct;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoHome()
        {
            driver.Url = this.url;
            wait.Until(d => d.Title == "Online Store | My Store");
        }

        public int GetQuantity()
        {
            return int.Parse(quantityElement.Text);
        }

        public void BuyDucks(int count)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            for(int i = 0; i < count; i++)
            {
                duckProduct[rand.Next(0, duckProduct.Count - 1)].Click();
                ProductPage prodPage = PageFactory.InitElements<ProductPage>(driver);
                prodPage.BuyOne();
                GoHome();
            }
            Assert.IsTrue(GetQuantity() != 0);
        }

        public void RemoveDucks()
        {
            cartLink.Click();
            CartPage cartPage = PageFactory.InitElements<CartPage>(driver);
            cartPage.wait = wait;
            cartPage.RemoveDucks();
            GoHome();
            Assert.IsTrue(GetQuantity() == 0);
        }
    }
}
