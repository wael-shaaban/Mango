﻿namespace Mango.Services.JwtConfigurations
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
