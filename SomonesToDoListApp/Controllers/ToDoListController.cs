using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using SomeonesToDoListApp.Requests;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        // TODO: Add pagination
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> ListAsync(CancellationToken cancellationToken)
        {
            var toDoCollection = await _toDoRepository.GetAllAsync(Guid.NewGuid(), cancellationToken);
            var orderedToDoCollection = toDoCollection.OrderByDescending(x => x.CreatedAt).ToArray();
            var toDoResponseCollection = _mapper.Map<IReadOnlyList<ToDoResponse>>(orderedToDoCollection.ToArray());

            return Ok(toDoResponseCollection);
        }
    }
}