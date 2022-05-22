using System.Reflection.Metadata;

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

        public string ReadString(string name, bool isRequired)
        {
            IParameter<string> parameter = this.Read
        }

        public IParameter<T> ReadParameter<T>(string name, bool isRequired, bool valueIsRequired)
        {
            IParameter<T> parameter = this.ReadParameter<T>(name, isRequired);
        }

        public IParameter<T> ReadParameter<T>(string name, bool isRequired)
        {
            Parameter<T> parameter = new Parameter<T>()
            {
                Name = name
            };
            object obj;
            if (!this._values.TryGetValue(name, out obj))
            {
                if (isRequired)
                    throw new UnprocessableEntityException(string.Format("invalid_{0}", name), string.Format("value of {0} is required", name));
                return parameter;
            }
        }
    }
}
