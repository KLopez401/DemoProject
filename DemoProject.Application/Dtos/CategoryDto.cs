namespace DemoProject.Application.Dtos
{
    /// <summary>
    /// category query that pass category data to service
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
