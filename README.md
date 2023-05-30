# DNA Testing Kit Order Placement Service

Simple library developed in C# ASP.NET that handles the placement of orders for DNA testing kits. The library provides functionality to place an order with specific logic for calculating discounts and listing all customer orders

## Getting Started

To clone the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in your preferred IDE (Visual Studio is preferred).

## Running Tests

To run the unit tests for the library, follow these steps:

1. Ensure that you have the necessary NuGet packages restored. The project uses the NUnit testing framework for unit testing.
2. Open the test project in your IDE.
3. Build the test project to ensure that all dependencies are resolved.
4. Run the unit tests to verify the functionality of the library.

## Methods

The library provides the following methods:

- `PlaceOrder(int customerId, int desiredAmount, DateTime deliveryDate, DnaKit kit)`: Places an order for a DNA testing kit with the specified parameters, including the customer ID, desired amount of kits, delivery date, and the specific kit variant with varying base price.
- `ListAllOrders()`: Lists all orders.
- `CalculateDiscount(int desiredAmount)`: Calculates the discount percentage based on the desired amount of kits ordered.

## NuGet Packages Used

The following NuGet packages were used in this project:

- NUnit: A unit testing framework for .NET applications.
## Note 
- Formatting, Naming Convention, Choice of paradigms and nuget packages is subject to agreement between developers
- The library is developed with the rules and conventions used by the author. 
- If any changes are required to any of those conventions, it can be easily adjusted and it has no effect towards functionality.

Feel free to explore the codebase and suggest improvements in case it violates any requirements.

