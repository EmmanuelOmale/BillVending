using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; } 
        public bool HasError => !Status;
        public string Description { get; set; }
        public DateTime ActivityTIme { get; set; } = DateTime.UtcNow;

    }
}