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
	using Nabble.Core.Sarif;

	/// <summary>
	/// </summary>
	public class AppVeyorAnalyzerResultAccessor : IAnalyzerResultAccessor
	{
		private string apiUrl = "https://ci.appveyor.com/api/";


		/// <summary>
		/// </summary>
		/// <param name="restClient"></param>
		/// <param name="jsonDeserializer"></param>
		/// <param name="sarifResultJsonDeserializer"></param>
		/// <param name="analyzerResultBuilder"></param>
		public AppVeyorAnalyzerResultAccessor(IRestClient restClient, IJsonDeserializer jsonDeserializer,
			ISarifJsonDeserializer sarifResultJsonDeserializer, IAnalyzerResultBuilder analyzerResultBuilder)
		{
			RestClient = restClient;
			JsonDeserializer = jsonDeserializer;
			AnalyzerResultJsonDeserializer = sarifResultJsonDeserializer;
			AnalyzerResultBuilder = analyzerResultBuilder;
		}

		/// <summary>
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// </summary>
		public IAnalyzerResultBuilder AnalyzerResultBuilder { get; set; }

		/// <summary>
		/// </summary>
		public ISarifJsonDeserializer AnalyzerResultJsonDeserializer { get; set; }

		/// <summary>
		/// </summary>
		public string BuildBranch { get; set; }

		/// <summary>
		/// </summary>
		public IJsonDeserializer JsonDeserializer { get; set; }

		/// <summary>
		/// </summary>
		public string ProjectSlug { get; set; }

		/// <summary>
		/// </summary>
		public IRestClient RestClient { get; set; }

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

			// TODO: Decide what to do if no reportName is returned
			IEnumerable<string> reportNames = await GetReportNamesForJobIdAsync(jobId);

			ICollection<SarifResult> sarifResults = new List<SarifResult>();

			// TODO: Make name configurable
			foreach (string reportName in reportNames.Where(x => x.EndsWith("report.json")))
			{
				Stream stream = await DownloadArtifactStreamAsync(jobId, reportName);
				sarifResults.Add(AnalyzerResultJsonDeserializer.DeserializeFromStream(stream));
			}

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

			return result.Build.Jobs.First().JobId;
		}

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
	}
}