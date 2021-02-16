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