// <copyright file="BadgeBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public class BadgeBuilder : IBadgeBuilder
	{
		/// <summary>
		/// </summary>
		/// <param name="badgeClient"></param>
		public BadgeBuilder(IBadgeClient badgeClient)
		{
			BadgeClient = badgeClient;
		}

		public IBadgeClient BadgeClient { get; set; }

		/// <inheritdoc />
		public async Task<Badge> BuildBadgeAsync(BadgeBuilderProperties badgeBuilderProperties,
			IAnalyzerResultAccessor analyzerResultAccessor)
		{
			string label = badgeBuilderProperties.Label;
			BadgeStyle badgeStyle = badgeBuilderProperties.Style;
			string format = badgeBuilderProperties.Format;

			try
			{
				BadgeClientProperties badgeClientProperties = new BadgeClientProperties() { Label = label, Style = badgeStyle, Format = format };

				AnalyzerResult analyzerResult = await analyzerResultAccessor.GetAnalyzerResultAsync();

				badgeClientProperties.Color = DetermineColor(badgeBuilderProperties, analyzerResult);

				string template = DetermineTemplate(badgeBuilderProperties, analyzerResult);
				int violations = DetermineViolations(badgeBuilderProperties, analyzerResult);

				badgeClientProperties.Status = violations > 0 ? string.Format(template, violations) : template;

				return await BadgeClient.RequestBadgeAsync(badgeClientProperties);
			}
			catch (Exception)
			{
				return
					await
						BadgeClient.RequestBadgeAsync(
							new BadgeClientProperties()
							{
								Label = label,
								Status = badgeBuilderProperties.StatusTemplateInaccessible,
								Style = badgeStyle,
								Color = badgeBuilderProperties.ColorInaccessible,
								Format = format
							});
			}
		}

		private BadgeColor DetermineColor(BadgeBuilderProperties badgeBuilderProperties, AnalyzerResult analyzerResult)
		{
			if (analyzerResult.NumberOfErrors > 0)
			{
				return badgeBuilderProperties.ColorError;
			}

			if (analyzerResult.NumberOfWarnings > 0)
			{
				return badgeBuilderProperties.ColorWarning;
			}

			if (analyzerResult.NumberOfInfos > 0 && badgeBuilderProperties.CountInfos)
			{
				return badgeBuilderProperties.ColorInfo;
			}

			return badgeBuilderProperties.ColorSuccess;
		}

		private string DetermineTemplate(BadgeBuilderProperties badgeBuilderProperties, AnalyzerResult analyzerResult)
		{
			if (analyzerResult.NumberOfErrors > 0)
			{
				return badgeBuilderProperties.AggregateValues
					? badgeBuilderProperties.StatusTemplateAggregate : badgeBuilderProperties.StatusTemplateError;
			}

			if (analyzerResult.NumberOfWarnings > 0)
			{
				return badgeBuilderProperties.AggregateValues
					? badgeBuilderProperties.StatusTemplateAggregate : badgeBuilderProperties.StatusTemplateWarning;
			}

			if (analyzerResult.NumberOfInfos > 0 && badgeBuilderProperties.CountInfos)
			{
				return badgeBuilderProperties.AggregateValues
					? badgeBuilderProperties.StatusTemplateAggregate : badgeBuilderProperties.StatusTemplateInfo;
			}

			return badgeBuilderProperties.StatusTemplateSuccess;
		}

		private int DetermineViolations(BadgeBuilderProperties badgeBuilderProperties, AnalyzerResult analyzerResult)
		{
			int violations = analyzerResult.NumberOfErrors;

			if (violations == 0 || badgeBuilderProperties.AggregateValues)
			{
				violations += analyzerResult.NumberOfWarnings;
			}

			if (badgeBuilderProperties.CountInfos && (violations == 0 || badgeBuilderProperties.AggregateValues))
			{
				violations += analyzerResult.NumberOfInfos;
			}

			return violations;
		}
	}
}