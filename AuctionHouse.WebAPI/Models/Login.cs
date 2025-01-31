namespace AuctionHouse.WebAPI.Models {
    /// <summary>
    /// Represents the login credentials for a user.
    /// </summary>
    public class Login {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
