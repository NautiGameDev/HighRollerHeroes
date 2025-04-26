
using Blazor.Extensions.Canvas.Canvas2D;
using HighRollerHeroes.Blackjack.Data;

namespace HighRollerHeroes.Blackjack.Entities
{
    public class TextElement : Entity
    {
        private string message { get; set; }
        private string fontSize { get; set; }
        private string fontFamily { get; set; } = "Serif";
        private TextAlign textalign { get; set; } = TextAlign.Center;

        float xPos { get; set; }
        float yPos { get; set; }

        public TextElement(string message, int fontSize, float xPos, float yPos)
        {
            this.message = message;
            this.fontSize = (fontSize * Settings.canvasScale).ToString() + "px";
            this.xPos = xPos * Settings.canvasScale;
            this.yPos = yPos * Settings.canvasScale;
        }

        public void UpdateMessage(string newMessage)
        {
            message = newMessage;
        }

        public async override Task Render()
        {
            await Game.context.SetFontAsync(fontSize + " " + fontFamily);
            await Game.context.SetFillStyleAsync("#ffffff");
            await Game.context.SetTextAlignAsync(textalign);
            await Game.context.FillTextAsync(message, xPos, yPos);
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
