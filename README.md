# serilog-kafka-sink
A Serilog sink for sending structured logging events to Apache Kafka


Add this to your global.asax.cs file

```string brokers = "hostname of broker";
    string topic = "topic-name";

    Log.Logger = new LoggerConfiguration()
        .WriteTo
        .Kafka(batchSizeLimit: 50, period: 1, brokers: brokers, topic: topic)
        .CreateLogger();```