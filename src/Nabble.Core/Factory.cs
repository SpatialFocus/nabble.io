// <copyright file="Factory.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.Collections.Generic;
	using Nabble.Core.AppVeyor;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Sarif;

	/// <summary>
	/// A class used to create common objects.
	/// </summary>
	public static class Factory
	{
		/// <summary>
		/// Creates a new instance of the <see cref="AppVeyorAnalyzerResultAccessor" /> class.
		/// </summary>
		/// <param name="rules">The collection of rules used to analyze SARIF results.</param>
		/// <param name="accountName">The AppVeyor AccountName used to retrieve the analyzer log build artefact.</param>
		/// <param name="projectSlug">The AppVeyor ProjectSlug used to retrieve the analyzer log build artefact.</param>
		/// <param name="buildBranch">The AppVeyor BuildBranch used to retrieve the analyzer log build artefact.</param>
		/// <returns>An instance of the created <see cref="AppVeyorAnalyzerResultAccessor" /> class.</returns>
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
		/// Creates a new instance of the <see cref="IBadgeBuilder" /> interface.
		/// </summary>
		/// <returns>An instance of the created <see cref="IBadgeBuilder" /> interface.</returns>
		public static IBadgeBuilder CreateBadgeBuilder()
		{
			return new BadgeBuilder(new BadgeClient(new RestClient()));
		}
	}
}