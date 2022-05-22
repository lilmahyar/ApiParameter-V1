namespace ApiParameter_V1
{
    public class Parameter<TValue> : IParameter<TValue>
    {
        private TValue _value;

        public string Name
        {
            get;
            set;
        }

        public bool HasValue
        {
            get;
            set;
        }

        public TValue Value
        {
            get
            {
                if (!this.HasValue)
                    throw new InvalidOperationException("Parameter must have a value .");
                return this._value;
            }
            set
            {
                this._value = value; 
                this.HasValue = true;
            }
        }
    }
}
