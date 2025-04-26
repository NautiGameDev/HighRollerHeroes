/*
    This class is used for static graphics
 */


using HighRollerHeroes.Blackjack.Data;
using Microsoft.AspNetCore.Components;

namespace HighRollerHeroes.Blackjack.Entities
{
    public class Sprite : Entity
    {
        ElementReference graphic { get; set; }
        int spriteWidth { get; set; }
        int spriteHeight { get; set; }

        float xPos { get; set; }
        float yPos { get; set; }

        public Sprite(string graphicType, float xPos, float yPos, int width, int height, float scaleOffset)
        {
            this.xPos = xPos * Settings.canvasScale;
            this.yPos = yPos * Settings.canvasScale;
            graphic = Graphics.Interface[graphicType];
            this.spriteWidth = (int)(width * (Settings.canvasScale + scaleOffset));
            this.spriteHeight = (int)(height * (Settings.canvasScale + scaleOffset));
        }

        public async override Task Render()
        {
            await Game.context.DrawImageAsync(graphic, xPos, yPos, spriteWidth, spriteHeight);
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
