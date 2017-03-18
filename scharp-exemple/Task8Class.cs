using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace NUnit.Tests1
{
    [TestFixture]
    public class Task8Class
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
        public void Task8Test()
        {
            driver.Url = "http://localhost:8080/litecart/";
            wait.Until(d => d.Title == "Online Store | My Store");
            foreach(var element in driver.FindElements(By.CssSelector("ul.listing-wrapper products li a.link")))
            {
                var wrappers = element.FindElements(By.CssSelector("div.image-wrapper")).Count;
                Assert.True(wrappers == 1);
            }
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
