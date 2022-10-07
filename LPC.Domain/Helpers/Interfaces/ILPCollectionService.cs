using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;

namespace LPC.Domain.Helpers.Interfaces;

public interface ILPCollectionService
{
    Task<int> AddRecordToCollection(Library libraryToAdd, CancellationToken cancellationToken);
    Task<int> AddRecordToCollection(Wishlist wishlistToAdd, CancellationToken cancellationToken);
    Task<List<Wishlist>> GetRecordsInWishlist(CancellationToken cancellationToken);
    Task<List<Library>> GetRecordsInLibrary(CancellationToken cancellationToken);
    Task<List<Record>> GetAllRecords(CancellationToken cancellationToken);
}