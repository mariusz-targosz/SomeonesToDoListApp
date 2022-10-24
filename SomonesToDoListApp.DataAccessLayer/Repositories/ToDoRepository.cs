using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Specifications;

namespace SomeonesToDoListApp.DataAccessLayer.Repositories
{
    public interface IToDoRepository
    {
        Task AddAsync(ToDo toDo, CancellationToken cancellationToken);
        Task UpdateAsync(ToDo toDo, CancellationToken cancellationToken);
        Task DeleteAsync(ToDo toDo, CancellationToken cancellationToken);
        Task<ToDo> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ToDo>> GetAllAsync(Specification<ToDo> specification, CancellationToken cancellationToken);
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

        public async Task<IEnumerable<ToDo>> GetAllAsync(Specification<ToDo> specification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return _someonesToDoListContext.ToDos
                .AsNoTracking()
                // TODO: Uncomment
                //.Where(specification.ToExpression())
                .AsEnumerable();
        }
    }
}