using System;

public interface IDestroyable<T>
{
    public event Action<T> Destroyed;
}
