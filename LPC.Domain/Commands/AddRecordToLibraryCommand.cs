using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LPC.Domain.Helpers;

namespace LPC.Domain.Commands;

public class AddRecordToLibraryCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class AddRecordToLibraryCommandHandler : IRequestHandler<AddRecordToLibraryCommand, int>
{
    private readonly LpcDbContext _dbContext;

    public AddRecordToLibraryCommandHandler(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(AddRecordToLibraryCommand request, CancellationToken cancellationToken)
    {
        var libraryToAdd = new RecordsLibrary
        {
            RecordOwned = request.Id
        };

        var validator = new ValidationHelper(_dbContext);
        if(await validator.ToValidate(request.Id) == 0)
        {
            return 0;
        }
        
        var result = await _dbContext.OwnedRecords.AddAsync(libraryToAdd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
}