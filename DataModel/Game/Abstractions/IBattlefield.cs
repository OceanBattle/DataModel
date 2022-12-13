using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanBattle.DataModel.Game.Abstractions
{
    public interface IBattlefield
    {
        User? Owner { get; set; }
        IEnumerable<Ship> Ships { get; }
        IEnumerable<Ship> AnonimizedShips { get; }
        Cell[][] Grid { get; }
        Cell[][] AnonimizedGrid { get; }
        bool PlaceShip(int x, int y, Ship ship);
        bool Hit(int x, int y, Weapon weapon);
        IObservable<(int x, int y)> GotHit { get; } 
    }
}
