using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Requests;

namespace SomeonesToDoListApp.Mappers
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            CreateMap<ToDo, ToDoResponse>();
        }
    }
}
