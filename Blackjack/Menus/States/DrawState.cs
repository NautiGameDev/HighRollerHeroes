using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class DrawState : State
    {
        
        public DrawState(Play playMenu) : base(playMenu)
        {
            
        }

        public async override void Enter()
        {
            /*
             * May need to hard code card positions in settings based on cards drawn.
             * Also hard code the scaling in card class to shorten this code
             */
            
            string newCard = await playMenu.player.DrawCardFromDeck();
            Card pCardOne = new Card(Card.DeckType.SUN, 200, 900, newCard, false);
            playMenu.AddEntityToEntities(pCardOne);

            newCard = await playMenu.player.DrawCardFromDeck();
            Card pCardTwo = new Card(Card.DeckType.SUN, 400, 950, newCard, false);
            playMenu.AddEntityToEntities(pCardTwo);

            newCard = await playMenu.dealer.DrawCardFromDeck();
            Card dCardOne = new Card(Card.DeckType.MOON, 200, 200, newCard, true);
            playMenu.AddEntityToEntities(dCardOne);

            newCard = await playMenu.dealer.DrawCardFromDeck();
            Card dCardTwo = new Card(Card.DeckType.MOON, 400, 250, newCard, false);
            playMenu.AddEntityToEntities(dCardTwo);
        }

        public override void Update(float deltaTime)
        {
            if (readyToExit)
            {
                Exit();
            }
        }

        public override void Exit()
        {

        }
    }
}
