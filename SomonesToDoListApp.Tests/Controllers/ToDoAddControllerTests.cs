using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using NSubstitute;
using Shouldly;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using SomeonesToDoListApp.Requests;
using SomeonesToDoListApp.Services.Services;
using SomeonesToDoListApp.Tests.Fakes;
using Xunit;

namespace SomeonesToDoListApp.Tests.Controllers
{
    public class ToDoAddControllerTests
    {
        private readonly ToDo _toDo;
        private readonly ToDoAddRequest _toDoAddRequest;

        private readonly IToDoRepository _toDoRepository;

        private readonly ToDoController _sut;

        public ToDoAddControllerTests()
        {
            _toDo = new ToDo(Guid.NewGuid(), new ToDoTitle("Buy milk"), "Remember to buy it twice", DateTime.UtcNow, Guid.NewGuid());
            _toDoAddRequest = new ToDoAddRequest(_toDo.Title.ToString(), _toDo.Description);
            var toDoResponse = new ToDoResponse(_toDo.Id, _toDo.Title.ToString(), _toDo.Description);

            var toDoFactory = Substitute.For<IToDoFactory>();
            toDoFactory.Create(_toDoAddRequest.Title, _toDoAddRequest.Description, Arg.Any<Guid>())
                .Returns(_toDo);

            _toDoRepository = new ToDoInMemoryRepository();

            var mapper = Substitute.For<IMapper>();
            mapper.Map<ToDoResponse>(_toDo)
                .Returns(toDoResponse);

            _sut = new ToDoController(toDoFactory, _toDoRepository, mapper);
        }

        [Fact]
        public async Task AddAsync_WhenCalled_ReturnsToDo()
        {
            // Act
            var result = await _sut.AddAsync(_toDoAddRequest, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CreatedNegotiatedContentResult<ToDoResponse>>();

            var response = result as CreatedNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;
            toDoResponse.ShouldNotBeNull();
            toDoResponse.Id.ShouldNotBe(Guid.Empty);
            toDoResponse.Title.ShouldBe(_toDo.Title.ToString());
            toDoResponse.Description.ShouldBe(_toDo.Description);
        }

        [Fact]
        public async Task AddAsync_WhenCalled_AddsToDo()
        {
            // Act
            var result = await _sut.AddAsync(_toDoAddRequest, CancellationToken.None);

            // Assert
            var response = result as CreatedNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;

            var toDo = await _toDoRepository.GetByIdAsync(toDoResponse.Id, CancellationToken.None);
            toDo.ShouldNotBeNull();
            toDo.Id.ShouldNotBe(Guid.Empty);
            toDo.Title.Value.ShouldBe(_toDoAddRequest.Title);
            toDo.Description.ShouldBe(_toDoAddRequest.Description);
        }

        [Fact]
        public async Task AddAsync_WhenCalled_SetsLocationHeader()
        {
            // Act
            var result = await _sut.AddAsync(_toDoAddRequest, CancellationToken.None);

            // Assert
            var response = result as CreatedNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;
            response.Location.ToString().ShouldBe($"api/todos/{toDoResponse.Id}");
        }
    }
}