@StartingPort_1026
Feature: PlaceOrder
	As an application user
	I want to be able to place the order
	So that I can purchase and recieve ordered products according to specified information

@Service_SetCart
@Scenario_1
Scenario: 1. User should be able to navigate to the Place Order page
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user clicks on "Place your order" button
	Then the "Place Order" page is open

@Service_SetCart
@Scenario_2
Scenario: 2. User is able to place order after populatin all required information
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user clicks on "Place your order" button
	And user fills the personal information to place order
		| Name      | Address      | City      | State/Province | Country      |
		| Test name | Test address | Test city | Test province  | Test country |
	And user clicks on "Place Order" button to submit the order
	Then the "Order Confirmation" page is open
	And page contains next product information
		| Product Name   | Quantity | Price |
		| Test product 1 | 1        | 9.99  |
	And page contains the next customer information
		| Name      | Address      | City      | State/Province | Country      |
		| Test name | Test address | Test city | Test province  | Test country |

@Service_SetCart
@Scenario_3
Scenario: 3. User is not able to place order if any of required fields of cusomer information was not populated
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user clicks on "Place your order" button
	And user clicks on "Place Order" button
	Then the validation messages are displayed for the next fields
		| Field Name     |
		| Name           |
		| Address        |
		| City           |
		| State/Province |
		| Country        |