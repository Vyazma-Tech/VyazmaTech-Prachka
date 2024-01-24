﻿using Application.Core.Contracts.Common;
using Application.Core.Specifications;
using Application.DataAccess.Contracts;
using Domain.Common.Result;
using Domain.Core.Queue;
using static Application.Core.Contracts.Queues.Queries.QueueById;

namespace Application.Handlers.Queue.Queries;

internal sealed class QueueByIdQueryHandler : IQueryHandler<Query, Result<Response>>
{
    private readonly IPersistenceContext _context;

    public QueueByIdQueryHandler(IPersistenceContext context)
    {
        _context = context;
    }

    public async ValueTask<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        Result<QueueEntity> result = await _context.Queues
            .FindByIdAsync(request.Id, cancellationToken);

        if (result.IsFaulted)
            return new Result<Response>(result.Error);

        QueueEntity queue = result.Value;

        return queue.ToDto();
    }
}