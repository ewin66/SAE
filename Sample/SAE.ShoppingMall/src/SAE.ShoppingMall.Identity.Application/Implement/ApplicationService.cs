using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Nelibur.ObjectMapper;
using SAE.CommonLibrary.EventStore.Document;
using SAE.ShoppingMall.Identity.Application.Maps;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public abstract class ApplicationService
    {
        protected readonly IDocumentStore _documentStore;
        public ApplicationService(IDocumentStore documentStore)
        {
            this._documentStore = documentStore;
        }
        static ApplicationService()
        {
            TypeDescriptor.AddAttributes(typeof(App), new TypeConverterAttribute(typeof(AppConverter)));
            TinyMapper.Bind<User, UserDto>();
            TinyMapper.Bind<App, AppDto>();
            TinyMapper.Bind<AppDto, App>();
        }
    }
}
