Feature: ProductList
	Our app should list products added through the product service api


Scenario: a Product listed in the home screen
	Given product name is 'Practical Remove' Created 
	When mobile app is open 
	Then home screen has at least a product
	And  home screen has product name is 'Practical Remove'