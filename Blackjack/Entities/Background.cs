using HighRollerHeroes.Blackjack.Data;
using Microsoft.AspNetCore.Components;

namespace HighRollerHeroes.Blackjack.Entities
{
    public class Background : Entity
    {
        public enum BGType { HORIZONTAL, VERTICAL }
        private BGType currentType {  get; set; }

        private ElementReference graphic {  get; set; }
        private int spriteWidth { get; set; }
        private int spriteHeight { get; set; }

        private float xPos = 0f;
        private float yPos = 0f;

        public Background(BGType backgroundType)
        {
            this.currentType = backgroundType;
            LoadGraphics();
        }

        private void LoadGraphics()
        {
            switch (currentType)
            {
                case BGType.HORIZONTAL:

                    break;
                case BGType.VERTICAL:
                    graphic = Graphics.Backgrounds["Vertical"];
                    spriteWidth = (int)(896 * Settings.canvasScale);
                    spriteHeight = (int)(1576 * Settings.canvasScale);
                    Console.WriteLine("Loading vertical background.");
                    Console.WriteLine($"{graphic} : {spriteWidth} : {spriteHeight}");
                    break;
            }
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
