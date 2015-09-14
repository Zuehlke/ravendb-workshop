# ravendb-workshop
RavenDB Workshop for Zühlke Days 2015 (NoSQL Kickoff: Modelling and querying football data with RavenDB) which takes place on 24.9.2015 13:35 - 16:20.

CI-Build-Master: [![Build status](https://ci.appveyor.com/api/projects/status/sa8q7wv9b47ihf48)](https://ci.appveyor.com/project/tobiaszuercher/ravendb-workshop)

## Description of the Workshop
In this tutorial, attendees will learn the most important concepts of document databases by getting a deep introduction into RavenDB, a document store particularly popular with .NET. They will learn how to translate a complex domain model into a reasonable document representation, how to build indexes for these documents and how to query them efficiently, and how to write unit tests to keep things maintainable. Attendees should join if they are interested in learning how to solve non-trivial persistency challenges in a non-relational way.

## How to start the solution
* Open the solution
* Build the solution (Ctrl + Shift + B) to get the NuGet packages
* start the ravendb server located in `packages/RavenDb.Server.%version%/RavenDb.Server.exe`
* Now you are ready to do the exercises

##Use Cases / Exercises

The exercises are located in `NoSqlKickoff.Tests/Exercises`

1. Store and load players:
	* As a user I want save a list of the following players: "Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger".
	* As a user I want to be able to receive back the whole list of players ("Christiano Ronaldo", "Lionel Messi" and "Bastian Schweinsteiger") I stored before and load them by their id for verification. 
2. Filter and paged list of players:
	* As a user I need to get a list of 5 players at once (paged list).
	* As a user I need to get the second list of 5 players at once (paged list).
	* As a user I want to find the player "Christiano Ronaldo".
	* As a user I want to query for all players that start with a "C" in the firstname.
3. Team member list:
	* Return the player list of Dortmund
	* Return a list of players of AC Milan from the 1990 season
4. Reporting and statistics:
	* Return the total number of players who ever played in each team
		* Return the total number of players who ever played in Real Madrid
	* Return the number of active players per team (players that currently play in that team)
		* Return the total number of active players in Bayern München
	* Return the average salary per team
		* filtered by active players only
		* only for FC Barcelona
	* Return the average salary per nationality of player
	* Return the average salary per country of team
5. Full-text search:
	* As a user I want to query for all players whose last name contains the keyword "van".

##Contributors
- [mzoellner](https://github.com/mzoellner) (Michael Zöllner)
- [tobiaszuercher](https://github.com/tobiaszuercher) (Tobias Zürcher)
