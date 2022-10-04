using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using MediatR;

namespace LPC.Domain.Commands;

public class AddRecordToWishlistCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class AddRecordToWishlistCommandHandler : IRequestHandler<AddRecordToWishlistCommand, int>
{
    private readonly LpcDbContext _dbContext;

    public AddRecordToWishlistCommandHandler(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(AddRecordToWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlistToAdd = new Wishlist
        {
            RecordWished = request.Id
        };
        var result = await _dbContext.Wishlists.AddAsync(wishlistToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
}