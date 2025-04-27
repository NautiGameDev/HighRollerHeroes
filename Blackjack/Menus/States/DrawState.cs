using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class DrawState : State
    {

        bool hasDealtCards = false;

        public DrawState(Play playMenu) : base(playMenu)
        {
            
        }

        public async override void Enter()
        {
            playMenu.player.health -= playMenu.bet;
            playMenu.playerHPText.UpdateMessage(playMenu.player.health.ToString());

            await Task.Delay(250);

            string newCard = await playMenu.player.DrawCardFromDeck();
            Card pCardOne = new Card(Card.DeckType.SUN, 200, 900, newCard, false);
            playMenu.player.AddCardToHand(pCardOne);
            playMenu.AddEntityToEntities(pCardOne);

            await Task.Delay(250);

            newCard = await playMenu.player.DrawCardFromDeck();
            Card pCardTwo = new Card(Card.DeckType.SUN, 300, 925, newCard, false);
            playMenu.player.AddCardToHand(pCardTwo);
            playMenu.AddEntityToEntities(pCardTwo);

            await Task.Delay(250);

            newCard = await playMenu.dealer.DrawCardFromDeck();
            Card dCardOne = new Card(Card.DeckType.MOON, 200, 200, newCard, true);
            playMenu.dealer.AddCardToHand(dCardOne);
            playMenu.AddEntityToEntities(dCardOne);

            await Task.Delay(250);

            newCard = await playMenu.dealer.DrawCardFromDeck();
            Card dCardTwo = new Card(Card.DeckType.MOON, 300, 225, newCard, false);
            playMenu.dealer.AddCardToHand(dCardTwo);
            playMenu.AddEntityToEntities(dCardTwo);

            playMenu.playerHandValue.UpdateMessage(playMenu.player.handValue.ToString());
            playMenu.dealerHandValue.UpdateMessage(playMenu.dealer.handValue.ToString());

            hasDealtCards = true;
        }

        public override void Update(float deltaTime)
        {
            if (readyToExit) return;

            if (hasDealtCards)
            {
                readyToExit = true;
                PlayerTurnState playerTurnState = new PlayerTurnState(playMenu);
                playMenu.ChangeState(playerTurnState);
            }
        }

        public override void Exit()
        {

        }
    }
}
