using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        using (IWebDriver driver = new ChromeDriver())
        {

            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/SearchFilter/");

            Transactions transactionsPage = new Transactions(driver);


            List<IWebElement> allElements = transactionsPage.GetAllElements();

            foreach (var element in allElements)
            {
                Console.WriteLine($"Element TagName: {element.TagName}, ID: {element.GetAttribute("id")}");
            }
        }
    }
}
