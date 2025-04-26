
using HighRollerHeroes.Blackjack.Data;
using Microsoft.AspNetCore.Components;

namespace HighRollerHeroes.Blackjack.Entities
{
    public class Button : Entity
    {
        ElementReference graphic {  get; set; }
        ElementReference graphicHovered { get; set; }
        ElementReference graphicDisabled { get; set; }
        int spriteWidth { get; set; }
        int spriteHeight { get; set; }

        float xPos { get; set; }
        float yPos { get; set; }

        string buttonType { get; set; }
        bool isHovered { get; set; } = false;
        public bool isDisabled { get; set; } = false;

        public Button(string buttonType, float xPos, float yPos)
        {
            this.buttonType = buttonType;
            this.xPos = xPos * Settings.canvasScale;
            this.yPos = yPos * Settings.canvasScale;
            LoadGraphic(buttonType);
        }

        private void LoadGraphic(string buttonType)
        {
            graphic = Graphics.Buttons[buttonType];
            graphicHovered = Graphics.Buttons[buttonType + " Hover"];
            if (Graphics.Buttons.ContainsKey(buttonType + " Disabled"))
            {
                graphicDisabled = Graphics.Buttons[buttonType + " Disabled"];
            }
            
            spriteWidth = (int)(261 * 0.87 * Settings.canvasScale);
            spriteHeight = (int)(127 * 0.87 * Settings.canvasScale);
        }

        public async override Task Render()
        {
            if (isDisabled)
            {
                await Game.context.DrawImageAsync(graphicDisabled, xPos, yPos, spriteWidth, spriteHeight);
                return;
            }

            if (!isHovered)
            {
                await Game.context.DrawImageAsync(graphic, xPos, yPos, spriteWidth, spriteHeight);
            }
            else
            {
                await Game.context.DrawImageAsync(graphicHovered, xPos, yPos, spriteWidth, spriteHeight);
            }
        }

        public override void Update(float deltaTime)
        {
            isHovered = CheckIfHovered();
        }

        private bool CheckIfHovered()
        {
            if (PlayerInput.mouseXPos > xPos &&
                PlayerInput.mouseXPos < (xPos + spriteWidth) &&
                PlayerInput.mouseYPos > yPos &&
                PlayerInput.mouseYPos < (yPos + spriteHeight) &&
                !isDisabled)
            {
                CheckIfClicked();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckIfClicked()
        {
            if (PlayerInput.mouseXClicked > xPos &&
                PlayerInput.mouseXClicked < (xPos + spriteWidth) &&
                PlayerInput.mouseYClicked > yPos &&
                PlayerInput.mouseYClicked < (yPos + spriteHeight) &&
                !isDisabled)
            {
                PlayerInput.AddActionToQueue(buttonType);
                PlayerInput.ClearMouseClicked();
            }
        }

       
    }
}
