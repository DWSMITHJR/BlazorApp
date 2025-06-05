using System.Threading.Tasks;
using BlazorDemo.Models;

namespace BlazorDemo.Services
{
    public interface IApiService
    {
        Task<int> GenerateCodeAsync(string email);
        Task<string> CallApiAsync(string endpoint);
    }
}
