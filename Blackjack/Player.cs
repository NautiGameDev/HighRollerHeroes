
namespace HighRollerHeroes.Blackjack
{
    public class Player
    {
        public int health { get; set; } = 1000;

        private Deck deckReference {  get; set; }
        private List<string> hand = new List<string>();

        public Player(int health, Deck deck)
        {
            this.health = health;
            this.deckReference = deck;
        }

        public async Task<string> DrawCardFromDeck()
        {
            string card = await deckReference.DrawCard();
            hand.Add(card);
            return card;
        }

        public void ClearHand()
        {
            hand.Clear();
        }
    }
}
