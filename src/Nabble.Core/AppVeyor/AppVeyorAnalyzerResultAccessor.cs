// <copyright file="AppVeyorAnalyzerResultAccessor.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.AppVeyor
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Exceptions;
	using Nabble.Core.Sarif;

	/// <summary>
	/// Provides an implementation of <see cref="IAnalyzerResultAccessor" /> to access analyzer results from AppVeyor builds.
	/// </summary>
	public class AppVeyorAnalyzerResultAccessor : IAnalyzerResultAccessor
	{
		private string apiUrl = "https://ci.appveyor.com/api/";

		/// <summary>
		/// Initializes a new instance of the <see cref="AppVeyorAnalyzerResultAccessor" /> class.
		/// </summary>
		/// <param name="restClient">The <see cref="IRestClient" /> used to communicate with AppVeyor.</param>
		/// <param name="jsonDeserializer">The <see cref="IJsonDeserializer" /> used to deserialize JSON streams.</param>
		/// <param name="sarifResultJsonDeserializer">
		/// The <see cref="ISarifJsonDeserializer" /> used to deserialize SARIF JSON streams.
		/// </param>
		/// <param name="analyzerResultBuilder">
		/// The <see cref="IAnalyzerResultBuilder" /> used to generate analyzer results from analyzer log files.
		/// </param>
		/// <param name="cache">The <see cref="ICache" /> used for caching.</param>
		/// <param name="statisticsService">The <see cref="IStatisticsService" /> used to modify and get certain badge statistics.</param>
		public AppVeyorAnalyzerResultAccessor(IRestClient restClient, IJsonDeserializer jsonDeserializer,
			ISarifJsonDeserializer sarifResultJsonDeserializer, IAnalyzerResultBuilder analyzerResultBuilder, ICache cache, IStatisticsService statisticsService)
		{
			RestClient = restClient;
			JsonDeserializer = jsonDeserializer;
			AnalyzerResultJsonDeserializer = sarifResultJsonDeserializer;
			AnalyzerResultBuilder = analyzerResultBuilder;
			Cache = cache;
			StatisticsService = statisticsService;
		}

		/// <summary>
		/// Gets or sets the AppVeyor AccountName used to retrieve the analyzer log build artefact.
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the AnalyzerResultBuilder used to generate analyzer results from analyzer log files.
		/// </summary>
		public IAnalyzerResultBuilder AnalyzerResultBuilder { get; set; }

		/// <summary>
		/// Gets or sets the AnalyzerResultJsonDeserializer used to deserialize SARIF JSON streams.
		/// </summary>
		public ISarifJsonDeserializer AnalyzerResultJsonDeserializer { get; set; }

		/// <summary>
		/// Gets or sets the AppVeyor BuildBranch used to retrieve the analyzer log build artefact.
		/// </summary>
		public string BuildBranch { get; set; }

		/// <summary>
		/// Gets or sets the Cache used for caching.
		/// </summary>
		public ICache Cache { get; set; }

		/// <summary>
		/// Gets or sets JsonDeserializer used to deserialize JSON streams.
		/// </summary>
		public IJsonDeserializer JsonDeserializer { get; set; }

		/// <summary>
		/// Gets or sets the AppVeyor ProjectSlug used to retrieve the analyzer log build artefact.
		/// </summary>
		public string ProjectSlug { get; set; }

		/// <summary>
		/// Gets or sets the ReportFileName used to identify the report artefacts.
		/// </summary>
		public string ReportFileName { get; set; }

		/// <summary>
		/// Gets or sets the RestClient used to communicate with AppVeyor.
		/// </summary>
		public IRestClient RestClient { get; set; }

		/// <summary>
		/// Gets or sets the StatisticsService used to count badge creations and requests.
		/// </summary>
		public IStatisticsService StatisticsService { get; set; }

		/// <inheritdoc />
		public async Task<AnalyzerResult> GetAnalyzerResultAsync()
		{
			string jobId;

			if (string.IsNullOrEmpty(BuildBranch))
			{
				jobId = await GetJobIdFromLastBuildAsync(AccountName, ProjectSlug);
			}
			else
			{
				jobId = await GetJobIdFromLastBuildAsync(AccountName, ProjectSlug, BuildBranch);
			}

			await StatisticsService.AddProjectEntryIfNotExistsAsync(AccountName, ProjectSlug);
			await StatisticsService.AddBadgeEntryIfNotExistsAsync(jobId);

			ICollection<SarifResult> sarifResults = await GetSarifResultsForJobIdAsync(jobId);

			return AnalyzerResultBuilder.AnalyzeSarifResults(sarifResults);
		}

		private Task<Stream> DownloadArtifactStreamAsync(string jobId, string artifactFileName)
		{
			return RestClient.GetJsonAsStreamAsync(
				new Uri(this.apiUrl),
				"buildjobs/{0}/artifacts/{1}",
				new object[] { jobId, artifactFileName },
				new KeyValuePair<object, object>[] { });
		}

		[Cache(Duration = 5)]
		private async Task<string> GetJobIdFromLastBuildAsync(string accountName, string projectSlug)
		{
			ProjectBuildResult result =
				JsonDeserializer.DeserializeFromStream<ProjectBuildResult>(
					await
						RestClient.GetJsonAsStreamAsync(
							new Uri(this.apiUrl),
							"projects/{0}/{1}",
							new object[] { accountName, projectSlug },
							new KeyValuePair<object, object>[] { }));

			if (result.Build.Jobs.First().Finished == null)
			{
				throw new BuildPendingException();
			}

			return result.Build.Jobs.First().JobId;
		}

		[Cache(Duration = 5)]
		private async Task<string> GetJobIdFromLastBuildAsync(string accountName, string projectSlug, string buildBranch)
		{
			ProjectBuildResult result =
				JsonDeserializer.DeserializeFromStream<ProjectBuildResult>(
					await
						RestClient.GetJsonAsStreamAsync(
							new Uri(this.apiUrl),
							"projects/{0}/{1}/branch/{2}",
							new object[] { accountName, projectSlug, buildBranch },
							new KeyValuePair<object, object>[] { }));

			if (result.Build.Jobs.First().Finished == null)
			{
				throw new BuildPendingException();
			}

			return result.Build.Jobs.First().JobId;
		}

		private async Task<IEnumerable<string>> GetReportNamesForJobIdAsync(string jobId)
		{
			Artifact[] result =
				JsonDeserializer.DeserializeFromStream<Artifact[]>(
					await
						RestClient.GetJsonAsStreamAsync(
							new Uri(this.apiUrl),
							"buildjobs/{0}/artifacts",
							new object[] { jobId },
							new KeyValuePair<object, object>[] { }));

			return result.Select(x => x.FileName);
		}

		[Cache(Duration = 7 * 24 * 60 * 60)]
		private async Task<ICollection<SarifResult>> GetSarifResultsForJobIdAsync(string jobId)
		{
			IEnumerable<string> reportNames =
				(await GetReportNamesForJobIdAsync(jobId)).Where(
					x => x == ReportFileName || x.EndsWith(string.Format("/{0}", ReportFileName))).ToList();

			if (!reportNames.Any())
			{
				throw new SarifResultNotFoundException();
			}

			ICollection<SarifResult> sarifResults = new List<SarifResult>();

			foreach (string reportName in reportNames)
			{
				Stream stream = await DownloadArtifactStreamAsync(jobId, reportName);
				sarifResults.Add(AnalyzerResultJsonDeserializer.DeserializeFromStream(stream));
			}

			return sarifResults;
		}
	}
}