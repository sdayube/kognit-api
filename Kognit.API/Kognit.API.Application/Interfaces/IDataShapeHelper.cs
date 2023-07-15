using Kognit.API.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces
{
    public interface IDataShapeHelper<T> where T : class
    {
        IEnumerable<DynamicEntity<T>> ShapeData(IEnumerable<T> entities, string fieldsString);

        Task<IEnumerable<DynamicEntity<T>>> ShapeDataAsync(IEnumerable<T> entities, string fieldsString);

        DynamicEntity<T> ShapeData(T entity, string fieldsString);
    }
}