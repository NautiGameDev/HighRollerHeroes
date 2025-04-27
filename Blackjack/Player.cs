
using System.Numerics;
using HighRollerHeroes.Blackjack.Entities;
using Microsoft.VisualBasic;

namespace HighRollerHeroes.Blackjack
{
    public class Player
    {
        public int health { get; set; } = 1000;

        private Deck deckReference {  get; set; }
        private List<Card> hand = new List<Card>();
        public int handValue { get; private set; } = 0;

        public Player(int health, Deck deck)
        {
            this.health = health;
            this.deckReference = deck;
        }

        public async Task<string> DrawCardFromDeck()
        {
            string card = await deckReference.DrawCard();            
            return card;
        }

        public void AddCardToHand(Card card)
        {
            hand.Add(card);
            CalculateHandValue();
        }

        public void CalculateHandValue()
        {
            handValue = 0;
            int aces = 0;

            foreach (Card card in hand)
            {
                if (!card.isFlipped)
                {
                    if (card.cardValue == "J" || card.cardValue == "Q" || card.cardValue == "K")
                    {
                        handValue += 10;
                    }
                    else if (card.cardValue == "A")
                    {
                        aces += 1;
                    }
                    else
                    {
                        try
                        {
                            int cardValue = int.Parse(card.cardValue);
                            handValue += cardValue;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Couldn't parse card value {card.cardValue}");
                        }
                    }
                }

                /*
                 FIXME: Aces aren't being calculated correctly on initial draw
                 */

                //Handle aces
                if (aces > 0)
                {

                    for (int i = 0; i < aces; i++)
                    {
                        if (handValue + 11 > 21)
                        {
                            handValue += 1;
                        }
                        else
                        {
                            handValue += 11;
                        }
                    }
                }

            }
        }

        public int GetHandSize()
        {
            return hand.Count();
        }

        public List<Card> GetHand()
        {
            return hand;
        }

        public void ClearHand()
        {
            hand.Clear();
            CalculateHandValue();
        }
    }
}
