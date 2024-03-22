    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public class E2ETest :Base
        {



            [Test]
            [TestCase("rahulshettyacademy", "learning")]
            public void EndToEndFlow1(String username, String password)

            {

                String[] expectedProducts = { "iphone X", "Blackberry" };
                String[] actualProducts = new string[2];
                driver.Value.FindElement(By.Id("username")).SendKeys(username);
                driver.Value.FindElement(By.Name("password")).SendKeys(password);
                driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
                driver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();
                WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

               IList<IWebElement> products = driver.Value.FindElements(By.TagName("app-card"));

                foreach( IWebElement product in products)
                {

                 if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))

                    {
                       product.FindElement(By.CssSelector(".card-footer button")).Click();
                    }
                 TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);

                }
                driver.Value.FindElement(By.PartialLinkText("Checkout")).Click();
                IList <IWebElement> checkoutCards = driver.Value.FindElements(By.CssSelector("h4 a"));

                for(int i =0; i< checkoutCards.Count;i++)

                {
                    actualProducts[i] = checkoutCards[i].Text;



                }
                Assert.That(actualProducts, Is.EqualTo(expectedProducts));

                driver.Value.FindElement(By.CssSelector(".btn-success")).Click();

                driver.Value.FindElement(By.Id("country")).SendKeys("ind");

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
                driver.Value.FindElement(By.LinkText("India")).Click();


                driver.Value.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
                driver.Value.FindElement(By.CssSelector("[value='Purchase']")).Click();
               String confirText= driver.Value.FindElement(By.CssSelector(".alert-success")).Text;

                StringAssert.Contains("Success", confirText);

            }
        }

    }
