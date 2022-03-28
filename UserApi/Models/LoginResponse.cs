namespace UserApi.Models
{
    public record LoginResponse
    {
        public string Token { get; init; }
        public string Error { get; set; }
    }
}
