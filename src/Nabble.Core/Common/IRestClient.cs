// <copyright file="IRestClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an interface that provides methods to send HTTP GET requests.
	/// </summary>
	public interface IRestClient
	{
		/// <summary>
		/// Sends a HTTP request to the specified location and returns the <see cref="HttpResponseMessage" /> as an asynchronous operation.
		/// </summary>
		/// <param name="baseUri">The Uri the request is sent to.</param>
		/// <param name="path">The path the request is sent to.</param>
		/// <param name="pathParameters">The path parameter values used in <paramref name="path" />.</param>
		/// <param name="getParameters">The get parameters used for this request.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<HttpResponseMessage> GetHttpResponseAsync(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters);

		/// <summary>
		/// Sends a HTTP request to the specified location and returns the JSON <see cref="Stream" /> as an asynchronous operation.
		/// </summary>
		/// <param name="baseUri">The Uri the request is sent to.</param>
		/// <param name="path">The path the request is sent to.</param>
		/// <param name="pathParameters">The path parameter values used in <paramref name="path" />.</param>
		/// <param name="getParameters">The get parameters used for this request.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<Stream> GetJsonAsStreamAsync(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters);
	}
}