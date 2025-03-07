using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumTests.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public void ClickAbout()
        {
            var aboutLink = Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//a[contains(@class, 'top-navigation__item-link') and @href='/about']")));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", aboutLink);
            Thread.Sleep(500);
            aboutLink.Click();
        }

        public void ClickInsights()
        {
            var insightsLink = Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//a[contains(@class, 'top-navigation__item-link') and @href='/insights']")));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", insightsLink);
            Thread.Sleep(500);
            insightsLink.Click();
        }
    }
}
