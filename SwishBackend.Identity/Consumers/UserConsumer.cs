using MassTransit;
using MassTransitCommons.Common.Order;
using Microsoft.AspNetCore.Identity;
using SwishBackend.Identity.Models;


namespace SwishBackend.Identity.Consumers
{
    public class UserConsumer : IConsumer<UserLookupMessage>
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserConsumer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task Consume(ConsumeContext<UserLookupMessage> context)
        {
            var user = await _userManager.FindByNameAsync(context.Message.UserName);
            if (user != null)
            {
                if (user.UserName != null)
                {

                    await context.RespondAsync<UserLookupResponse>(new UserLookupResponse
                    {
                        UserName = user.UserName,
                        UserId = user.Id,
                    });
                }
                else
                {
                    await context.RespondAsync<UserLookupResponse>(new UserLookupResponse
                    {
                        UserName = "Not Validated",
                        UserId = user.Id
                    });
                }
            }

        }
    }
}
