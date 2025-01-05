namespace UserAPI.Models
{
    public class UserModel
    {
        //These fields are needed when registering a user in the system
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
