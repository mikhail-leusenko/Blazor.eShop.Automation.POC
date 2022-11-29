The original Blazor.eShop repository is located by https://github.com/brovig/Blazor_eshop.git.
It was choosen because of its simplicity and suitability for automated tests with parallel running.

In scope of the changes, some minor fixes and features were added to the original code:
1. Navigation to the Products page was added to the next pages:
  - View Product 
  - Shopping Cart 
  - Place Order 
  - Confirm Order
2. Minimal product count in cart was set to '1' to avoid creation of order with empty cart
3. Reset Search button was added to allow potential users to cleanup search state with minimal actions
4. Changed storage dictionaries access modifiers from private to public in OrderRepository.cs and ProductRepository.cs for the testing purposes
5. Added id's to web elements for the testing purposes
6. Updated styles to fix trivial allignment issues of product info on Shopping Cart page
7. Update Startup.cs to avoid duplication of services (for the test purposes DI)

In scope of the automated test framework creation the next stuff was implemented:
1. Test Core was created with all of the currently required shared implementations:
- implementation of the WebDriver initialization on the feature level
- implementation of the base classes for step definitions and pages
- implementation of the POM-like structure to store the web elements in appropriate classes-representations of the application pages with page storage
- implementation of the custom attributes with appropriate helper classes to grant ease-of-access to the attribute-related instances
- implementation of the Web Host to initialize the application instance for the test run purpose
- implementation of the test data classes like test repositories and constants
2. UI test project was created with the next structure:
- Features, which were created with SpecFlow and Gherking syntax according to BDD approach
- Steps, which contains the feature specific parameterized step definitions
- Hooks, which contains the Before/After Feature and Before/After Scenario test fixtures:
 1. init/dispose WebDriver instance
 2. create ServiceCollection for host with test services (DI)
 3. start the WebHost for application on separate port (depending on Feature tag)
 4. bunch of helping methods and scenario specific cleanups
