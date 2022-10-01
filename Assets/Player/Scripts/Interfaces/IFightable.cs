namespace Interfaces
{
    public interface IFightable
    {
        public bool IsFighting();
        public void Init(IAnimated animated, IStater stater);
        public void UpdateScript();
    }
}