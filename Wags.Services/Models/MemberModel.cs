namespace Wags.Services.Models
{
    public class MemberModel : PlayerModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }    
        public Address Address { get; set; }
    }
}
