using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Net;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var todo = await _toDoRepository.GetByIdAsync(id, cancellationToken);
            if (todo == null)
                return NotFound();

            await _toDoRepository.DeleteAsync(todo, cancellationToken);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}