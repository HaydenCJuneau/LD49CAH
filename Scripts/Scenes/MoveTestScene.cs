using Godot;
using System;

namespace Collapse.Scenes
{
    public class MoveTestScene : Node2D
    {

        //Godot methods
        public override void _Ready()
        {
            base._Ready();

        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            //Toggles fullscreen mode
            if (@event.IsActionPressed("debug_fullscreen")) { OS.WindowFullscreen = !OS.WindowFullscreen; }
        }
    }
}