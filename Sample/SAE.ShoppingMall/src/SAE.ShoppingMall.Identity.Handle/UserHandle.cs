using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Handle
{
    public class UserHandle : IHandler<RegisterUserEvent>,
                              IHandler<ChangeUserInfoEvent>
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IDocumentService<UserDto> _userDocumentServer;
        public UserHandle(IUserQueryService userQueryService,IDocumentService<UserDto> userDocumentServer)
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
                },
                Status = @event.Status,
                Name = @event.LoginName,
                CreateTime = @event.CreateTime
            };

            this._userDocumentServer.Add(userDto);
        }

        public void Handle(ChangeUserInfoEvent @event)
        {
            var userDto = this._userQueryService.GetById((@event as IEvent).Id);
            UserInfoDto userInfoDto;
            if (userDto.Information == null)
            {
                userDto.Information = new UserInfoDto();
            }
            userInfoDto = userDto.Information;
            userInfoDto.BirthDate = @event.BirthDate;
            userInfoDto.Email = @event.Email;
            userInfoDto.Hometown = @event.Hometown;
            userInfoDto.Phone = @event.Phone;
            userInfoDto.QQ = @event.QQ;
            userInfoDto.Sex = @event.Sex;
            this._userDocumentServer.Update(userDto);
        }
    }
}
