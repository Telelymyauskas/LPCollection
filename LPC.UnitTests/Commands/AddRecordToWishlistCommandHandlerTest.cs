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

public class AddRecordToWishlistCommandHandlerTest
{
    private readonly Mock<ILPCollectionService> _lpCollectionService;
    private readonly Mock<IValidationHelper> _vaildatorHelper;
    private readonly IRequestHandler<AddRecordToWishlistCommand, int> _handler;

    public AddRecordToWishlistCommandHandlerTest()
    {
        _vaildatorHelper = new Mock<IValidationHelper>();
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new AddRecordToWishlistCommandHandler(_lpCollectionService.Object, _vaildatorHelper.Object);
    }

    [Fact]
    public async Task RecourdShouldBeAddedToWishlist()
    {
        //Arrange
        Wishlist testWishlist = new Wishlist
        {
            Id = new Random().Next(),
            RecordWished = new Random().Next()
        };

        _vaildatorHelper.Setup(x => x.ValidateRecordDuplication(It.IsAny<int>())).Returns(Task.FromResult(true));

        _lpCollectionService.Setup(x => x.AddRecordToCollection(It.IsAny<Wishlist>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(testWishlist.Id));

        var request = new AddRecordToWishlistCommand
        {
            Id = testWishlist.RecordWished
        };
        //Act 
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        _vaildatorHelper.Verify(x => x.ValidateRecordDuplication(testWishlist.RecordWished), Times.Once());
    }

    [Fact]
    public async Task RecourdShouldNotBeAddedToWishlist()
    {
        //Arrange
        Wishlist testWishlist = new Wishlist
        {
            Id = new Random().Next(),
            RecordWished = new Random().Next()
        };

        _vaildatorHelper.Setup(x => x.ValidateRecordDuplication(It.IsAny<int>())).Returns(Task.FromResult(false));

        _lpCollectionService.Setup(x => x.AddRecordToCollection(It.IsAny<Wishlist>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(testWishlist.Id));

        var request = new AddRecordToWishlistCommand
        {
            Id = testWishlist.RecordWished
        };
        //Act 
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        _vaildatorHelper.Verify(x => x.ValidateRecordDuplication(testWishlist.RecordWished), Times.Once());
        result.ShouldBe(0);
    }
}