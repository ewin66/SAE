using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Application.Maps
{
    internal class AppConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof(AppDto) == destinationType;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var app = value as App;

            if (app == null) return null;

            return new AppDto
            {
                AppId = app.Client?.Id,
                AppSecret = app.Client?.Secret,
                CreateTime = app.CreateTime,
                Name = app.Name,
                Signin = app.Endpoint?.Signin,
                Signout = app.Endpoint?.Signout,
                Status = (int)app.Status
            };
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(AppDto);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var appDto = value as AppDto;

            if (appDto == null) return null;

            return new App(appDto.Name,
                           new ClientCredentials(appDto.AppId, appDto.AppSecret),
                           new SignEndpoint(appDto.Signin, appDto.Signout));
        }
    }
}
