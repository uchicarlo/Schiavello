using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace blazor_todo.Client.Utility
{
	public static class SharedQueryHelper
	{
		private static JsonSerializerSettings _jsonSerializerSettings = new() { DateTimeZoneHandling = DateTimeZoneHandling.Utc };
		public static async Task<(TResponseType?, GraphQLError[]?)> HistoricalSelect<TResponseType>(this HttpClient http,string query, string? operationName = "",
			object? parameters = null, CancellationToken cancellationToken = default!)
		{
			try
			{
				JsonConvert.DefaultSettings = () => new() { DateTimeZoneHandling = DateTimeZoneHandling.Utc };
				if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException(nameof(query));
				GraphQLHttpClientOptions options = new() { EndPoint = http.BaseAddress };
				var _graphQLClient = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer(_jsonSerializerSettings), http);
				var request = new GraphQLRequest();
				if (parameters == null)
				{
					request = new GraphQLRequest
					{
						Query = query
					};
				}
				else
				{
					if (string.IsNullOrWhiteSpace(operationName))
					{
						request = new GraphQLRequest
						{
							Query = query,
							Variables = parameters
						};
					}
					else
					{
						request = new GraphQLRequest
						{
							Query = query,
							OperationName = operationName,
							Variables = parameters
						};
					}
				}
				var response = await _graphQLClient.SendQueryAsync<TResponseType?>(request, cancellationToken);
				return (response.Data, response.Errors);
			}
			catch (GraphQLHttpRequestException ex)
			{
				List<GraphQLError> errors = new()
				{
					new()
					{
						Message = ex.Message,
					}
				};
				var exception = ex;
				return (new GraphQLResponse<TResponseType>().Data, errors.ToArray());
			}
			catch (Exception ex)
			{
				List<GraphQLError> errors = new()
				{
					new()
					{
						Message = ex.Message,
					}
				};
				var exception = ex;
				return (new GraphQLResponse<TResponseType>().Data, errors.ToArray());
			}
		}
	}
}
