using Godot;
using System;

namespace Collapse.Player
{
    public class Player : KinematicBody2D
    {
        //Stats
        [Export] public float MoveSpeed { get; private set; }
        [Export] public float JumpHeight { get; private set; }

        //Physics
        public Vector2 TickVelocity { get; private set; } = Vector2.Zero; //Velocity in the current tick
        public bool TickOnGround { get; private set; } = false; //Is the player on the ground in the current tick
        public bool TickMoving { get; private set; } = false; //Is the player moving in the current tick

        //Nodes
        private AnimatedSprite Sprite { get; set; }
        private Label TestLabel { get; set; }

        // - - - Godot Methods - - - \\
        public override void _Ready()
        {
            base._Ready();

            Sprite = GetNode<AnimatedSprite>("Sprite");
            TestLabel = GetNode<Label>("Camera/TestLabel");
        }

        public override void _Process(float delta)
        {
            base._Process(delta);

            ProcessAnimation();
        }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);

            ProcessMovement(delta);
        }

        // - - - Physics and Movement - - - \\
        private void ProcessMovement(float delta)
        {
            //Tick based stats
            Vector2 horVelocity = Vector2.Zero;
            Vector2 vertVelocity = new Vector2(0, TickVelocity.y);

            //Horizontal movement
            if (Input.IsActionPressed("movement_right")) { horVelocity.x += 1; }
            if (Input.IsActionPressed("movement_left")) { horVelocity.x -= 1; }

            //Vertical movement
            vertVelocity.y += GameGlobals.PLAYERGRAVITY * delta; //Apply gravity
            if (TickOnGround && Input.IsActionJustPressed("movement_jump")) { vertVelocity.y = -JumpHeight; }

            //If player is moving this tick, process movement
            if(horVelocity != Vector2.Zero || vertVelocity != Vector2.Zero)
            {
                TickMoving = true;
                //Normalize and combine velocities
                horVelocity = horVelocity.Normalized() * MoveSpeed;
                TickVelocity = horVelocity + vertVelocity;
                //Move and slide
                TickVelocity = MoveAndSlide(TickVelocity, Vector2.Up);
            }

            TestLabel.Text = TickOnGround + "\n" + TickVelocity.ToString();

            TickOnGround = IsOnFloor();
        }

        // - - - Animation - - - \\
        private void ProcessAnimation()
        {
            //Called every frame, determines what animation should be done depending on velocity and state
            //Cast these to ints so that decimal velocities dont effect animations
            int horMovement = (int)TickVelocity.x;
            int vertMovement = (int)TickVelocity.y;
            //Animations based on input
            if (Input.IsActionJustPressed("movement_jump")) { Sprite.Play("jump"); }
            //Animations based on movement
            if(vertMovement != 0)
            {
                if(vertMovement < 0) { Sprite.Animation = "jump"; }
                else { Sprite.Animation = "fall"; }
            }
            else if(horMovement != 0)
            {
                Sprite.Animation = "walk";
                //If player is moving left, flip sprite
                Sprite.FlipH = (horMovement < 0);
            }
            else
            {
                Sprite.Animation = "idle";
            }
        }
    }
}