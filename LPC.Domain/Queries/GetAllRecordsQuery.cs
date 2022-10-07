using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Linq;
using LPC.Domain.Helpers.Interfaces;

namespace LPC.Domain.Queries;

public class GetAllRecordsQuery : IRequest<List<GetAllRecordsQueryResult>>
{
    public int Id { get; set; }
}

public class GetAllRecordsQueryResult
{
    public int Id { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string ImgURL { get; set; }
}
public class GetAllRecordsQueryHandler : IRequestHandler<GetAllRecordsQuery, List<GetAllRecordsQueryResult>>
{
    private readonly IMapper _lpcMapper;
    private readonly ILPCollectionService _lpCollectionService;
    public List<GetAllRecordsQueryResult> ResultList { get; set; }
    public GetAllRecordsQueryHandler(IMapper lpcMapper, ILPCollectionService lpCollectionService)
    {
        _lpcMapper = lpcMapper;
        _lpCollectionService = lpCollectionService;
        ResultList = new List<GetAllRecordsQueryResult>();
    }

    public async Task<List<GetAllRecordsQueryResult>> Handle(GetAllRecordsQuery request, CancellationToken cancellationToken)
    {
        var allRecords = await _lpCollectionService.GetAllRecords(cancellationToken);
        ResultList = allRecords
            .Select(x => _lpcMapper.Map<GetAllRecordsQueryResult>(x))
            .ToList();
        return ResultList;
    }
}