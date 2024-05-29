using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTabGLQ.Models
{
    public record struct APISettings
    {
        public string User {  get; init; }
        public string Password { get; init; }
        public string Database { get; init; }
        public string Server { get; init; }

    }

}
