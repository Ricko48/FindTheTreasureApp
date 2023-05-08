using Refit;

namespace FindTheTreasure.Services.User.API
{
    public interface IUserApiClient
    {
        [Get("/api/user/{userName}")]
        Task<UserModel> GetUserAsync(string userName);

        [Post("/api/user")]
        Task<int> CreateUserAsync(UserModel userModel);

        [Delete("/api/user/delete/{userName}")]
        Task DeleteAsync(string userName);
    }
}
