# FallGuyMatchHistory
Simple Fall Guy match history analysis from logs.  Based on https://github.com/ShootMe/FallGuysStats

Trying to whip up something to do a best effort tracking of who gets eliminated and when in each Fall Guys game in which the user participates.

![image](https://user-images.githubusercontent.com/620343/160304651-9535cdb3-baa7-4f32-96a6-1deecabb803d.png)

## Where is the important stuff?

### Projects

* TeamTrackMatchHistory - A GUI interface for looking at match history.
* ConsoleMatchHistory - A Console interface for looking at match history.
* FallGuyMatchHistory.Engine - The log parsing and rank determining business logic.
* FallGuyMatchHistory.Contracts - The contract classes representing the output of and basic settings used by the Engine.

### Important Code Locations

* FallGuyMatchHistory.Engine\MatchLogParser.cs
	- Examines log rows and triggers transitions or updates to data in the LogParsingContext.
	
* FallGuyMatchHistory.Engine\LogParsingContext.cs
	- Updated by MatchLogParser as it parses rows.
	- Outputs its data by way of exposed events that the LogFileWatcher subscribes to.
	- Takes care of cleanup tasks like turning the end of round/show data into real ranks.
	
* FallGuyMatchHistory.Engine\LogFileWatcher.cs
	- Stolen and slightly modified from https://github.com/ShootMe/FallGuysStats for our purposes.
	- Starts up threads that parse through your existing log data, and then wait for new log data to come in so that it can continue to parse that.
	- Creates the instance of LogParsingContext that ends up having round/show data modified and fires events
	- Exposes its own events that basically just wrap LogParsingContext's events in a way that other systems like the GUI and Console apps can register with.

### Gotchas

* I was going to make a way of merging "participant" data with active game data, but ran out of time.  I've got a baby to watch!  Someone else can do this if you care.

* I have not tested this thoroughly.  Only with a couple of games in my log as of 2022-03-27

* Unlike FallGuysStats, this does *NOT* attempt to persist data.  If you close and reopen it, it will re-scan existing logs, but any older logs will have been lost forever.  It's an exercise for the reader to add persistence, as I don't care about it for my use case.
