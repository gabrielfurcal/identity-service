namespace identity_service.DTOs
{
    public record UserPageResponse
    {
        public UserPageResponse()
        {
            
        }

        public UserPageResponse(List<UserDTO> Users, int TotalCount)
        {
            this.Users = Users;
            this.TotalCount = TotalCount;
        }

        public List<UserDTO> Users { get; init; } = [];
        public int TotalCount { get; init; } = 0;
    }
}