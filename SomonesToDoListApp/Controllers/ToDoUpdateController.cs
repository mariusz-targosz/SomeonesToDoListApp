using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Net;
using SomeonesToDoListApp.Requests;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpPut]
        [Route("{id}")]
        [ActionName("Update")]
        public async Task<IHttpActionResult> UpdateAsync([FromUri] Guid id, [FromBody] ToDoUpdateRequest request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);
            if (toDo == null)
            {
                // TODO: Replace CreatedBy
                toDo = _toDoFactory.Create(request.Title, request.Description, Guid.NewGuid());
                await _toDoRepository.AddAsync(toDo, cancellationToken);

                return CreatedAtRoute(nameof(GetByIdAsync), toDo.Id, toDo);
            }

            toDo.Update(request.Title, request.Description);
            await _toDoRepository.UpdateAsync(toDo, cancellationToken);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}