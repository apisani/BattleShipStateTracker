using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame
{
    public class AlreadyDamagedShipCellException : Exception
    {
        public AlreadyDamagedShipCellException()
        {
        }

        public AlreadyDamagedShipCellException(string message)
            : base(message)
        {
        }
    }

    public class ShipAlreadySunkException : Exception
    {
        public ShipAlreadySunkException()
        {
        }

        public ShipAlreadySunkException(string message)
            : base(message)
        {
        }
    }

    public class AttackCoordinatesInvalidException : Exception
    {
        public AttackCoordinatesInvalidException()
        {
        }

        public AttackCoordinatesInvalidException(string message)
            : base(message)
        {
        }
    }
}
