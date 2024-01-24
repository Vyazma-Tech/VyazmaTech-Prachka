using Application.Core.Querying.Common;
using Application.DataAccess.Contracts.Querying.Queue;
using static Application.Core.Contracts.Queues.Queries.QueueByQuery;

namespace Application.Core.Querying.Queue;

public class AssignmentDateQueryLink : QueryLinkBase<Query, IQueryBuilder>
{
    protected override IQueryBuilder Apply(Query query, IQueryBuilder builder)
    {
        if (query.AssignmentDate is not null)
            builder = builder.WithAssignmentDate(query.AssignmentDate.Value);

        return builder;
    }
}