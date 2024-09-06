## [1.2.1] - 2024-09-06
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