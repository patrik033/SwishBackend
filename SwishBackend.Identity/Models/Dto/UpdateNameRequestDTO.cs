namespace SwishBackend.Models.Dto
{
    public class UpdateNameRequestDTO
    {
        public string UserId { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

    }
}
