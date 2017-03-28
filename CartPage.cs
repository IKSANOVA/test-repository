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
    public class CartPage
    {
        public IWebDriver driver { get; set; }
        public WebDriverWait wait { get; set; }

        [FindsBy(How = How.Name, Using = "remove_cart_item")]
        IList<IWebElement> removeButton;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void RemoveDucks()
        {
            while (removeButton != null && removeButton.Count > 0)
            {
                removeButton[0].Click();
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
        }
        
    }
}
