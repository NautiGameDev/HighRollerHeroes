namespace HighRollerHeroes.Blackjack.Entities
{
    public abstract class Entity
    {
        public abstract void Update(float deltaTime);

        public abstract Task Render();
    }
}
