using DemoProject.Application.Dtos;
using DemoProject.Application.Interface.Service;
using DemoProject.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Api.Controllers
{
    /// <summary>
    /// This api manages the book catalog
    /// </summary>
    /// <remarks>
    /// It contains add, get, update, and delete book
    /// </remarks>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private IBookService _bookService;
        /// <summary>
        /// constructor that injects service
        /// </summary>
        /// <param name="bookService"></param>
        public BooksApiController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Retrieves filtered or all books
        /// </summary>
        /// <param name="paginationDto"></param>
        /// <param name="bookDto"></param>
        /// <returns>all books</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] PaginationDto paginationDto, [FromQuery] QueryBookDto bookDto)
        {
            var books = await _bookService.GetAllBooks(paginationDto, bookDto);

            return Ok(books);
        }

        /// <summary>
        /// Retrieve a single book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>book</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            return Ok(book);
        }

        /// <summary>
        /// Add book information
        /// </summary>
        /// <param name="bookDto"></param>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] QueryBookDto bookDto)
        {
            await _bookService.AddBooks(bookDto);

            return Ok();
        }

        /// <summary>
        /// Update a book 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns>true/false</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] QueryBookDto bookDto)
        {
            var isUpdated = await _bookService.UpdateBook(id, bookDto);

            return Ok(isUpdated);
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _bookService.DeleteBook(id);

            return Ok(isDeleted);
        }
    }
}
