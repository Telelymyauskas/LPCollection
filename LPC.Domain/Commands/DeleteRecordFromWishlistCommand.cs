using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LPC.Domain.Helpers.Interfaces;

namespace LPC.Domain.Commands;

public class DeleteRecordCommand : IRequest<DeleteRecordCommandResult>
{
    public int Id { get; set; }
    public string CollectionType { get; set; }
}

public class DeleteRecordCommandResult
{
    public bool DeleteIsSuccessful { get; init; }
}
public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand, DeleteRecordCommandResult>
{
    private readonly ILPCollectionService _lpCollectionService;

    public DeleteRecordCommandHandler(ILPCollectionService lpCollectionService)
    {
        _lpCollectionService = lpCollectionService;
    }

    public async Task<DeleteRecordCommandResult> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
    {
        return new DeleteRecordCommandResult
        {
            DeleteIsSuccessful = await _lpCollectionService.DeleteRecord(request.Id, request.CollectionType, cancellationToken)
        };
    }
}