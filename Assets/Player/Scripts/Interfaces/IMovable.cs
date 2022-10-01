namespace Interfaces
{
    public interface IMovable
    {
        public float HorizontalMovement { get; }

        public void Init(IAnimated animated, Jumpable jumpable, IFightable fightable, IStater stater);
        public void UpdateScript();
    }
}