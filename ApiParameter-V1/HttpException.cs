using System.Net;

namespace ApiParameter_V1
{
    public class HttpException : Exception
    {
        public HttpStatusCode HttpStatusCode
        {
            get;
            private set;
        }

        public string ErrorCode
        {
            get;
            set;
        }

        public string Parameter
        {
            get;
            set;
        }

        public HttpException(HttpStatusCode httpStatusCode , string errorCode ,string message) : this(httpStatusCode,errorCode, message,(string)null)
        {
        }
         
        public HttpException(HttpStatusCode httpStatusCode,string errorCode , string message, string @params) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
            Parameter = @params;
        }
    }
}
