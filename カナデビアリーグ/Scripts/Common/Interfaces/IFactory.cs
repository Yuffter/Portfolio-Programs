using UnityEngine;

namespace MinutesGame.Common.Interfaces
{
    public interface IFactory<T>
    {
        T Create();
    }
}