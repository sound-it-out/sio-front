namespace SIO.Domain.States
{
    internal abstract class State<T> : IState<T>
    {
        public T Value { get; private set; }

        public bool Loaded { get; private set; }

        public void Load(T data)
        {
            Value = data;
            Loaded = true;
        }
    }
}
