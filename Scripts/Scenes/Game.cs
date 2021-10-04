using Godot;
using System;

namespace Collapse.Scenes
{
    //Script for the main game scene
    public class Game : Node2D
    {
        //Instances
        private PackedScene TowerScene { get; set; }

        //Nodes
        private Position2D SpawnPoint { get; set; }

        //Godot methods
        public override void _Ready()
        {
            base._Ready();

            SpawnPoint = GetNode<Position2D>("Spawnpoint");

            TowerScene = GD.Load<PackedScene>("res://Scenes/Test/platforms.tscn");

            //Spawn towers
            Node2D t1 = TowerScene.Instance() as Node2D;
            Node2D t2 = TowerScene.Instance() as Node2D;
            Node2D t3 = TowerScene.Instance() as Node2D;

            t1.Position = SpawnPoint.Position;
            t2.Position = SpawnPoint.Position + new Vector2(100, 0);
            t3.Position = SpawnPoint.Position + new Vector2(200, 0);

            AddChild(t1);
            AddChild(t2);
            AddChild(t3);
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
                GetTree().ChangeScene("res://Scenes/MainTitle.tscn");
            }
        }
    }
}