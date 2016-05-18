// <copyright file="BadgeBuilderProperties.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	/// <summary>
	/// A class providing configuration for the <see cref="Badge" /> creation used by <see cref="IBadgeBuilder" /> instances.
	/// </summary>
	public class BadgeBuilderProperties
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BadgeBuilderProperties" /> class.
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
			StatusTemplatePending = "pending";
			StatusTemplateInaccessible = "inaccessible";

			Format = "svg";
		}

		/// <summary>
		/// Gets or sets a value indicating whether values of different severities should be aggregated or not.
		/// </summary>
		public bool AggregateValues { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="BadgeColor" /> used for Badges when the highest occuring severity is Error.
		/// </summary>
		public BadgeColor ColorError { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="BadgeColor" /> used for Badges when the analyzer log couldn't be requested / analyzed.
		/// </summary>
		public BadgeColor ColorInaccessible { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="BadgeColor" /> used for Badges when the highest occuring severity is Info.
		/// </summary>
		public BadgeColor ColorInfo { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="BadgeColor" /> used for Badges without any errors.
		/// </summary>
		public BadgeColor ColorSuccess { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="BadgeColor" /> used for Badges when the highest occuring severity is Warning.
		/// </summary>
		public BadgeColor ColorWarning { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether values with severity Info should be counted or not.
		/// </summary>
		public bool CountInfos { get; set; }

		/// <summary>
		/// Gets or sets the Badge Format (svg, png, jpg, gif, json).
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Gets or sets the Badge Label.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateAggregate used for Badges when AggregateValues is true.
		/// </summary>
		public string StatusTemplateAggregate { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateError used for Badges when the highest occuring severity is Error.
		/// </summary>
		public string StatusTemplateError { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateError used for Badges when the analyzer log couldn't be requested / analyzed.
		/// </summary>
		public string StatusTemplateInaccessible { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateError used for Badges when the highest occuring severity is Info.
		/// </summary>
		public string StatusTemplateInfo { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplatePending used for Badges when the build is still running.
		/// </summary>
		public string StatusTemplatePending { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateError used for Badges without any errors.
		/// </summary>
		public string StatusTemplateSuccess { get; set; }

		/// <summary>
		/// Gets or sets the StatusTemplateError used for Badges when the highest occuring severity is Warning.
		/// </summary>
		public string StatusTemplateWarning { get; set; }

		/// <summary>
		/// Gets or sets the Badge Style.
		/// </summary>
		public BadgeStyle Style { get; set; }
	}
}