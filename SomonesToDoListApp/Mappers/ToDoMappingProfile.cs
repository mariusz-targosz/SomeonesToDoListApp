using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.ViewModels;

namespace SomeonesToDoListApp.Mappers
{
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            CreateMap<ToDo, ToDoViewModel>();
        }
    }
}
