
using HighRollerHeroes.Blackjack.Data;
using Microsoft.AspNetCore.Components;

namespace HighRollerHeroes.Blackjack.Entities
{
    public class Card : Entity
    {
        public enum DeckType { SUN, MOON }
        public DeckType currentDeck = DeckType.SUN;

        ElementReference cardGraphic { get; set; }
        ElementReference cardBack { get; set; }
        int spriteWidth { get; set; }
        int spriteHeight { get; set; }

        public float xPos { get; set; }
        public float yPos { get; set; }
        //Unscaled position is used for accurate reference points when drawing new cards
        //This is needed because scaling is done within the card class. Using xPos, yPos to place new cards creates flawed offset
        public float unscaledXPos { get; set; }
        public float unscaledYPos { get; set; }

        public string cardValue { get; set; }
        public bool isFlipped { get; set; }

        public Card(DeckType deckType, float xPos, float yPos, string cardValue, bool isFlipped)
        {
            this.currentDeck = deckType;
            this.unscaledXPos = xPos;
            this.unscaledYPos = yPos;
            this.xPos = xPos * Settings.canvasScale;
            this.yPos = yPos * Settings.canvasScale;
            this.cardValue = cardValue;
            this.isFlipped = isFlipped;

            LoadGraphic();
        }

        private void LoadGraphic()
        {
            if (currentDeck == DeckType.SUN)
            {
                cardGraphic = Graphics.sunCards[cardValue];
                cardBack = Graphics.sunCards["Back"];
            }

            else if (currentDeck == DeckType.MOON)
            {
                cardGraphic = Graphics.moonCards[cardValue];
                cardBack = Graphics.moonCards["Back"];
            }

            spriteWidth = (int)(328 * Settings.canvasScale);
            spriteHeight = (int)(461 * Settings.canvasScale);
        }

        public void MoveCard(float x, float y)
        {
            unscaledXPos = x;
            xPos = x * Settings.canvasScale;
            unscaledYPos = y;
            yPos = y * Settings.canvasScale;
        }

        public void MoveCardByOffset(float x, float y)
        {
            unscaledXPos += x;
            xPos = unscaledXPos * Settings.canvasScale;
            unscaledYPos += y;
            yPos = unscaledYPos * Settings.canvasScale;
        }

        public override async Task Render()
        {
            if (!isFlipped)
            {
                await Game.context.DrawImageAsync(cardGraphic, xPos, yPos, spriteWidth, spriteHeight);
            }
            else
            {
                await Game.context.DrawImageAsync(cardBack, xPos, yPos, spriteWidth, spriteHeight);
            }

        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
