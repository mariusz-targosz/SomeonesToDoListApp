using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services.ViewModels;

namespace SomeonesToDoListApp.Services.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<ToDoMappingProfile>();
            });
        }
    }
    
    public class ToDoMappingProfile : Profile
    {
        public ToDoMappingProfile()
        {
            CreateMap<ToDo, ToDoViewModel>();
        }
    }
}
