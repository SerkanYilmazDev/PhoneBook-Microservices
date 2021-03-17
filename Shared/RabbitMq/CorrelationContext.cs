using System;
using Newtonsoft.Json;


namespace Shared.RabbitMq
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid CorrelationId { get; }
        public Guid UserId { get; }

        public CorrelationContext()
        {
        }

        [JsonConstructor]
        private CorrelationContext(Guid correlationId, Guid userId)
        {
            CorrelationId = correlationId;
            UserId = userId;
        }

        public static ICorrelationContext Create(Guid correlationId, Guid userId)
        {
            return new CorrelationContext(correlationId, userId);
        }
    }
}
