namespace SwishBackend.Models.Dto
{ 
    public class RegisterResponseDTO
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
