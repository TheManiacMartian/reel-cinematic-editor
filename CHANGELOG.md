## [1.3.12] - 2025-07-23
### Fix reel stop
- Stops all reel node coroutines if the reel is stopped midway through.

## [1.3.11] - 2025-07-23
### Get current reel function
- Self explanatory, replaces "GetReelIsRunning"

## [1.3.10] - 2025-07-23
### Stop Reel Option
- Can now call "StopReel" to stop the reel midway through.

## [1.3.8] - 2025-07-23
### Build Error Fix
- Fixed some build errors with some imports

## [1.3.8] - 2025-07-22
### Event Portals Fix
- Fixed Event Portals so they get cleared and won't run after the graph is completed.

## [1.3.7] - 2025-07-22
### Event Portals
- Added Event Portals, these allow skipping to certain nodes if an event is triggered while the portal is active. After a portal is used it is then discarded and needs to be setup again (this also
allows for looping graphs).

## [1.3.6] - 2025-06-12
### Build error fix
- Fixed some build errors with some imports

## [1.3.5] - 2025-06-09
### Dialogue that doesn't need button
- Added options to the Dialogue Node 

## [1.3.4] - 2025-06-09
### Events
- Added "Clear Dialogue" options to "Trigger Event" and "Wait for Event" that gives the option to clear the dialogue view when it hits.

## [1.3.3] - 2025-06-09
### Events
- Added "Trigger Event" node which will trigger an event from within the reel graph.

## [1.3.2] - 2025-06-06
### Events
- Added "Wait for Event" which will essentially pause the graph until a certain event is triggered via script.

## [1.3.1] - 2025-06-06
### Hotfix
- Fixed error when nothing was subscribed to the OnReelStart and OnReelEnd events.

## [1.3.0] - 2025-06-06
### CSV file support
- Added "Get CSV file" and "Get Line from ID" that allow for dialogue lines to be gotten from CSV files, allowing for easy editing of entire scripts.
- Tested with CSV exports from google sheets, simply have the first column as a id for the line and the second column as the line, each row should be a seperate line.
- Different languages planned later down the line for the CSV file support.

## [1.2.1] - 2024-09-06
### Cameras and Subjects
- Updated README

## [1.2.0] - 2024-09-06
### Cameras and Subjects
- Added Reel Subjects that can be referenced in reel graphs. Just add the reel subject component to any game object and enter any tag.
- Added the Reel Camera that can focus on subjects through the reel graph.
- Added a preview window tool under Window > Reel > Camera Preview that will let you (kind of) preview the camera position and angle variables. You can copy and paste the information between the node and preview window.
- Added a "Is Synchronous" variable to Reel Nodes that will define whether or not the reel graph will wait for the node to complete before continuing.

## [1.1.1] - 2024-09-03
### Oops I forgot
- Added Reel Director Prefab that can be imported into any scene.

## [1.1.0] - 2024-09-03
### Extremely Basic but Working
- Added Dialogue Nodes that wait for input before moving next.
- Added Delay Nodes.
- Added Reel Director, now you can move through the nodes one by one in a coroutine.
- Added Reel Views, to display dialogue information. Any information can be added to dialogue information, as long as it fits in string form. In the example
it can be shown to store things like the color variable of a Speakers name. This can be used for more than dialogue as well, for example portraits and their various emotions.
- Various other smaller things, including a VERY simple example scene.

## [1.0.0] - 2024-09-02
### First Release
- There is literally nothing in here yet.
- No really, it's empty. Purely to test UPM releases here.
- Unity will not let me make this a preview package so technically this is 1.0.0 I guess.