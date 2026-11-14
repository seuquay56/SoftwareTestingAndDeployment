using Allure.NUnit;
using Allure.NUnit.Attributes;
using CsvHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace LoginAutomation;

[AllureNUnit]
[AllureSuite("Lab 9 - Login Automation")]
[AllureFeature("Login page")]
public class LoginTests
{
    public static IEnumerable<object[]> ValidLoginCases()
    {
        var csvPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "LoginTestData.csv");
        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var row in csv.GetRecords<LoginData>())
            yield return new object[] { row.Username, row.Password };
    }

    [TestCaseSource(nameof(ValidLoginCases))]
    [AllureStory("Valid credentials")]
    public void Login_WithValidCredentials_ShouldSucceed(string username, string password)
    {
        using var driver = CreateDriver();
        driver.Navigate().GoToUrl(GetLoginPageUrl());
        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("loginButton")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var message = wait.Until(d => d.FindElement(By.Id("message")));
        Assert.That(message.Text, Is.EqualTo("Login successful."));
    }

    [Test]
    [AllureStory("Invalid credentials")]
    public void Login_WithInvalidCredentials_ShowsError()
    {
        using var driver = CreateDriver();
        driver.Navigate().GoToUrl(GetLoginPageUrl());
        driver.FindElement(By.Id("username")).SendKeys("unknown-user");
        driver.FindElement(By.Id("password")).SendKeys("wrong-password");
        driver.FindElement(By.Id("loginButton")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        var message = wait.Until(d => d.FindElement(By.Id("message")));
        Assert.That(message.Text, Is.EqualTo("Invalid username or password."));
    }

    private static IWebDriver CreateDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1280,900");
        options.AddArgument("--disable-gpu");
        return new ChromeDriver(options); // Selenium Manager resolves a compatible driver when needed.
    }

    private static string GetLoginPageUrl()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "login.html");
        if (!File.Exists(path))
            throw new FileNotFoundException("login.html was not copied to the test output directory.", path);
        return new Uri(path).AbsoluteUri;
    }

    public sealed class LoginData
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
