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
	/// </summary>
	public interface IRestClient
	{
		/// <summary>
		/// </summary>
		/// <param name="baseUri"></param>
		/// <param name="path"></param>
		/// <param name="pathParameters"></param>
		/// <param name="getParameters"></param>
		/// <returns></returns>
		Task<HttpResponseMessage> GetHttpResponse(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters);

		/// <summary>
		/// </summary>
		/// <param name="baseUri"></param>
		/// <param name="path"></param>
		/// <param name="pathParameters"></param>
		/// <param name="getParameters"></param>
		/// <returns></returns>
		Task<Stream> GetJsonAsStreamAsync(Uri baseUri, string path, object[] pathParameters,
			IEnumerable<KeyValuePair<object, object>> getParameters);
	}
}