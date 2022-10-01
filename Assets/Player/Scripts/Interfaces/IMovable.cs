namespace Interfaces
{
    public interface IMovable : IUpdatable
    {
        public float HorizontalMovement
        {
            get;
        }
        public void Init(IAnimated animated, IJumpable jumpable, IFightable fightable);
    }
}