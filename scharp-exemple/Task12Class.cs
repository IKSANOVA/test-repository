using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnit.Tests1
{
    [TestFixture]
    public class Task12Class
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
        public void Task12Test()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            IWebElement element2 = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "My Store");
            driver.FindElement(By.LinkText("Catalog")).Click();
            driver.FindElement(By.LinkText("Add New Product")).Click();

            var name = "testname";
            //General
            driver.FindElements(By.Name("status"))[0].Click();
            driver.FindElement(By.Name("name[en]")).SendKeys(name);
            driver.FindElement(By.Name("code")).SendKeys("testcode");
            driver.FindElements(By.Name("categories[]"))[1].Click();
            driver.FindElements(By.Name("product_groups[]"))[2].Click();
            driver.FindElement(By.Name("quantity")).SendKeys("1");
            driver.FindElement(By.Name("date_valid_from")).SendKeys("2017-03-01");
            driver.FindElement(By.Name("date_valid_to")).SendKeys("2017-03-31");

            var files = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "duck.jpg");
            driver.FindElement(By.Name("new_images[]")).SendKeys(System.IO.Path.GetFullPath(files[0]));
            //Information
            driver.FindElement(By.LinkText("Information")).Click();
            var manufacturer = driver.FindElement(By.Name("manufacturer_id"));
            var act = new Actions(driver);
            act.Click(manufacturer).SendKeys(manufacturer, Keys.ArrowDown).SendKeys(manufacturer, Keys.Enter).Perform();
            driver.FindElement(By.Name("keywords")).SendKeys("keywords");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("short descrition");
            driver.FindElement(By.Name("description[en]")).SendKeys("descrition");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("test title");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("test descrition");
            
            //Prices. 
            driver.FindElement(By.LinkText("Prices")).Click();
            driver.FindElement(By.Name("purchase_price")).SendKeys("11");
            var curr = driver.FindElement(By.Name("purchase_price_currency_code"));
            act.Click(curr).SendKeys(curr, Keys.ArrowDown).SendKeys(curr, Keys.Enter).Perform();
            driver.FindElement(By.Name("gross_prices[USD]")).SendKeys("12");
            driver.FindElement(By.Name("gross_prices[EUR]")).SendKeys("13");

            driver.FindElement(By.Name("save")).Click();
            wait.Until(d => d.Title == "Catalog | My Store");

            var product = driver.FindElement(By.LinkText(name));

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
