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

            Player player = playMenu.player;
            Player dealer = playMenu.dealer;

            player.handIndex = 0;

            Hand playerHand = player.GetCurrentHand();
            Hand dealerHand = dealer.GetCurrentHand();


            TestWinConditions(playerHand, dealerHand);
        }

        private void TestWinConditions(Hand playerHand, Hand dealerHand)
        {
            if (playerHand.handValue > 21)
            {
                UIBox = new Sprite("Player Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
            }
            else if (dealerHand.handValue > 21)
            {
                UIBox = new Sprite("Dealer Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                HandlePlayerWin(playerHand);
            }
            else if (playerHand.handValue > dealerHand.handValue)
            {
                UIBox = new Sprite("Player Win", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                HandlePlayerWin(playerHand);
            }
            else if (dealerHand.handValue >= playerHand.handValue)
            {
                UIBox = new Sprite("Dealer Win", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
            }

            confirmButton = new Button("Confirm", (Settings.canvasVerticalWidth / 2) - (261 / 2), 840);
            playMenu.AddEntityToEntities(UIBox);
            playMenu.AddEntityToEntities(confirmButton);
        }

        private void HandlePlayerWin(Hand playerHand)
        {
            Player player = playMenu.player;
            Player dealer = playMenu.dealer;

            dealer.health -= playerHand.GetBet();
            player.health += (playerHand.GetBet() * 2);
            playMenu.playerHPText.UpdateMessage(player.health.ToString());
            playMenu.dealerHPText.UpdateMessage(dealer.health.ToString());
        }

        public override void Update(float deltaTime)
        {
            if (readyToExit || PlayerInput.buttonsPressed.Count == 0) return;

            if (PlayerInput.buttonsPressed.Peek() == "Confirm")
            {
                PlayerInput.buttonsPressed.Dequeue();
                Player player = playMenu.player;

                if (player.hasSplit && player.handIndex == 0)
                {
                    player.handIndex = 1;
                    Hand playerHand = player.GetCurrentHand();
                    Hand dealerHand = playMenu.dealer.GetCurrentHand();

                    TestWinConditions(playerHand, dealerHand);
                }
                else
                {
                    readyToExit = true;
                    ResetState resetState = new ResetState(playMenu);
                    playMenu.ChangeState(resetState);
                }
                
            }
        }

        public override void Exit()
        {
            playMenu.RemoveEntityFromEntities(UIBox);
            playMenu.RemoveEntityFromEntities(confirmButton);
        }

        
    }
}
