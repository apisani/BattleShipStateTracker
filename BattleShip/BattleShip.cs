using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using BattleShipGame.Models;

namespace BattleShipGame
{
    public class BattleShip
    {
        public List<List<BoardCell>> _board { get; set; }
        public List<Ship> _ships { get; set; }
        public int _boardSize { get; set; }
        public string errorMessage { get; set; }
        public string statusUpdate { get; set; }
        public bool gameOver { get; set; }

        public BattleShip(int boardSize)
        {
            _ships = new List<Ship>();
            SetupBoard(boardSize);
        }

        // Board size is customisable, it will always be a square grid.
        private void SetupBoard(int boardSize)
        {
            _boardSize = boardSize;
            _board = new List<List<BoardCell>>();

            for (int i = 0; i != _boardSize; ++i)
            {
                _board.Add(new List<BoardCell>());

                for (int j = 0; j != _boardSize; ++j)
                {
                    _board[i].Add(new BoardCell(i, j));
                }
            }

            InitializeBoardCells();
        }

        //Empty board only has water cells
        private void InitializeBoardCells()
        {
            for (int i = 0; i != _boardSize; ++i)
            {
                for (int j = 0; j != _boardSize; ++j)
                {
                    _board[i][j].ResetCell(BoardCellType.Water);
                }
            }
        }

        public bool AddShip(BoardCell cell, Orientation orientation, int length)
        {
            Ship ship = new Ship(cell, orientation, length);

            bool isAdded = false;
            int remainingLength = ship._health;


            if (ship._orientation == Orientation.Horiztontal)
                isAdded = AddHorizontaly(ship);
            else
                isAdded = AddVerticaly(ship);

            if (isAdded)
            {
                errorMessage = string.Empty;
                _ships.Add(ship);
            }

            return isAdded;
        }

        private bool IsPlacementPossible(Ship ship)
        {
            try
            {
                int row = ship._headPosition._row;
                int col = ship._headPosition._col;
                int lenght = ship._length;

                if (ship._orientation == Orientation.Horiztontal)
                {
                    for (int currentCol = col; lenght != 0; currentCol++)
                    {
                        if (BoardCellFree(row, currentCol) == false)
                        {
                            if (string.IsNullOrEmpty(errorMessage))
                                errorMessage = "Not all cells are available for this ship to be place at these coordinates";
                            return false;
                        }
                        --lenght;
                    }
                    return true;
                }
                else
                {
                    for (int currentRow = row; lenght != 0; currentRow++)
                    {
                        if (BoardCellFree(currentRow, col) == false)
                        {
                            if (string.IsNullOrEmpty(errorMessage))
                                errorMessage = "Not all cells are available for this ship to be place at these coordinates";
                            return false;
                        }
                        --lenght;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                errorMessage = "There was an internal issue and the ship cannot be added. Please refer to execption: " + e.Message;
                return false;
            }
        }

        private bool BoardCellFree(int row, int col)
        {
          if (checkCoordinatesValidity(row, col))
            {
                return (_board[row][col]._shipIndex == -1) ? true : false;
            }
            else
            {
                errorMessage = "Coordinates for the ship do not match board witdh and height";
                return false;
            }
        }

        private bool checkCoordinatesValidity(int row, int col)
        {
            if (row < _boardSize && col < _boardSize && row >= 0 && col >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AddHorizontaly(Ship ship)
        {
            int row = ship._headPosition._row;
            int col = ship._headPosition._col;
            int lenght = ship._length;

            if (IsPlacementPossible(ship))
            {
                for (int currentCol = col; lenght != 0; ++currentCol)
                {
                    _board[row][currentCol]._type = BoardCellType.Undamaged;
                    _board[row][currentCol]._shipIndex = _ships.Count;
                    --lenght;
                }
                return true;
            }

            return false;
        }

        private bool AddVerticaly(Ship ship)
        {
            int row = ship._headPosition._row;
            int col = ship._headPosition._col;
            int lenght = ship._length;

            if (IsPlacementPossible(ship))
            {
                for (int currentRow = row; lenght != 0; ++currentRow)
                {
                    _board[currentRow][col]._type = BoardCellType.Undamaged;
                    _board[currentRow][col]._shipIndex = _ships.Count;
                    --lenght;
                }
                return true;
            }

            return false;
        }

        public BoardCellType Attack(int x, int y)
        {
            if (checkCoordinatesValidity(x, y))
            {
                switch (_board[x][y]._type)
                {
                    case BoardCellType.Water:
                        return BoardCellType.Water;

                    case BoardCellType.Undamaged:
                        var attackedShip = _ships[_board[x][y]._shipIndex];
                        attackedShip._health = attackedShip._health - 1;

                        if (attackedShip._health > 0)
                        {
                            _board[x][y]._type = BoardCellType.Damaged;
                            return BoardCellType.Damaged;
                        }
                        else
                        {
                            _board[x][y]._type = BoardCellType.Sunk;
                            gameOver = CheckGameOver();
                            return BoardCellType.Sunk;
                        }

                    case BoardCellType.Damaged:
                        statusUpdate = "Ship is already damaged at these coordinates";
                        throw new AlreadyDamagedShipCellException(statusUpdate);

                    case BoardCellType.Sunk:
                        statusUpdate = "Ship at these coordinates is already sunk";
                        throw new ShipAlreadySunkException(statusUpdate);

                    default:
                        throw new Exception("There was an error with the attack");
                }
            }
            else
            {
                throw new AttackCoordinatesInvalidException();
            }
        }

        private bool CheckGameOver()
        {
            int shipsAlive = 0;

            foreach (Ship ship in _ships)
            {
                if (!ship.IsSunk())
                    shipsAlive++;
            }

            return shipsAlive == 0 ? true : false;
        }


        public string GetShipsList()
        {
            string shipList = string.Empty;
            for (int index = 0; index != _ships.Count; index++)
            {
                string status = string.Empty;
                if (_ships[index]._health == _ships[index]._length)
                    status = "Unadamaged";
                else if (_ships[index]._health == 0)
                    status = "Sunk";
                else
                    status = "Damaged";

                var orientation = _ships[index]._orientation == Orientation.Horiztontal ? "Horizontal" : "Vertical";

                shipList += String.Format("Ship #{0} - Head Position: [{1}][{2}] - Health:{3}/{4} - Orientation: {5} - Status: {6}\n",
                    index, _ships[index]._headPosition._row, _ships[index]._headPosition._col, _ships[index]._health,
                    _ships[index]._length, orientation, status);
            }

            return shipList;
        }

        public string GetStatusString()
        {
            string status;
            int shipsAlive = 0;
            int shipsSunk = 0;

            foreach (Ship ship in _ships)
            {
                if (!ship.IsSunk())
                    shipsAlive++;
                else
                    shipsSunk++;
            }

            if (shipsAlive == 0 && shipsSunk == 0)
            {
                status = "You currently have no ship placed on the board. Please add at least one ship to continue.";
            }
            else if (shipsAlive == 0)
            {
                status = "All of your ships sunk. GAME OVER...";
            }
            else
            {
                status = String.Format("You currently have {0} ship(s) afloat, and {1} ship(s) sunk\n", shipsAlive,
                    shipsSunk);
                status += "\nShips List:\n" + GetShipsList();
            }

            return status;
        }
    }
}
