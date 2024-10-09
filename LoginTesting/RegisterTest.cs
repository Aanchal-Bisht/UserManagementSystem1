using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V127.Network;
using OpenQA.Selenium.Support.UI;
namespace LoginTesting
{
    public class RegisterTest
    {
        [Test]
        public void Test()
        {
             IWebDriver driver=new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5272/User/Register");
            driver.FindElement(By.Id("username")).SendKeys("riya");
            driver.FindElement(By.Id("email")).SendKeys("riya12@gmail.com ");
            driver.FindElement(By.Id("password")).SendKeys("1225");
            driver.FindElement(By.Id("dob")).SendKeys("2006-5-22");
            SelectElement genderSelect = new SelectElement(driver.FindElement(By.Id("Gender")));
            genderSelect.SelectByValue("");
            SelectElement departmentSelect = new SelectElement(driver.FindElement(By.Id("Department")));
            departmentSelect.SelectByValue("HR");
            driver.FindElement(By.Name("phone")).SendKeys("");
            driver.FindElement(By.Id("RegUser")).Submit();
            WebDriverWait Wait = new WebDriverWait(driver, new TimeSpan(0, 0, 50));
            Console.WriteLine("hello");
            Assert.Pass();
            driver.Close();
            driver.Quit();
        }
    }
}
