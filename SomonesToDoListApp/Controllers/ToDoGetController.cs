using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using SomeonesToDoListApp.Requests;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var todo = await _toDoRepository.GetByIdAsync(id, cancellationToken);
            if (todo == null)
                return NotFound();

            var toDoViewModel = _mapper.Map<ToDoResponse>(todo);
            return Ok(toDoViewModel);
        }
    }
}