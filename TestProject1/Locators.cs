using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace TestProject1
{
    internal class Locators
    {
        IWebDriver driver;
        

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig()); // uses webdrivermanager to get the latest chrome exe file and adds to project
            driver = new ChromeDriver(); // initializes a new chrome browser
            driver.Manage().Window.Maximize(); //maximizes the browser window
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void LocatorsIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy1");
            driver.FindElement(By.Name("password")).SendKeys("learning1");
            //css selector syntax is tagname[attribute='value'] e.g. input[Id='username']
            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            //xpath syntax is //tagName[@attribute='value']
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //Thread.Sleep(3000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));
            String errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.Write(errorMessage);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hreftAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
            //validate link text
            Assert.AreEqual(expectedUrl, hreftAttr);
            Assert.That(hreftAttr, Is.EqualTo(expectedUrl)); //new way to assert
            driver.Quit();
        }

    }
}
