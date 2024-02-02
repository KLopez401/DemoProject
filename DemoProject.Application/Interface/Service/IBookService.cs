using DemoProject.Application.Dtos;
using DemoProject.Domain.Model;

namespace DemoProject.Application.Interface.Service
{
    /// <summary>
    /// Book interface that contains contracts implemented on bookservice
    /// </summary>
    public interface IBookService
    {
        Task AddBooks(QueryBookDto bookDto);
        Task<PaginatedBookDto> GetAllBooks(PaginationDto paginationDto, QueryBookDto bookDto);
        Task<BookDto> GetBookById(int id);
        Task<bool> UpdateBook(int id, QueryBookDto bookDto);
        Task<bool> DeleteBook(int id);

    }
}
