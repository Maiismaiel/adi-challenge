Feature: ReviewPosting
	our app could be used to post and show product reviews


Scenario: a product review is added from the app
	Given product name is 'product' Created
	And a review text is 'my review' and rate is '3'
	When navigate to product review
	Then then proudct is shown

#Examples:
#    | product_name | review_text | rating |
#    |    "product" |  "my review" |    3 |
#	 |    "product2" |  "my review2" |    5 |