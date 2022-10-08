using AutoMapper;
using LPC.Domain.Helpers.Interfaces;
using LPC.Domain.Mapper;
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
        var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new LPCProfile());
                });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
        _lpCollectionService = new Mock<ILPCollectionService>();
        _handler = new GetAllRecordsQueryHandler(_mapper, _lpCollectionService.Object);
    }

    [Fact]
    public async Task AllRecordsShouldBeRetrievedFromDb()
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