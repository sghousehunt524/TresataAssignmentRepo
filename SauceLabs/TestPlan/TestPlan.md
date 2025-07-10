# Test Plan for SauceDemo Web Application

## Objective
To validate the core functionalities of the SauceDemo web application (https://www.saucedemo.com/) through automated UI testing using BDD principles, ensuring reliability, usability, and correctness of user flows.

## Testing Types
- **Functional Testing**: Validate login, logout, cart, and checkout functionalities.
- **Negative Testing**: Test error handling for invalid inputs (e.g., incorrect login credentials).
- **UI Testing**: Verify UI elements, navigation, and visibility after actions.
- **Regression Testing**: Ensure existing functionalities remain unaffected by future changes.

## Tools Used
- **SpecFlow**: BDD framework for writing Gherkin scenarios and step definitions.
- **Selenium WebDriver**: For browser automation.
- **MSTest**: Test runner for executing and validating test cases.
- **ChromeDriver**: For running tests on the Chrome browser.
- **ExtentReports**: For generating HTML reports with screenshots for failed test cases.

## Testing Methodologies
- **Behavior-Driven Development (BDD)**: Tests are written in Gherkin syntax to align with business requirements.
- **Page Object Model (POM)**: Organizes code to improve maintainability and reduce duplication.
- **Data-Driven Testing**: Uses Scenario Outline for negative login scenarios with multiple test data sets.
- **Continuous Integration**: Tests can be integrated into CI/CD pipelines (e.g., Jenkins, GitHub Actions).

## Browser Compatibility
- **Primary Browser**: Google Chrome (latest stable version).
- **Future Scope**: Extend to Firefox, Edge, and Safari using Selenium's cross-browser capabilities.
- **Configuration**: Tests use ChromeDriver, with plans to implement BrowserStack or SauceLabs for multi-browser testing.

## Scope
- **In-Scope**:
  - User login/logout
  - Adding products to the cart
  - Completing the checkout process
  - Handling negative login scenarios
- **Out-of-Scope**:
  - Performance testing
  - Security testing
  - Mobile responsiveness testing

## Test Environment
- **URL**: https://www.saucedemo.com/
- **Browser**: Chrome (via ChromeDriver)
- **Operating System**: Windows
- **Dependencies**: .NET SDK 9.0, ChromeDriver

## Risks and Mitigation
- **Risk**: Flaky tests due to dynamic elements or network delays.
  - **Mitigation**: Use explicit waits and retry mechanisms.
- **Risk**: Browser compatibility issues.
  - **Mitigation**: Plan for cross-browser testing in future iterations.
- **Risk**: Changes in UI breaking locators.
  - **Mitigation**: Use robust locators (e.g., IDs, CSS selectors) and maintain POM.

## Reporting
- HTML reports generated using ExtentReports.
- Screenshots captured for failed test cases.
- Reports include test status, execution time, step numbers, and failure details.