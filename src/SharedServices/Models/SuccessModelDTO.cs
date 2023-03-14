// LightningBits
using System;
namespace SharedServices.Models
{
    public class SuccessModelDTO
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }
    }
}

