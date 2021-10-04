using Godot;
using System;

public class SegmentSpawner : Position2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private KinematicBody2D plr;
    private PackedScene tile;
    private Node2D tileClone;
    private Node2D twr;
    private bool completed = false;
    private TileMap chosenPlatforms;
    // Called when the node enters the scene tree for the first time.
    
    public override void _Ready()
    {
        plr = GetNode<KinematicBody2D>("/root/Main/Player");
        tile = GD.Load<PackedScene>("res://Scenes/platforms.tscn");
        twr = (Node2D)GetParent().GetParent();
    }

    public void SpawnSegment()
    {
        tileClone = (Node2D)tile.Instance();
        twr.AddChild(tileClone);
        tileClone.GlobalPosition = GlobalPosition;
        chosenPlatforms = (TileMap)tileClone.GetChild(0);
        chosenPlatforms.Show();
        chosenPlatforms.CollisionLayer = 1;
    }
  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 pos = GlobalPosition;
        //GD.Print((plr.Position - pos).Length());
        if ((plr.Position - pos).Length() < 400 && completed == false) //close to this position
        {
            completed = true;
            SpawnSegment();
            
        }
    }
}
