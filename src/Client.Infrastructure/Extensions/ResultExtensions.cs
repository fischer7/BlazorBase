using GlobalShared.Constants.Erro;
using GlobalShared.Wrapper;
using MudBlazor;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Infrastructure.Extensions
{
    internal static class ResultExtensions
    {
        internal static async Task<Result<T>> ToResult<T>(this HttpResponseMessage response, ISnackbar? _snackBar = null)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            responseObject = (responseObject?.IsSuccess ?? false) ? responseObject :
                            JsonSerializer.Deserialize<ValidationResult<T>>(responseAsString, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                                ReferenceHandler = ReferenceHandler.Preserve,

                            });
            if ((responseObject?.IsFailure ?? false) && responseObject is ValidationResult<T>)
            {
                var validationResult = responseObject as ValidationResult<T>;

                foreach (var erro in validationResult!.Errors)
                {
                    _snackBar?.Add(erro.Message, Severity.Error);
                }
            }
            return responseObject ?? Result.Create<T>(default);
        }

        internal static async Task<Result> ToResult(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return responseObject ?? Result.Failure(ErrorConstants.NullValue);
        }

        internal static async Task<PaginatedQueryResult<T>> ToPaginatedResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PaginatedQueryResult<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return responseObject ?? new PaginatedQueryResult<T>();
        }
    }
}