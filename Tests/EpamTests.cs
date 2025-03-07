using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class EpamTests : TestBase
    {
        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void ValidateFileDownload(string expectedFileName)
        {
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            HomePage homePage = new HomePage(Driver);
            homePage.ClickAbout();

            AboutPage aboutPage = new AboutPage(Driver);
            aboutPage.ScrollToGlanceSection();
            aboutPage.ClickDownloadButton();

            string downloadPath = Path.Combine(DownloadDirectory, expectedFileName);
            bool fileDownloaded = false;
            int waitSeconds = 30;
            for (int i = 0; i < waitSeconds; i++)
            {
                if (File.Exists(downloadPath))
                {
                    fileDownloaded = true;
                    break;
                }
                Thread.Sleep(1000);
            }

            Assert.That(fileDownloaded, Is.True, $"Файл {expectedFileName} не был скачан.");
        }

        [Test]
        public void ValidateArticleTitle()
        {
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            HomePage homePage = new HomePage(Driver);
            homePage.ClickInsights();

            InsightsPage insightsPage = new InsightsPage(Driver);
            string expectedTitle = insightsPage.GetCarouselTitle();

            insightsPage.ClickReadMore();

            Thread.Sleep(2000);
            var actualTitleElement = Driver.FindElement(By.XPath("//span[@class='museo-sans-light']"));
            string actualTitle = actualTitleElement.Text;

            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "Заголовок статьи не совпадает.");
        }
    }
}
