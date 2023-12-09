﻿using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.Fine
{
    public class FineForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
