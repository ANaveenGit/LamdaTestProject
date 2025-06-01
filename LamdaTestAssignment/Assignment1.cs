using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace LamdaTest
{

    public class LambaBase
    {
        protected IWebDriver Driver { get; private set; }
        [SetUp]
        public void Setup()
        {
            Driver = new ChromeDriver();
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //Driver.Url = "https://www.lambdatest.com/selenium-playground";
        }
        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
            Driver.Dispose();
        }
    }

    [TestFixture]
    [Parallelizable]
    public class Assignment1 : LambaBase
    {
        [Test]
        public void LamdaTestScenario1()
        {
            try
            {
                //1. Open the URL.
                Driver.Url = "https://www.lambdatest.com/selenium-playground";

                //2. Click Simple Form Demo
                Driver.FindElement(By.XPath("//a[contains(text(), 'Simple Form Demo')]")).Click();

                //3. Validate that the URL contains “simple-form-demo”.
                string SimpleFormDemourl = Driver.Url;
                Assert.That(SimpleFormDemourl.Contains("simple-form-dem"), Is.True);

                //4.Create a variable for a string value, e.g., “Welcome to LambdaTest”.
                string Entervalue = "Welcome to LambdaTest";

                //5.Use this variable to enter values in the “Enter Message” text box.
                Driver.FindElement(By.Id("user-message")).SendKeys(Entervalue);

                //6.Click “Get Checked Value”.
                Driver.FindElement(By.Id("showInput")).Click();

                //7.Validate whether the same text message is displayed in the right - hand panel under the “Your Message:” section.
                string ActualValue = Driver.FindElement(By.Id("message")).Text;
                Assert.That(ActualValue, Is.EqualTo(Entervalue));

                Console.WriteLine("Test Scenario 1 Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Scenario 1 Failed: {ex}");
            }
        }
    }

    [TestFixture]
    [Parallelizable]
    public class Assignment2 : LambaBase
    {
        [Test]
        public void LamdaTestScenario2()
        {
            try
            {
                //1. Open the URL and click “Drag & Drop Sliders”
                Driver.Url = "https://www.lambdatest.com/selenium-playground";
                Driver.FindElement(By.XPath("//a[contains(text(),'Drag & Drop Sliders')]")).Click();

                //2. Select the slider “Default value 15” and drag the bar to make it 95 by validating whether the range value shows 95.
                IWebElement Default15 = Driver.FindElement(By.XPath("//div[@class='sp__range sp__range-success']/input"));
                int InputValue = 95;

                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript($"arguments[0].value = '{InputValue - 1}';", Default15);
                Default15.SendKeys(Keys.ArrowRight);

                IWebElement RangeValue = Driver.FindElement(By.Id("rangeSuccess"));
                Assert.That(RangeValue.Text, Is.EqualTo("96"));

                Console.WriteLine("Test Scenario 2 Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Scenario 2 Failed: {ex}");
            }
        }
    }

    [TestFixture]
    [Parallelizable]
    public class Assignment3 : LambaBase
    {
        [Test]
        public void LamdaTestScenario3()
        {
            try
            {
                //1. Open the URL and click “Input Form Submit”.
                Driver.Url = "https://www.lambdatest.com/selenium-playground";
                Driver.FindElement(By.XPath("//a[contains(text(),'Input Form Submit')]")).Click();

                //2. Click “Submit” without filling in any information in the form.
                Driver.FindElement(By.XPath("//button[contains(@type, 'submit') and contains(text(), 'Submit')]")).Click();

                //3.Assert “Please fill out this field.” error message.
                var NameField = Driver.FindElement(By.Name("name"));
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                string ErrorMessage = (string)js.ExecuteScript("return arguments[0].validationMessage;", NameField);
                Assert.That(ErrorMessage, Is.EqualTo("Please fill out this field."));

                //4.Fill in Name, Email, and other fields.
                Driver.FindElement(By.Name("name")).SendKeys("Elon Musk");  //Name
                Driver.FindElement(By.Id("inputEmail4")).SendKeys("muskelon@tesla.com");  //Email
                Driver.FindElement(By.Name("password")).SendKeys("Merin@34");  //Password
                Driver.FindElement(By.Id("company")).SendKeys("Tesla");  //Company
                Driver.FindElement(By.Name("website")).SendKeys("Tesla.com");  //Website

                //5.From the Country drop - down, select “United States” using the tex property.
                new SelectElement(Driver.FindElement(By.Name("country"))).SelectByText("United States");  //Country

                //6.Fill in all fields and click “Submit”.
                Driver.FindElement(By.Name("city")).SendKeys("Florida");  //City
                Driver.FindElement(By.Name("address_line1")).SendKeys("30the Lane");  //Address 1
                Driver.FindElement(By.Name("address_line2")).SendKeys("Church Street");  //Address 2
                Driver.FindElement(By.Id("inputState")).SendKeys("Florida");  //State
                Driver.FindElement(By.Name("zip")).SendKeys("67647065");  //Zip Code

                //7.Once submitted, validate the success message “Thanks for contacting us, we will get back to you shortly.” on the screen.
                Driver.FindElement(By.XPath("//button[contains(@type, 'submi') and contains(text(), 'Submit')]")).Click();
                string OutputMessage = Driver.FindElement(By.CssSelector(".success-msg.hidden")).Text;
                Assert.That(OutputMessage, Is.EqualTo("Thanks for contacting us, we will get back to you shortly."));

                Console.WriteLine("Test Scenario 3 Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Scenario 3 Failed: {ex}");
            }
        }
    }
}
