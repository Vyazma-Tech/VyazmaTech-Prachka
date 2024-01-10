﻿using Infrastructure.DataAccess.Contracts;
using Infrastructure.DataAccess.Models;

namespace Infrastructure.DataAccess.Specifications.Order;

public sealed class OrderByPageSpecification : Specification<OrderModel>
{
    public OrderByPageSpecification(int page, int recordsPerPage)
        : base(null)
    {
        AddPaging(page, recordsPerPage);
        AsNoTracking();
    }

    public override string ToString()
        => string.Empty;
}