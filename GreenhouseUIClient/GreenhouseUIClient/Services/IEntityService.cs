using GreenhouseUIClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenhouseUIClient.Services
{
    public interface IEntityService<TEntity>
            where TEntity : BaseEntity
    {
        IEnumerable<TEntity> Get();
        void ChangeStateCommand(TEntity entity);
        void UpdateState();
    }
}
