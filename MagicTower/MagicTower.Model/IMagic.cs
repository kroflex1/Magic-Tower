using System;
using System.Net.Mime;

namespace MagicTower.Model
{
    public interface IMagic
    {
        double PosX { get; }
        double PosY { get; }
        int Speed { get; }
        int Damage { get; }
        (double X, double Y) DirectionVector { get; set; }
        
        void TakeStepInDirection();
        
    }
}