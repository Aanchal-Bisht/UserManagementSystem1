namespace UMSAPI
{
    public class UpdateUserBody
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Gender { get; set; }
        public string? DOB { get; set; }
        public string? DeptId { get; set; }
    }
}
