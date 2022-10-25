using System.Web.Http;
using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.Services;
using SomeonesToDoListApp.Services.Services;

namespace SomeonesToDoListApp.Controllers
{
    [Authorize]
    [RoutePrefix(Routes.ToDoListApi)]
    public partial class ToDoController : ApiController
	{
        private readonly IToDoFactory _toDoFactory;
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ToDoController(IToDoFactory toDoFactory, IToDoRepository toDoRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _toDoFactory = toDoFactory;
            _toDoRepository = toDoRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
    }
}