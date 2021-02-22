Feature: Location
	Test the location functionality

Background: 
Given I get JWT authentication of User with following values
	| email             | password  |
	| karthik@email.com | haha123   |

@smoke
Scenario: Get the first location and verify its city
	Given I perform GET operation for "location/?id={id}"
	When I perform operation for location as "1" 
	Then I should see the "city" name as "chennai" in response

@smoke
Scenario: Create new Location and verify its Address details
	Given I perform POST operation to create new location with following details
		| city     | country | street    | flat no | pincode | type    |
		| Auckland | NZ      | 11th grey | 121A    | 0629    | primary |
	And I perform PUT operation to update the address details
		| city     | country | street        | flat no | pincode | type    |
		| Auckland | NZ      | 12th New Lynn | 121A    | 0629    | primary |
	Then I should see the "address" name as "12th New Lynn" for address
