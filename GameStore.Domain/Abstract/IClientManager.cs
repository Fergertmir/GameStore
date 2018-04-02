using GameStore.Domain.Entities;
using System;

namespace GameStore.Domain.Abstract
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
