using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SoftwareTesting
{
    [TestClass]
    public class Task9
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestInitialize]
        public void Init()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = "http://localhost:8080/litecart/admin/";
            wait.Until(d => d.Title == "My Store");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("password");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(d => d.Title == "My Store");
        }

        [TestMethod]
        public void TestMethod1()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            wait.Until(d => d.Title == "Countries | My Store");
            var rows = driver.FindElements(By.CssSelector("table.dataTable tbody tr.row"));
            var countries = new List<string>();
            var zonesurls = new List<string>();
            foreach (var row in rows)
            {
                var columns = row.FindElements(By.TagName("td"));
                var countryName = columns[4].Text;
                var zonesCount = int.Parse(columns[5].Text);
                countries.Add(countryName);
                if(zonesCount > 0)
                    zonesurls.Add(columns[4].FindElement(By.TagName("a")).GetAttribute("href"));
            }
            Assert.IsTrue(CheckSort(countries), "Проверка сортировки стран");

            foreach(string url in zonesurls)
            {
                driver.Url = url;
                wait.Until(d => d.Title == "Edit Country | My Store");
                var zrows = driver.FindElements(By.CssSelector("table.dataTable tbody tr"));
                var zones = new List<string>();
                for (int i = 1; i < zrows.Count - 1; i++)
                    zones.Add(zrows[i].FindElements(By.TagName("td"))[2].Text);
                Assert.IsTrue(CheckSort(countries), "Проверка сортировки зон по ссылке "+url);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
            wait.Until(d => d.Title == "Geo Zones | My Store");
            var rows = driver.FindElements(By.CssSelector("table.dataTable tbody tr.row"));
            var urls = new List<string>();
            foreach (var row in rows)
                urls.Add(row.FindElements(By.TagName("td"))[2].FindElement(By.TagName("a")).GetAttribute("href"));
            foreach (var url in urls)
            {
                driver.Url = url;
                wait.Until(d => d.Title == "Edit Geo Zone | My Store");
                var zrows = driver.FindElements(By.CssSelector("table.dataTable tbody tr"));
                var zones = new List<string>();
                for (int i = 1; i < zrows.Count - 1; i++)
                    zones.Add(zrows[i].FindElements(By.TagName("td"))[2].Text);
                Assert.IsTrue(CheckSort(zones), "Проверка сортировки зон по ссылке " + url);
            }
        }

        private bool CheckSort(List<string> list)
        {
            var array = new string[list.Count];
            list.CopyTo(array);
            list.Sort();
            for (int i = 0; i < list.Count; i++)
                if (!list[i].Equals(array[i]))
                    return false;
            return true;
        }

        [TestCleanup]
        public void Stop()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
