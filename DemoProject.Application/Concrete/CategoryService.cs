using DemoProject.Application.Dtos;
using DemoProject.Application.Interface.Repository;
using DemoProject.Application.Interface.Service;
using DemoProject.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Application.Concrete
{
    /// <summary>
    /// category service that contains logic for managing category
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        /// <summary>
        /// Add category that inserts data to the database
        /// </summary>
        /// <param name="categoryDto"></param>
        public async Task AddCategory(CategoryDto categoryDto)
        {
            var category = new Category()
            {
                Name = categoryDto.Name,
            };

            await _categoryRepository.Add(category);
        }
        /// <summary>
        /// delete category base on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category != null)
            {
                await _categoryRepository.Delete(category);

                return true;
            }

            return false;
        }
        /// <summary>
        /// get all categories base on pagination and category query
        /// </summary>
        /// <param name="paginationDto"></param>
        /// <param name="categoryDto"></param>
        /// <returns>all/filtered categories</returns>
        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll().Result.ToListAsync();

            var categoriesDto = categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name ?? "",
            }).OrderByDescending(x => x.Id)
              .ToList();

            return categoriesDto;
        }
        /// <summary>
        /// get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>category</returns>
        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetById(id);

            var categoryDto = new CategoryDto();
            if (category != null)
            {
                categoryDto.Id = category.Id;
                categoryDto.Name = category.Name ?? string.Empty;
            }

            return categoryDto;
        }
        /// <summary>
        /// update category on the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDto"></param>
        /// <returns>true/false</returns>
        public async Task<bool> UpdateCategory(int id, string name)
        {
            var category = await _categoryRepository.GetById(id);

            if (category != null)
            {
                category.Name = name;

                await _categoryRepository.Update(category);

                return true;
            }

            return false;
        }
    }
}
