namespace Nabble.Core.Test
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Moq;
	using Nabble.Core.AppVeyor;
	using Nabble.Core.Builder;
	using Nabble.Core.Common;
	using Nabble.Core.Sarif;
	using Ploeh.AutoFixture.Xunit2;
	using Xunit;

	public class AppVeyorAnalyzerResultAccessorTests
	{
		[Theory, AutoMoqData]
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
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } })
			{
				AccountName = "TestAccount",
				ProjectSlug = "TestProject",
				BuildBranch = "TestBranch"
			};

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 1);
			Assert.True(analyzerResult.NumberOfErrors == 1);
		}

		[Theory, AutoMoqData]
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
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } })
			{
				AccountName = "TestAccount",
				ProjectSlug = "TestProject"
			};

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 1);
			Assert.True(analyzerResult.NumberOfErrors == 1);
		}

		[Theory, AutoMoqData]
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
				new AnalyzerResultBuilder() { Rules = new[] { string.Empty } })
			{
				AccountName = "TestAccount",
				ProjectSlug = "TestProject"
			};

			AnalyzerResult analyzerResult = await resultAccessor.GetAnalyzerResultAsync();

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 2);
			Assert.True(analyzerResult.NumberOfErrors == 2);
		}
	}
}