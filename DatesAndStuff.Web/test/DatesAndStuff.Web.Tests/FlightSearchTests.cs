using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;

namespace BlazeDemo.Tests;

[TestFixture]
public class FlightSearchTests
{
    private IWebDriver driver;
    private const string Url = "https://blazedemo.com";

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void FlightSearch_MexicoCityToDublin_ShouldHaveAtLeastThreeFlights()
    {
        driver.Navigate().GoToUrl(Url);
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var departureSelect = new SelectElement(driver.FindElement(By.Name("fromPort")));
        departureSelect.SelectByValue("Mexico City");

        var arrivalSelect = new SelectElement(driver.FindElement(By.Name("toPort")));
        arrivalSelect.SelectByValue("Dublin");

        driver.FindElement(By.CssSelector("input.btn-primary")).Click();

        wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("table")));

        var flightRows = driver.FindElements(By.XPath("//table/tbody/tr"));

        flightRows.Count.Should().BeGreaterThanOrEqualTo(3, $"Expected at least 3 flight, but found {flightRows.Count}.");
    }
}