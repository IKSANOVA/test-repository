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
    public class ProductPage
    {
        IWebDriver driver { get; set; }

        [FindsBy(How = How.Name, Using = "options[Size]")]
        IList<IWebElement> options;

        [FindsBy(How = How.Name, Using = "add_cart_product")]
        IWebElement buyButton;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private void SetOptions()
        {
            if(options != null && options.Count > 0)
            {
                var act = new Actions(driver);
                act.Click(options[0]).SendKeys(options[0], Keys.ArrowDown).SendKeys(options[0], Keys.Enter).Perform();
            }
        }

        public void BuyOne()
        {
            SetOptions();
            buyButton.Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
        
    }
}
