using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{

    public class ConfirmationPage
    {
        IWebDriver driver;
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement DeliveryLocation;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement LocationDropDownOption;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement termsAndConditionsCheckBox;

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement purchaseButton;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMessage;

        public void enterPartialLocation(string location)
        {
            DeliveryLocation.SendKeys(location);
        }

        public void waitForLocationDropDown()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public void selectLocation()
        {
            LocationDropDownOption.Click();
        }

        public void tickTermsAndConditions()
        {
            termsAndConditionsCheckBox.Click();
        }

        public void clickPurchaseButton() 
        {
            purchaseButton.Click();
        }

        public string getSuccessMessageText()
        {
            return successMessage.Text;
        }
    }
}
