using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;

 namespace Lab5KosinovaAiTests;

[TestClass]
public class TransactionsTests
{
    private IWebDriver driver;

    private int OriginalRowCount; 

    [TestInitialize]
    public void Initialize()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/SearchFilter/");

        Transactions transactionsPage = new Transactions(driver);

        
        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        OriginalRowCount = rows.Count;
    }

    [TestMethod]
    public void VerifyAllElementsExist()
    {
        Transactions transactionsPage = new Transactions(driver);
        List<IWebElement> allElements = transactionsPage.GetAllElements();

        foreach (var element in allElements)
        {
            Assert.IsNotNull(element, $"Element with ID {element.GetAttribute("id")} is not present.");
        }
    }

    [TestMethod]
    public void TestFilterByPayee()
    {
        Transactions transactionsPage = new Transactions(driver);

        transactionsPage.SearchByPayeeInput.SendKeys("Sample Payee");

        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        foreach (var row in rows)
        {
            Assert.IsTrue(row.Text.Contains("Sample Payee"), $"Payee name not found in search results: {row.Text}");
        }
    }

    [TestMethod]
    public void TestFilterByAccount()
    {
        Transactions transactionsPage = new Transactions(driver);

        var accountDropdown = new SelectElement(transactionsPage.SearchByAccountDropdown);
        accountDropdown.SelectByValue("SampleAccountID");
        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        foreach (var row in rows)
        {
            Assert.IsTrue(row.Text.Contains("Sample Account"), $"Account name not found in search results: {row.Text}");
        }
    }

    [TestMethod]
    public void TestFilterByTransactionType()
    {
        Transactions transactionsPage = new Transactions(driver);

        var typeDropdown = new SelectElement(transactionsPage.SearchByTypeDropdown);
        typeDropdown.SelectByValue("EXPENDITURE");

        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        foreach (var row in rows)
        {
            Assert.IsTrue(row.Text.Contains("EXPENDITURE"), $"Transaction type not found in search results: {row.Text}");
        }
    }

    [TestMethod]
    public void TestFilterByExpenditurePayees()
    {
        Transactions transactionsPage = new Transactions(driver);

        transactionsPage.SearchByExpenditurePayeesInput.SendKeys("ExpenditurePayee");

        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        foreach (var row in rows)
        {
            Assert.IsTrue(row.Text.Contains("ExpenditurePayee"), $"Expenditure payee not found in search results: {row.Text}");
        }
    }

    [TestMethod]
    public void TestClearFilters()
    {
        Transactions transactionsPage = new Transactions(driver);

    
        transactionsPage.ClearFilters();

        List<IWebElement> rows = new List<IWebElement>(transactionsPage.SearchResultsTable.FindElements(By.TagName("tr")));
        Assert.AreEqual(OriginalRowCount, rows.Count, "Search results table not restored after clearing filters");
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }
}
