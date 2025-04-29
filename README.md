## High Roller Heroes
High Roller Heroes is a web game being devoled in Blazor Webassembly that combines Blackjack with Trading Card Game mechanics. This game is still under development but I expect a live version to be available on itch.io within the next month. The goal is to make a Dark Fantasy themed blackjack game with some card abilities that impact the traditional Blackjack gameplay. Current plans are to use face cards as the special cards that will impact the general strategy in some way, such as making the dealer's facedown card visible, seeing the next card in the deck, adding points to the total value without busting, or increasing the amount of currency (health) won if the player wins by x%.

The goal for the project is to make a minimum viable product to be uploaded onto itch.io. The check list below is the laid out goals in mind for the MVP. If there is enough interest in a full game, I'll develop it further with more cards and potentially roguelike mechanics. If the project doesn't seem to garner enough interest then it will be kept in the state of the minimum viable product.

### Current State

**MVP Progress:**
- [x] Card and asset design 
- [x] User Interface (Player/Dealer health, betting, player/dealer hand values, win/loss alerts)
- [x] Player and dealer health systems with betting, doubling and splitting functions
- [x] State machine for each phase of the round
- [x] Win/Loss conditions (Each round)
- [ ] Card Abilities
- [ ] Gameplay polish (Animations and sound effects)
- [ ] Main menu and How to play screen
- [ ] Player win/loss conditions (Game)
- [ ] Deploy to itch.io

**Video Previews**

[Early visuals preview (Basic Blackjack Game)](https://www.youtube.com/shorts/L5V5OT7_2bE)

**Update Notes**

*4/29/25*

Split action has been implemented and is now working. I attempted to program this functionality last night then realized the route I was taking was creating a lot of messy and disorganized code, plus I was setting myself up for failure for when I start implementing card abilities. I had to take a step back and create a Hand class that held all of the methods for card drawing, calculating hand value, moving cards, and so on. This took a few hours of refactoring to go through all of the states and rewrite/tidy up some code with new implementation. However, the game is working with the split action in place. This was also a necessary change in order to factor in ability cards for each hand, which likely would have been impossible by just storing the cards in a couple of Lists. There's a few bugs here and there with the action bar UI and some UI elements not being removed from the entities array to stop rendering, but they are easy fixes that will require some tracking down. Also some bugs on betting, if the previous bet is more than the player's remaining hp they can still place the bet. Also an easy fix, just a matter of implementation.

*4/27/25*

The current version of the game is a basic Blackjack game with a starter card set. There isn't any player/dealer win conditions as far as health dropping below zero. But the player can adjust the bet and play as many rounds as desired. All basic game states are implemented, such as bet, deal, player turn, dealer turn, and payout. Currently, none of the abilities have been implemented into the game but that's next on my list. The game is just basic Blackjack. I'm also still missing the split action from the player, which will be implemented later when I have more energy to program. I also uploaded the first preview to youtube: [here](https://www.youtube.com/shorts/L5V5OT7_2bE)
