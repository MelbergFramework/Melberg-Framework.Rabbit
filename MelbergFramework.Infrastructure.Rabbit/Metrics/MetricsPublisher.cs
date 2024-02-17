using MelbergFramework.Infrastructure.Rabbit.Publishers;

namespace MelbergFramework.Infrastructure.Rabbit.Metrics;

public interface IMetricPublisher
{
    void SendMetric(string metric, long timeInMS, DateTime timeStamp);
}
public class MetricPublisher : IMetricPublisher
{
    private readonly IStandardPublisher<MetricMessage> _metricPublisher;

    public MetricPublisher( IStandardPublisher<MetricMessage> publisher)
    {
        _metricPublisher = publisher;
    }

    public void SendMetric(string metric, long timeInMS, DateTime timeStamp) => 
        _metricPublisher.Send(
            new MetricMessage()
            {
                Application = metric,
                TimeInMS = timeInMS,
                TimeStamp = timeStamp
            });
}
