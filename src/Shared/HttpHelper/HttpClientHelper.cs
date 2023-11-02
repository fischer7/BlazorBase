using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalShared.HttpHelper;
public static class HttpClientHelper
{
    /// <summary>
    /// Frequently used http post.
    /// </summary>
    public static async Task<HttpResponseMessage> PostWithJsonBody(this HttpClient httpClient, string endpoint,
        object request, CancellationToken cancellationToken)
    {
        var serializedObj = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(serializedObj, Encoding.UTF8, MediaTypeNames.Application.Json);
        return await httpClient.PostAsync(endpoint, stringContent, cancellationToken);
    }

    public static async Task<HttpResponseMessage> PutWithJsonBody(this HttpClient httpClient, string endpoint,
    object request, CancellationToken cancellationToken)
    {
        var serializedObj = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(serializedObj, Encoding.UTF8, MediaTypeNames.Application.Json);
        return await httpClient.PutAsync(endpoint, stringContent, cancellationToken);
    }

}
