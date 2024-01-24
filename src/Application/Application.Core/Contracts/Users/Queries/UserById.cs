﻿using Application.Core.Contracts.Common;
using Domain.Common.Result;
using Domain.Core.User;

namespace Application.Core.Contracts.Users.Queries;

public static class UserById
{
    public record Query(Guid Id) : IQuery<Result<Response>>;

    public record struct Response(
        Guid Id,
        string TelegramId,
        string Fullname,
        DateTime? ModifiedOn,
        DateOnly CreationDate);

    public static Response ToDto(this UserEntity user)
    {
        return new Response(
            user.Id,
            user.TelegramId,
            user.Fullname,
            user.ModifiedOn?.Value,
            user.CreationDate);
    }
}