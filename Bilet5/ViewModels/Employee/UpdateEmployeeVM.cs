namespace Bilet5.ViewModels
{
    public class UpdateEmployeeVM
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
