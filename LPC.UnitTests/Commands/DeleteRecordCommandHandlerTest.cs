using System;
using System.Threading;
using System.Threading.Tasks;
using LPC.Domain.Commands;
using LPC.Domain.Helpers.Interfaces;
using MediatR;
using Moq;
using Shouldly;

namespace LPC.UnitTests.Commands;

public class DeleteRecordCommandHandlerTest
{
    private readonly Mock<ILPCollectionService> _lpCollectionService;
    private readonly IRequestHandler<DeleteRecordCommand, DeleteRecordCommandResult> _handler;

    public DeleteRecordCommandHandlerTest()
    {
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new DeleteRecordCommandHandler(_lpCollectionService.Object);
    }

    [Fact]
    public async Task DeleteRecordShouldDelete()
    {
        //Arrange
        var command = new DeleteRecordCommand
        {
            Id = new Random().Next(),
            CollectionType = Guid.NewGuid().ToString()
        };

        _lpCollectionService.Setup(x => x.DeleteRecord(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));

        //Act 
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<DeleteRecordCommandResult>();
        result.DeleteIsSuccessful.ShouldBe(true);
    }

    [Fact]
    public async Task DeleteRecordShouldNotDelete()
    {
        //Arrange
        var command = new DeleteRecordCommand
        {
            Id = new Random().Next(),
            CollectionType = Guid.NewGuid().ToString()
        };

        _lpCollectionService.Setup(x => x.DeleteRecord(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(false));

        //Act 
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.ShouldBeOfType<DeleteRecordCommandResult>();
        result.DeleteIsSuccessful.ShouldBe(false);
    }
}