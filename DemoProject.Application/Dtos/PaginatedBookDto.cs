namespace DemoProject.Application.Dtos
{
    /// <summary>
    /// A dto that returns the paginated book
    /// </summary>
    public class PaginatedBookDto
    {
        public PaginationDto Pagination { get; set; } = new PaginationDto();
        public IEnumerable<BookDto> BookListDto { get; set; } = Enumerable.Empty<BookDto>();
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
