using AutoMapper;
using NSubstitute;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using SomeonesToDoListApp.Requests;
using SomeonesToDoListApp.Services.Services;
using SomeonesToDoListApp.Tests.Fakes;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Shouldly;
using SomeonesToDoListApp.Services;
using Xunit;

namespace SomeonesToDoListApp.Tests.Controllers
{
    public class ToDoGetControllerTests
    {
        private readonly ToDo _toDo;

        private readonly IToDoRepository _toDoRepository;

        private readonly ToDoController _sut;

        public ToDoGetControllerTests()
        {
            var createdBy = Guid.NewGuid().ToString();
            _toDo = new ToDo(Guid.NewGuid(), new ToDoTitle("Buy milk"), "Remember to buy it twice", DateTime.UtcNow, Guid.NewGuid().ToString());
            var toDoResponse = new ToDoResponse(_toDo.Id, _toDo.Title.ToString(), _toDo.Description);

            var toDoFactory = Substitute.For<IToDoFactory>();
            _toDoRepository = new ToDoInMemoryRepository();

            var mapper = Substitute.For<IMapper>();
            mapper.Map<ToDoResponse>(_toDo)
                .Returns(toDoResponse);

            var currentUserService = Substitute.For<ICurrentUserService>();
            currentUserService.UserId.Returns(createdBy);

            _sut = new ToDoController(toDoFactory, _toDoRepository, mapper, currentUserService);
        }

        [Fact]
        public async Task GetByIdAsync_ToDoExists_ReturnsToDo()
        {
            // Arrange
            await _toDoRepository.AddAsync(_toDo, CancellationToken.None);

            // Act
            var result = await _sut.GetByIdAsync(_toDo.Id, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<OkNegotiatedContentResult<ToDoResponse>>();

            var response = result as OkNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;
            toDoResponse.ShouldNotBeNull();
            toDoResponse.Id.ShouldBe(_toDo.Id);
            toDoResponse.Title.ShouldBe(_toDo.Title.ToString());
            toDoResponse.Description.ShouldBe(_toDo.Description);
        }

        [Fact]
        public async Task GetByIdAsync_ToDoDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = await _sut.GetByIdAsync(_toDo.Id, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
