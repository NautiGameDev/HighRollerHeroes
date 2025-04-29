/*
    Root class of the blackjack game. Update method is called every frame from the blazor page.
 */


/*
 Dev Notes:
    Hands need to be refactored into a separate class with all functionality of drawing cards and calculating values based on that class.
    This way, when the player splits the hand, a List<Hand> variable can be used seemlessly with checks of the size of this list.
    Might possibly need to split the player turn state and payout state into separate states for normal play and split hand play
        Will observe after refactoring out hand functionality

    Hand class check list:
        Draw card
        Send card to be rendered
        Contain bet size for that hand
        Calculate value
        Resolve ability cards
 */

using Blazor.Extensions;
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
