namespace Tera_API.Entities
{
    public class UserObj
    {
        public int userId { get; set; } = 0;
        public string userGovId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string userFirstSurname { get; set; } = string.Empty;
        public string userSecondSurname { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string userPassword { get; set; } = string.Empty;
        public bool userActive { get; set; } = false;
        public int userRoleId { get; set; } = 0;
        public int userSiteId { get; set; } = 0;
    }
}
