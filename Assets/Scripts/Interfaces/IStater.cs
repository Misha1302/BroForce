namespace Interfaces
{
    public interface IStater
    {
        public bool CanFight { get; }
        public bool CanRun { get; }
        public bool CanJump { get; }

        public void Init(IFightable fightable, Jumpable jumpable);
    }
}