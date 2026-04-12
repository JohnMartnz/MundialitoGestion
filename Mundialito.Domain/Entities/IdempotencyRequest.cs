using Mundialito.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Domain.Entities
{
    public class IdempotencyRequest : BaseEntity
    {
        public Guid Key { get; private set; }
        public string Result { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private IdempotencyRequest() { }

        public IdempotencyRequest(Guid key, string result)
        {
            Id = Guid.NewGuid();
            Key = key;
            Result = result;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
