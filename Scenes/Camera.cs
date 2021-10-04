using Godot;
using System;

public class Camera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private KinematicBody2D plr;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        plr = (KinematicBody2D)GetParent().FindNode("Player");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        if (plr.Position.y < Position.y)
        {
            Position = new Vector2(Position.x, plr.Position.y);
        }
    }
}
