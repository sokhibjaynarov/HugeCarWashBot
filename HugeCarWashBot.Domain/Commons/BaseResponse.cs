﻿using System.Text.Json.Serialization;

namespace HugeCarWashBot.Domain.Commons
{
    public class BaseResponse<TSource>
    {
        [JsonIgnore]
        public int Code { get; set; } = 200;

        public TSource Data { get; set; }

        public ErrorResponse Error { get; set; }
    }
}
