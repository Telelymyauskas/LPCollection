using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using MediatR;
using LPC.Domain.Helpers;
using LPC.Domain.Helpers.Interfaces;

namespace LPC.Domain.Commands;

public class AddRecordToLibraryCommand : IRequest<int>
{
    public int Id { get; set; }
}

public class AddRecordToLibraryCommandHandler : IRequestHandler<AddRecordToLibraryCommand, int>
{
    private readonly LpcDbContext _dbContext;
    private readonly ILPCollectionService _lpCollectionService;

    public AddRecordToLibraryCommandHandler(LpcDbContext dbContext, ILPCollectionService lpCollectionService)
    {
        _dbContext = dbContext;
        _lpCollectionService = lpCollectionService;
    }

    public async Task<int> Handle(AddRecordToLibraryCommand request, CancellationToken cancellationToken)
    {
        var libraryToAdd = new Library
        {
            RecordOwned = request.Id
        };

        var validator = new ValidationHelper(_dbContext);
        if (!await validator.ValidateRecordDuplication(request.Id))
        {
            return 0;
        }

        return await _lpCollectionService.AddRecordToCollection(libraryToAdd, cancellationToken);
    }
}