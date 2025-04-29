using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class DealerTurnState : State
    {
        float cardDrawTimer = 0;
        float cardDrawDelay = 1f;

        public DealerTurnState(Play playMenu) : base(playMenu)
        {
        }

        public override void Enter()
        {
            playMenu.ToggleActionBar(true); //Disable player action bar

            Player dealer = playMenu.dealer;
            Hand dealerHand = dealer.GetCurrentHand();

            foreach (Card card in dealerHand.cards) 
            { 
                card.isFlipped = false;
            }

            dealerHand.CalculateHandValue();
            playMenu.dealerHandValue.UpdateMessage(dealer.GetHandValue());
        }

        public override void Update(float deltaTime)
        {
            if (cardDrawTimer > 0)
            {
                cardDrawTimer -= deltaTime;
            }

            if (readyToExit) return;

            Hand dealerHand = playMenu.dealer.GetCurrentHand();

            if (dealerHand.handValue < 17)
            {
                if (cardDrawTimer <= 0)
                {
                    DealerDrawCard();
                    cardDrawTimer = cardDrawDelay;
                }
            }
            else
            {
                readyToExit = true;
                PayOutState payoutState = new PayOutState(playMenu);
                playMenu.ChangeState(payoutState);
            }

        }

        private async void DealerDrawCard()
        {
            Player dealer = playMenu.dealer;

            dealer.DrawCardFromDeck(false, Card.DeckType.MOON);


            playMenu.dealerHandValue.UpdateMessage(dealer.GetHandValue());

            TestForBust();
        }

        public void TestForBust()
        {
            Player dealer = playMenu.dealer;
            Hand dealerHand = dealer.GetCurrentHand();

            if (dealerHand.handValue > 21)
            {
                readyToExit = true;
                PayOutState payoutState = new PayOutState(playMenu);
                playMenu.ChangeState(payoutState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}
