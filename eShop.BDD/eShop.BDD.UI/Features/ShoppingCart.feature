@StartingPort_1029
Feature: ShoppingCart
	As an application user
	I want to have a possibility to add products to Cart
	So that I will be able to manage products that I want to purchase

@Scenario_1
Scenario: 1. User should be able to navigate to cart
	Given user is on the "Products" page
	When user clicks on "Cart" navigation link
	Then the "Shopping Cart" page is open

@Service_SetCart
@Scenario_2
Scenario: 2. User should be able to add product to Cart from the products page
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	Then the "Test product 1" is displayed in cart

@Service_SetCart
@Scenario_3
Scenario: 3. User should be able to navigate to Products page from the Cart with products by clicking on Back to Products button
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user clicks on "Back to Products" button
	Then the "Products" page is open

@Scenario_4
Scenario: 4. User should be able to navigate to Products page from the empty Cart by clicking on Back to Products link
	Given user is on the "Products" page
	When user clicks on "Cart" navigation link
	And user clicks on "Back to Products" link
	Then the "Products" page is open

@Service_SetCart
@Scenario_5
Scenario: 5. User should be able to delete product from cart by clicking on Delete button
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user deletes "Test product 1" product from the cart
	Then the "Test product 1" product is not displayed in cart

@Service_SetCart
@Scenario_6
Scenario: 6. User should be able to change the product quantity
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user sets the "Test product 1" quantity as "2"
	Then the "Test product 1" price is 9.99
	And total price value is 19.98

@Service_SetCart
@Scenario_7
Scenario: 7. User should be able to delete product by setting it quantity as 0
	Given user is on the "Products" page
	When user adds "Test product 1" to the cart
	And user clicks on "Cart" navigation link
	And user sets the "Test product 1" quantity as "0"
	Then the "Test product 1" product is not displayed in cart

@Service_SetCart
@Scenario_8
Scenario: 8. User should be able to add to the cart multiple products with different quantities
	Given user is on the "Products" page
	When user adds the next products to the cart
		| Product Name   | Product Quantity |
		| Test product 1 | 2                |
		| Test 2         | 3                |
		| Test product 3 | 5                |
	And user clicks on "Cart" navigation link
	Then the next products are displayed in cart
		| Product Name   | Product Price | Product Quantity |
		| Test product 1 | 9.99          | 2                |
		| Test 2         | 19.99         | 3                |
		| Test product 3 | 29.99         | 5                |
	And the total items count is 3
	And total price is equal to the sum of all product prices multuiplied on it quantities and is 229.90