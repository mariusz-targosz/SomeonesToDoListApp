using AutoMapper;
using NSubstitute;
using SomeonesToDoListApp.Controllers;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using SomeonesToDoListApp.Services.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.Results;
using Shouldly;
using Xunit;

namespace SomeonesToDoListApp.Tests.Controllers
{
    public class ToDoDeleteControllerTests
    {
        private readonly ToDo _toDo;

        private readonly IToDoRepository _toDoRepository;

        private readonly ToDoController _sut;

        public ToDoDeleteControllerTests()
        {
            _toDo = new ToDo(Guid.NewGuid(), new ToDoTitle("Buy milk"), "Remember to buy it twice", DateTime.UtcNow, Guid.NewGuid());

            _toDoRepository = Substitute.For<IToDoRepository>();
            var toDoFactory = Substitute.For<IToDoFactory>();
            var mapper = Substitute.For<IMapper>();

            _sut = new ToDoController(toDoFactory, _toDoRepository, mapper);
        }

        [Fact]
        public async Task DeleteAsync_ToDoExists_ReturnsNoContent()
        {
            // Arrange
            _toDoRepository.GetByIdAsync(_toDo.Id, CancellationToken.None)
                .Returns(_toDo);

            // Act
            var result = await _sut.DeleteAsync(_toDo.Id, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<StatusCodeResult>();

            var statusCodeResult = result as StatusCodeResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteAsync_ToDoDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = await _sut.DeleteAsync(_toDo.Id, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
