﻿using AutoMapper;
using Talabat.APIS.DTOS;
using Talabat.Core.Eintites;

namespace Talabat.APIS.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))

                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty; 
        }
    }
}
