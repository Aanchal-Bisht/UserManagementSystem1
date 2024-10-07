using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace LoginTest
{
    public class Test
    {
       

        [Test]
        public void Test1()
        {
                string url = "http://localhost:5272/User/Login";
                IWebDriver _driver = new ChromeDriver();
                _driver.Navigate().GoToUrl(url);
                _driver.Manage().Window.Maximize();
                _driver.FindElement(By.Id("userId")).SendKeys("aanchal");
                _driver.FindElement(By.Id("pwd")).SendKeys("yuyurty");
                _driver.FindElement(By.Id("exampleCheck1")).Click();
                _driver.FindElement(By.Id("btnLogin")).Click();
                WebDriverWait Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _driver.Close();
                _driver.Dispose();
                       
           
        }
    }
}