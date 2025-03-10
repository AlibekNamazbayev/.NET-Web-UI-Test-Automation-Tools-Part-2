using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class EpamTests : TestBase
    {
        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public async Task ValidateFileDownload(string expectedFileName)
        {
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            HomePage homePage = new HomePage(Driver);
            homePage.ClickAbout();

            AboutPage aboutPage = new AboutPage(Driver);
            aboutPage.ScrollToGlanceSection();
            aboutPage.ClickDownloadButton();

            string downloadPath = Path.Combine(DownloadDirectory, expectedFileName);
            bool fileDownloaded = false;

            for (int i = 0; i < 30; i++)
            {
                if (File.Exists(downloadPath))
                {
                    fileDownloaded = true;
                    break;
                }
                await Task.Delay(1000);
            }

            Assert.That(fileDownloaded, Is.True, $"File {expectedFileName} was not downloaded.");
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

            var actualTitleElement = Wait.Until(driver => driver.FindElement(By.XPath("//span[@class='museo-sans-light']")));
            string actualTitle = actualTitleElement.Text;

            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "Article title does not match.");
        }
    }
}
