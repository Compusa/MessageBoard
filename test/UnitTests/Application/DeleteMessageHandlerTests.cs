﻿using MessageBoard.Application.Messages.Commands;
using MessageBoard.Application.SeedWork.Results.StatusCodes;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Domain.SeedWork;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Application
{
    public class DeleteMessageHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        private readonly Mock<IBoardMessageRepository> _mockedRepository;

        public DeleteMessageHandlerTests()
        {
            _mockedUnitOfWork = new Mock<IUnitOfWork>();
            _mockedRepository = new Mock<IBoardMessageRepository>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public async Task Should_fail_with_status_bad_request_when_client_id_is_null_or_empty(string clientId)
        {
            // Arrange
            var command = new DeleteMessageCommand(1, clientId);
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);
        }

        [Fact]
        public async Task Should_fail_with_status_not_found_when_deleting_message_that_does_not_exist()
        {
            // Arrange
            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((BoardMessage)null);

            var command = new DeleteMessageCommand(1, Guid.NewGuid().ToString());
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<NotFound>(result.StatusCode);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
        }

        [Fact]
        public async Task Should_fail_with_status_bad_request_when_attempting_to_delete_message_created_by_another_client()
        {
            // Arrange
            var message = MockedMessageBuilder
                .SetId(1)
                .SetMessage("The message")
                .SetClientId(Guid.NewGuid().ToString())
                .Build()
                .Object;

            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(message);

            var command = new DeleteMessageCommand(message.Id, Guid.NewGuid().ToString());
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Failed);
            Assert.IsType<BadRequest>(result.StatusCode);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
        }

        [Fact]
        public async Task Should_succeed_with_status_deleted_when_delete_is_valid()
        {
            // Arrange
            var message = MockedMessageBuilder
                .SetId(1)
                .SetMessage("The message")
                .SetClientId(Guid.NewGuid().ToString())
                .Build()
                .Object;

            _mockedRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(message);
            _mockedRepository.Setup(x => x.UnitOfWork).Returns(_mockedUnitOfWork.Object);
            _mockedUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new DeleteMessageCommand(message.Id, message.ClientId);
            var handler = new DeleteMessageCommandHandler(_mockedRepository.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.Succeeded);
            Assert.IsType<Deleted>(result.StatusCode);

            _mockedRepository.Verify(x => x.GetAsync(command.MessageId), Times.Once);
            _mockedRepository.Verify(x => x.Remove(message), Times.Once);
            _mockedUnitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}
