namespace ApiParameter_V1
{
    public class ApiParametersBase
    {
        private readonly IDictionary<string,object> _values = (IDictionary<string,object>) new Dictionary<string, object>();

        public object ContentObject
        {
            get;
            internal set; 
        }

        public IEnumerable<string> Keys => _values.Keys;

        public ApiParametersBase()
        {
            
        }

        public ApiParametersBase(IDictionary<string, object> values)
        {
            _values = values;
        }

        public void AddValue(string key, object value)
        {
            if (_values.ContainsKey(key))
                _values[key] = value;
            else 
                _values.Add(key,value);
        }

    }
}
