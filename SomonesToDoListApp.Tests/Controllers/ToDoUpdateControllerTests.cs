using AutoMapper;
using NSubstitute;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.Services.Services;
using SomeonesToDoListApp.Tests.Fakes;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.Results;
using System;
using System.Net;
using Shouldly;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using Xunit;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using SomeonesToDoListApp.Models;
using SomeonesToDoListApp.Services;

namespace SomeonesToDoListApp.Tests.Controllers
{
    public class ToDoUpdateControllerTests
    {
        private readonly ToDo _toDo;
        private readonly ToDoUpdateRequest _toDoUpdateRequest;

        private readonly IToDoRepository _toDoRepository;

        private readonly ToDoController _sut;

        public ToDoUpdateControllerTests()
        {
            var createdBy = Guid.NewGuid().ToString();
            _toDo = new ToDo(Guid.NewGuid(), new ToDoTitle("Buy milk"), "Remember to buy it twice", DateTime.UtcNow, Guid.NewGuid().ToString());
            _toDoUpdateRequest = new ToDoUpdateRequest(_toDo.Title.ToString(), _toDo.Description);
            var toDoResponse = new ToDoResponse(_toDo.Id, _toDo.Title.ToString(), _toDo.Description);

            var toDoFactory = Substitute.For<IToDoFactory>();
            toDoFactory.Create(_toDoUpdateRequest.Title, _toDoUpdateRequest.Description, createdBy)
                .Returns(_toDo);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<ToDoResponse>(_toDo)
                .Returns(toDoResponse);

            _toDoRepository = new ToDoInMemoryRepository();

            var currentUserService = Substitute.For<ICurrentUserService>();
            currentUserService.UserId.Returns(createdBy);

            _sut = new ToDoController(toDoFactory, _toDoRepository, mapper, currentUserService);
        }

        [Fact]
        public async Task UpdateAsync_ToDoDoesNotExist_ReturnsToDo()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _sut.UpdateAsync(id, _toDoUpdateRequest, CancellationToken.None);

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
        public async Task UpdateAsync_ToDoDoesNotExist_AddsToDo()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _sut.UpdateAsync(id, _toDoUpdateRequest, CancellationToken.None);

            // Assert
            var response = result as CreatedNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;

            var toDo = await _toDoRepository.GetByIdAsync(toDoResponse.Id, CancellationToken.None);
            toDo.ShouldNotBeNull();
            toDo.Id.ShouldNotBe(Guid.Empty);
            toDo.Title.Value.ShouldBe(_toDoUpdateRequest.Title);
            toDo.Description.ShouldBe(_toDoUpdateRequest.Description);
        }

        [Fact]
        public async Task UpdateAsync_ToDoDoesNotExist_SetsLocationHeader()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await _sut.UpdateAsync(id, _toDoUpdateRequest, CancellationToken.None);

            // Assert
            var response = result as CreatedNegotiatedContentResult<ToDoResponse>;
            response.ShouldNotBeNull();

            var toDoResponse = response.Content;
            response.Location.ToString().ShouldBe($"api/todos/{toDoResponse.Id}");
        }

        [Fact]
        public async Task UpdateAsync_ToDoExists_UpdatesToDo()
        {
            // Arrange
            const string newTitle = "New title";
            const string newDescription = "New description";

            await _toDoRepository.AddAsync(_toDo, CancellationToken.None);
            var toDoUpdateRequest = new ToDoUpdateRequest(newTitle, newDescription);

            // Act
            var _ = await _sut.UpdateAsync(_toDo.Id, toDoUpdateRequest, CancellationToken.None);

            // Assert
            var todo = await _toDoRepository.GetByIdAsync(_toDo.Id, CancellationToken.None);
            todo.ShouldNotBeNull();
            todo.Id.ShouldBe(_toDo.Id);
            todo.Title.ToString().ShouldBe(newTitle);
            todo.Description.ShouldBe(newDescription);
        }

        [Fact]
        public async Task UpdateAsync_ToDoExists_ReturnsNoContent()
        {
            // Arrange
            await _toDoRepository.AddAsync(_toDo, CancellationToken.None);
            var toDoUpdateRequest = new ToDoUpdateRequest("New title", "New description");

            // Act
            var result = await _sut.UpdateAsync(_toDo.Id, toDoUpdateRequest, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<StatusCodeResult>();

            var response = result as StatusCodeResult;
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
