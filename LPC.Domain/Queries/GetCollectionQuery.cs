using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LPC.Domain.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
    public List<GetCollectionQueryResult> ResultList { get; set; }
    public GetCollectionQueryHandler(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
        ResultList = new List<GetCollectionQueryResult>();
    }

    public async Task<List<GetCollectionQueryResult>> Handle(GetCollectionQuery request, CancellationToken cancellationToken)
    {
        if (request.CollectionType == "wishlist")
        {
            var recordsInWishlist = await _dbContext.Wishlists.Include(x => x.Record).ToListAsync();
            foreach (var records in recordsInWishlist)
            {
                var result = new GetCollectionQueryResult
                {
                    Artist = records.Record.Artist,
                    Album = records.Record.Album,
                    ImgURL = records.Record.ImgURL
                };
                ResultList.Add(result);
            }
        }
        if (request.CollectionType == "library")
        {
            var recordsInLibrary = await _dbContext.Libraries.Include(x => x.Record).ToArrayAsync();
            foreach (var records in recordsInLibrary)
            {
                var result = new GetCollectionQueryResult
                {
                    Artist = records.Record.Artist,
                    Album = records.Record.Album,
                    ImgURL = records.Record.ImgURL
                };
                ResultList.Add(result);
            }
        }
        return ResultList;
    }
}