using Refit;

namespace FindTheTreasure.Services.User.API
{
    public interface IUserApiClient
    {
        [Get("/user/signin")]
        Task<UserModel> SignInAsync(string userName);

        [Post("/user/signup")]
        Task<bool> SignUpAsync(UserModel userModel);

        [Get("/user/delete")]
        Task<bool> DeleteAsync(string userName);
    }
}
