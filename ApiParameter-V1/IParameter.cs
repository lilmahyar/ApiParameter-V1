namespace ApiParameter_V1
{
    public interface IParameter<out TValue>
    {
        string Name
        {
            get;
        }

        bool HasValue
        {
            get; 
        }

        TValue Value
        {
            get; 

        }
    }
}
