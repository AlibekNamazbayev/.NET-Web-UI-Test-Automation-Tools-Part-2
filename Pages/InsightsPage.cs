using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace SeleniumTests.Pages
{
    public class InsightsPage : BasePage
    {
        public InsightsPage(IWebDriver driver) : base(driver) { }

        public void CloseCookieBanner()
        {
            try
            {
                var cookieBanner = Driver.FindElement(By.Id("onetrust-accept-btn-handler"));
                if (cookieBanner.Displayed)
                {
                    cookieBanner.Click();
                    Thread.Sleep(1000);
                }
            }
            catch (NoSuchElementException)
            {
                
            }
        }

        public string GetCarouselTitle()
        {
            try
            {
                CloseCookieBanner();

                var nextButton = Wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.slider__right-arrow.slider-navigation-arrow")));
                nextButton.Click();
                Thread.Sleep(1000);
                nextButton.Click();
                Thread.Sleep(1000);

                var titleElement = Wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[@class='museo-sans-light' and contains(text(),'Three Ways Leaders Impede Their Company’s')]")));
                return titleElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Failed to find the article title in the carousel.");
            }
        }

        public void ClickReadMore()
        {
            try
            {
                // Find the first "Read More" button using XPath by text and class
                var readMoreButton = Wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("(//a[contains(@class,'slider-cta-link') and contains(text(),'Read More')])[1]")));
                
                // Scroll to the button
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", readMoreButton);
                Thread.Sleep(1000);

                // Click the button
                readMoreButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("The 'Read More' button was not found or is unavailable.");
            }
        }

        public string GetArticleTitle()
        {
            try
            {
                var articleTitle = Wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[@class='museo-sans-light' and contains(text(),'The Complex Path of Generative AI Adoption')]")));
                return articleTitle.Text;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Failed to find the article title on the page.");
            }
        }
    }
}
