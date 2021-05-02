using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarshipsFun.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace StarshipsFun.Services.Helpers
{
    public static class ServiceExecutor
	{
        public static async Task<ServiceResponse<T>> ExecuteAsync<T>(ILogger logger, Func<Task<HttpResponseMessage>> func)
		{
			var callingMethod = $"{ func.Method.DeclaringType } { func.Method.Name}";
			try
			{
				using var httpResponseMessage = await func.Invoke();			
				var statusCode = httpResponseMessage.StatusCode;
				if (!httpResponseMessage.IsSuccessStatusCode)
				{
					logger.LogWarning($"Unsuccessful response from {callingMethod} Status code:{statusCode}");
					return new ServiceResponse<T>(statusCode, default);
				}

				logger.LogInformation($"Successful response from {callingMethod} Status code:{statusCode}");
				var content = await httpResponseMessage.Content.ReadAsStringAsync();
				T data = JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				logger.LogInformation("Successful api response and object deserializaton");
				return new ServiceResponse<T>(statusCode, data);									
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"There was an error executing {callingMethod} : error message:{ex.Message}", default);
				return new ServiceResponse<T>(HttpStatusCode.InternalServerError);
			}
		}		
	}
}
