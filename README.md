# AFIRegistration
## Animal Friends Insurance Registration API
Animal Friends Insurance (AFI) requires a new REST API to allow customers to register for the AFI
customer portal. _(Note: this product is purely fictious and this exercise is only going to be used as
part of a technical test.)_

This new API should contain one endpoint. This endpoint should validate the customers information
and return a unique online Customer ID. The registration data required is:

1. Policy holder’s first name
1. Policy holder’s surname
1. Policy Reference number
1. Either the policy holders DOB OR the policy holder’s email
#### Technical Requirements
The REST API should preferably be written using .NET core (although .NET Framework is acceptable)
and for ease of Development, the API does not need to have any authentication.

The Customer ID should be unique integer.

The submitted registration data should be stored, along with the customer ID to form a customer
record. This can be in any DB of your choosing, MS SQL Server, SQLite, flat file etc.

Please can you publish your solution to a public Git Repo with regular commits.
#### Validation Requirement
In reality this registration data would be validated against a back-office policy system. But for the
ease of this exercise the following validation should take place:
1. Policy holder’s first name and surname are both required and should be between 3 and 50
chars.
1. Policy Reference number is required and should match the following format XX-999999.
Where XX are any capitalised alpha character followed by a hyphen and 6 numbers.
1. If supplied the policy holders DOB should mean the customer is at least 18 years old at the
point of registering.
1. If supplied the policy holders email address should contain a string of at least 4 alpha
numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric
chars. The email address should end in either ‘.com’ or ‘.co.uk’.
#### Assessment Criteria
We are not looking for a perfectly polished solution. Ideally the API should be functional. We will be
focussing on coding standards and styles chosen. You are free to use any third-party libraries or
services as you feel appropriate. You are also free to look up information online as required.

There is no time limit associated with this task, but as a guide, we would anticipate this to take
approximately 2-3 hours. Your application to the role could still be successful even with an
incomplete solution.

Following this technical test, you will be expected to discuss your solution with developers at AFI.
___
### My Solution
This is an ASP.NET Core Web API project using .NET 5.

First the Customer model was added along with a simple Entity Framework DB Context which persists data to an in memory store.  Then a CustomersController was scaffolded using these objects which gave me a full impementation of basic asctions for the Customer entity.

After removing the endpoints that were not needed for this exercise and modifying the POST endpoint to return Customer Id, I implemented model validation using [Fluent Validation](https://fluentvalidation.net/) and added corresponding unit tests.

I would normally add tests for controllers but in this case the single endpoint is as simple as you could imagine so have omitted them.  I would also normally add a repository pattern for data access but again omitted this for simplicity.
