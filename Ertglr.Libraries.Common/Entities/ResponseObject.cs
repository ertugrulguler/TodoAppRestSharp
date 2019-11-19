using System;
using System.Net;

namespace Ertglr.Libraries.Common.Entities
{
    public class ResponseObject<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public bool IsRedirectTo { get; set; }
        public string RedirectionUrl { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}