using Godot;
using System;

public class player : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public int speed = 100;
    [Export] public float jumpPower = -35f;
    [Export] public float gravity = 60f;
    private float vertVelocity = 0f;
    private Vector2 velocity = new Vector2();
    private AnimatedSprite _animatedSprite;
    private bool movingSideways = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        velocity = new Vector2();
       
        movingSideways = false;
        if (Input.IsActionPressed("right"))
        {
            velocity.x += 1f;
            movingSideways = true;
            _animatedSprite.FlipH = false;
            
        }
        if (Input.IsActionPressed("left"))
        {
            velocity.x -= 1f;
            movingSideways = true;
            _animatedSprite.FlipH = true;
        }
        velocity = velocity.Normalized() * speed + new Vector2(0f, vertVelocity);
        velocity = MoveAndSlide(velocity, Vector2.Up);
        if (IsOnFloor())
        {
            vertVelocity = 1f;
            if (movingSideways == true)// && _animatedSprite.Animation != "walk")
            {
                _animatedSprite.Play("walk");
            }
            else
            {
                _animatedSprite.Play("idle");
            }
            if (Input.IsActionPressed("up")) //jump!
            {
                vertVelocity = jumpPower;
            }
        }
        else
        {
            vertVelocity += gravity * delta;
            if (vertVelocity > 0)
                _animatedSprite.Play("fall");
            else
                _animatedSprite.Play("jump");

        }
       
        
        
    }
}
