# ravendb-workshop
RavenDB Workshop for Zühlke Days 2015 which takes place on 25

# How to start the solution
* Open the solution
* Build the solution (Ctrl + Shift + B) to get the NuGet packages
* start the ravendb server located in packages/RavenDb.Server.%version%/RavenDb.Server.exe
* Now you are ready to do the exercises

#Use Cases / Exercises

* Store a list of players and load them by their id
* Store the player list and filter out Christiano Ronaldo
* Return a list of players filtered by first name and paged
* Return the player list of Dortmund
* Return the total number of players who ever played in each team
	* Return the total number of players who ever played in Real Madrid
* Return the number of active players per team (players that currently play in that team)
	* Return the total number of active players in Bayern München
* Return the average salary per team
	* filtered by active players only
	* only for FC Barcelona
* Return the average salary per nationality of player
* Return the average salary per country of team
* Return a list of players of AC Milan from the 1990 season

* Get a list of all players whose first name starts with "C"
* Get a list of all players whose last name contains the keyword "van"
