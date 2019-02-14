using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog.kafka.sink
{
    public class KafkaLogMessage
    {
        public string Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }
        public string RenderedMessage { get; set; }
        public string Application { get; set; }

        public string GetJson()
        {
            var payload = new Payload
            {
                Timestamp = Timestamp,
                Level = Level,
                MessageTemplate = MessageTemplate,
                RenderedMessage = RenderedMessage,
                Application = Application
            };
            var payloadString = JsonConvert.SerializeObject(payload);
            return payloadString;
        }
    }

    internal class Payload
    {
        public string Timestamp;
        public string Level;
        public string MessageTemplate;
        public string RenderedMessage;
        public string Application;
    }
}
