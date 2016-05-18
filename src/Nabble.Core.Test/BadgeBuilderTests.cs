namespace Nabble.Core.Test
{
	using System.Threading.Tasks;
	using Moq;
	using Nabble.Core.Builder;
	using Ploeh.AutoFixture.Xunit2;
	using Xunit;

	public class BadgeBuilderTests
	{
		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateAndCountTrueWithErrorsAndWarningsAndInfosUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 1, NumberOfInfos = 1 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 3";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						AggregateValues = true,
						CountInfos = true,
						StatusTemplateAggregate = statusTemplate
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateAndCountTrueWithInfosUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						AggregateValues = true,
						CountInfos = true,
						StatusTemplateAggregate = statusTemplate
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueAndCountFalseWithWarningsUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 1, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						AggregateValues = true,
						CountInfos = false,
						StatusTemplateAggregate = statusTemplate
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueWithErrorsAndWarningsAndInfosUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 1, NumberOfInfos = 1 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 2";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { AggregateValues = true, StatusTemplateAggregate = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueWithErrorsAndWarningsUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 1, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 2";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { AggregateValues = true, StatusTemplateAggregate = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueWithErrorsUsesStatusTemplateAggregate(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { AggregateValues = true, StatusTemplateAggregate = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueWithInfosUsesStatusTemplateSuccess(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			string statusTemplate = "My Template";
			string status = "My Template";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { AggregateValues = true, StatusTemplateSuccess = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncAggregateTrueWithNoViolationsUsesStatusTemplateSuccess(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string statusTemplate = "My Template";
			string status = "My Template";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { AggregateValues = true, StatusTemplateSuccess = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncCountFalseWithInfosUsesColorSuccess([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						CountInfos = false,
						ColorInaccessible = BadgeColor.Red,
						ColorError = BadgeColor.Red,
						ColorWarning = BadgeColor.Red,
						ColorInfo = BadgeColor.Red,
						ColorSuccess = BadgeColor.Blue
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncCountFalseWithInfosUsesStatusTemplateSuccess([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			string statusTemplate = "My Template";
			string status = "My Template";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { CountInfos = false, StatusTemplateSuccess = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncCountTrueWithInfosUsesColorInfo([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						CountInfos = true,
						ColorInaccessible = BadgeColor.Red,
						ColorError = BadgeColor.Red,
						ColorWarning = BadgeColor.Red,
						ColorInfo = BadgeColor.Blue,
						ColorSuccess = BadgeColor.Red
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncCountTrueWithInfosUsesStatusTemplateInfos([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 1 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { CountInfos = true, StatusTemplateInfo = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncNoViolationsUsesBadgeStyle([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { Style = BadgeStyle.Social },
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Style == BadgeStyle.Social)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncNoViolationsUsesColorSuccess([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						ColorInaccessible = BadgeColor.Red,
						ColorError = BadgeColor.Red,
						ColorWarning = BadgeColor.Red,
						ColorInfo = BadgeColor.Red,
						ColorSuccess = BadgeColor.Blue
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncNoViolationsUsesFormat([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string format = "My Format";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildBadgeAsync(new BadgeBuilderProperties() { Format = format }, analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Format == format)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncNoViolationsUsesLabel([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string label = "My Label";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildBadgeAsync(new BadgeBuilderProperties() { Label = label }, analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Label == label)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncNoViolationsUsesStatusTemplateSuccess([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string statusTemplate = "My Template";
			string status = "My Template";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { StatusTemplateSuccess = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithErrorsAndWarningsAndInfosUsesColorError([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 1, NumberOfInfos = 1 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						ColorInaccessible = BadgeColor.Red,
						ColorError = BadgeColor.Blue,
						ColorWarning = BadgeColor.Red,
						ColorInfo = BadgeColor.Red,
						ColorSuccess = BadgeColor.Red
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithErrorsAndWarningsUsesStatusTemplateError([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 1, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { StatusTemplateError = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithErrorsUsesStatusTemplateError([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 1, NumberOfWarnings = 0, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { StatusTemplateError = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithWarningsAndInfosUsesColorWarning([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 1, NumberOfInfos = 1 }));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties()
					{
						ColorInaccessible = BadgeColor.Red,
						ColorError = BadgeColor.Red,
						ColorWarning = BadgeColor.Blue,
						ColorInfo = BadgeColor.Red,
						ColorSuccess = BadgeColor.Red
					},
					analyzerResultAccessor.Object);

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithWarningsAndInfosUsesStatusTemplateWarning(
			[Frozen] Mock<IBadgeClient> badgeClient, [Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 1, NumberOfInfos = 1 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { StatusTemplateWarning = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildBadgeAsyncWithWarningsUsesStatusTemplateWarning([Frozen] Mock<IBadgeClient> badgeClient,
			[Frozen] Mock<IAnalyzerResultAccessor> analyzerResultAccessor)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			analyzerResultAccessor.Setup(x => x.GetAnalyzerResultAsync())
				.Returns(Task.Run(() => new AnalyzerResult() { NumberOfErrors = 0, NumberOfWarnings = 1, NumberOfInfos = 0 }));

			string statusTemplate = "My Template {0}";
			string status = "My Template 1";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildBadgeAsync(
					new BadgeBuilderProperties() { StatusTemplateWarning = statusTemplate },
					analyzerResultAccessor.Object);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}

		[Theory, AutoMoqData]
		public async void BuildErrorBadgeAsyncUsesBadgeStyle([Frozen] Mock<IBadgeClient> badgeClient)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildErrorBadgeAsync(new BadgeBuilderProperties() { Style = BadgeStyle.Social }, "TestStatus");

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Style == BadgeStyle.Social)));
		}

		[Theory, AutoMoqData]
		public async void BuildErrorBadgeAsyncUsesColorInaccessible([Frozen] Mock<IBadgeClient> badgeClient)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await
				badgeBuilder.BuildErrorBadgeAsync(
					new BadgeBuilderProperties()
					{
						ColorInaccessible = BadgeColor.Blue,
						ColorError = BadgeColor.Red,
						ColorWarning = BadgeColor.Red,
						ColorInfo = BadgeColor.Red,
						ColorSuccess = BadgeColor.Red
					},
					"TestStatus");

			badgeClient.Verify(
				x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Color == BadgeColor.Blue)));
		}

		[Theory, AutoMoqData]
		public async void BuildErrorBadgeAsyncUsesUsesFormat([Frozen] Mock<IBadgeClient> badgeClient)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			string format = "My Format";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildErrorBadgeAsync(new BadgeBuilderProperties() { Format = format }, "TestStatus");

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Format == format)));
		}

		[Theory, AutoMoqData]
		public async void BuildErrorBadgeAsyncUsesUsesLabel([Frozen] Mock<IBadgeClient> badgeClient)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			string label = "My Label";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildErrorBadgeAsync(new BadgeBuilderProperties() { Label = label }, "TestStatus");

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Label == label)));
		}

		[Theory, AutoMoqData]
		public async void BuildErrorBadgeAsyncUsesUsesStatus([Frozen] Mock<IBadgeClient> badgeClient)
		{
			badgeClient.Setup(x => x.RequestBadgeAsync(It.IsAny<BadgeClientProperties>())).Returns(Task.Run(() => new Badge()));

			string status = "My Status";
			IBadgeBuilder badgeBuilder = new BadgeBuilder(badgeClient.Object);

			await badgeBuilder.BuildErrorBadgeAsync(new BadgeBuilderProperties(), status);

			badgeClient.Verify(x => x.RequestBadgeAsync(It.Is<BadgeClientProperties>(property => property.Status == status)));
		}
	}
}