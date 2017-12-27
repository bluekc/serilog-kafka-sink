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
        private string schema = "{\"schema\": { \"type\": \"struct\", \"fields\": [{ \"type\": \"string\", \"optional\": false, \"field\": \"Timestamp\" }, { \"type\": \"string\", \"optional\": false, \"field\": \"Level\" }, { \"type\": \"string\", \"optional\": false, \"field\": \"MessageTemplate\" }, { \"type\": \"string\", \"optional\": false, \"field\": \"RenderedMessage\" }], \"optional\": \"false\", \"name\": \"Message\" }, \"payload\": ";

        private string _timestamp;
        private string _level;
        private string _messageTemplate;
        private string _renderedMessage;

        public string Timestamp
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }
        public string Level
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = value;
            }
        }
        public string MessageTemplate
        {
            get
            {
                return this._messageTemplate;
            }
            set
            {
                this._messageTemplate = value;
            }
        }

        public string RenderedMessage
        {
            get
            {
                return this._renderedMessage;
            }
            set
            {
                this._renderedMessage = value;
            }
        }

        public string GetJson()
        {
            var payload = new Payload
            {
                Timestamp = Timestamp,
                Level = Level,
                MessageTemplate = MessageTemplate,
                RenderedMessage = RenderedMessage
            };
            var payloadString = JsonConvert.SerializeObject(payload);
            return schema + payloadString + "}";
        }
    }

    class Payload
    {
        public string Timestamp;
        public string Level;
        public string MessageTemplate;
        public string RenderedMessage;
    }
}
