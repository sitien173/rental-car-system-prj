using Newtonsoft.Json;

namespace NGOT.Common.Models;

public class PagingResult<T>
{
    [JsonProperty("count")]
    public long Count { get; set; }

    [JsonProperty("result")] 
    public IEnumerable<T> Result { get; set; } = Enumerable.Empty<T>();
}