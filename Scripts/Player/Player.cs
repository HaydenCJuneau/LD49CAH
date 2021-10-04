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
        public bool TickOnWall { get; private set; } = false; //Is the player on the wall in the current tick
        public bool TickMoving { get; private set; } = false; //Is the player moving in the current tick
        private Vector2 snap = new Vector2(0, 32);

        //Nodes
        private AnimatedSprite Sprite { get; set; }
        private Camera2D Camera { get; set; }
        private Label TestLabel { get; set; }

        //Visual
        private Vector2 CameraZoomMin { get; set; } = new Vector2(0.2f, 0.2f);
        private Vector2 CameraZoomMax { get; set; } = new Vector2(0.9f, 0.9f);

        // - - - Godot Methods - - - \\
        public override void _Ready()
        {
            base._Ready();

            Sprite = GetNode<AnimatedSprite>("Sprite");
            Camera = GetParent().GetNode<Camera2D>("Camera");
            TestLabel = GetParent().GetNode<Label>("Camera/TestLabel");
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

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);

            if (@event.IsActionPressed("camera_zoomin")) { ZoomCamera(true); }
            if (@event.IsActionPressed("camera_zoomout")) { ZoomCamera(false); }
        }

        // - - - Physics and Movement - - - \\
        private void ProcessMovement(float delta)
        {
            //Tick based stats
           // Vector2 horVelocity = Vector2.Zero;
            Vector2 vertVelocity = new Vector2(0, TickVelocity.y);
            Vector2 horVelocity = new Vector2(TickVelocity.x,0);
            //Horizontal movement
            if (Input.IsActionPressed("movement_right")) { horVelocity.x += 7*delta; }
            if (Input.IsActionPressed("movement_left")) { horVelocity.x -= 7*delta; }
            if (!(Input.IsActionPressed("movement_right") || Input.IsActionPressed("movement_left")))
            {
                horVelocity.x = 0f;
            }
            if (horVelocity.x > 12f)
            {
                horVelocity.x = 12f;
            }
            if (horVelocity.x < -12f)
            {
                horVelocity.x = -12f;
            }
            //Vertical movement
            vertVelocity.y += GameGlobals.PLAYERGRAVITY * delta; //Apply gravity

            //Floor jump
            if (TickOnGround && Input.IsActionJustPressed("movement_jump")) { 
                vertVelocity.y = -JumpHeight;
                snap = new Vector2(0, 0); //unsnap from floor
            }

            //Wall jump
            /*if(TickOnWall && Input.IsActionJustPressed("movement_jump")) {
                Vector2 hitpos = GetSlideCollision(0).Position; 
                if (hitpos.x > Position.x)
                {
                    horVelocity.x = -12;
                }
                else
                {
                    horVelocity.x = 12;
                } 
                vertVelocity.y = -12;

            } */

            //If player is moving this tick, process movement
            if(horVelocity != Vector2.Zero || vertVelocity != Vector2.Zero)
            {
                TickMoving = true;
                //Normalize and combine velocities
                horVelocity = horVelocity.Normalized() * MoveSpeed;
                TickVelocity = horVelocity + vertVelocity;
                //Move and slide
                TickVelocity = MoveAndSlideWithSnap(TickVelocity, snap, Vector2.Up);

                if (IsOnFloor()) { snap = new Vector2(0, 32); }
                else { snap = new Vector2(0, 0); }
            }

            TestLabel.Text = TickOnGround + "\n" + TickVelocity.ToString();

            TickOnGround = IsOnFloor();
            TickOnWall = IsOnWall();
           
        }

        // - - - Animation & Visual - - - \\
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
            }
            else
            {
                Sprite.Animation = "idle";
            }

            //If player is moving left, flip sprite
            Sprite.FlipH = (horMovement < 0);
        }

        //Zooms the camera in or out, true for in false for out
        private void ZoomCamera(bool zoomIn)
        {
            Vector2 currentZoom = Camera.Zoom;
            //Set the step for zoom
            if (zoomIn) { currentZoom -= new Vector2(.05f, .05f); }
            else { currentZoom += new Vector2(.05f, .05f); }
            //If zoom is higher than max, set to max. if lower than min, set to min
            if(currentZoom > CameraZoomMax) { Camera.Zoom = CameraZoomMax; }
            else if(currentZoom < CameraZoomMin) { Camera.Zoom = CameraZoomMin; }
            else { Camera.Zoom = currentZoom; }
        }
    }
}