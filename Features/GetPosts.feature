﻿Feature: GetPosts
	Test GET Posts operation with RestSharp.NET
	 

Background: 
Given I get JWT authentication of User with following values
	| email             | password  |
	| karthik@email.com | haha123   |

Scenario: Verify author of the posts 1
	Given I perform GET operation for "posts/{postid}"
	When I perform operation for post "1" 
	Then I should see the "author" name as "Karthik KK"

Scenario: Verify author of the posts 2
	Given I perform GET operation for "posts/{postid}"
	When I perform operation for post "2" 
	Then I should see the "author" name as "Karthik KK"

Scenario: Verify author of the posts 6
	Given I perform GET operation for "posts/{postid}"
	When I perform operation for post "6" 
	Then I should see the "author" name as "ExecuteAutomation"