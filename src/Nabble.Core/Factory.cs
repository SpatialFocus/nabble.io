// <copyright file="AnalyzerResultAccessorFactory.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.Collections.Generic;
	using Nabble.Core.AppVeyor;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Preview;
	using Nabble.Core.Sarif;

	/// <summary>
	/// </summary>
	public static class Factory
	{
		/// <summary>
		/// </summary>
		/// <param name="rules"></param>
		/// <param name="accountName"></param>
		/// <param name="projectSlug"></param>
		/// <param name="buildBranch"></param>
		/// <returns></returns>
		public static IAnalyzerResultAccessor CreateAppVeyorAnalyzerResultAccessor(ICollection<string> rules,
			string accountName, string projectSlug, string buildBranch)
		{
			return new AppVeyorAnalyzerResultAccessor(
				new RestClient(),
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = rules })
			{
				AccountName = accountName,
				ProjectSlug = projectSlug,
				BuildBranch = buildBranch
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="previewSettings"></param>
		/// <returns></returns>
		public static IAnalyzerResultAccessor CreatePreviewAnalyzerResultAccessor(PreviewSettings previewSettings)
		{
			return new PreviewAnalyzerResultAccessor { PreviewSettings = previewSettings };
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public static IBadgeBuilder CreateBadgeBuilder()
		{
			return new BadgeBuilder(new BadgeClient(new RestClient()));
		}
	}
}