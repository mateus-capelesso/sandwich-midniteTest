# sandwich-midniteTest
Test developed for Midnite Studios

The main idea behind this test was to create a copy of Sandwich, a mobile hyper-casual game. First things first, on Unity, I'm only using one scene, with all happening on it, from level loading, level generation, and gameplay.

The UI is simple, just some buttons to start a new level or reset it, nothing complicated.

Some objects on the scene are essential for the game, like the sound manager object, the object handler, and the input manager, called SwipeDetection. They all send and receive data through events, and each one handles a specific part of the game. The ObjectHandler manages all the prefabs on the game. SwipeDetection detects the swipe direction, and SoundManager listens to which sound should be played.

We can create each level in three different ways. The first one is creating a new Level from a scriptable object, and on Nodes, add every content we want on it. It is worth mentioning that there must be two "bread" ingredients side by side. The second manner to create a level is by marking AutoGenerated. You need to inform a seed, the number of ingredients, including the two bread ingredients, and the other prefabs that you want on the level. The level manager can also create new levels automatically on the editor, with random nodes. The level manager generates random information to instantiate the level and stores it in the Resources folder.

The level manager also handles the actual level of the user. I decided to use PlayerPrefs to save this data on the device because it is easy to implement.

The algorithm that generates new levels using the information on the levels scriptable object pick a random position on the 4x4 grid and adds its surrounding nodes to a list. By surrounding nodes, I mean top, right, bottom, and left nodes. The first two items that are instantiated are the pieces of bread, then on a for loop, the algorithm picks a random position of these surroundings and place a new ingredient on it. 
Every time a new position is picked, the algorithm removes it from the list. Then new surrounding nodes are added to the list based on the picked positions. You can found about it on LevelGenerator.cs.

The GridManager contains all the rules of the game. This manager instantiates the nodes from the LevelManager and handles the movement of each node. 
On each node of the grid, there is information about it and also its surroundings. For instance, if some node is moved into another node, it is set as not interactable. Its movements now are handled by the node that it moved into. With this logic, it is easier to move and rotate the game objects.
When a swipe is detected, the grid manager receives the direction and the selected node. Then, it analyses if there are any other objects in that direction. If there is an object, the selected node is moved to the new position. 
If there are two or more stacks of ingredients, and none of them has any available objects around it, the game is over. If there is only one stack of objects, there must be one bread that wasn't moved, and the last-placed node should be bread. If the game meets these conditions, we have a win.

I decided to work with DOTween on this test since there is a lot of object translation. The tween gives me the ability to animate the movement through code and chain animations with little effort.

If you need any more information about the game, feel free to message me. 

Update from 04/02:
I refactored some code to create the optional task. Some scripts now inherit from others. For instance, the grid manager has two derived classes, PowerTwo and Sandwich. The main code is on GridManager, and specific functions are on the derived classes. The same happens with the LevelManager, which now has two derived classes.
Unfortunately, I wasn't able to conclude the optional task. I tried to work out a way to do it with my current game logic made for the sandwich game. But I couldn't find a way to do it. My last attempt was to use raycast to detect which type of node is under or above some other node, but it wasn't working.

It took me around 12 hours to create the sandwich clone. I worked about 5 hours on the PowerTwo game. I won't be able to work more on the test for the next couple of days, that's why I decided to send it, even with the optional task not finished.
