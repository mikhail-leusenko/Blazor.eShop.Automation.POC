@StartingPort_1030
Feature: ViewProduct
	As an application user
	I want to be able to navigate to the view product page
	So that I can get more details regarding the desired product

@Scenario_1
Scenario: 1. User should be able to navigate to the View Product page
	Given user is on the "Products" page
	When user navigates to the View Product page of "Test product 1" product
	Then user can see the next Product data
		| Product Name   | Product Brand | Product Description                                | Product Price |
		| Test product 1 | QA & QC       | Some test description just to populate all fields. | 9.99          |