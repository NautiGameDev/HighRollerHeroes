/*
    Root class of the blackjack game. Update method is called every frame from the blazor page.
 */
using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using HighRollerHeroes.Blackjack.Menus;
using Microsoft.JSInterop;

namespace HighRollerHeroes.Blackjack
{
    public class Game
    {
        public static BECanvasComponent canvas { get; private set; }
        public static Canvas2DContext context { get; private set; }
        public static IJSObjectReference JSModule { get; private set; }
        
        Menu currentMenu {  get; set; }

        public Game(BECanvasComponent canvas, Canvas2DContext context, IJSObjectReference JSModule)
        {
            Game.canvas = canvas;
            Game.context = context;
            Game.JSModule = JSModule;

            
            currentMenu = new Play();
        }

        public void RenderAndUpdate(float deltaTime)
        {
            if (currentMenu != null)
            { 
                currentMenu.Update(deltaTime);
                currentMenu.Render();
            }
        }
    }
}
