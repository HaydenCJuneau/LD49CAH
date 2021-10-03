using Godot;
using System;

public class MainTitle : Control
{
    
    private TextureRect img;

    private Texture mainImg;
    private Texture playPress;
    private Texture playHover;
    private Texture quitPress;
    private Texture quitHover;
   
   
    public override void _Ready()
    {
        img = GetNode<TextureRect>("CanvasLayer/TextureRect");

        mainImg = (Texture)GD.Load("res://importedSprites/title/TitleScreenMain.jpg");
        playPress = (Texture)GD.Load("res://importedSprites/title/TitleScreenNewRunClicked.jpg");
        playHover = (Texture)GD.Load("res://importedSprites/title/TitleScreenNewRunHover.jpg");
        quitPress = (Texture)GD.Load("res://importedSprites/title/TitleScreenQuitClicked.jpg");
        quitHover = (Texture)GD.Load("res://importedSprites/title/TitleScreenQuitHover.jpg");
}
    public void _on_play_mouse_entered()
    {
        img.Texture = playHover;
    }
    public void _on_play_mouse_exited()
    {
        img.Texture = mainImg;
    }
    public void _on_play_button_down() //this is just for the texture, "pressed" is for actually finishing a click
    {
        img.Texture = playPress;
    }
    public void _on_quit_mouse_entered()
    {
        img.Texture = quitHover;
    }
    public void _on_quit_mouse_exited()
    {
        img.Texture = mainImg;
    }
    public void _on_quit_button_down()
    {
        img.Texture = quitPress;
    }




    public void _on_play_pressed()
    {
        //LOAD MAIN SCENE
    }
    public void _on_quit_pressed()
    {
        GetTree().Quit();
    }
}
