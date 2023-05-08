namespace FindTheTreasure.Pages.User.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ParticipantId { get; set; }
    }
}