namespace SIO.Domain.States
{
    public interface IState<T>
    {
        bool Loaded { get; }
        void Load(T data);
        T Value { get; }
    }
}
