namespace GroupTracker.DTOs.Friends
{
    public class FriendProfileDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public string Companies { get; set; }
        public string Subjects { get; set; }
        public int GroupCount { get; set; }
        public string Email { get; set; }

        // socials
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string Website { get; set; }
        public string YouTube { get; set; }
    }
}
