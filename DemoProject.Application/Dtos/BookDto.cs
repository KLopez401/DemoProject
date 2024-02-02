namespace DemoProject.Application.Dtos
{
    /// <summary>
    /// book data that returns bookdata to the service
    /// </summary>
    public class BookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PublishedDate { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

    }
    /// <summary>
    /// book query that pass bookdata to the service
    /// </summary>
    public class QueryBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

    }
}
