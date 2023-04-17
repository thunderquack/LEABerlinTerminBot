using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;

EdgeOptions options = new EdgeOptions();
options.AddArgument("--disable-blink-features=AutomationControlled");
options.AddArgument("--disable-gpu");
bool terminIsFound = true;
bool aNew = true;
IWebDriver driver;
driver = new EdgeDriver(options);
IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
js.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => false})");
Stopwatch stopwatch;
stopwatch = Stopwatch.StartNew();
do
{
    try
    {

        // find termin part

        driver.Navigate().GoToUrl("https://otv.verwalt-berlin.de/ams/TerminBuchen");
        var button = driver.FindElement(By.LinkText("Termin buchen"));
        button.Click();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("xi-cb-1")));
        var checkbox = driver.FindElement(By.Id("xi-cb-1"));
        checkbox.Click();
        button = driver.FindElement(By.Id("applicationForm:managedForm:proceed"));
        button.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.Name("sel_staat")));
        var select = new SelectElement(driver.FindElement(By.Name("sel_staat")));
        Thread.Sleep(1000);
        select.SelectByText("Belarus");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Name("personenAnzahl_normal")));
        select = new SelectElement(driver.FindElement(By.Name("personenAnzahl_normal")));
        select.SelectByText("eine Person");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Name("lebnBrMitFmly")));
        select = new SelectElement(driver.FindElement(By.Name("lebnBrMitFmly")));
        select.SelectByText("ja");
        wait.Until(ExpectedConditions.ElementIsVisible(By.Name("fmlyMemNationality")));
        select = new SelectElement(driver.FindElement(By.Name("fmlyMemNationality")));
        select.SelectByText("Belarus");
        var address = By.XPath("//p[contains(text(), 'Aufenthaltstitel - verlängern')]");
        wait.Until(ExpectedConditions.ElementToBeClickable(address));
        driver.FindElement(address).Click();
        address = By.XPath("//p[contains(text(), 'Familiäre Gründe')]");
        wait.Until(ExpectedConditions.ElementToBeClickable(address));
        driver.FindElement(address).Click();
        address = By.XPath("//label[contains(text(), 'ausländischen Familienangehörigen')]");
        wait.Until(ExpectedConditions.ElementToBeClickable(address));
        driver.FindElement(address).Click();
        address = By.Id("applicationForm:managedForm:proceed");
        wait.Until(ExpectedConditions.ElementToBeClickable(address));
        Thread.Sleep(1000);
        driver.FindElement(address).Click();
        address = By.XPath("//li[contains(@class,'antcl_active')]/span[text()='Terminauswahl']");
        try
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(address));
            terminIsFound = true;
        }
        catch
        {
            terminIsFound = false;
        }
    }
    catch { }
    finally
    {
        if (stopwatch.Elapsed.TotalMinutes > 25)
        {
            driver.Close();
            driver = new EdgeDriver(options);            
            stopwatch.Restart();
        }
    }
}
while (!terminIsFound);

Console.WriteLine("Termin is found, exiting");