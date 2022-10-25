using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using SomeonesToDoListApp.DataAccessLayer.Specifications;
using SomeonesToDoListApp.Models;

namespace SomeonesToDoListApp.Controllers
{
    public partial class ToDoController
    {
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> ListAsync([FromUri] ToDoListRequest toDoListRequest, CancellationToken cancellationToken)
        {
            var toDosCreatedByUserSpecification = new ToDosCreatedByUserSpecification(_currentUserService.UserId);
            var pagedResult = await _toDoRepository.GetAllAsync(toDosCreatedByUserSpecification, toDoListRequest.PageNumber, toDoListRequest.PageSize, cancellationToken);
            var toDoResponseCollection = _mapper.Map<IReadOnlyList<ToDoResponse>>(pagedResult.Items.ToArray());
            var toDoListResponse = new ToDoListResponse(toDoListRequest.PageSize, toDoListRequest.PageNumber, pagedResult.TotalCount, toDoResponseCollection);

            return Ok(toDoListResponse);
        }
    }
}