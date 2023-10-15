﻿using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Feedbacks.Wishlists;
public class WishlistForUpdateDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long BookId { get; set; }
    public WishListStatus Status { get; set; }
}