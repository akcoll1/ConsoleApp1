using System;
using System.Collections.Generic;
using System.Linq;
using CSharpSeleniumFramework.PageObjects;
using CSharpSeleniumFramework.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using System.Runtime.Loader;
using NUnit.Framework.Legacy;

namespace CSharpSeleniumFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class E2ECheckOutTest : Base
    {


        [Test, TestCaseSource("AddTestDataConfig"), Category("Regression")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String username, String password, string[] expectedProducts)

        {

            
            string[] actualProducts = new string[2];
            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productPage = loginPage.validLogin(username, password);
            productPage.waitForCheckOutDisplay();
            IList<IWebElement> products = productPage.getCards();

            foreach (IWebElement product in products)
            {

                if (expectedProducts.Contains(product.FindElement(productPage.getCardTitle()).Text))

                {
                    product.FindElement(productPage.addToCartButton()).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);

            }
            CheckoutPage checkoutPage = productPage.checkout();
            IList<IWebElement> checkoutCards = checkoutPage.getCards();

            for (int i = 0; i < checkoutCards.Count; i++)

            {
                actualProducts[i] = checkoutCards[i].Text;



            }           
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            ConfirmationPage confirmationPage = checkoutPage.checkOut();
            confirmationPage.enterPartialLocation("ind");
            confirmationPage.waitForLocationDropDown();
            confirmationPage.selectLocation();


            confirmationPage.tickTermsAndConditions();
            confirmationPage.clickPurchaseButton();
            confirmationPage.getSuccessMessageText();


            Assert.That(confirmationPage.getSuccessMessageText(), Does.Contain("Success"));

        }

        [Test, Category("Smoke")]
        public void LocatorsIdentification()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy1");
            driver.Value.FindElement(By.Name("password")).SendKeys("learning1");
            //css selector syntax is tagname[attribute='value'] e.g. input[Id='username']
            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            //xpath syntax is //tagName[@attribute='value']
            driver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            //Thread.Sleep(3000);

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.Value.FindElement(By.Id("signInBtn")), "Sign In"));
            String errorMessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.Write(errorMessage);

            IWebElement link = driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hreftAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
            //validate link text
            Assert.That(hreftAttr, Is.EqualTo(expectedUrl));
            Assert.That(hreftAttr, Is.EqualTo(expectedUrl)); //new way to assert
            driver.Value.Quit();
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }

}
