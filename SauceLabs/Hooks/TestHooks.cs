using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config; // Required for Theme enum
using System;
using System.IO;
using OpenQA.Selenium.Support.Extensions;

namespace SauceDemoAutomation.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private IWebDriver _driver;
        private int _stepCounter = 1; // Instance-level counter per scenario

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            try
            {
                // Resolve absolute path for the Reports folder
                string projectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
                string reportsPath = Path.Combine(projectPath, "Reports");
                string screenshotsPath = Path.Combine(reportsPath, "Screenshots");

                // Create Reports and Screenshots directories
                Directory.CreateDirectory(reportsPath);
                Directory.CreateDirectory(screenshotsPath);

                // Initialize ExtentReports (will be used for all scenarios)
                _extent = new ExtentReports();

                Console.WriteLine($"Reports directory created at: {reportsPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing ExtentReports: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _stepCounter = 1;

            // Initialize WebDriver
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _scenarioContext["WebDriver"] = _driver;

            // Create scenario node
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title, _scenarioContext.ScenarioInfo.Description);

            // Initialize ExtentSparkReporter with scenario-specific file path
            string projectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
            string safeScenarioTitle = _scenarioContext.ScenarioInfo.Title.Replace(" ", "_").Replace(":", "_").Replace("/", "_"); // Sanitize title
            string reportFilePath = Path.Combine(projectPath, "Reports", $"{safeScenarioTitle}.html");
            var htmlReporter = new ExtentSparkReporter(reportFilePath);
            htmlReporter.Config.DocumentTitle = $"SauceDemo - {_scenarioContext.ScenarioInfo.Title}";
            htmlReporter.Config.ReportName = $"SauceDemo BDD - {_scenarioContext.ScenarioInfo.Title}";
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.TimelineEnabled = true;

            // Attach reporter for this scenario
            _extent.AttachReporter(htmlReporter);

            Console.WriteLine($"Report for scenario '{_scenarioContext.ScenarioInfo.Title}' will be generated at: {reportFilePath}");
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = _scenarioContext.StepContext.StepInfo.Text;
            var status = _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError
                ? Status.Fail
                : Status.Pass;

            _scenario.Log(status, $"Step {_stepCounter}: {stepType} {stepText} [{DateTime.Now:HH:mm:ss.fff}]");

            if (status == Status.Fail)
            {
                try
                {
                    string projectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
                    string safeScenarioTitle = _scenarioContext.ScenarioInfo.Title.Replace(" ", "_").Replace(":", "_").Replace("/", "_");
                    string fileName = $"{safeScenarioTitle}_Step{_stepCounter}_{DateTime.Now.Ticks}.png";
                    string filePath = Path.Combine(projectPath, "Reports", "Screenshots", fileName);
                    var screenshot = _driver.TakeScreenshot();
                    screenshot.SaveAsFile(filePath);
                    _scenario.AddScreenCaptureFromPath(filePath, $"Screenshot for Step {_stepCounter} Failure");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error capturing screenshot: {ex.Message}");
                }
            }

            _stepCounter++;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var status = _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError
                ? Status.Fail
                : Status.Pass;
            _scenario.Log(status, $"Scenario completed: {_scenarioContext.ScenarioInfo.Title} [{DateTime.Now:HH:mm:ss.fff}]");

            // Flush report for this scenario
            try
            {
                _extent.Flush();
                Console.WriteLine($"Report flushed for scenario: {_scenarioContext.ScenarioInfo.Title}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error flushing report for scenario '{_scenarioContext.ScenarioInfo.Title}': {ex.Message}");
            }

            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            // No flush here since it's done per scenario
            Console.WriteLine("All scenarios completed. Reports are saved in the Reports folder.");
        }
    }
}