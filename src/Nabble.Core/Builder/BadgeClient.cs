// <copyright file="BadgeClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Nabble.Core.Common;

	/// <summary>
	/// Provides an implementation of <see cref="IBadgeClient" /> to request Badges.
	/// </summary>
	public class BadgeClient : IBadgeClient
	{
		private readonly IDictionary<BadgeStyle, string> badgeStyleMapping = new Dictionary<BadgeStyle, string>()
		{
			{ BadgeStyle.Default, "default" },
			{ BadgeStyle.Plastic, "plastic" },
			{ BadgeStyle.Flat, "flat" },
			{ BadgeStyle.FlatSquare, "flat-square" },
			{ BadgeStyle.Social, "social" }
		};

		// TODO: Read from configuration file
		private readonly string badgesUrl = "http://localhost:8080/";

		/// <summary>
		/// Initializes a new instance of the <see cref="BadgeClient" /> class.
		/// </summary>
		/// <param name="restClient">The <see cref="IRestClient" /> used to communicate with the Badge service.</param>
		public BadgeClient(IRestClient restClient)
		{
			RestClient = restClient;
		}

		/// <summary>
		/// Gets or sets the RestClient used to communicate with the Badge service.
		/// </summary>
		public IRestClient RestClient { get; set; }

		/// <inheritdoc />
		public async Task<Badge> RequestBadgeAsync(BadgeClientProperties badgeClientProperties)
		{
			Dictionary<object, object> getParameters = new Dictionary<object, object>()
			{
				{ "color", badgeClientProperties.Color.ToString().ToLower() },
				{ "label", badgeClientProperties.Label },
				{ "status", badgeClientProperties.Status },
				{ "style", this.badgeStyleMapping[badgeClientProperties.Style] },
				{ "format", badgeClientProperties.Format }
			};

			HttpResponseMessage httpResponseMessage =
				await RestClient.GetHttpResponseAsync(new Uri(this.badgesUrl), string.Empty, new object[] { }, getParameters);

			return new Badge()
			{
				ContentType = httpResponseMessage.Content.Headers.ContentType.MediaType,
				Stream = await httpResponseMessage.Content.ReadAsStreamAsync()
			};
		}
	}
}