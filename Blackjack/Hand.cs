using System;
using System.ComponentModel;
using HighRollerHeroes.Blackjack.Entities;
using HighRollerHeroes.Blackjack.Menus;

namespace HighRollerHeroes.Blackjack
{
    public class Hand
    {
        private Play playMenu { get; set; }
        private Deck deck { get; set; }
        private Player player { get; set; }
        public Stack<Card> cards { get; private set; } = new Stack<Card>();

        public int handValue { get; private set; }
        public int bet { get; private set; }

        public Hand(Player player, Play playMenu, Deck deck)
        {
            this.playMenu = playMenu;
            this.deck = deck;
            this.player = player;
        }

        public void SetBet(int bet)
        {
            this.bet = bet;
        }

        public int GetBet()
        {
            return bet;
        }

        public async Task DrawCardFromDeck(bool isFaceDown, Card.DeckType deckType)
        {
            float cardXPosition;
            float cardYPosition;

            if (cards.Count == 0)
            {
                if (deckType == Card.DeckType.SUN) //Player card position
                {
                    cardXPosition = 300;
                    cardYPosition = 900;
                }
                else //Dealer card position
                {
                    cardXPosition = 300;
                    cardYPosition = 200;
                }
                
            }
            else
            {
                if (!player.hasSplit)
                {
                    foreach (Card card in cards)
                    {
                        card.MoveCardByOffset(-25, -25);
                    }
                }
                
                Card lastCard = cards.Peek();

                //If hand is split, card is placed vertically on top of previous card, otherwise there is offset on x-axis
                cardXPosition = player.hasSplit ? lastCard.unscaledXPos : lastCard.unscaledXPos + 100;
                cardYPosition = lastCard.unscaledYPos + 50;
            }

            string cardValue = await deck.DrawCard();
            Card newCard = new Card(deckType, cardXPosition, cardYPosition, cardValue, isFaceDown);
            cards.Push(newCard);
            playMenu.AddEntityToEntities(newCard);
            CalculateHandValue();
        }

        public void CalculateHandValue()
        {
            handValue = 0;
            int aces = 0;

            foreach (Card card in cards)
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
            }

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

        public bool TestCanSplit()
        {
            if (cards.FirstOrDefault().cardValue == cards.Peek().cardValue)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void MoveHand(float x, float y)
        {
            Card firstCard = cards.FirstOrDefault();
            firstCard.MoveCard(x, y);
        }
    }
}
