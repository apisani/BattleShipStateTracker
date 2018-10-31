using System;
using BattleShipGame;
using BattleShipGame.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BSST_Test
{
    [TestClass]
    public class BSST_Tests
    {
        [TestMethod]
        public void Create_BoardCell_Expect_CorrectCellCreation()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);

            Assert.IsNotNull(ship1Cell);
        }

        [TestMethod]
        public void Create_BoardCell_Expect_CorrectCoordinates()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);

            Assert.AreEqual(5, ship1Cell._row);
            Assert.AreEqual(6, ship1Cell._col);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectBoardCreation()
        {
            BattleShip game = new BattleShip(10);

            Assert.IsNotNull(game._board);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectRowNumber()
        {
            BattleShip game = new BattleShip(10);

            Assert.AreEqual(game._board.Count, 10);
        }

        [TestMethod]
        public void Create_Board_Expect_CorrectColNumber()
        {
            BattleShip game = new BattleShip(10);

            for (int i = 0; i != game._boardSize; ++i)
            {
                Assert.AreEqual(game._board[i].Count, 10);
            }
        }

        [TestMethod]
        public void Initialize_BoardWithWater_ExpectAllCellsToBeWater()
        {
            BattleShip game = new BattleShip(10);

            for (int i = 0; i != game._boardSize; ++i)
            {
                for (int j = 0; j != game._boardSize; ++j)
                {
                    Assert.AreEqual(game._board[i][j]._type, BoardCellType.Water);
                }
            }
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectCreation()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ship ship = new Ship(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            Assert.IsNotNull(ship);
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectHeadPosition()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ship ship = new Ship(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            Assert.AreEqual(ship._headPosition._row, 5);
            Assert.AreEqual(ship._headPosition._col, 6);
        }

        [TestMethod]
        public void Create_Ship_Expect_CorrectOrientation()
        {
            BoardCell ship1Cell = new BoardCell(5, 6);
            Ship ship = new Ship(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            Assert.AreEqual(ship._orientation, Orientation.Horiztontal);
        }


        [TestMethod]
        public void Add_VerticalShipToBoard_Expect_CorrectPlacement()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(5, 6);
            Ship ship = new Ship(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            Assert.AreEqual(ship._orientation, Orientation.Horiztontal);
        }

        [TestMethod]
        public void Add_HorizontalShipToBoard_Expect_AddedShip()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            Assert.AreEqual(game._ships.Count, 1);
        }

        [TestMethod]
        public void Add_VerticalShipToBoard_Expect_AddedShip()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            game.AddShip(ship1Cell, Orientation.Vertical, (int)ShipType.Destroyer);

            Assert.AreEqual(game._ships.Count, 1);
        }

        [TestMethod]
        public void Add_TooLongVerticalShipToBoard_Expect_OutOfBoardErrorMessage()
        {
            BattleShip game = new BattleShip(2);

            BoardCell ship1Cell = new BoardCell(1, 1);

            var returnValue = game.AddShip(ship1Cell, Orientation.Vertical, 15);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_TooLongHorizontalShipToBoard_OutOfBoardErrorMessage()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(5, 6);

            var returnValue = game.AddShip(ship1Cell, Orientation.Horiztontal, 15);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_SeveralValidShips_Expect_CorrectCountAddedShips()
        {
            BattleShip game = new BattleShip(100);

            BoardCell ship1Cell = new BoardCell(1, 1);
            BoardCell ship2Cell = new BoardCell(10, 10);
            BoardCell ship3Cell = new BoardCell(20, 20);
            BoardCell ship4Cell = new BoardCell(30, 30);

            game.AddShip(ship1Cell, Orientation.Vertical, (int)ShipType.Battleship);
            game.AddShip(ship2Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);
            game.AddShip(ship3Cell, Orientation.Vertical, (int)ShipType.Carrier);
            game.AddShip(ship4Cell, Orientation.Horiztontal, (int)ShipType.PatrolBoat);

            Assert.AreEqual(game._ships.Count, 4);
        }

        [TestMethod]
        public void Add_InvalidShips_Expect_InvalidCoordinatesError()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(15, 12);
            bool returnValue = game.AddShip(ship1Cell, Orientation.Vertical, (int)ShipType.Battleship);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Coordinates for the ship do not match board witdh and height");
        }

        [TestMethod]
        public void Add_ShipOnAlreadyPlacedShip_Expect_NoCellAvailableError()
        {
            BattleShip game = new BattleShip(10);

            BoardCell ship1Cell = new BoardCell(1, 1);
            BoardCell ship2Cell = new BoardCell(1, 1);

            game.AddShip(ship1Cell, Orientation.Vertical, (int)ShipType.Destroyer);
            bool returnValue = game.AddShip(ship2Cell, Orientation.Vertical, (int)ShipType.Destroyer);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Not all cells are available for this ship to be place at these coordinates");
        }

        [TestMethod]
        public void Add_Ship_WhenShipOverlay_Expect_NoCellAvailableError()
        {
            BattleShip game = new BattleShip(50);

            BoardCell ship1Cell = new BoardCell(20, 20);
            BoardCell ship2Cell = new BoardCell(18, 22);

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            bool returnValue = game.AddShip(ship2Cell, Orientation.Vertical, (int)ShipType.Carrier);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Not all cells are available for this ship to be place at these coordinates");
        }

        [TestMethod]
        public void Add_Ship_OnAlreadySunkShip_Expect_NoCellAvailableError()
        {
            BattleShip game = new BattleShip(50);

            BoardCell ship1Cell = new BoardCell(20, 20);
            BoardCell ship2Cell = new BoardCell(18, 22);

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            game.Attack(20, 20);
            game.Attack(20, 21);
            game.Attack(20, 22);
            game.Attack(20, 23);
            game.Attack(20, 24);
            //ship1 is now sunk
            Assert.AreEqual(game._ships[0].IsSunk(), true);
            bool returnValue = game.AddShip(ship2Cell, Orientation.Vertical, (int)ShipType.Carrier);

            Assert.AreEqual(returnValue, false);
            Assert.AreEqual(game.errorMessage, "Not all cells are available for this ship to be place at these coordinates");
        }

        [TestMethod]
        public void Attack_Ship_Expect_ShipToBeDamaged()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            int shipHealth = game._ships[0]._health;

            game.Attack(x, y);

            Assert.AreEqual(game._ships[0]._health, shipHealth - 1);
        }

        [TestMethod]
        public void Attack_Ship_Expect_CellUpdated()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            int shipHealth = game._ships[0]._health;

            game.Attack(x, y);

            Assert.AreEqual(game._board[x][y]._type, BoardCellType.Damaged);
        }

        [TestMethod]
        public void Attack_Ship_Expect_CellAroundDamageToBeUndamaged()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            int shipHealth = game._ships[0]._health;

            game.Attack(x, y);

            Assert.AreEqual(game._board[x][y - 1]._type, BoardCellType.Undamaged);
            Assert.AreEqual(game._board[x][y]._type, BoardCellType.Damaged);
            Assert.AreEqual(game._board[x][y + 1]._type, BoardCellType.Undamaged);
        }

        [TestMethod]
        public void Attack_Ship_Expect_ShipToBeDamagedButNotSunk()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 2;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            int shipHealth = game._ships[0]._health;

            game.Attack(x, y);

            Assert.AreEqual(game._ships[0].IsSunk(), false);
        }

        [TestMethod]
        public void Attack_AllShipCells_Expect_ShipToBeSunk()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);
            int shipHealth = game._ships[0]._health;

            // Carrier has 5 cells
            game.Attack(x, y);
            game.Attack(x, y + 1);
            game.Attack(x, y + 2);
            game.Attack(x, y + 3);
            game.Attack(x, y + 4);

            Assert.AreEqual(game._ships[0].IsSunk(), true);
        }

        [TestMethod]
        public void Attack_WaterCell_Expect_FailAttackAndWaterCell()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 8;
            int y = 9;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            game.Attack(x, y);

            Assert.AreEqual(game._board[x][y]._type, BoardCellType.Water);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyDamagedShipCellException))]
        public void Attack_AlreadyDamagedShipCell_Expect_FailAttackWith_AlreadyDamagedShipCellException()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            game.Attack(x, y); // Here we hit the ship for the first time, updating the cell to 'damaged'
            game.Attack(x, y); // As the ship is already damage at this coordinates, exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(ShipAlreadySunkException))]
        public void Attack_AlreadySunkShipCell_Expect_FailAttackWith_ShipAlreadySunkException()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 1;
            int y = 1;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Carrier);

            // Carrier has 5 cells
            game.Attack(x, y);
            game.Attack(x, y + 1);
            game.Attack(x, y + 2);
            game.Attack(x, y + 3);
            game.Attack(x, y + 4);
            // by this point the ship is sunk

            game.Attack(x, y + 4); // expect exception to be thrown here
        }

        [TestMethod]
        [ExpectedException(typeof(AttackCoordinatesInvalidException))]
        public void AttackWith_InvalidCoordinates_Expect_FailAttackWith_AttackCoordinatesInvalidException()
        {
            BattleShip game = new BattleShip(10);
            BoardCell ship1Cell = new BoardCell(1, 1);
            int x = 15;
            int y = 22;

            game.AddShip(ship1Cell, Orientation.Horiztontal, (int)ShipType.Destroyer);

            game.Attack(x, y);
        }

    }
}
