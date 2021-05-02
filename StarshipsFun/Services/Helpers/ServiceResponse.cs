using Newtonsoft.Json;
using System.Net;

namespace StarshipsFun.Services.Helpers
{
    public class ServiceResponse<T>
	{
		public ServiceResponse(HttpStatusCode status)
		{
			Status = status;
		}
		public ServiceResponse(HttpStatusCode status, T data)
		{
			Status = status;
			Data = data;
		}
		public ServiceResponse() { }
		public T Data { get; set; }

		//Error converting api value "success" to type 'System.Net.HttpStatusCode', hence the attribute
		[JsonIgnore]
		public HttpStatusCode Status { get; set; }
	}
}
