using AutoMapper;
using LPC.Domain.Helpers.Interfaces;
using LPC.UnitTests.Helpers;
using LPC.Domain.Queries;
using MediatR;
using Moq;
using Shouldly;
using LpRecord = LPC.Contracts.Database.Record;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace LPC.UnitTests.Queries;

public class GetAllRecordsQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILPCollectionService> _lpCollectionService;
    private readonly IRequestHandler<GetAllRecordsQuery, List<GetAllRecordsQueryResult>> _handler;

    public GetAllRecordsQueryHandlerTest()
    {
        _mapper = MapperCreator.CreateMapper();
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new GetAllRecordsQueryHandler(_mapper, _lpCollectionService.Object);
    }

    [Fact]
    public async Task AllRecordsShouldBeFoundInDb()
    {
        //Arrange 
        List<LpRecord> testRecords = new List<LpRecord>()
        {
            new LpRecord
            {
                Id = new Random().Next(),
                Artist = Guid.NewGuid().ToString(),
                Album = Guid.NewGuid().ToString(),
                ImgURL = Guid.NewGuid().ToString()
            }
        };

        _lpCollectionService.Setup(x => x.GetAllRecords(It.IsAny<CancellationToken>())).Returns(Task.FromResult(testRecords));

        // Act
        var result = await _handler.Handle(new GetAllRecordsQuery(), CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
        result[0].ShouldBeOfType<GetAllRecordsQueryResult>();
        result[0].Id.ShouldBe(testRecords[0].Id);
        result[0].Artist.ShouldBe(testRecords[0].Artist);
        result[0].Album.ShouldBe(testRecords[0].Album);
        result[0].ImgURL.ShouldBe(testRecords[0].ImgURL);
    }
}
