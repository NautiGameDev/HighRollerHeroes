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
            ResetPlayerHand();
            ResetDealerHand();
            readyToExit = true;

            BetState betState = new BetState(playMenu);
            playMenu.ChangeState(betState);
        }

        private void ResetPlayerHand()
        {
            List<Card> playerHand = playMenu.player.GetHand();
            foreach (Card card in playerHand)
            {
                playMenu.RemoveEntityFromEntities(card);
            }
            playMenu.player.ClearHand();
            playMenu.playerHandValue.UpdateMessage(playMenu.player.handValue.ToString());
        }

        private void ResetDealerHand()
        {
            List<Card> dealerHand = playMenu.dealer.GetHand();
            foreach (Card card in dealerHand)
            {
                playMenu.RemoveEntityFromEntities(card);
            }
            playMenu.dealer.ClearHand();
            playMenu.dealerHandValue.UpdateMessage(playMenu.dealer.handValue.ToString());
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
