using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Models;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.DataAccessLayer.Specifications;

namespace SomeonesToDoListApp.Tests.Fakes
{
    public class ToDoInMemoryRepository : IToDoRepository
    {
        private readonly Dictionary<Guid, ToDo> _data = new Dictionary<Guid, ToDo>();

        public Task AddAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            _data[toDo.Id] = toDo;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            _data[toDo.Id] = toDo;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            _data.Remove(toDo.Id);
            return Task.CompletedTask;
        }

        public async Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return _data.TryGetValue(id, out var value) ? value : null;
        }

        public async Task<PagedResult<ToDo>> GetAllAsync(Specification<ToDo> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var query = _data.Values
                .Where(specification.IsSatisfiedBy)
                .ToList();

            var skip = (pageNumber - 1) * pageSize;
            var result = query.Skip(skip).Take(pageSize);

            return new PagedResult<ToDo>(query.Count, result);
        }
    }
}
