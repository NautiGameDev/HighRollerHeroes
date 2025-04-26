### High Roller Heroes
High Roller Heroes is a web game being devoled in Blazor Webassembly that combines Blackjack with Trading Card Game mechanics. This game is still under development but I expect a live version to be available on itch.io within the next month. The goal is to make a Dark Fantasy themed blackjack game with some card abilities that impact the traditional Blackjack gameplay. Current plans are to use face cards as the special cards that will impact the general strategy in some way, such as making the dealer's facedown card visible, seeing the next card in the deck, adding points to the total value without busting, or increasing the amount of currency (health) won if the player wins by x%.

## Current State

**Finished:**
- UI is completed with dynamic working interface that sends player input to the game for processing
- Basic cards have been designed to get started, more cards will be added in the future
- Player and Dealer classes with health have been implemented

**Currently in progress**
- Player turn state
- Dealer turn state
- Implementing card abilities
- Evaluating win conditions
- Resolving win/loss effects on player and dealer health.

**Future plans**
- Add more cards for increased player strategy
- Roguelike mechanics that allow unlockable cards as bosses(dealers) are defeated.
- Deck building mechanics
- Game play polish (Animations, sound effects)
- Deploy to itch.io
