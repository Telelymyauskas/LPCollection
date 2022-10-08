using AutoMapper;
using LPC.Domain.Mapper;

namespace LPC.UnitTests.Helpers;

public static class MapperCreator
{
    public static IMapper CreateMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
                   {
                       mc.AddProfile(new LPCProfile());
                   });
        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }
}