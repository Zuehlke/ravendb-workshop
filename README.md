# ravendb-workshop
RavenDB Workshop for Zühlke Days 2015 (NoSQL Kickoff: Modelling and querying football data with RavenDB) which takes place on 24.9.2015 13:35 - 16:20.

## Description of the Workshop
In this tutorial, attendees will learn the most important concepts of document databases by getting a deep introduction into RavenDB, a document store particularly popular with .NET. They will learn how to translate a complex domain model into a reasonable document representation, how to build indexes for these documents and how to query them efficiently, and how to write unit tests to keep things maintainable. Attendees should join if they are interested in learning how to solve non-trivial persistency challenges in a non-relational way.

## How to start the solution
* Open the solution
* Build the solution (Ctrl + Shift + B) to get the NuGet packages
* start the ravendb server located in packages/RavenDb.Server.%version%/RavenDb.Server.exe
* Now you are ready to do the exercises

##Use Cases / Exercises

1. Overview List of all Players: RavenDB Basics
	* Exercise 01: As a user I want store a list of the following players: "Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger".
	* Exercise 02: As a user I want to be able to receive back the whole list of players ("Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger") I stored before. 
	* Exercise 03: As a user I want to get a list of 5 players at once (paged list).
	* Exercise 04: As a user I want to get the second list of 5 players at once (paged list).
3. Searching and Finding Specific Players: Indexes and Querying Unrelated Documents
	* Exercise 05: As a user I want to find the player "Christiano Ronaldo".
	* Exercise 06: As a user I want to query for all players that start with a "C" in the first name
	* Exercise 07: As a user I want to find the player "Christiano Ronaldo" by querying the full name
	* Exercise 08: As a user I want to find all players that have the Nationality "Brazil"
	* Exercise 09: As a user I want to find players that contain the name fragment "van", "di" or "de"
	* Exercise 10: As a user I want to find players whose first name ends with "an"
4. Team Profile and Player Profile: Modelling and Querying Relationships
	* Exercise 11a, 11b, 11c: As a user I want to know what players had been employed by Dortmund for the season "2013-2014". 
	* Exercise 12a, 12b, 12c: As a user I want to find all employments of "Gonzalo Higuaín"
5. Statistics and Reporting: Aggregations (Map/Reduce)
	* Exercise 13: As a user I want to have a list of all teams with the total number of players that ever played in each team
	* Exercise 14: As a user I want to have the total number of players that ever played in "Real Madrid"
	* Exercise 15: As a user I want to have a list of all teams with their average salary
	* Exercise 16: As a user I want to have a list of all nationalities with their average salary
	* Exercise 17: As a user I want to have a list of all countries with their average salary

6. Advanced RavenDB Features
	* Highlighting
	* Suggestions
	* Facets
	* More like this
	* Replication/Failover
	* Migrations
	* Notifications/Subscriptions
	* Bundles

##Contributors
- [mzoellner](https://github.com/mzoellner) (Michael Zöllner)
- [tobiaszuercher](https://github.com/tobiaszuercher) (Tobias Zürcher)
