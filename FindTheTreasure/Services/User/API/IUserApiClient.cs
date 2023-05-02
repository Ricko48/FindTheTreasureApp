using Refit;

namespace FindTheTreasure.Services.User.API
{
    public interface IUserApiClient
    {
        [Get("/api/user/signin")]
        Task<UserModel> SignInAsync(string userName);

        [Post("/api/user/signup")]
        Task<bool> SignUpAsync(UserModel userModel);

        [Get("/api/user/delete")]
        Task<bool> DeleteAsync(string userName);

        [Get("/api/user/")]
        Task<UserModel> GetUserAsync(int id);
    }
}
