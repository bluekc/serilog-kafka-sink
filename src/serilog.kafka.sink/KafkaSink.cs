using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog.kafka.sink
{
    public class KafkaSink : PeriodicBatchingSink
    {

        private string topic;
        private Producer<Null, string> producer;
        private JsonFormatter formatter;
        private Dictionary<string, object> config;

        public KafkaSink(
            int batchSizeLimit,
            int period,
            string brokers,
            string topic) : base(batchSizeLimit, TimeSpan.FromSeconds(period))
        {
            config = new Dictionary<string, object> { { "bootstrap.servers", brokers } };
            producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
            formatter = new JsonFormatter(closingDelimiter: null, renderMessage: true);
            this.topic = topic;

        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            foreach (var @event in events)
            {
                var message = new KafkaLogMessage
                {
                    Timestamp = @event.Timestamp.ToString(),
                    Level = @event.Level.ToString(),
                    MessageTemplate = @event.MessageTemplate.ToString(),
                    RenderedMessage = @event.RenderMessage().ToString()
                };
                await producer.ProduceAsync(topic, null, message.GetJson());
            }
        }

        protected override void Dispose(bool disposing)
        {
            producer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
