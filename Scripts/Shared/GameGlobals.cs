using System;
using Godot;

namespace Collapse
{
    public static class GameGlobals
    {
        //Consts will never change through the course of the game
        public const float PLAYERGRAVITY = 100;

        //Static properties can change but can be accessed anywhere
        public static int GAMEFPS = 120;
    }
}