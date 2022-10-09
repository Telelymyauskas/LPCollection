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
    private readonly ILPCollectionService _lpCollectionService;
    private readonly IValidationHelper _validatorHelper;

    public AddRecordToLibraryCommandHandler(ILPCollectionService lpCollectionService, IValidationHelper validatorHelper)
    {
        _lpCollectionService = lpCollectionService;
        _validatorHelper = validatorHelper;
    }

    public async Task<int> Handle(AddRecordToLibraryCommand request, CancellationToken cancellationToken)
    {
        var libraryToAdd = new Library
        {
            RecordOwned = request.Id
        };

        if (!await _validatorHelper.ValidateRecordDuplication(request.Id))
        {
            return 0;
        }

        return await _lpCollectionService.AddRecordToCollection(libraryToAdd, cancellationToken);
    }
}