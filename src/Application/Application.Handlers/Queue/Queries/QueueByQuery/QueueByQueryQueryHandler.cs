﻿using Application.Core.Common;
using Application.Core.Configuration;
using Application.Core.Contracts;
using Application.Core.Extensions;
using Application.DataAccess.Contracts;
using Domain.Core.Queue;
using Infrastructure.DataAccess.Contracts;
using Infrastructure.DataAccess.Specifications.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Application.Handlers.Queue.Queries.QueueByQuery.QueueByQueryQuery;

namespace Application.Handlers.Queue.Queries.QueueByQuery;

internal sealed class QueueByQueryQueryHandler : IQueryHandler<Query, PagedResponse<Response>>
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly int _recordsPerPage;

    public QueueByQueryQueryHandler(
        IPersistenceContext persistenceContext,
        IOptions<PaginationConfiguration> paginationConfiguration)
    {
        _persistenceContext = persistenceContext;
        _recordsPerPage = paginationConfiguration.Value.RecordsPerPage;
    }

    public async ValueTask<PagedResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var spec = new QueuePageSpecification(request.Page, _recordsPerPage);
        long totalCount = await _persistenceContext.Queues.CountAsync(spec, cancellationToken);

        List<QueueEntity> queues = await _persistenceContext.Queues
            .QueryAsync(spec, cancellationToken)
            .ToListAsync(cancellationToken);

        Response[] result = queues
            .FilterBy(request.AssignmentDate)
            .Select(x => x.ToDto())
            .ToArray();

        return new PagedResponse<Response>
        {
            Bunch = result,
            CurrentPage = request.Page + 1,
            RecordPerPage = _recordsPerPage,
            TotalPages = (totalCount / _recordsPerPage) + 1
        };
    }
}