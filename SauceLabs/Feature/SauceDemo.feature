Feature: SauceDemo E-Commerce Functionality
  As a user, I want to log in, add products to my cart, complete checkout, and handle login errors effectively.

  Background:
    Given I am on the SauceDemo login page

  Scenario: Successful user login
    When I enter valid credentials with username "standard_user" and password "secret_sauce"
    And I click the login button
    Then I should be redirected to the products page
    And I should see the product inventory

  Scenario: Failed login with invalid credentials
    When I enter invalid credentials with username "invalid_user" and password "wrong_password"
    And I click the login button
    Then I should see an error message "Epic sadface: Username and password do not match any user in this service"

  Scenario: Adding a product to the cart
    Given I am logged in as "standard_user" with password "secret_sauce"
    When I add the product "Sauce Labs Backpack" to the cart
    Then I should see the cart badge updated to "1"
    And I should see the product "Sauce Labs Backpack" in the cart

  Scenario: Completing the checkout process
    Given I am logged in as "standard_user" with password "secret_sauce"
    When I add the product "Sauce Labs Backpack" to the cart
    And I navigate to the cart
    And I proceed to checkout
    And I enter checkout information with first name "John", last name "Doe", and zip code "12345"
    And I complete the checkout process
    Then I should see the order confirmation message "Thank you for your order!"

  Scenario: User logout
    Given I am logged in as "standard_user" with password "secret_sauce"
    When I click the menu button
    And I click the logout link
    Then I should be redirected to the login page

  Scenario Outline: Negative login scenarios
    When I enter credentials with username "<username>" and password "<password>"
    And I click the login button
    Then I should see an error message "<error_message>"

    Examples:
      | username        | password     | error_message                                                             |
      |                 | secret_sauce | Epic sadface: Username is required                                        |
      | standard_user   |              | Epic sadface: Password is required                                        |
      | locked_out_user | secret_sauce | Epic sadface: Sorry, this user has been locked out.                       |
      | invalid_user    | invalid_pass | Epic sadface: Username and password do not match any user in this service |

  #Bug Investigation Scenario
  Scenario: Cart badge does not update after adding a product
    Given I am logged in as "standard_user" with password "secret_sauce"
    When I add the product "Sauce Labs Backpack" to the cart
    Then I should see the cart badge updated to "1"