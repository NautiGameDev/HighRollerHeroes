using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(Play playMenu) : base(playMenu)
        {

        }

        public override void Enter()
        {
            playMenu.ToggleActionBar(false);
            List<Card> playerHand = playMenu.player.GetHand();

            if (playerHand.FirstOrDefault().cardValue != playerHand.Last().cardValue)
            {
                playMenu.ToggleSplitButton(true); //Disable split button if two cards aren't identical
            }

        }

        public async override void Update(float deltaTime)
        {
            if (readyToExit || PlayerInput.buttonsPressed.Count == 0) return;

            if (PlayerInput.buttonsPressed.Peek() == "Hit")
            {
                HandlePlayerHit();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Stand")
            {
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Double")
            {
                readyToExit = true;
                playMenu.player.health -= playMenu.bet;
                playMenu.bet *= 2;
                playMenu.betAmount.UpdateMessage(playMenu.bet.ToString());
                playMenu.playerHPText.UpdateMessage(playMenu.player.health.ToString());
                HandlePlayerHit();
                EndTurn();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Split")
            {
                //Logic to handle splitting
                PlayerInput.buttonsPressed.Dequeue();
            }
        }

        private void EndTurn()
        {
            DealerTurnState dealerTurnState = new DealerTurnState(playMenu);
            playMenu.ChangeState(dealerTurnState);
        }

        private async void HandlePlayerHit()
        {
            List<Card> playerHand = playMenu.player.GetHand();

            foreach (Card card in playerHand)
            {
                card.xPos -= 25;
            }

            string newCard = await playMenu.player.DrawCardFromDeck();

            /*
             * New Card position is calculated by taking base position (300,925) and subtracting/adding offsets
             * Offsets are calculated by taking the amount of hits player has taken + 1 multiplied by offset amount
             */

            int timesHit = playerHand.Count() - 2;
            float newCardXPos = (300 - (25 * (timesHit + 1))) + (100 * (timesHit + 1));
            float newCardYPos = 925 + (25 * (timesHit + 1));
            Card drawnCard = new Card(Card.DeckType.SUN, newCardXPos, newCardYPos, newCard, false);

            playMenu.player.AddCardToHand(drawnCard);
            playMenu.AddEntityToEntities(drawnCard);
            playMenu.playerHandValue.UpdateMessage(playMenu.player.handValue.ToString());

            TestForBust();
            PlayerInput.buttonsPressed.Dequeue();
        }

        public void TestForBust()
        {
            if (playMenu.player.handValue > 21)
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
