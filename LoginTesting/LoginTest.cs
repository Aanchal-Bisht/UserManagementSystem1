using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V127.Network;
using OpenQA.Selenium.Support.UI;

namespace LoginTesting
{
    public class LoginTest
    {
        private IWebDriver driver;

        //[SetUp]
        //public void Setup()
        //{
            
        //}

        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5272/User/Login");
            //IWebDriver driver = new ChromeDriver();
            driver.FindElement(By.Name("userName")).SendKeys("suman");
            driver.FindElement(By.Name("password")).SendKeys("78");
            driver.FindElement(By.Id("btnLogin")).Click();
           

            WebDriverWait Wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            Assert.Pass();
            driver.Close();
            driver.Quit();
        }
    }
}