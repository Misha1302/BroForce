namespace Interfaces
{
    public interface IJumpable : IUpdatable
    {
        public void Init(IAnimated animated, IMovable movable);
        public bool IsGrounded();
    }
}