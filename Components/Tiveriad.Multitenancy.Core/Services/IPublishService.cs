namespace Tiveriad.Multitenancy.Core.Services;

public interface IPublishService
{
    void Publish<T>(T body);
}