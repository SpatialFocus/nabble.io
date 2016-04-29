// <copyright file="BadgeBuilderProperties.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	/// <summary>
	/// </summary>
	public class BadgeBuilderProperties
	{
		/// <summary>
		/// </summary>
		public BadgeBuilderProperties()
		{
			AggregateValues = false;
			CountInfos = false;

			ColorInaccessible = BadgeColor.Lightgray;
			ColorError = BadgeColor.Red;
			ColorWarning = BadgeColor.Yellow;
			ColorInfo = BadgeColor.Green;
			ColorSuccess = BadgeColor.BrightGreen;

			Label = "Analyzers";

			StatusTemplateError = "{0} errors";
			StatusTemplateWarning = "{0} warnings";
			StatusTemplateInfo = "{0} infos";
			StatusTemplateAggregate = "{0} violations";
			StatusTemplateSuccess = "passing";
			StatusTemplateInaccessible = "inaccessible";

			Format = "svg";
		}

		/// <summary>
		/// </summary>
		public bool AggregateValues { get; set; }

		/// <summary>
		/// </summary>
		public BadgeColor ColorError { get; set; }

		/// <summary>
		/// </summary>
		public BadgeColor ColorInaccessible { get; set; }

		/// <summary>
		/// </summary>
		public BadgeColor ColorInfo { get; set; }

		/// <summary>
		/// </summary>
		public BadgeColor ColorSuccess { get; set; }

		/// <summary>
		/// </summary>
		public BadgeColor ColorWarning { get; set; }

		/// <summary>
		/// </summary>
		public bool CountInfos { get; set; }

		/// <summary>
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateAggregate { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateError { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateInaccessible { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateInfo { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateSuccess { get; set; }

		/// <summary>
		/// </summary>
		public string StatusTemplateWarning { get; set; }


		/// <summary>
		/// </summary>
		public BadgeStyle Style { get; set; }
	}
}