using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using SomeonesToDoListApp.Models;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> AddAsync([FromBody] ToDoAddRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest();

            var toDo = _toDoFactory.Create(request.Title, request.Description, _currentUserService.UserId);
            await _toDoRepository.AddAsync(toDo, cancellationToken);

            var toDoResponse = _mapper.Map<ToDoResponse>(toDo);

            return Created($"{Routes.ToDoListApi}/{toDoResponse.Id}", toDoResponse);
        }
    }
}