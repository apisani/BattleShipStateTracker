using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BattleShipGame.Models
{
    public enum ShipType
    {
        Carrier = 5,
        Battleship = 4,
        Destroyer = 3,
        Submarine = 2,
        PatrolBoat = 1
    };

    public enum Orientation
    {
        Vertical = 0,
        Horiztontal = 1
    };

    //Interface initially implemented to respect the S.O.L.I.D principles
    //For future implentation where different ship classes could be required (i.e attack differently based on ship type etc...)
    interface IShip
    {
        bool IsSunk();
        bool DamagedAt();

    }

    public class Ship : IShip
    {
        public int _health;
        public int _length;
        public BoardCell _headPosition;
        //public ShipType _shipType;
        public Orientation _orientation;

        // This dictionnary was initially created based on the fact that ships that can be added are limited to this list
        // But base on requirements, user can add ship of 1xn dimensation - so this has been commented out.

        //private static readonly Dictionary<ShipType, int> shipLengths =
        //    new Dictionary<ShipType, int>()
        //    {
        //        {ShipType.Carrier, 5},
        //        {ShipType.Battleship, 4},
        //        {ShipType.Destroyer, 3},
        //        {ShipType.Submarine, 3},
        //        {ShipType.PatrolBoat, 2}
        //    };

        public Ship(BoardCell cell, Orientation orientation, int length)
        {
            _headPosition = cell;
            _orientation = orientation;
            //_shipType = type;
            //_health = shipLengths[_shipType];
            _health = length;
            _length = length;
        }

        public bool DamagedAt()
        {
            _health--;
            return IsSunk();
        }


        public bool IsSunk()
        {
            return _health == 0 ? true : false;
        }
    }
}
