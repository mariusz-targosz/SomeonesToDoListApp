using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Net;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using SomeonesToDoListApp.Requests;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateAsync([FromUri] Guid id, [FromBody] ToDoUpdateRequest request, CancellationToken cancellationToken)
        {
            var toDo = await _toDoRepository.GetByIdAsync(id, cancellationToken);
            if (toDo == null)
            {
                // TODO: Replace CreatedBy
                toDo = _toDoFactory.Create(request.Title, request.Description, Guid.NewGuid());
                await _toDoRepository.AddAsync(toDo, cancellationToken);

                var toDoResponse = _mapper.Map<ToDoResponse>(toDo);

                return Created($"{Routes.ToDoListApi}/{toDoResponse.Id}", toDoResponse);
            }

            var title = new ToDoTitle(request.Title);
            toDo.Update(title, request.Description);
            await _toDoRepository.UpdateAsync(toDo, cancellationToken);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}