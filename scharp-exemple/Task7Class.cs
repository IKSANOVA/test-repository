using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace NUnit.Tests1
{
    [TestFixture]
    public class Task7Class
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
        public void Task7Test()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            IWebElement element2 = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "My Store");
            for(int i = 0; i < driver.FindElements(By.CssSelector("li#app-")).Count; i++)
            {
                driver.FindElements(By.CssSelector("li#app-"))[i].Click();
                Assert.True(IsHeaderPresent(driver));
                for(int j = 0; j < driver.FindElements(By.CssSelector("[id^=doc-]")).Count; j++)
                {
                    driver.FindElements(By.CssSelector("[id^=doc-]"))[j].Click();
                    Assert.True(IsHeaderPresent(driver));
                }
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
