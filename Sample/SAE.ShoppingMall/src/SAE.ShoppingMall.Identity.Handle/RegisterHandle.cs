using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Handle
{
    public class RegisterHandle : IHandler<RegisterUserEvent>
    {
        private readonly IUserQueryServer _userQueryService;
        private readonly IDocumentServer<UserDto> _userDocumentServer;
        public RegisterHandle(IUserQueryServer userQueryService,IDocumentServer<UserDto> userDocumentServer)
        {
            this._userQueryService = userQueryService;
            this._userDocumentServer = userDocumentServer;
        }
        public void Handle(RegisterUserEvent @event)
        {
            var userDto = new UserDto
            {
                Id = @event.Id,
                Credentials = new CredentialsDto
                {
                    Name = @event.LoginName,
                    Password = @event.Password
                }
            };

            this._userDocumentServer.Save(userDto);
        }
    }
}
