namespace MelbergFramework.Infrastructure.Rabbit.Common;

public enum MetricsEnum
{
    DevOnly = 2^0,
    ProdOnly = 2^1,
    Always = DevOnly & ProdOnly,
    Never = 0
}
