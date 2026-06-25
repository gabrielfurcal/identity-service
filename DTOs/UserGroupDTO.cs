namespace identity_service.DTOs
{
    public record UserGroupDTO(Guid Id, UserDTO User, GroupDTO Group) {}
}