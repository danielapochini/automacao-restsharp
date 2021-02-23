using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using RestsharpSpecflow.Base;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace RestsharpSpecflow.Hooks
{ 
    [Binding]
    public class TestInitialize
    { 
        private Settings _settings;

        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentReports extent; 
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public TestInitialize(Settings settings, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _settings = settings;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext; 
        }


        [BeforeScenario]
        public void TestSetup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _settings.BaseUrl = new Uri(config["URL"].ToString());
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string file = "ExtentReport.html";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
             

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
            } 
        }

        [BeforeScenario]
        public void Initialize()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

        }
    }
}
