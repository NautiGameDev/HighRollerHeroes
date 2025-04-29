
using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;

namespace HighRollerHeroes.Blackjack.Menus.States
{
    public class PlayerTurnState : State
    {
        Sprite playerBusted {  get; set; }
        Button confirmButton { get; set; }

        public PlayerTurnState(Play playMenu) : base(playMenu)
        {

        }

        public override void Enter()
        {
            playMenu.ToggleActionBar(false);
            Player player = playMenu.player;

            //Tests if the drawn hand of the player has duplicate cards. If method returns false, split button is disabled
            if (!player.hands[0].TestCanSplit())
            {
                playMenu.ToggleSplitButton(true);
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
                HandlePlayerStand();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Double")
            {
                HandlePlayerDouble();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Split")
            {
                HandlePlayerSplit();
            }
            else if (PlayerInput.buttonsPressed.Peek() == "Confirm")
            {
                HandlePlayerConfirm();
            }
        }

        private void EndTurn()
        {
            DealerTurnState dealerTurnState = new DealerTurnState(playMenu);
            playMenu.ChangeState(dealerTurnState);
        }

        private async void HandlePlayerHit()
        {
            playMenu.ToggleDoubleButton(true); //Disable double button
            playMenu.ToggleSplitButton(true);

            Player player = playMenu.player;
            PlayerInput.buttonsPressed.Dequeue();

            player.DrawCardFromDeck(false, Card.DeckType.SUN);

            playMenu.playerHandValue.UpdateMessage(player.GetHandValue());

            /* Test if player has busted current hand.
             If true and player has not split, move on to payout
             If true and player has split, test which hand is active
                If first hand is active, switch to second hand
                If second hand is active, set index to first hand and move on to payout state
            If player hasn't busted, repeat current state*/

            if (player.TestCurrentHandBust())
            {
                if(!player.hasSplit)
                {
                    readyToExit = true;
                    PayOutState payoutState = new PayOutState(playMenu);
                    playMenu.ChangeState(payoutState);
                }
                else if (player.hasSplit && player.handIndex == 0)
                {
                    playMenu.ToggleActionBar(true);
                    playerBusted = new Sprite("Player Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                    confirmButton = new Button("Confirm", (Settings.canvasVerticalWidth / 2) - (261 / 2), 840);
                    playMenu.AddEntityToEntities(playerBusted);
                    playMenu.AddEntityToEntities(confirmButton);
                }
                else
                {
                    playMenu.ToggleActionBar(true);
                    playerBusted = new Sprite("Player Bust", (Settings.canvasVerticalWidth / 2) - (800 / 2), 400, 800, 632, 0);
                    confirmButton = new Button("Confirm", (Settings.canvasVerticalWidth / 2) - (261 / 2), 840);
                    playMenu.AddEntityToEntities(playerBusted);
                    playMenu.AddEntityToEntities(confirmButton);
                }
            }
        }

        private void HandlePlayerStand()
        {
            Player player = playMenu.player;

            /* Check if player has split hand
             If false: Exit current state and move on to the next state
             If True: Check which hand is active
                If second hand is active move on to next state
                If first hand is active, switch to second hand
             */

            if (!player.hasSplit)
            {
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }
            else if (player.hasSplit && player.handIndex == 0)
            {
                playMenu.player.handIndex = 1;
                PlayerInput.buttonsPressed.Dequeue();
            }
            else if (player.hasSplit && player.handIndex == 1)
            {
                player.handIndex = 0;
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }
        }

        private void HandlePlayerDouble()
        {
            /*
             If player has doubled, get the bet for the current hand and double it
             Remove original bet size from player health and update the player health text

             Test if player has split:
                False: End turn and move on to dealer turn
                True and player's active hand is first hand, switch to second hand
                True and player's active hand is second hand, end turn and move on to dealer turn
             */

            Player player = playMenu.player;
            
            int currentBet = player.hands[player.handIndex].GetBet();
            player.hands[player.handIndex].SetBet(currentBet * 2);
            player.health -= currentBet;

            player.DrawCardFromDeck(false, Card.DeckType.SUN);

            playMenu.playerHPText.UpdateMessage(player.health.ToString());
            playMenu.playerHandValue.UpdateMessage(player.GetHandValue());

            if (!player.hasSplit)
            {
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }
            else if (player.hasSplit && player.handIndex == 0)
            {
                playMenu.player.handIndex = 1;
                PlayerInput.buttonsPressed.Dequeue();
            }
            else
            {
                player.handIndex = 0;
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }
        }

        private void HandlePlayerSplit()
        {
            Player player = playMenu.player;
            
            player.SplitHand();
            player.health -= player.hands[0].GetBet();
            
            playMenu.betAmount.UpdateMessage(player.GetBets());
            playMenu.playerHandValue.UpdateMessage(player.GetHandValue());

            playMenu.ToggleSplitButton(true);
            PlayerInput.buttonsPressed.Dequeue();
        }

        private void HandlePlayerConfirm()
        {
            Player player = playMenu.player;

            if (player.handIndex == 0)
            {
                playMenu.ToggleActionBar(false);
                playMenu.ToggleSplitButton(true);
                
                player.handIndex = 1;
                PlayerInput.buttonsPressed.Dequeue();
            }
            else
            {
                player.handIndex = 0;
                readyToExit = true;
                EndTurn();
                PlayerInput.buttonsPressed.Dequeue();
            }

            playMenu.RemoveEntityFromEntities(playerBusted);
            playMenu.RemoveEntityFromEntities(confirmButton);
                
        }

        public override void Exit()
        {
           
        }

        
    }
}
