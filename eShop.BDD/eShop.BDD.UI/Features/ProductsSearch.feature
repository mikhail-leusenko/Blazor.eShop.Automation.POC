@StartingPort_1028
Feature: ProductsSearch
	As an application user
	I want to be able to search the products
	So that I can save my time while selecting the desired product

@Scenario_1
Scenario: 1. User should be able to search the product by name
	Given user is on the "Products" page
	When user searches "Unique name product"
	Then the single product with name "Unique name product" is displayed

@Scenario_2
Scenario: 2. User should be able to search multiple products by partial name
	Given user is on the "Products" page
	When user searches "Test"
	Then each displayed product contains "Test" in its name

@Scenario_3
Scenario: 3. User should be able to search product by brand
	Given user is on the "Products" page
	When user searches "Unique brand"
	Then the single product with brand name "Unique brand" is displayed

@Scenario_4
Scenario: 4. User should be able to search multiple products by partial brand name
	Given user is on the "Products" page
	When user searches "QA"
	Then each displayed product contains "QA" in its brand name

@Scenario_5
Scenario: 5. No products should be displayed if search does not match any of them
	Given user is on the "Products" page
	When user searches "unexisting name"
	Then no products are displayed
	And "No products found, try search again" notification message is displayed

@Scenario_6
Scenario: 6. When the search was initiated, the Reset Search button should be displayed
	Given user is on the "Products" page
	When user searches "Test"
	Then the "Reset Search" button displayed

@Scenario_7
Scenario: 7. When the search was initiated, the Reset Search button should not be displayed
	Given user is on the "Products" page
	Then the "Reset Search" button is not displayed