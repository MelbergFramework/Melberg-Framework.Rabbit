using LightBDD.MsTest3;
using Microsoft.AspNetCore.Builder;

namespace MelbergFramework.ComponentTests;
public class BaseTestFrame : FeatureFixture
{
    public WebApplication App;

    public T GetClass<T>() => (T)App
        .Services
        .GetService(typeof(T));
    
}
