
using Mango.Services.RewardsApi.Messages;

namespace Mango.Services.RewardsApi.Services.IServices
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
