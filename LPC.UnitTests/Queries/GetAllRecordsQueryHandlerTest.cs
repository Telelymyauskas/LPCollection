using AutoMapper;
using LPC.Domain.Helpers.Interfaces;
using LPC.UnitTests.Helpers;
using LPC.Domain.Queries;
using MediatR;
using Moq;
using Shouldly;
using LpRecord = LPC.Contracts.Database.Record;

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
        List<LpRecord> TestRecords = new List<LpRecord>()
        {
            new LpRecord
            {
                Id = new Random().Next(),
                Artist = Guid.NewGuid().ToString(),
                Album = Guid.NewGuid().ToString(),
                ImgURL = Guid.NewGuid().ToString()
            }
        };

        _lpCollectionService.Setup(x => x.GetAllRecords(It.IsAny<CancellationToken>())).Returns(Task.FromResult(TestRecords));

        // Act
        var result = await _handler.Handle(new GetAllRecordsQuery(), CancellationToken.None);

        //Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
    }

}
