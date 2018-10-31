﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame.Models
{
    //This class represent one cell on the board, with all the necessary information for tracking
    public enum BoardCellType
    {
        Water,
        Undamaged,
        Damaged,
        Sunk
    }

    public class BoardCell
    {
        public int _row { get; set; }
        public int _col { get; set; }
        public int _shipIndex { get; set; }
        public BoardCellType _type { get; set; }

        public BoardCell(int row, int col)
        {
            this._row = row;
            this._col = col;
        }

        public void ResetCell(BoardCellType type)
        {
            this._type = type;
            this._shipIndex = -1;
        }
    }
}
