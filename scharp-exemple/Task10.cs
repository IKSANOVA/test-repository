using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SoftwareTesting
{
    [TestClass]
    public class Task10
    {
        IWebDriver driver;
        WebDriverWait wait;

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            //driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //TestMethod2(driver, wait);
            //driver.Quit();
            driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            TestMethod2(driver, wait);
            driver.Quit();
            driver.Dispose();
        }

        public void TestMethod2(IWebDriver driver, WebDriverWait wait)
        {
            driver.Url = "http://localhost:8080/litecart/";
            wait.Until(d => d.Title == "Online Store | My Store");
            var e = driver.FindElement(By.CssSelector("div#box-campaigns div ul li a"));
            var url = e.GetAttribute("href");
            var name = e.FindElement(By.CssSelector("div.name")).Text;

            var priceNormal = e.FindElement(By.CssSelector("div.price-wrapper s.regular-price"));
            var priceNormalDecoration = priceNormal.GetCssValue("text-decoration");
            var priceNormalValue = priceNormal.Text;
            var priceNormalSize = priceNormal.Size;
            var priceNormalColor = priceNormal.GetCssValue("color");

            var priceAction = e.FindElement(By.CssSelector("div.price-wrapper strong.campaign-price"));
            var priceActionValue = priceAction.Text;
            var priceActionSize = priceAction.Size;
            var priceAtionColor = priceAction.GetCssValue("color");
            var priceActionDecoration = priceAction.GetCssValue("font-weight");

            driver.Url = url;
            var cname = driver.FindElement(By.CssSelector("h1.title")).Text;
            var cpriceNormal = driver.FindElement(By.CssSelector("div.price-wrapper s.regular-price"));
            var cpriceNormalDecoration = cpriceNormal.GetCssValue("text-decoration");
            var cpriceNormalValue = cpriceNormal.Text;
            var cpriceNormalSize = cpriceNormal.Size;
            var cpriceNormalColor = cpriceNormal.GetCssValue("color");

            var cpriceAction = driver.FindElement(By.CssSelector("div.price-wrapper strong.campaign-price"));
            var cpriceActionValue = cpriceAction.Text;
            var cpriceActionSize = cpriceAction.Size;
            var cpriceAtionColor = cpriceAction.GetCssValue("color");
            var cpriceActionDecoration = cpriceAction.GetCssValue("font-weight");

            //а) на главной странице и на странице товара совпадает текст названия товара
            Assert.IsTrue(name == cname, "Проверка совпадения названий.");
            //б) на главной странице и на странице товара совпадают цены(обычная и акционная)
            Assert.IsTrue(priceNormalValue == cpriceNormalValue, "Проверка совпадения обычной цены.");
            Assert.IsTrue(priceActionValue == cpriceActionValue, "Проверка совпадения акционной цены.");
            //в) обычная цена зачёркнутая и серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
            Assert.IsTrue(priceNormalDecoration.Contains("line-through"), "Обычная цена на главной странице зачеркнута.");
            Assert.IsTrue(cpriceNormalDecoration.Contains("line-through"), "Обычная цена на странице товара зачеркнута.");
            if ((driver is OpenQA.Selenium.Chrome.ChromeDriver)||(driver is OpenQA.Selenium.IE.InternetExplorerDriver))
            {
                var color1 = CromeColorToARGB(priceNormalColor);
                Assert.IsTrue(color1.R == color1.G && color1.R == color1.B, "Обычная цена на главной странице серая."); 
                var color2 = CromeColorToARGB(cpriceNormalColor);
                Assert.IsTrue(color2.R == color2.G && color2.R == color2.B, "Обычная цена на главной странице серая."); 
            }
            //г) акционная жирная и красная(можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
            if (driver is OpenQA.Selenium.Chrome.ChromeDriver)
            {
                Assert.IsTrue(priceActionDecoration.Contains("bold"), "Акционная цена на главной странице выделена жирным шрифтом.");
                Assert.IsTrue(cpriceActionDecoration.Contains("bold"), "Акционная цена на странице товара выделена жирным шрифтом.");
            }
            if (driver is OpenQA.Selenium.IE.InternetExplorerDriver)
            {
                Assert.IsTrue(int.Parse(priceActionDecoration) > 400 || priceActionDecoration.Contains("bold"), "Акционная цена на главной странице выделена жирным шрифтом.");
                Assert.IsTrue(int.Parse(cpriceActionDecoration) > 400 || cpriceActionDecoration.Contains("bold"), "Акционная цена на странице товара выделена жирным шрифтом.");
            }
            if ((driver is OpenQA.Selenium.Chrome.ChromeDriver) || (driver is OpenQA.Selenium.IE.InternetExplorerDriver))
            {
                var color1 = CromeColorToARGB(priceAtionColor);
                Assert.IsTrue(color1.G == 0 && color1.B == 0, "Акционная цена на главной странице красная."); 
                var color2 = CromeColorToARGB(cpriceAtionColor);
                Assert.IsTrue(color2.G == 0 && color2.B == 0, "Акционная цена на главной странице красная.");
            }
            //г) акционная цена крупнее, чем обычная(это тоже надо проверить на каждой странице независимо)
            Assert.IsTrue(((priceNormalSize.Height < priceActionSize.Height) && (priceNormalSize.Width < priceActionSize.Width)), "Проверка размера цен на главной странице.");
            Assert.IsTrue(((cpriceNormalSize.Height < cpriceActionSize.Height) && (cpriceNormalSize.Width < cpriceActionSize.Width)), "Проверка размера цен на странице товара.");
        }

        [TestCleanup]
        public void Stop()
        {
            
        }

        private System.Drawing.Color CromeColorToARGB(string color)
        {
            //rgba(119, 119, 119, 1)
            var parts = color.Replace("rgba(", "").Replace(")", "").Replace(",", "").Split(' ');
            var c = System.Drawing.Color.FromArgb(int.Parse(parts[3]), int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
            return c;
        }
    }
}
