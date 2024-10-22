Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>

Project for doing a Tatedrez game following the rules as described in https://github.com/kk-homa/tatedrez-game

How to Play:
- During the first Piece Placement, you are supposed to click the chess piece and then click on the cell you want it placed, wherever you click, you move the chess piece, if you click another chess piece, you select that one instead.
- During the "Dynamic mode", you select a piece, you will only be able to move it to valid movement cells, to unselect a piece, click again on it.
- The Selected chess piece is highlighted in red


The game has the following patterns:

- Service Locator : see GameService.cs
- MVC : Cell has UI_Cell, CellModel and Cell separating the logic, view and data
- State : the game follows a chain of States for setting and injecting the services, see Game.cs and IGameState

The game also uses pooling to avoid deleting and reloading continuosly objects.

The game gets started through the class SceneInjector that creates the instance of Game, game follows a series of states that construct, inject and set the different parts of the game

The game knows about the board state through the service BoardState, which is arguably the most important class of the game, the turn cycle is handled by TurnSystem.


Things left to do due to time constraints:

- Unit Tests: due to time constraints I could not implement the Unit Tests, some rework would have to be done on the injection of the UI to make it possible
- Improve the Input handling to make it work as a mobile app through drag and drop
- Create Model for Chess Piece
- Prettier UI/effects
