﻿Feature: FileUpload
	Test file upload feature

Background: 
Given I get JWT authentication of User with following values
	| email             | password  |
	| karthik@email.com | haha123   |

@smoke
Scenario: Test file upload functionality
	Given I perform POST operation for "/uploads"
	Then I see the file is being uploaded with response as OK