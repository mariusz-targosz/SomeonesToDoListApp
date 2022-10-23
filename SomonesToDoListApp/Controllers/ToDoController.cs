using System.Web.Http;
using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.Services.Services;

namespace SomeonesToDoListApp.Controllers
{
    [RoutePrefix(Routes.ToDoApi)]
    public partial class ToDoController : ApiController
	{
        private readonly IToDoFactory _toDoFactory;
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;

        public ToDoController(IToDoFactory toDoFactory, IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoFactory = toDoFactory;
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }
    }
}