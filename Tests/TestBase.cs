using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace SeleniumTests.Tests
{
    public abstract class TestBase
    {
        protected IWebDriver Driver { get; private set; } = null!;
        protected ChromeOptions Options { get; private set; } = null!;
        protected string DownloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

        [OneTimeSetUp]
        public void GlobalSetup()
        {
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
