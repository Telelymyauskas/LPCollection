using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using LPC.Domain.Helpers;
using LPC.Domain.Helpers.Interfaces;
using MediatR;

namespace LPC.Domain.Commands;

public class AddRecordToWishlistCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class AddRecordToWishlistCommandHandler : IRequestHandler<AddRecordToWishlistCommand, int>
{
    private readonly LpcDbContext _dbContext;
    private readonly ILPCollectionService _lpCollectionService;


    public AddRecordToWishlistCommandHandler(LpcDbContext dbContext, ILPCollectionService lpCollectionService)
    {
        _dbContext = dbContext;
        _lpCollectionService = lpCollectionService;
    }

    public async Task<int> Handle(AddRecordToWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlistToAdd = new Wishlist
        {
            RecordWished = request.Id
        };

        var validator = new ValidationHelper(_dbContext);
        if(!await validator.ValidateRecordDuplication(request.Id))
        {
            return 0;
        }
        
        return await _lpCollectionService.AddRecordToCollection(wishlistToAdd, cancellationToken);
    }
}