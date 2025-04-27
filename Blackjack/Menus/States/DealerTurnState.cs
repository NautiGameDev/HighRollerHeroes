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
            List<Card> dealerHand = playMenu.dealer.GetHand();

            foreach (Card card in dealerHand) 
            { 
                card.isFlipped = false;
            }

            playMenu.dealer.CalculateHandValue();
            playMenu.dealerHandValue.UpdateMessage(playMenu.dealer.handValue.ToString());
        }

        public override void Update(float deltaTime)
        {
            if (cardDrawTimer > 0)
            {
                cardDrawTimer -= deltaTime;
            }

            if (readyToExit) return;

            if (playMenu.dealer.handValue < 17)
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
            List<Card> dealerHand = playMenu.dealer.GetHand();

            foreach (Card card in dealerHand)
            {
                card.xPos -= 25;
            }

            string newCard = await playMenu.dealer.DrawCardFromDeck();

            /*
             * New Card position is calculated by taking base position (300,925) and subtracting/adding offsets
             * Offsets are calculated by taking the amount of hits player has taken + 1 multiplied by offset amount
             */

            int timesHit = dealerHand.Count() - 2;
            float newCardXPos = (300 - (25 * (timesHit + 1))) + (100 * (timesHit + 1));
            float newCardYPos = 225 + (25 * (timesHit + 1));
            Card drawnCard = new Card(Card.DeckType.MOON, newCardXPos, newCardYPos, newCard, false);

            playMenu.dealer.AddCardToHand(drawnCard);
            playMenu.AddEntityToEntities(drawnCard);
            playMenu.dealerHandValue.UpdateMessage(playMenu.dealer.handValue.ToString());

            TestForBust();
        }

        public void TestForBust()
        {
            if (playMenu.dealer.handValue > 21)
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
