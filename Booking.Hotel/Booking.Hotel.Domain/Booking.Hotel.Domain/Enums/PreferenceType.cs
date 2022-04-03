using System.Text.Json.Serialization;

namespace Booking.Hotel.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PreferenceType
    {
        None,
        Recommanded,
        Popular,
        TopRating
    }
}
