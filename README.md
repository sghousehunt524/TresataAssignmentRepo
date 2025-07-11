# SauceDemo Automation Test Suite

This repository contains an automated UI test suite for the [SauceDemo web application](https://www.saucedemo.com/) using Behavior-Driven Development (BDD) principles. Built with **SpecFlow**, **Selenium WebDriver**, and **ExtentReports 5.0.2** in .NET, the suite follows the Page Object Model (POM) for maintainability. It validates key features: user login/logout, adding products to the cart, completing the checkout process, and handling negative login scenarios.

## Table of Contents
1. [How to Execute the Test Suite](#how-to-execute-the-test-suite)
2. [Browser Setup Instructions](#browser-setup-instructions)
3. [Test Design Document](#test-design-document)
4. [Bug Scenario](#bug-scenario)

## How to Execute the Test Suite

### Prerequisites
- **.NET SDK**: 9.0 
- **IDE**: Visual Studio 2022 or Visual Studio Code with C# extensions
- **Google Chrome**: Latest stable version
- **NuGet Packages**:
  - SpecFlow (Version 3.9.74)
  - SpecFlow.MSTest (Version 3.9.74)
  - Selenium.WebDriver (Version 4.34.0)
  - Selenium.Support (Version 4.34.0)
  - Selenium.WebDriver.ChromeDriver (Version 138.0.7204.9400)
  - ExtentReports (Version 5.0.2)

### Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd SauceDemoAutomation

### Command to Restore Nuget Packages   
dotnet restore

### Install required NuGet packages (if not already included):
dotnet add package SpecFlow (Version 3.9.74)
dotnet add package SpecFlow.MSTest (Version 3.9.74)
dotnet add package Selenium.WebDriver (Version 4.34.0)
dotnet add package Selenium.Support (Version 4.34.0)
dotnet add package Selenium.WebDriver.ChromeDriver (Version 138.0.7204.9400)
dotnet add package ExtentReports (version 5.0.2)

### Execution CommandsBuild the project:
dotnet build

### Run the tests:
dotnet test

### Debugging
Open the project in Visual Studio.
Use the Test Explorer to run or debug tests.
Set breakpoints in StepDefinitions/SauceDemoSteps.cs or Hooks/Hooks.cs for detailed inspection.

### Browser Setup Instructions
Primary Browser: Google Chrome (latest stable version).
ChromeDriver:
Managed automatically by the Selenium.WebDriver.ChromeDriver NuGet package, ensuring compatibility with the installed Chrome version.
No manual download required unless testing specific Chrome versions.

### Configuration:
Tests maximize the browser window and set a 5-second implicit wait (Hooks.cs).
Explicit waits (WebDriverWait, 10-second timeout) are used in Page Object classes to handle dynamic elements.

### Cross-Browser Testing (Future Scope):
Extend support for Firefox (GeckoDriver), Edge (EdgeDriver), or Safari (SafariDriver).
Use cloud platforms like BrowserStack or Sauce Labs:Obtain account credentials.
Update Hooks.cs to initialize WebDriver with cloud service capabilities.
Configure desired browsers/versions.


### Test Design Document
### Objective
Validate the core functionalities of the SauceDemo web application using automated UI testing with BDD principles, ensuring reliability, usability, and correctness of user flows.
### Scope
In-Scope:User login/logout
Adding products to the cart
Completing the checkout process
Handling negative login scenarios

### Out-of-Scope:
Performance testing
Security testing
Mobile responsiveness testing

### Testing Types
Functional Testing: Validates login, logout, cart, and checkout functionalities.
Negative Testing: Tests error handling for invalid inputs (e.g., incorrect credentials, locked-out users).
UI Testing: Verifies UI elements, navigation, and visibility post-action.
Regression Testing: Ensures existing functionalities remain intact after updates.
Data-Driven Testing: Uses Scenario Outlines for multiple negative login scenarios.

### Testing Methodologies
Behavior-Driven Development (BDD): Gherkin scenarios in Features/SauceDemo.feature align with business requirements.
Page Object Model (POM): Encapsulates UI interactions in page classes (Pages/*.cs) for maintainability.
Data-Driven Testing: Scenario Outlines test multiple input combinations efficiently.
Chained Test Execution: Scenarios reflect user journeys (e.g., login → cart → checkout).
Explicit Waits: WebDriverWait ensures robust handling of dynamic elements.

### Tools
SpecFlow: BDD framework for Gherkin and step definitions.
Selenium WebDriver: Browser automation.
NUnit: Test runner for executing tests.
ChromeDriver: Chrome browser automation.
ExtentReports 5.0.2: HTML reporting with scenario-specific files and screenshots.
.NET SDK 6.0+: Development environment.

### Test Scenarios
Location: Features/SauceDemo.feature
Scenarios:Successful user login
Failed login with invalid credentials
Adding a product to the cart
Completing the checkout process
User logout
Negative login scenarios (empty fields, locked-out user, etc.)

### ReportingFormat:
 HTML reports named after scenario titles (e.g., Reports/Successful_user_login.html).
Content: Feature/scenario nodes, step logs with timestamps, pass/fail statuses, screenshots for failures, and a timeline view.
Location: Reports folder in the project root.

Risks and Mitigation
Risk: Flaky tests due to dynamic elements.Mitigation: Use explicit waits and robust locators (e.g., IDs).
Risk: UI changes breaking tests.Mitigation: Maintain POM and update locators as needed.
Risk: Report generation failures.Mitigation: Use absolute paths, ensure write permissions, log errors in Hooks.cs.
