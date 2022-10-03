using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LPC.Contracts.Database;
using LPC.Domain.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LPC.Domain.Commands;

public class GetAllRecordsQuery : IRequest<List<Record>>
{
    public Guid Id { get; set; }
}

public class GetAllRecordsQueryHandler : IRequestHandler<GetAllRecordsQuery, List<Record>>
{
    private readonly LpcDbContext _dbContext;

    public GetAllRecordsQueryHandler(LpcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Record>> Handle(GetAllRecordsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Records.ToListAsync();
    }
}