using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiParameter_V1
{
    public class UnprocessableEntityException : HttpException
    {
        public UnprocessableEntityException() :this( null )
        {
        }

        public UnprocessableEntityException(string param) : base((HttpStatusCode)422 , "unprocessable_entity" , "The request was welformed but was unable to be followed due to semantic error",param)
        {
        }

        public UnprocessableEntityException(string errorCode , string message) : this(errorCode,message,null)
        {
        }

        public UnprocessableEntityException(string errorCode , string message , string param) : base((HttpStatusCode)422 , errorCode, message,param)
        {
        }
    }
}
