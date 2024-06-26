﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    //driver.FindElement(By.Name("password")).SendKeys("learning");
    //driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
    //driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }



        //pageobject factory

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign In']")]
        private IWebElement signInButton;

        public ProductsPage validLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkBox.Click();
            signInButton.Click();
            return new ProductsPage(driver);
        }

        public IWebElement getUserName()
        {
            return username;
        }




    }



}
