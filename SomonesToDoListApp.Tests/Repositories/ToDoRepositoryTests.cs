using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.DataAccessLayer.ValueObjects;
using System.Linq;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.Tests.Extensions;
using Xunit;

namespace SomeonesToDoListApp.Tests.Repositories
{
    public class ToDoRepositoryTests
    {
        private readonly ToDo _toDo;
        private readonly Mock<SomeonesToDoListContext> _context;

        private readonly ToDoRepository _sut;

        public ToDoRepositoryTests()
        {
            _toDo = new ToDo(Guid.NewGuid(), new ToDoTitle("Get a new bike"), string.Empty, DateTime.UtcNow, Guid.NewGuid().ToString());
            _context = new Mock<SomeonesToDoListContext>();

            _sut = new ToDoRepository(_context.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ToDoExists_ReturnsToDo()
        {
            // Arrange
            var toDos = new[] { _toDo }.AsQueryable();
            var mockSet = new Mock<DbSet<ToDo>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<CancellationToken>(), _toDo.Id))
                .Returns(Task.FromResult(_toDo));

            var mockToDoSet = EntityFrameworkExtensions.SetupMockSetAsync(mockSet, toDos);
            _context.Setup(s => s.ToDos).Returns(mockToDoSet.Object);
            
            // Act
            var toDo = await _sut.GetByIdAsync(_toDo.Id, CancellationToken.None);

            // Arrange
            toDo.ShouldNotBeNull();
            toDo.Id.ShouldBe(_toDo.Id);
            toDo.Title.ShouldBe(_toDo.Title);
            toDo.Description.ShouldBe(_toDo.Description);
            toDo.CreatedAt.ShouldBe(_toDo.CreatedAt);
            toDo.CreatedBy.ShouldBe(_toDo.CreatedBy);
        }

        [Fact]
        public async Task GetByIdAsync_ToDoDoesNotExist_ReturnsNull()
        {
            // Arrange
            var toDos = Array.Empty<ToDo>().AsQueryable();
            var mockToDoSet = EntityFrameworkExtensions.SetupMockSetAsync(new Mock<DbSet<ToDo>>(), toDos);
            _context.Setup(s => s.ToDos).Returns(mockToDoSet.Object);

            // Act
            var toDo = await _sut.GetByIdAsync(_toDo.Id, CancellationToken.None);

            // Arrange
            toDo.ShouldBeNull();
        }
    }
}