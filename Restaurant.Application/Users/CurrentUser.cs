namespace Restaurant.Application.Users
{
    public class CurrentUser
    {
        public string Id { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }

        public CurrentUser(string id, string email, IEnumerable<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
