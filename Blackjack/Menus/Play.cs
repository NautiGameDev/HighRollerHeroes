
using HighRollerHeroes.Blackjack.Data;
using HighRollerHeroes.Blackjack.Entities;
using HighRollerHeroes.Blackjack.Menus.States;

namespace HighRollerHeroes.Blackjack.Menus
{
    public class Play : Menu
    {
        private List<Entity> entities = new List<Entity>();
        private List<Button> actionBar = new List<Button>();
        public TextElement playerHPText { get; set; }
        public TextElement dealerHPText { get; set; }
        public TextElement betAmount { get; set; }


        private Deck deck {  get; set; }
        public Player player { get; set; }
        public Player dealer { get; set; }
        public int bet { get; set; } = 50;
        public int betInterval { get; set; } = 50;

        private State currentState { get; set; }

        public Play()
        {
            deck = new Deck();
            this.player = new Player(1000, deck);
            this.dealer = new Player(1000, deck);

            InstantiateBaseEntities();
            currentState = new BetState(this);
            currentState.Enter();
        }

        private void InstantiateBaseEntities()
        {
            //Background
            Background background = new Background(Background.BGType.VERTICAL);
            entities.Add(background);
                        
            //Buttons
            Button hitButton = new Button("Hit", -10, 1460);
            Button standButton = new Button("Stand", 215, 1460);
            Button doubleButton = new Button("Double", 440, 1460);
            Button splitButton = new Button("Split", 665, 1460);

            entities.Add(hitButton);
            entities.Add(standButton);
            entities.Add(doubleButton);
            entities.Add(splitButton);
            actionBar.Add(hitButton);
            actionBar.Add(standButton);
            actionBar.Add(doubleButton);
            actionBar.Add(splitButton);

            //UI
            Sprite playerHealthBar = new Sprite("Player Health", 118, 25, 261, 138, 0f);
            Sprite dealerHealthBar = new Sprite("Dealer Health", 517, 25, 261, 138, 0f);
            Sprite betBackground = new Sprite("Bet Box", 379, 25, 138, 138, 0f);

            entities.Add(playerHealthBar);
            entities.Add(dealerHealthBar);
            entities.Add(betBackground);

            //Text elements
            playerHPText = new TextElement(player.health.ToString(), 48, 118 + (261/2), 60 + (138/2));
            entities.Add(playerHPText);

            dealerHPText = new TextElement(dealer.health.ToString(), 48, 517 + (261/2), 60 + (138/2));
            entities.Add(dealerHPText);

            

            betAmount = new TextElement(ConvertBetToString(), 48, 379 + (138 / 2), 60 + (138 / 2));
            entities.Add(betAmount);
            
        }

        public string ConvertBetToString()
        {
            Console.WriteLine($"Current Bet: {bet}");
            string adjustedBet = "";

            if (bet >= 1000)
            {
                int thousands = (int)bet / 1000;
                int hundreds = (int)(bet % 1000 / 100);

                if (hundreds > 0)
                {
                    adjustedBet = thousands.ToString() + "." + hundreds.ToString() + "k";
                }
                else
                {
                    adjustedBet = thousands.ToString() + "k";
                }
            }
            else
            {
                adjustedBet = bet.ToString();
            }

            return adjustedBet;
        }

        public void AddEntityToEntities(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntityFromEntities(Entity entity)
        {
            entities.Remove(entity);
        }

        public void ToggleActionBar(bool value)
        {
            foreach (Button button in actionBar)
            {
                button.isDisabled = value;
            }
        }

        public override async void Render()
        {
            foreach (Entity entity in entities)
            {
                await entity.Render();
            }
        }

        public override void Update(float deltaTime)
        {
            foreach (Entity entity in entities)
            {
                entity.Update(deltaTime);
            }

            currentState.Update(deltaTime);
        }
    }
}
