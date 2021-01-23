Feature: GetPosts
	Test GET Posts operation with RestSharp.NET
	 

Scenario: Verify author of the posts 1
	Given I perform GET operation for "posts/{postid}"
	When I perform operation for post "1" 
	Then i should see the "author" name as "Karthik KK"