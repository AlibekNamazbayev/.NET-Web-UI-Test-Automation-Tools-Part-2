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
                // Если баннера нет — продолжаем тест
            }
        }

        public string GetCarouselTitle()
        {
            try
            {
                CloseCookieBanner();

                // Кликаем дважды на кнопку "Next" в карусели
                var nextButton = Wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.slider__right-arrow.slider-navigation-arrow")));
                nextButton.Click();
                Thread.Sleep(1000);
                nextButton.Click();
                Thread.Sleep(1000);

                // Получаем заголовок статьи в карусели
                var titleElement = Wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[@class='museo-sans-light' and contains(text(),'Three Ways Leaders Impede Their Company’s')]")));
                return titleElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Не удалось найти заголовок статьи в карусели.");
            }
        }

        public void ClickReadMore()
        {
            try
            {
                // Ищем первую кнопку "Read More", используя XPath по тексту и классу
                var readMoreButton = Wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("(//a[contains(@class,'slider-cta-link') and contains(text(),'Read More')])[1]")));
                
                // Прокручиваем к кнопке
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", readMoreButton);
                Thread.Sleep(1000);

                // Кликаем по кнопке
                readMoreButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Кнопка 'Read More' не найдена или недоступна.");
            }
        }

        public string GetArticleTitle()
        {
            try
            {
                // Получаем заголовок статьи на странице (ожидаем, что он содержит "The Complex Path of Generative AI Adoption")
                var articleTitle = Wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//span[@class='museo-sans-light' and contains(text(),'The Complex Path of Generative AI Adoption')]")));
                return articleTitle.Text;
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Не удалось найти заголовок статьи на странице.");
            }
        }
    }
}
