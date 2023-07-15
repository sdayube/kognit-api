using Kognit.API.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces
{
    public interface IDataShapeHelper<T>
    {
        IEnumerable<DynamicEntity> ShapeData(IEnumerable<T> entities, string fieldsString);

        Task<IEnumerable<DynamicEntity>> ShapeDataAsync(IEnumerable<T> entities, string fieldsString);

        DynamicEntity ShapeData(T entity, string fieldsString);
    }
}