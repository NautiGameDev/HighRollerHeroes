using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class ResetState : State
    {
        public ResetState(Play playMenu) : base(playMenu)
        {
        }

        public override void Enter()
        {
            Hand playerHand = playMenu.player.GetCurrentHand();
            BetState betState = new BetState(playMenu, playerHand.GetBet());

            ResetPlayerHand();
            ResetDealerHand();
            readyToExit = true;

            
            playMenu.ChangeState(betState);
        }

        private void ResetPlayerHand()
        {
            Player player = playMenu.player;

            player.ResetHands();
            playMenu.playerHandValue.UpdateMessage(0.ToString());
        }

        private void ResetDealerHand()
        {
            Player dealer = playMenu.dealer;

            dealer.ResetHands();
            playMenu.playerHandValue.UpdateMessage(0.ToString());
        }

        public override void Update(float deltaTime)
        {
            if (readyToExit) return;
        }

        public override void Exit()
        {
            
        }
    }
}
