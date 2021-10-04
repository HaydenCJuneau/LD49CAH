using Godot;
using System;

namespace Collapse.Scenes
{
    public class MainTitle : Control
    {
        //The menu screen is done this way because it is not composed of elements, instead just individual pictures

        //Fields for all textures and main Rect
        private TextureRect ImageRect { get; set; }
        //Textures
        private Texture ImgDefault { get; set; }
        private Texture ImgPlayPress { get; set; }
        private Texture ImgPlayHover { get; set; }
        private Texture ImgQuitPress { get; set; }
        private Texture ImgQuitHover { get; set; }

        // - - - Godot Methods
        public override void _Ready()
        {
            //Rect
            ImageRect = GetNode<TextureRect>("CanvasLayer/Texture");
            //Load textures
            ImgDefault = GD.Load<Texture>("res://importedSprites/title/TitleScreenMain.jpg");
            ImgPlayPress = GD.Load<Texture>("res://importedSprites/title/TitleScreenNewRunClicked.jpg");
            ImgPlayHover = GD.Load<Texture>("res://importedSprites/title/TitleScreenNewRunHover.jpg");
            ImgQuitPress = GD.Load<Texture>("res://importedSprites/title/TitleScreenQuitClicked.jpg");
            ImgQuitHover = GD.Load<Texture>("res://importedSprites/title/TitleScreenQuitHover.jpg");
        }

        //Signals for mouse entering events
        public void _on_play_mouse_entered()
        {
            ImageRect.Texture = ImgPlayHover;
        }
        public void _on_play_mouse_exited()
        {
            ImageRect.Texture = ImgDefault;
        }
        public void _on_play_button_down() //this is just for the texture, "pressed" is for actually finishing a click
        {
            ImageRect.Texture = ImgPlayPress;
        }
        public void _on_quit_mouse_entered()
        {
            ImageRect.Texture = ImgQuitHover;
        }
        public void _on_quit_mouse_exited()
        {
            ImageRect.Texture = ImgDefault;
        }
        public void _on_quit_button_down()
        {
            ImageRect.Texture = ImgQuitPress;
        }




        public void _on_play_pressed()
        {
            GetTree().ChangeScene("res://Scenes/Game.tscn");
        }
        public void _on_quit_pressed()
        {
            GetTree().Quit();
        }
    }
}