using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exepections;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Users.Commands.UpdateUser
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        #region Context
        private readonly ILogger<UpdateUserDetailsCommand> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;
        private readonly IUserStore<User> userStore;

        public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommand> logger , IUnitOfWork unitOfWork ,  
            IMapper mapper , IUserContext userContext , IUserStore<User> userStore)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userContext = userContext;
            this.userStore = userStore;
        }
        #endregion
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation($"Updating User : user Id{user} with request {request}");
            var dbUser = await userStore.FindByIdAsync(user!.Id , cancellationToken);

            if( dbUser == null )
                throw new NotFoundExepection("User is null");

            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
