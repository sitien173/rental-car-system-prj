using Newtonsoft.Json;
using NGOT.Common.Extensions;

namespace NGOT.Common.Models;

public class ServerSideQueryRequest
{
    [JsonProperty("take")]
    public int? Take { get; set; }
    
    [JsonProperty("skip")]
    public int? Skip { get; set; }
    
    private IEnumerable<string> _group = Enumerable.Empty<string>();

    [JsonProperty("group")]
    public IEnumerable<string> Group
    {
        get => _group;
        set
        {
            _group = value.Select(x => x.UpperFirstLetter());
        }
    }

    [JsonProperty("requiresCounts")]
    public bool RequiresCounts { get; set; }

    [JsonProperty("search")]
    public IEnumerable<SearchOption> Search { get; set; } = Enumerable.Empty<SearchOption>();
    
    [JsonProperty("sorted")]
    public IEnumerable<SortOption> Sort { get; set; } = Enumerable.Empty<SortOption>();
    
    [JsonProperty("where")]
    public IEnumerable<WhereOption> Where { get; set; } = Enumerable.Empty<WhereOption>();
    
    public class WhereOption
    {
        [JsonProperty("isComplex")]
        public bool IsComplex { get; set; }
        
        [JsonProperty("ignoreCase")]
        public bool IgnoreCase { get; set; }
        
        [JsonProperty("ignoreAccent")]
        public bool IgnoreAccent { get; set; }
        
        [JsonProperty("condition")]
        public string? Condition { get; set; }
        
        [JsonProperty("predicates")]
        public IEnumerable<WhereOption>? Predicates { get; set; }
        
        private string? _field;

        [JsonProperty("field")]
        public string? Field
        {
            get => _field;
            set => _field = value?.UpperFirstLetter();
        }
        
        private string? _operator;
        [JsonProperty("operator")]
        public string? Operator { 
            get => _operator;
            set => _operator = value?.UpperFirstLetter();
        }
        
        [JsonProperty("value")]
        public string? Value { get; set; }
    }
    public class SearchOption
    {
        private IEnumerable<string> _fields = Enumerable.Empty<string>();
        
        [JsonProperty("fields")]
        public IEnumerable<string> Fields  
        {
            get => _fields;
            set
            {
                _fields = value.Select(x => x.UpperFirstLetter());
            }
        }
        private string? _operator;
        
        [JsonProperty("operator")]
        public string? Operator { 
            get => _operator;
            set => _operator = value?.UpperFirstLetter();
        }
        
        [JsonProperty("key")]
        public string? Key { get; set; }
        
        [JsonProperty("ignoreCase")] 
        public bool IgnoreCase { get; set; }
    }
    
    public class SortOption
    {
        private string? _name;
        
        [JsonProperty("name")]
        public string? Name 
        {
            get => _name;
            set => _name = value?.UpperFirstLetter();
        }
        
        [JsonProperty("direction")]
        public DirectionOpt Direction { get; set; } = DirectionOpt.Ascending;
    }
    
    public enum DirectionOpt
    {
        [JsonProperty("ascending")]
        Ascending,
        [JsonProperty("descending")]
        Descending
    }
}

