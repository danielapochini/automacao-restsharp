Feature: PostProfile
	Test POST operation using Restsharp
	 
Scenario: Verify Post operation for Profile
	Given I perform POST operation for "/posts/{profileId}/profile" with body
	| name | profile |
	| Sams | 2       |
	Then I should see the "name" name as "Sams"