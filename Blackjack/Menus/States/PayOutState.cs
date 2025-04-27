using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class PayOutState : State
    {
        Sprite UIBox { get; set; }
        Button confirmButton { get; set; }

        public PayOutState(Play playMenu) : base(playMenu)
        {

        }

        public async override void Enter()
        {

            playMenu.ToggleActionBar(true); //Disable action bar

            await Task.Delay(500);

            if (playMenu.player.handValue > 21)
            {
                UIBox = new Sprite("Player Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
            }
            else if (playMenu.dealer.handValue > 21)
            {
                UIBox = new Sprite("Dealer Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                HandlePlayerWin();
            }
            else if(playMenu.player.handValue > playMenu.dealer.handValue)
            {
                UIBox = new Sprite("Player Win", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                HandlePlayerWin();
            }
            else if (playMenu.dealer.handValue >= playMenu.player.handValue)
            {
                UIBox = new Sprite("Dealer Win", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
            }

            confirmButton = new Button("Confirm", (Settings.canvasVerticalWidth / 2) - (261 / 2), 840);
            playMenu.AddEntityToEntities(UIBox);
            playMenu.AddEntityToEntities(confirmButton);
        }

        private void HandlePlayerWin()
        {
            playMenu.dealer.health -= playMenu.bet;
            playMenu.player.health += (playMenu.bet * 2);
            playMenu.playerHPText.UpdateMessage(playMenu.player.health.ToString());
            playMenu.dealerHPText.UpdateMessage(playMenu.dealer.health.ToString());
        }

        public override void Update(float deltaTime)
        {
            if (readyToExit || PlayerInput.buttonsPressed.Count == 0) return;

            if (PlayerInput.buttonsPressed.Peek() == "Confirm")
            {
                PlayerInput.buttonsPressed.Dequeue();
                readyToExit = true;

                ResetState resetState = new ResetState(playMenu);
                playMenu.ChangeState(resetState);
            }
        }

        public override void Exit()
        {
            playMenu.RemoveEntityFromEntities(UIBox);
            playMenu.RemoveEntityFromEntities(confirmButton);
        }

        
    }
}
