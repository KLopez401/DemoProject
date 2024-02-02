using DemoProject.Application.Dtos;
using DemoProject.Application.Interface.Repository;
using DemoProject.Application.Interface.Service;
using DemoProject.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DemoProject.Application.Concrete
{
    /// <summary>
    /// Book service that contains all the logic in managing book catalog
    /// </summary>
    public class BookService : IBookService
    {
        private IRepository<Book> _bookRepository;
        private ICategoryService _categoryService;
        public BookService(IRepository<Book> bookRepository, ICategoryService categoryService)
        {
            _bookRepository = bookRepository;
            _categoryService = categoryService;

        }
        /// <summary>
        /// Insert book information
        /// </summary>
        /// <param name="bookDto"></param>
        public async Task AddBooks(QueryBookDto queryDto)
        {
            var book = new Book()
            {
                Title = queryDto.Title,
                Description = queryDto.Description,
                Category = new Category()
                {
                    Name = queryDto.CategoryName,
                },
                PublishDateUtc = DateTime.UtcNow,
            };

            await _bookRepository.Add(book);
        }
        /// <summary>
        /// delete book method that delete book on the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBook(int id)
        {
           var book = await _bookRepository.GetById(id);

            if (book != null)
            {
                await _bookRepository.Delete(book);

                if(book.Category != null)
                {
                    await _categoryService.DeleteCategory(book.CategoryId.GetValueOrDefault());
                }

                return true;
            }

            return false;
        }
        /// <summary>
        /// get all books base on the pagination and filter query
        /// </summary>
        /// <param name="paginationDto"></param>
        /// <param name="bookDto"></param>
        /// <returns>all/filtered books</returns>
        public async Task<PaginatedBookDto> GetAllBooks(PaginationDto paginationDto, QueryBookDto queryBookDto)
        {
            var bookList = await _bookRepository.GetAll().Result.ToListAsync();

            foreach (var book in bookList) { }
            var bookDtoList = bookList.Select(x => new BookDto
            {
                Title = x.Title ?? string.Empty,
                Description = x.Description ?? string.Empty,
                PublishedDate = x.PublishDateUtc.ToString() ?? string.Empty,
                CategoryName = _categoryService.GetCategoryById(x.CategoryId.GetValueOrDefault()).Result.Name
            }).ToList();

            bookDtoList = FilteredBooks(queryBookDto, bookDtoList).ToList();

            var paginatedBook = PaginateBook(bookDtoList, paginationDto);

            return paginatedBook;
        }
        /// <summary>
        /// get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>book</returns>
        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);

            var bookDto = new BookDto();
            if (book != null)
            {
                bookDto.Title = book.Title ?? string.Empty;
                bookDto.Description = book.Description ?? string.Empty;
                bookDto.PublishedDate = book.PublishDateUtc.ToString() ?? string.Empty;
                bookDto.CategoryName = _categoryService.GetCategoryById(book.CategoryId.GetValueOrDefault()).Result.Name;
            }

            return bookDto;
        }
        /// <summary>
        /// update book base on the query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns>true/false</returns>
        public async Task<bool> UpdateBook(int id, QueryBookDto queryBookDto)
        {
            var book = await _bookRepository.GetById(id);

            if (book != null)
            {
                book.Title = queryBookDto.Title ?? string.Empty;
                book.Description = queryBookDto.Description ?? string.Empty;
                if (book.Category != null)
                {
                    await _categoryService.UpdateCategory(book.CategoryId.GetValueOrDefault(), queryBookDto.CategoryName);
                }

                await _bookRepository.Update(book);

                return true;
            }

            return false;
        }

        /// <summary>
        /// parse datestring to datetime
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        private DateTime ParsDate(string dateString)
        {
            var cultureInfo = new CultureInfo("en-US");
            var dateTime = DateTime.ParseExact(dateString, "dd-MM-yyyy hh:mm:ss", cultureInfo);

            return dateTime;
        }
        /// <summary>
        /// a private method that filters book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <param name="bookList"></param>
        /// <returns>filtered books</returns>
        private IEnumerable<BookDto> FilteredBooks(QueryBookDto queryDto, List<BookDto> bookList)
        {
            if (bookList.Any())
            {
                if (queryDto != null)
                {
                    if (!string.IsNullOrEmpty(queryDto.Title))
                    {
                        bookList = bookList.Where(x => x.Title?.ToLower().Contains(queryDto.Title.ToLower()) == true).ToList();
                    }

                    if (!string.IsNullOrEmpty(queryDto.Description))
                    {
                        bookList = bookList.Where(x => x.Description?.ToLower().Contains(queryDto.Description.ToLower()) == true).ToList();
                    }

                    if (!string.IsNullOrEmpty(queryDto.CategoryName))
                    {
                        bookList = bookList.Where(x => x.CategoryName.ToLower().Contains(queryDto.CategoryName.ToLower()) == true).ToList();
                    }
                }
            }

            return bookList;
        }
        /// <summary>
        /// private method that paginate books
        /// </summary>
        /// <param name="bookList"></param>
        /// <param name="paginationDto"></param>
        /// <returns>paginatedbook</returns>
        private PaginatedBookDto PaginateBook(IEnumerable<BookDto> bookList, PaginationDto paginationDto)
        {
            var paginatedBook = new PaginatedBookDto();
            paginatedBook.Pagination.PageNumber = paginationDto.PageNumber;
            paginatedBook.Pagination.PageSize = paginationDto.PageSize;
            paginatedBook.BookListDto = bookList.Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
                                    .Take(paginationDto.PageSize).ToList();
            paginatedBook.TotalCount = paginatedBook.BookListDto.Count();
            paginatedBook.TotalPages = ((bookList.Count() - 1) / paginatedBook.Pagination.PageSize) + 1;

            return paginatedBook;
        }
    }
}
