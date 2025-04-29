using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

/* 
 * Initial card draw state after player places their bet
 * Draws two cards for each player and dealer before moving on to player turn state 
 */

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
            Player player = playMenu.player;
            Player dealer = playMenu.dealer;

            player.health -= player.hands[0].bet;
            playMenu.playerHPText.UpdateMessage(player.health.ToString());

            await Task.Delay(250);
            player.DrawCardFromDeck(false, Card.DeckType.SUN);

            await Task.Delay(250);
            player.DrawCardFromDeck(false, Card.DeckType.SUN);

            await Task.Delay(250);
            dealer.DrawCardFromDeck(true, Card.DeckType.MOON);

            await Task.Delay(250);
            dealer.DrawCardFromDeck(false, Card.DeckType.MOON);

            
            playMenu.playerHandValue.UpdateMessage(player.GetHandValue());
            playMenu.dealerHandValue.UpdateMessage(dealer.GetHandValue());

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
