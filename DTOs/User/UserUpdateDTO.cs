namespace mentor.DTOs.User
{
    public record UserUpdateDto(
    string FullName,
    string Email,
    string Password,
    string GoalCarreer
);

}
