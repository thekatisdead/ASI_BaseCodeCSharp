﻿using Microsoft.Extensions.Configuration;

namespace Basecode.WebApp
{
    public class AzureAdOptions
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CallbackPath { get; set; }
    }
}
