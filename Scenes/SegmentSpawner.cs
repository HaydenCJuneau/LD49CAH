using Godot;
using System;

public class SegmentSpawner : Position2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private KinematicBody2D plr;
    private Node2D tile;
    private Node2D tileClone;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        plr = GetNode<KinematicBody2D>("/root/Main/Player");
        tile = GetNode<TileMap>("/root/Main/platforms2");
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 pos = GlobalPosition;
        //GD.Print((plr.Position - pos).Length());
        if ((plr.Position - pos).Length() < 400) //close to this position
        {
            tileClone = ;
            
        }
    }
}
