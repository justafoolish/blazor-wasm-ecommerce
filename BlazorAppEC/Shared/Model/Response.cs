using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorAppEC.Shared.Http
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data {get; set;}
    }
}
