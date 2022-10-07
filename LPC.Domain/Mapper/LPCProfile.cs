using AutoMapper;
using LPC.Domain.Queries;

namespace LPC.Domain.Mapper;

public class LPCProfile : Profile
{
    public LPCProfile()
    {
        CreateMap<LPC.Contracts.Database.Record, LPC.Domain.Queries.GetCollectionQueryResult>();
        CreateMap<LPC.Contracts.Database.Record, LPC.Domain.Queries.GetAllRecordsQueryResult>();
    }
}