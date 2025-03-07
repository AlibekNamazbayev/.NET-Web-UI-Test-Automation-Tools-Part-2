using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumTests.Pages
{
    public class AboutPage : BasePage
    {
        public AboutPage(IWebDriver driver) : base(driver) { }

        public void ScrollToGlanceSection()
        {
            try
            {
                var glanceElement = Wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[contains(text(),'EPAM at') and contains(text(),'Glance')]")));
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", glanceElement);
            }
            catch (WebDriverTimeoutException)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0,800);");
            }
            Thread.Sleep(1000);
        }

        public void ClickDownloadButton()
        {
            var downloadButton = Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(@class, 'button__content--desktop') and contains(text(),'DOWNLOAD')]")));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", downloadButton);
            Thread.Sleep(500);
            downloadButton.Click();
        }
    }
}
