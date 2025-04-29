
using System.Numerics;
using HighRollerHeroes.Blackjack.Entities;
using HighRollerHeroes.Blackjack.Menus;
using Microsoft.VisualBasic;

namespace HighRollerHeroes.Blackjack
{
    public class Player
    {
        public int health { get; set; } = 1000;

        private Deck deckReference {  get; set; }
        private Play playMenu { get; set; }
        public Hand[] hands { get; private set; } = new Hand[2];

        public bool hasSplit = false;
        public int handIndex = 0;

        public Player(int health, Deck deck, Play playMenu)
        {
            this.health = health;
            this.deckReference = deck;
            this.playMenu = playMenu;

            InstantiateHands();
        }

        public void InstantiateHands()
        {
            //Instantiate hands
            Hand firstHand = new Hand(this, playMenu, deckReference);
            hands[0] = firstHand;
            Hand secondHand = new Hand(this, playMenu, deckReference);
            hands[1] = secondHand;
        }

        public async Task DrawCardFromDeck(bool isFaceDown, Card.DeckType deckType)
        {
            await hands[handIndex].DrawCardFromDeck(isFaceDown, deckType);
        }

        public string GetHandValue()
        {
            if (!hasSplit)
            {
                return hands[0].handValue.ToString();
            }
            else
            {
                return hands[0].handValue.ToString() + "/" + hands[1].handValue.ToString();
            }
        }

        public Hand GetCurrentHand()
        {
            return hands[handIndex];
        }

        public string GetBets()
        {
            if (!hasSplit)
            {
                return hands[0].GetBet().ToString();
            }
            else
            {
                return hands[0].GetBet().ToString() + "/" + hands[1].GetBet().ToString();
            }
        }

        public bool TestCurrentHandBust()
        {
            Hand currentHand = hands[handIndex];
            if (currentHand.handValue > 21)
            {
                return true;
            }
            return false;
        }

        public void SplitHand()
        {
            handIndex = 0;
            hasSplit = true;
            Card secondCard = hands[0].cards.Peek();
            hands[1].cards.Push(secondCard);
            hands[0].cards.Pop();

            hands[0].MoveHand(100, 900);
            hands[1].MoveHand(450, 900);
        }

        public void ResetHands()
        {
            //Remove rendered cards
            foreach (Card card in hands[0].cards)
            {
                playMenu.RemoveEntityFromEntities(card);
            }

            if (hasSplit)
            {
                foreach (Card card in hands[1].cards)
                {
                    playMenu.RemoveEntityFromEntities(card);
                }
            }

            //Reset split actions this turn
            hasSplit = false;
            handIndex = 0;

            //New set of hand objects
            InstantiateHands();
        }
    }
}
