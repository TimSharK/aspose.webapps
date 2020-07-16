using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Models
{
    public class ConvertImageData
    {
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string Format { get; set; }
    }
}
