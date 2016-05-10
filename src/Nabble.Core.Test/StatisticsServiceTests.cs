namespace Nabble.Core.Test
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.Data.Entity;
	using Moq;
	using Nabble.Core.Common;
	using Nabble.Core.Data;
	using Nabble.Core.Data.Entities;
	using Ploeh.AutoFixture.Xunit2;
	using Xunit;

	public class StatisticsServiceTests
	{
		[Theory]
		[AutoMoqData]
		public async void AddBadgeEntryIfNotExistsAsyncAddsEntryIfNotExists([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddBadgeEntryIfNotExistsAsync("3");

			int totalEntries = await statisticsService.GetTotalBadgeEntriesAsync();

			Assert.True(totalEntries == 3);
		}

		[Theory]
		[AutoMoqData]
		public async void AddBadgeEntryIfNotExistsAsyncDoesNotAddEntryIfExists([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddBadgeEntryIfNotExistsAsync("1");

			int totalEntries = await statisticsService.GetTotalBadgeEntriesAsync();

			Assert.True(totalEntries == 2);
		}

		[Theory]
		[AutoMoqData]
		public async void AddProjectEntryIfNotExistsAsyncAddsEntryIfNotExists([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddProjectEntryIfNotExistsAsync("TestAccount2", "TestProject");

			int totalEntries = await statisticsService.GetTotalProjectEntriesAsync();

			Assert.True(totalEntries == 2);
		}

		[Theory]
		[AutoMoqData]
		public async void AddProjectEntryIfNotExistsAsyncAddsEntryIfNotExists2([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddProjectEntryIfNotExistsAsync("TestAccount", "TestProject2");

			int totalEntries = await statisticsService.GetTotalProjectEntriesAsync();

			Assert.True(totalEntries == 2);
		}

		[Theory]
		[AutoMoqData]
		public async void AddProjectEntryIfNotExistsAsyncAddsEntryIfNotExists3([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddProjectEntryIfNotExistsAsync("TestAccount2", "TestProject2");

			int totalEntries = await statisticsService.GetTotalProjectEntriesAsync();

			Assert.True(totalEntries == 2);
		}

		[Theory]
		[AutoMoqData]
		public async void AddProjectEntryIfNotExistsAsyncDoesNotAddEntryIfExists([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddProjectEntryIfNotExistsAsync("TestAccount", "TestProject");

			int totalEntries = await statisticsService.GetTotalProjectEntriesAsync();

			Assert.True(totalEntries == 1);
		}

		[Theory]
		[AutoMoqData]
		public async void AddRequestEntryAsyncAddsEntry([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			await statisticsService.AddRequestEntryAsync();

			int totalEntries = await statisticsService.GetTotalRequestEntriesAsync();

			Assert.True(totalEntries == 4);
		}

		[Theory]
		[AutoMoqData]
		public async void GetTotalBadgeEntriesAsyncReturnsCorrectResult([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			int totalEntries = await statisticsService.GetTotalBadgeEntriesAsync();

			Assert.True(totalEntries == 2);
		}


		[Theory]
		[AutoMoqData]
		public async void GetTotalProjectEntriesAsyncReturnsCorrectResult([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			int totalEntries = await statisticsService.GetTotalProjectEntriesAsync();

			Assert.True(totalEntries == 1);
		}

		[Theory]
		[AutoMoqData]
		public async void GetTotalRequestEntriesAsyncReturnsCorrectResult([Frozen] Mock<IQueryable<Project>> projects,
			[Frozen] Mock<IUnitOfWork> unitOfWork)
		{
			NabbleUnitOfWork nabbleUnitOfWork = await CreateNabbleUnitOfWork();

			StatisticsService statisticsService = new StatisticsService(nabbleUnitOfWork);

			int totalEntries = await statisticsService.GetTotalRequestEntriesAsync();

			Assert.True(totalEntries == 3);
		}

		private static async Task<NabbleUnitOfWork> CreateNabbleUnitOfWork()
		{
			DbContextOptionsBuilder<NabbleContext> optionsBuilder = new DbContextOptionsBuilder<NabbleContext>();
			optionsBuilder.UseInMemoryDatabase();

			NabbleUnitOfWork nabbleUnitOfWork = new NabbleUnitOfWork
			{
				CreationDelegate = () =>
				{
					NabbleContext nabbleContext = new NabbleContext(optionsBuilder.Options);
					nabbleContext.Database.EnsureDeleted();

					return nabbleContext;
				}
			};

			nabbleUnitOfWork.Add(new Project() { AccountName = "testaccount", ProjectName = "testproject" });
			nabbleUnitOfWork.Add(new Badge() { BadgeIdentifier = "1" });
			nabbleUnitOfWork.Add(new Badge() { BadgeIdentifier = "2" });
			nabbleUnitOfWork.Add(new Request() { });
			nabbleUnitOfWork.Add(new Request() { });
			nabbleUnitOfWork.Add(new Request() { });

			await nabbleUnitOfWork.SaveAsync();

			return nabbleUnitOfWork;
		}
	}
}