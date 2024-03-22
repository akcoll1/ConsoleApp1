using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using CSharpSeleniumFramework.Utilities;
using System.Threading;
using System.Runtime.Loader;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium.DevTools.V120.Page;

namespace CSharpSeleniumFramework.Utilities
{
    public class Base
    {
        public ExtentReports extent;
        public ExtentTest test;
        //dotnet test CSharpSeleniumFramework.csproj --filter TestCategory=Regression --% -- TestRunParameters.Parameter(name=\"browserName\", value=\"Edge\")
        String browserName;
        
        [OneTimeSetUp]
        public void Setup()
        {
            
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;            
            String reportPath = projectDirectory = "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local Host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Adam Collier");
        }

        //public required IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new();

        [SetUp]
        public void StartBrowser()
        {
            //configuration
            browserName = TestContext.Parameters["browserName"];

            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            
            InitBrowser(browserName);
            driver.Value.Manage().Window.Maximize(); 
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName) 
            {
                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();

        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Fail", captureScreenshot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {

            }
            extent.Flush();

            driver.Value.Dispose();
        }

        public MediaEntityModelProvider captureScreenshot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }
}
