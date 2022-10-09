using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LPC.Contracts.Database;
using LPC.Domain.Helpers.Interfaces;
using LPC.Domain.Mapper;
using LPC.Domain.Queries;
using LPC.UnitTests.Helpers;
using MediatR;
using Moq;
using Shouldly;


namespace LPC.UnitTests.Queries;

public class GetCollectionQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILPCollectionService> _lpCollectionService;
    private readonly IRequestHandler<GetCollectionQuery, List<GetCollectionQueryResult>> _handler;

    public GetCollectionQueryHandlerTest()
    {
        _mapper = MapperCreator.CreateMapper();
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new GetCollectionQueryHandler(_mapper, _lpCollectionService.Object);
    }

    [Fact]
    public async Task RecordsFromWishlistShouldBeFoundInDb()
    {
        //Arrange 
        List<Wishlist> TestRecords = new List<Wishlist>()
        {
            new Wishlist
            {
                Id = new Random().Next(),
                RecordWished = new Random().Next()
            }
        };

        _lpCollectionService.Setup(x => x.GetRecordsInWishlist(It.IsAny<CancellationToken>())).Returns(Task.FromResult(TestRecords));

        //Act
        var result = await _handler.Handle(new GetCollectionQuery(), CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task RecordsFromLibraryShouldBeFoundInDb()
    {
        //Arrange 
        List<Library> TestRecords = new List<Library>()
        {
            new Library
            {
                Id = new Random().Next(),
                RecordOwned = new Random().Next()
            }
        };

        _lpCollectionService.Setup(x => x.GetRecordsInLibrary(It.IsAny<CancellationToken>())).Returns(Task.FromResult(TestRecords));

        //Act
        var result = await _handler.Handle(new GetCollectionQuery(), CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
    }
}

