namespace Nabble.Core.Test
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Moq;
	using Nabble.Core.AppVeyor;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Exceptions;
	using Nabble.Core.Sarif;
	using Ploeh.AutoFixture.Xunit2;
	using Xunit;

	public class AppVeyorAnalyzerResultAccessorTests
	{
		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncBuildBranchConfigReturnsAnalyzerResultCorrectly(
			[Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}/branch/{2}",
						new object[] { "TestAccount", "TestProject", "TestBranch" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts",
						new object[] { "jobid" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_artifacts.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "TestProject/bin/Debug/report.json" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("report.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache())
			{
				AccountName = "TestAccount",
				ProjectSlug = "TestProject",
				BuildBranch = "TestBranch",
				ReportFileName = "report.json"
			};

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 1);
			Assert.True(analyzerResult.NumberOfErrors == 1);
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncCustomReportsReturnsAnalyzerResultCorrectly(
			[Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}",
						new object[] { "TestAccount", "TestProject" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts",
						new object[] { "jobid" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_artifacts_custom.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "TestProject/bin/Debug/custom.json" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("report.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "custom.json" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("report.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache()) { AccountName = "TestAccount", ProjectSlug = "TestProject", ReportFileName = "custom.json" };

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 2);
			Assert.True(analyzerResult.NumberOfErrors == 2);

			restClient.Verify(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "TestProject/bin/Debug/custom.json" },
						new KeyValuePair<object, object>[] { }));

			restClient.Verify(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "custom.json" },
						new KeyValuePair<object, object>[] { }));
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncMinimalConfigReturnsAnalyzerResultCorrectly(
			[Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}",
						new object[] { "TestAccount", "TestProject" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts",
						new object[] { "jobid" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_artifacts.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "TestProject/bin/Debug/report.json" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("report.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache()) { AccountName = "TestAccount", ProjectSlug = "TestProject", ReportFileName = "report.json" };

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 1);
			Assert.True(analyzerResult.NumberOfErrors == 1);
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncMissingReportThrowsException([Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}",
						new object[] { "TestAccount", "TestProject" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts",
						new object[] { "jobid" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_artifacts.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache()) { AccountName = "TestAccount", ProjectSlug = "TestProject", ReportFileName = "missing.json" };

			await Assert.ThrowsAsync<SarifResultNotFoundException>(async () => await resultAccessor.GetAnalyzerResultAsync());
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncMultipleReportsReturnsAnalyzerResultCorrectly(
			[Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}",
						new object[] { "TestAccount", "TestProject" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts",
						new object[] { "jobid" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_artifacts_multiple.json")));

			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"buildjobs/{0}/artifacts/{1}",
						new object[] { "jobid", "TestProject/bin/Debug/report.json" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("report.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache()) { AccountName = "TestAccount", ProjectSlug = "TestProject", ReportFileName = "report.json" };

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 2);
			Assert.True(analyzerResult.NumberOfErrors == 2);
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncPendingBuildBuildBranchThrowsException([Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}/branch/{2}",
						new object[] { "TestAccount", "TestProject", "TestBranch" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects_pending.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache())
			{
				AccountName = "TestAccount",
				ProjectSlug = "TestProject",
				BuildBranch = "TestBranch",
				ReportFileName = "report.json"
			};

			await Assert.ThrowsAsync<BuildPendingException>(async () => await resultAccessor.GetAnalyzerResultAsync());
		}

		[Theory]
		[AutoMoqData]
		public async void GetAnalyzerResultAsyncPendingBuildThrowsException([Frozen] Mock<IRestClient> restClient)
		{
			restClient.Setup(
				x =>
					x.GetJsonAsStreamAsync(
						It.IsAny<Uri>(),
						"projects/{0}/{1}",
						new object[] { "TestAccount", "TestProject" },
						new KeyValuePair<object, object>[] { }))
				.Returns(() => Task.Run(() => ResourceHelper.GetResourceStream("appveyor_projects_pending.json")));

			AppVeyorAnalyzerResultAccessor resultAccessor = new AppVeyorAnalyzerResultAccessor(
				restClient.Object,
				new JsonDeserializer(),
				new SarifJsonDeserializer(new JsonDeserializer()),
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } },
				new NullCache()) { AccountName = "TestAccount", ProjectSlug = "TestProject", ReportFileName = "report.json" };

			await Assert.ThrowsAsync<BuildPendingException>(async () => await resultAccessor.GetAnalyzerResultAsync());
		}
	}
}