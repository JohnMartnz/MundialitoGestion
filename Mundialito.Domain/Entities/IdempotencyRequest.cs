using Mundialito.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Domain.Entities
{
    public class IdempotencyRequest : BaseEntity
    {
        public Guid Key { get; private set; }
        public string Response { get; private set; } = string.Empty;
        public int StatusCode { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private IdempotencyRequest() { }

        public IdempotencyRequest(Guid key, string response, int statusCode)
        {
            Id = Guid.NewGuid();
            Key = key;
            Response = response;
            StatusCode = statusCode;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
