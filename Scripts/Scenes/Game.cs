using Godot;
using System;

namespace Collapse.Scenes
{
    //Script for the main game scene
    public class Game : Node2D
    {
        //Instances
        private PackedScene TowerScene { get; set; }

        //Godot methods
        public override void _Ready()
        {
            base._Ready();

            TowerScene = GD.Load<PackedScene>("res://Scenes/Test/platforms.tscn");
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            //Toggles fullscreen mode
            if (@event.IsActionPressed("debug_fullscreen")) { OS.WindowFullscreen = !OS.WindowFullscreen; }
        }

        //Signals
        public void KillzoneBodyEntered(Node body)
        {
            if(body is Player.Player)
            {

            }
        }
    }
}