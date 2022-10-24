using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using SomeonesToDoListApp.DataAccessLayer.Specifications;
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
            // TODO: Replace with CreatedBy
            var toDosCreatedByUserSpecification = new ToDosCreatedByUserSpecification(Guid.NewGuid());
            var toDoCollection = await _toDoRepository.GetAllAsync(toDosCreatedByUserSpecification, cancellationToken);
            var orderedToDoCollection = toDoCollection.OrderByDescending(x => x.CreatedAt).ToArray();
            var toDoResponseCollection = _mapper.Map<IReadOnlyList<ToDoResponse>>(orderedToDoCollection.ToArray());

            return Ok(toDoResponseCollection);
        }
    }
}