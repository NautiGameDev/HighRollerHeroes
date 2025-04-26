namespace HighRollerHeroes.Blackjack.Menus.States
{
    public abstract class State
    {
        protected Play playMenu { get; set; }
        protected bool readyToExit { get; set; }

        public State(Play playMenu)
        {
            this.playMenu = playMenu;
            readyToExit = false;
        }

        public abstract void Enter();

        public abstract void Update(float deltaTime);

        public abstract void Exit();
    }
}
