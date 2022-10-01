namespace Interfaces
{
    public interface IFightable : IUpdatable
    {
        public bool IsFighting();
        public void Init(IAnimated animated);
    }
}