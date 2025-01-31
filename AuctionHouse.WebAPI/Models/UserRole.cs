namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents a user role with a username and role name.
    /// </summary>
    public class UserRole {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role name of the user.
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
