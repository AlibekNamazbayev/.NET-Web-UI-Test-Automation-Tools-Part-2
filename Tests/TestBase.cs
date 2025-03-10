using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace SeleniumTests.Tests
{
    public class TestBase
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected ChromeOptions Options { get; private set; } = null!;
        protected WebDriverWait Wait { get; private set; } = null!;
        protected string DownloadDirectory { get; private set; } = null!;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            DownloadDirectory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Downloads");
            if (!Directory.Exists(DownloadDirectory))
            {
                Directory.CreateDirectory(DownloadDirectory);
            }
        }

        [SetUp]
        public void Setup()
        {
            Options = new ChromeOptions();
            Options.AddArgument("--start-maximized");

            string headless = TestContext.Parameters.Get("headless", "false");
            if (headless.ToLower() == "true")
            {
                Options.AddArgument("--headless=new");
            }

            Options.AddUserProfilePreference("download.default_directory", DownloadDirectory);
            Options.AddUserProfilePreference("download.prompt_for_download", false);
            Options.AddUserProfilePreference("safebrowsing.enabled", true);

            Driver = new ChromeDriver(Options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Driver?.Quit();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error closing WebDriver: {ex.Message}");
            }
        }
    }
}
