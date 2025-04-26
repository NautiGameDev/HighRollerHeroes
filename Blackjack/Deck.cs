namespace HighRollerHeroes.Blackjack
{
    public class Deck
    {
        Stack<string> cards { get; set; } = new Stack<string>();
        Stack<string> discardedPile { get; set; } = new Stack<string>();
        Random random { get; set; } = new Random();

        public Deck() 
        {
            BuildDeck();
        }

        private void BuildDeck()
        {
            List<string> cards = new List<string>();
            string[] cardValues = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];

            foreach (string cardValue in cardValues)
            {
                for (int i = 0; i < 4; i++)
                {
                    cards.Add(cardValue);
                }
            }

            ShuffleCards(cards);
        }

        public void ShuffleCards(List<string> cards)
        {
            List<string> shuffledCards = cards.OrderBy(x => random.Next()).ToList();

            foreach (string card in shuffledCards)
            {
                this.cards.Push(card);
            }
        }

        public async Task<string> DrawCard()
        {
            if (cards.Count == 0)
            {
                await ReshuffleCards();
            }

            string nextCard = cards.Pop();
            discardedPile.Push(nextCard);

            return nextCard;
        }

        public async Task ReshuffleCards()
        {
            List<string> shuffledCards = discardedPile.OrderBy(x => random.Next()).ToList();

            foreach (string card in shuffledCards)
            {
                this.cards.Push(card);
            }
        }
    }
}
