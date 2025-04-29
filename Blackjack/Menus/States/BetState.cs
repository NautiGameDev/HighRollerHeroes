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

        public int bet { get; set; }
        public int betInterval { get; set; } = 50;

        public BetState(Play playMenu, int currentBet) : base(playMenu)
        {
            this.bet = currentBet;
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

            newBet = new TextElement(Utilities.ConvertBetToString(bet), 72, (Settings.canvasVerticalWidth / 2), 500 + (632 / 2));
            playMenu.AddEntityToEntities(newBet);
        }

        public override void Update(float deltaTime)
        {
            TestButtonStates();

            if (readyToExit || PlayerInput.buttonsPressed.Count == 0) return;

            if (PlayerInput.buttonsPressed.Peek() == "Increase")
            {
                bet += betInterval;
                newBet.UpdateMessage(Utilities.ConvertBetToString(bet));
                playMenu.betAmount.UpdateMessage(Utilities.ConvertBetToString(bet));
                PlayerInput.buttonsPressed.Dequeue();
            }
            
            else if (PlayerInput.buttonsPressed.Peek() == "Decrease")
            {
                bet -= betInterval;
                newBet.UpdateMessage(Utilities.ConvertBetToString(bet));
                playMenu.betAmount.UpdateMessage(Utilities.ConvertBetToString(bet));
                PlayerInput.buttonsPressed.Dequeue();
            }

            else if (PlayerInput.buttonsPressed.Peek() == "Deal")
            {
                Player player = playMenu.player;
                player.hands[0].SetBet(bet);
                player.hands[1].SetBet(bet);
                PlayerInput.buttonsPressed.Dequeue();
                readyToExit = true;
                DrawState newState = new DrawState(playMenu);
                playMenu.ChangeState(newState);
            }
            
        }

        private void TestButtonStates()
        {
            Player player = playMenu.player;
            int nextBetLow = bet - betInterval;
            int nextBetHigh = bet + betInterval;

            //Decrease bet button
            if (nextBetLow < betInterval)
            {
                decreaseBetButton.isDisabled = true;
                
            }
            else if (nextBetLow >= betInterval)
            {
                decreaseBetButton.isDisabled = false;
            }

            //Increase bet button
            if (player.health >= nextBetHigh)
            {
                increaseBetButton.isDisabled = false;
            }
            else if (player.health < nextBetHigh)
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
