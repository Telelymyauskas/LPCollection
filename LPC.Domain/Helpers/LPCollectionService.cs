using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using LPC.Domain.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LPC.Domain.Helpers;

public class LPCollectionService : ILPCollectionService
{
    private readonly LpcDbContext _dbContext;

    public LPCollectionService(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddRecordToCollection(Library libraryToAdd, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Libraries.AddAsync(libraryToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<int> AddRecordToCollection(Wishlist wishlistToAdd, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Wishlists.AddAsync(wishlistToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<bool> DeleteRecord(int id, string collectionType, CancellationToken cancellationToken)
    {
        if (collectionType == "wishlist")
        {
            var recordToDelete = await _dbContext.Wishlists.FirstOrDefaultAsync(x => x.RecordWished == id);
            _dbContext.Attach(recordToDelete);
            var deletedRecord = _dbContext.Remove(recordToDelete);
            await _dbContext.SaveChangesAsync();
            return _dbContext.Entry(deletedRecord).State == EntityState.Deleted;
        }
        else
        {
            var recordToDelete = await _dbContext.Libraries.FirstOrDefaultAsync(x => x.RecordOwned == id);
            _dbContext.Attach(recordToDelete);
            var deletedRecord = _dbContext.Remove(recordToDelete);
            await _dbContext.SaveChangesAsync();
            return _dbContext.Entry(deletedRecord).State == EntityState.Deleted;
        }
        // return true;
    }

    public async Task<List<Record>> GetAllRecords(CancellationToken cancellationToken)
    {
        return await _dbContext.Records.ToListAsync();
    }

    public async Task<List<Library>> GetRecordsInLibrary(CancellationToken cancellationToken)
    {
        return await _dbContext.Libraries.Include(x => x.Record).ToListAsync(cancellationToken);
    }

    public async Task<List<Wishlist>> GetRecordsInWishlist(CancellationToken cancellationToken)
    {
        return await _dbContext.Wishlists.Include(x => x.Record).ToListAsync(cancellationToken);
    }
}
