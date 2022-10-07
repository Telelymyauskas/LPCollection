using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Linq;
using LPC.Domain.Helpers.Interfaces;

namespace LPC.Domain.Queries;

public class GetCollectionQuery : IRequest<List<GetCollectionQueryResult>>
{
    public string CollectionType { get; set; }
}
public class GetCollectionQueryResult
{
    public string Artist { get; init; }
    public string Album { get; init; }
    public string ImgURL { get; init; }
}
public class GetCollectionQueryHandler : IRequestHandler<GetCollectionQuery, List<GetCollectionQueryResult>>
{
    private readonly IMapper _lpcMapper;
    private readonly ILPCollectionService _lpCollectionService;
    public List<GetCollectionQueryResult> ResultList { get; set; }
    public GetCollectionQueryHandler(IMapper lpcMapper, ILPCollectionService lpCollectionService)
    {
        _lpCollectionService = lpCollectionService;
        _lpcMapper = lpcMapper;
        ResultList = new List<GetCollectionQueryResult>();
    }

    public async Task<List<GetCollectionQueryResult>> Handle(GetCollectionQuery request, CancellationToken cancellationToken)
    {
        if (request.CollectionType == "wishlist")
        {
            var recordsInWishlist = await _lpCollectionService.GetRecordsInWishlist(cancellationToken);
            ResultList = recordsInWishlist
                .Select(x => _lpcMapper.Map<GetCollectionQueryResult>(x.Record))
                .ToList();
        }
        if (request.CollectionType == "library")
        {
            var recordsInLibrary = await _lpCollectionService.GetRecordsInLibrary(cancellationToken);
            ResultList = recordsInLibrary
                .Select(x => _lpcMapper.Map<GetCollectionQueryResult>(x.Record))
                .ToList();
        }
        return ResultList;
    }
}