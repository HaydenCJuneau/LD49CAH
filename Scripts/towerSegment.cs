using Godot;
using System;

public class towerSegment : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public float speed { get; private set; } = .5f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RandomNumberGenerator rand = new RandomNumberGenerator();
        rand.Randomize();
        speed = rand.RandfRange(.1f, .27f);
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
