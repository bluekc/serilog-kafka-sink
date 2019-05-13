# serilog-kafka-sink
A Serilog sink for sending structured logging events to Apache Kafka

[![Build Status](https://bluekc.visualstudio.com/BlueKC/_apis/build/status/keisenb.serilog-kafka-sink?branchName=master)](https://bluekc.visualstudio.com/BlueKC/_build/latest?definitionId=1438&branchName=master)

Add this to your global.asax.cs file

```c#
string brokers = "kafka-server:9092,kafka-server2:9092"; //comma seperated list of kafka brokers
string topic = "kafka-topic-name"; //name of kafka topic to produce logs to
string application = "bills-app-service"; //name of service that is producing log messages

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .Kafka(batchSizeLimit: 50, period: 1, brokers: brokers, topic: topic, application: application)
    .CreateLogger();
```
