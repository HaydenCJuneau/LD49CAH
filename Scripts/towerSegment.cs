using Godot;
using System;

public class towerSegment : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public float speed { get; private set; } = .5f;
    [Export] public Color clr { get; private set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        GlobalPosition += new Vector2(0f, speed);
    }
}
