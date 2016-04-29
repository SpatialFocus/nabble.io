// <copyright file="RestClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public class RestClient : IRestClient
	{
		/// <inheritdoc />
		public async Task<HttpResponseMessage> GetHttpResponse(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = baseUri;
				client.DefaultRequestHeaders.Accept.Clear();

				string queryString = string.Join(
					"&",
					getParameters.Select(
						x => string.Format("{0}={1}", Uri.EscapeDataString(x.Key.ToString()), Uri.EscapeDataString(x.Value.ToString()))));

				return await client.GetAsync(string.Format("{0}?{1}", string.Format(path, pathParameters), queryString));
			}
		}

		/// <inheritdoc />
		public async Task<Stream> GetJsonAsStreamAsync(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = baseUri;
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				string queryString = string.Join(
					"&",
					getParameters.Select(
						x => string.Format("{0}={1}", Uri.EscapeDataString(x.Key.ToString()), Uri.EscapeDataString(x.Value.ToString()))));

				HttpResponseMessage response =
					await client.GetAsync(string.Format("{0}?{1}", string.Format(path, pathParameters), queryString));

				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStreamAsync();
			}
		}
	}
}