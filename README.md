# about the solution 
- Used both postman and swagger ui to test the product and reveiws APIs
- Used Studio 3T to view and edit database for testing 
- User Specflow (BDD) with C# and appium to automate app tests
- The tests automated are just an example and may not cover evey end to end scenario
- I logged only the bugs not every test scenario I executed, all bugs are logged here : https://github.com/Maiismaiel/adi-challenge/issues

# How to run test 
## Prerequisites
-  Appimum ( JDK, nodejs)
-  Dotnet SDK  3.1 | https://dotnet.microsoft.com/download/dotnet/3.1

## Run the test 
- Update the appsettings.json to add the required appium options and services urls | https://github.com/Maiismaiel/adi-challenge/blob/main/AdidasChallenge.EndToEndTests/appsettings.json
- Clone this repo to a local folder
- in the root folder of the local repo run this command **`dotnet test`** to execute tests 

## Notes
- Some scenarios will intentially fail because of missing app features, for example the search
