using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class BetState : State
    {
        Sprite placeBetBox {  get; set; }
        Button decreaseBetButton { get; set; }
        Button increaseBetButton { get; set; }
        Button dealButton { get; set; }

        TextElement newBet { get; set; }

        public BetState(Play playMenu) : base(playMenu)
        {
            
        }

        public override void Enter()
        {
            playMenu.ToggleActionBar(true); //Turn off action bar

            placeBetBox = new Sprite("Place Bet", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
            playMenu.AddEntityToEntities(placeBetBox);

            decreaseBetButton = new Button("Decrease", 200, 840);
            playMenu.AddEntityToEntities(decreaseBetButton);

            increaseBetButton = new Button("Increase", 450, 840);
            playMenu.AddEntityToEntities(increaseBetButton);

            dealButton = new Button("Deal", (Settings.canvasVerticalWidth / 2) - (261 / 2), 1050);
            playMenu.AddEntityToEntities(dealButton);

            newBet = new TextElement(playMenu.ConvertBetToString(), 72, (Settings.canvasVerticalWidth / 2), 500 + (632 / 2));
            playMenu.AddEntityToEntities(newBet);
        }

        public override void Update(float deltaTime)
        {
            TestButtonStates();

            if (readyToExit || PlayerInput.buttonsPressed.Count == 0) return;

            if (PlayerInput.buttonsPressed.Peek() == "Increase")
            {
                playMenu.bet += playMenu.betInterval;
                newBet.UpdateMessage(playMenu.ConvertBetToString());
                playMenu.betAmount.UpdateMessage(playMenu.ConvertBetToString());
                PlayerInput.buttonsPressed.Dequeue();
            }
            
            else if (PlayerInput.buttonsPressed.Peek() == "Decrease")
            {
                playMenu.bet -= playMenu.betInterval;
                newBet.UpdateMessage(playMenu.ConvertBetToString());
                playMenu.betAmount.UpdateMessage(playMenu.ConvertBetToString());
                PlayerInput.buttonsPressed.Dequeue();
            }

            else if (PlayerInput.buttonsPressed.Peek() == "Deal")
            {
                
            }
            
        }

        private void TestButtonStates()
        {
            int nextBetLow = playMenu.bet - playMenu.betInterval;
            int nextBetHigh = playMenu.bet + playMenu.betInterval;

            //Decrease bet button
            if (nextBetLow < playMenu.betInterval)
            {
                decreaseBetButton.isDisabled = true;
                
            }
            else if (nextBetLow >= playMenu.betInterval)
            {
                decreaseBetButton.isDisabled = false;
            }

            //Increase bet button
            if (playMenu.player.health >= nextBetHigh)
            {
                increaseBetButton.isDisabled = false;
            }
            else if (playMenu.player.health < nextBetHigh)
            {
                increaseBetButton.isDisabled= true;
            }

        }

        public override void Exit()
        {
            playMenu.RemoveEntityFromEntities(placeBetBox);
            playMenu.RemoveEntityFromEntities(decreaseBetButton);
            playMenu.RemoveEntityFromEntities(increaseBetButton);
            playMenu.RemoveEntityFromEntities(dealButton);
            playMenu.RemoveEntityFromEntities(newBet);
        }

        
    }
}
