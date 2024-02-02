using DemoProject.Application.Dtos;
using DemoProject.Domain.Model;

namespace DemoProject.Application.Interface.Service
{
    /// <summary>
    /// category interface that contains contracts implemented on categoryservice
    /// </summary>
    public interface ICategoryService
    {
        Task AddCategory(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<bool> UpdateCategory(int id, string name);
        Task<bool> DeleteCategory(int id);
    }
}
