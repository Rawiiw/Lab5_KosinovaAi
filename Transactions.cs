using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class Transactions
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public Transactions(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public IWebElement SearchByPayeeInput => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input1")));
    public IWebElement SearchByAccountDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input2")));
    public IWebElement SearchByTypeDropdown => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input3")));
    public IWebElement SearchByExpenditurePayeesInput => wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input4")));
    public IWebElement SearchResultsTable => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".table.table-striped.table-bordered")));

    public List<IWebElement> GetAllElements()
    {
        var elements = new List<IWebElement>
        {
            SearchByPayeeInput,
            SearchByAccountDropdown,
            SearchByTypeDropdown,
            SearchByExpenditurePayeesInput,
            SearchResultsTable
            
        };

        return elements;
    }
    public void ClearFilters()
    {
  
        SearchByPayeeInput.Clear();

       
        var accountDropdown = new SelectElement(SearchByAccountDropdown);
        accountDropdown.SelectByText("All Accounts");

        var typeDropdown = new SelectElement(SearchByTypeDropdown);
        typeDropdown.SelectByText("All Types");

     
        SearchByExpenditurePayeesInput.Clear();

  
    }

}

