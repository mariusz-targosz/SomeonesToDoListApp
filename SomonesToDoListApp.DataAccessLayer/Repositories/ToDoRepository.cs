using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Models;
using SomeonesToDoListApp.DataAccessLayer.Specifications;

namespace SomeonesToDoListApp.DataAccessLayer.Repositories
{
    public interface IToDoRepository
    {
        Task AddAsync(ToDo toDo, CancellationToken cancellationToken);
        Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken);
        Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken);
        Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<ToDo>> GetAllAsync(Specification<ToDo> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }

    public class ToDoRepository : IToDoRepository
    {
        private readonly SomeonesToDoListContext _someonesToDoListContext;

        public ToDoRepository(SomeonesToDoListContext someonesToDoListContext)
        {
            _someonesToDoListContext = someonesToDoListContext;
        }

        public async Task AddAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            _someonesToDoListContext.ToDos.Add(toDo);
            await _someonesToDoListContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            await _someonesToDoListContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken)
        {
            _someonesToDoListContext.ToDos.Remove(toDo);
            await _someonesToDoListContext.SaveChangesAsync(cancellationToken);
        }

        public Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _someonesToDoListContext.ToDos.FindAsync(cancellationToken, id);
        }

        public async Task<PagedResult<ToDo>> GetAllAsync(Specification<ToDo> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var query = _someonesToDoListContext.ToDos
                .AsNoTracking()
                .Where(specification.ToExpression());

            var total = query.Count();
            if (total == 0)
                return new PagedResult<ToDo>(0, Enumerable.Empty<ToDo>());

            var skip = (pageNumber - 1) * pageSize;
            var result = query.OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .AsEnumerable();

            return new PagedResult<ToDo>(total, result);
        }
    }
}