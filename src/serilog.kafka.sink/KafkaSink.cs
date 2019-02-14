using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace serilog.kafka.sink
{
    public class KafkaSink : PeriodicBatchingSink
    {

        private readonly string _topic;
        private readonly Producer<Null, string> _producer;
        private readonly string _application;

        public KafkaSink(
            int batchSizeLimit,
            int period,
            string brokers,
            string topic,
            string application) : base(batchSizeLimit, TimeSpan.FromSeconds(period))
        {
            var config = new Dictionary<string, object> { { "bootstrap.servers", brokers } };
            _producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
            _topic = topic;
            _application = application;

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
                    RenderedMessage = @event.RenderMessage(),
                    Application = _application      
                };
                await _producer.ProduceAsync(_topic, null, message.GetJson());
            }
        }

        protected override void Dispose(bool disposing)
        {
            _producer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
