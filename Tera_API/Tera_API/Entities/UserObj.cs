namespace Tera_API.Entities
{
    public class UserObj
    {
        public int userId { get; set; } = 0;
        public int userGovId { get; set; } = 0;

        public string userName { get; set; } = string.Empty;

        public string userFirstSurname { get; set; } = string.Empty;

        public string userSecondSurname { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public bool active { get; set; } = false;

        public int idRole { get; set; } = 0;


    }
}
