using System;
using NSubstitute;
using Shouldly;
using SomeonesToDoListApp.Services.Services;
using Xunit;

namespace SomeonesToDoListApp.Tests.Services
{
    public class ToDoFactoryTests
    {
        private const string Title = "Buy a book";
        private const string Description = "Buy a blue book from amazon";

        private readonly Guid _createdBy = Guid.NewGuid();

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly ToDoFactory _sut;

        public ToDoFactoryTests()
        {
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _sut = new ToDoFactory(_dateTimeProvider);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        public void Create_InvalidTitle_ThrowsException(string title)
        {
            // Act
            var exception = Record.Exception(() => _sut.Create(title, Description, _createdBy));

            // Assert
            exception.ShouldNotBeNull();
        }

        [Fact]
        public void Create_InvalidCreatedBy_ThrowsException()
        {
            // Act
            var exception = Record.Exception(() => _sut.Create(Title, Description, Guid.Empty));

            // Assert
            exception.ShouldNotBeNull();
        }

        [Fact]
        public void Create_EmptyDescription_ReturnsToDo()
        {
            // Act
            var toDo = _sut.Create(Title, string.Empty, _createdBy);

            // Assert
            toDo.ShouldNotBeNull();
            toDo.Description.ShouldBe(string.Empty);
        }

        [Fact]
        public void Create_ValidData_ReturnsToDo()
        {
            // Arrange
            var now = DateTime.UtcNow;

            _dateTimeProvider
                .NowUtc
                .Returns(now);

            // Act
            var toDo = _sut.Create(Title, Description, _createdBy);

            // Assert
            toDo.ShouldNotBeNull();
            toDo.Id.ShouldNotBe(Guid.Empty);
            toDo.Title.ShouldBe(Title);
            toDo.Description.ShouldBe(Description);
            toDo.CreatedBy.ShouldBe(_createdBy);
            toDo.CreatedAt.ShouldBe(now);
        }
    }
}
