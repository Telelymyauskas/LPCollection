using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LPC.Domain.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LPC.Contracts.Database;
using System.Linq;

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
    private readonly LpcDbContext _dbContext;
    private readonly IMapper _lpcMapper;
    public List<GetCollectionQueryResult> ResultList { get; set; }
    public GetCollectionQueryHandler(LpcDbContext dbContext, IMapper lpcMapper)
    {
        _dbContext = dbContext;
        _lpcMapper = lpcMapper;
        ResultList = new List<GetCollectionQueryResult>();
    }

    public async Task<List<GetCollectionQueryResult>> Handle(GetCollectionQuery request, CancellationToken cancellationToken)
    {
        if (request.CollectionType == "wishlist")
        {
            var recordsInWishlist = await _dbContext.Wishlists.Include(x => x.Record).ToListAsync();
            ResultList = recordsInWishlist
                .Select(x => _lpcMapper.Map<GetCollectionQueryResult>(x.Record))
                .ToList();
        }
        if (request.CollectionType == "library")
        {
            var recordsInLibrary = await _dbContext.Libraries.Include(x => x.Record).ToListAsync();
            ResultList = recordsInLibrary
                .Select(x => _lpcMapper.Map<GetCollectionQueryResult>(x.Record))
                .ToList();
        }
        return ResultList;
    }
}