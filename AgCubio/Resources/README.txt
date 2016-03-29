Kabir Sandhu and Isabelle Chalhoub
November 17, 2015

Gameplay:
	When the game launches a panel appears where the player can enter their name and the name of the server. The server is set to localhost by default but can be changed.
	The play button starts the game. If there is a problem connecting to the server, a message is displayed below the play button notifying the player. The game won't start
	until connection to the server is successful. Once the game is connected to the server, the panel disappears and the cubes are drawn on the window. The players name is displayed on the
	center of their cube if it is big enough to fit it. The player can move their cube using the mouse and split using the spacebar. The FPS, Amount of food in the world, mass of the player, 
	and width of the player are displayed at the top of the window. The total mass and width are displayed when the player has split. As the player's cube grows, the player's view of the world expands. 
	When the player is eaten by another cube, a message box appears indicating that the player has died and their final mass and width are displayed. After hitting ok on the message box, the game closes.

Scaling:
	The player's view is dependent on the size of their cube. The smaller the cube, the less of the world they can see. We determined a scaling factor by using a ratio between the world size
	and the size of the player. We found 1000 / (playerWidth * 2) to be a good scaling factor (assuming the size of the world is 1000x1000). To keep the player cube in the center of the screen 
	we subtracted the x and y of every cube by the player cube's x and y and added half the world width to each of them. The scaling and centering work even when the cube is split. After scaling the 
	player cube does not always go all the way to the mouse but it seems to be a server problem.

GUI Drawing:
	We made it gray in order so see the food better. Names are always centered on the cubes and if the name is too long, the name will appear once the cube is big enough.

Notes:
	We realize that the food is really small compared to the cube but the food size and initial player cube size were exactly what the server sent us. When we tried to scale up the food, we ran into mouse problems
	so we decided to leave the food small and the players big and plan to fix it in the server instead.