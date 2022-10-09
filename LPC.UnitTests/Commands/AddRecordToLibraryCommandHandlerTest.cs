using LPC.Domain.Helpers.Interfaces;
using MediatR;
using Moq;
using Shouldly;
using LPC.Domain.Commands;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using System;
using System.Threading;

namespace LPC.UnitTests.Commands;

public class AddRecordToLibraryCommandHandlerTest
{
    private readonly Mock<ILPCollectionService> _lpCollectionService;
    private readonly Mock<IValidationHelper> _vaildatorHelper;
    private readonly IRequestHandler<AddRecordToLibraryCommand, int> _handler;

    public AddRecordToLibraryCommandHandlerTest()
    {
        _vaildatorHelper = new Mock<IValidationHelper>();
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new AddRecordToLibraryCommandHandler(_lpCollectionService.Object, _vaildatorHelper.Object);
    }

    [Fact]
    public async Task RecourdShouldBeAddedToLibrary()
    {
        //Arrange
        Library testLibrary = new Library
        {
            Id = new Random().Next(),
            RecordOwned = new Random().Next()
        };

        _vaildatorHelper.Setup(x => x.ValidateRecordDuplication(It.IsAny<int>())).Returns(Task.FromResult(true));

        _lpCollectionService.Setup(x => x.AddRecordToCollection(It.IsAny<Library>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(testLibrary.Id));

        var request = new AddRecordToLibraryCommand
        {
            Id = testLibrary.RecordOwned
        };
        //Act 
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        _vaildatorHelper.Verify(x => x.ValidateRecordDuplication(testLibrary.RecordOwned), Times.Once());
    }

    [Fact]
    public async Task RecourdShouldNotBeAddedToLibrary()
    {
        //Arrange
        Library testLibrary = new Library
        {
            Id = new Random().Next(),
            RecordOwned = new Random().Next()
        };

        _vaildatorHelper.Setup(x => x.ValidateRecordDuplication(It.IsAny<int>())).Returns(Task.FromResult(false));

        _lpCollectionService.Setup(x => x.AddRecordToCollection(It.IsAny<Library>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(testLibrary.Id));

        var request = new AddRecordToLibraryCommand
        {
            Id = testLibrary.RecordOwned
        };
        //Act 
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        _vaildatorHelper.Verify(x => x.ValidateRecordDuplication(testLibrary.RecordOwned), Times.Once());
        result.ShouldBe(0);
    }
}