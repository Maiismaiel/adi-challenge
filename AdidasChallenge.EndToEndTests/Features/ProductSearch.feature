Feature: ProductSearch
	In our app we can search product


Scenario: Search product by name of existing product
	Given app open on home screen
	And search phrase is 'product'
	When search phrase entered in the search box
	Then the matching products are showen in the result