using System.Globalization;
using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace MelbergFramework.Infrastructure.Rabbit.Extensions;
public static class MessageExtensions
{
    public static class Headers
    {
        public const string CorrelationId = "coid";
        public const string Timestamp = "timestamp";
    }

    public static void SetTimestamp(this Message message, DateTime date)
    {
        message.Headers[Headers.Timestamp] = date.ToString();
    }

    public static DateTime GetTimestamp(this Message message )
    {
        if(message.Headers.TryGetValue(Headers.Timestamp,out var timestamp) && timestamp is string)
        {
            try
            {
                return DateTime.ParseExact((string)timestamp,"MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal); 
            }
            catch (Exception) { }
        }
        return DateTime.UtcNow;
    }

    public static void SetCoID(this Message message, Guid coId)
    {
        message.Headers[Headers.CorrelationId] = coId.ToString();
    }
    
    public static Guid GetCoID(this Message message)
    {
        if(message.Headers.TryGetValue(Headers.CorrelationId, out var coid) && coid is string)
        {
            try
            {
                return Guid.Parse((string)coid);
            }
            catch(Exception){}
        }
        
        return Guid.NewGuid();
    }
}
