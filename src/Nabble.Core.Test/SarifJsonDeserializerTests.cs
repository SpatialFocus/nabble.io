namespace Nabble.Core.Test
{
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using Nabble.Core.Common;
	using Nabble.Core.Sarif;
	using Xunit;

	public class SarifJsonDeserializerTests
	{
		[Fact]
		public void DeserializeFromStreamValidReportParsesCorrectly()
		{
			ISarifJsonDeserializer analyzerResultJsonDeserializer = new SarifJsonDeserializer(new JsonDeserializer());
			Stream resourceStream = ResourceHelper.GetResourceStream("report.json");

			SarifResult analyzerResult = analyzerResultJsonDeserializer.DeserializeFromStream(resourceStream);

			Debug.Assert(analyzerResult.RunLogs.Count() == 1);
			Debug.Assert(analyzerResult.RunLogs.Single().Results.Count() == 2);
			Debug.Assert(analyzerResult.RunLogs.Single().Results.ElementAt(0).Properties.Severity == Severity.Warning);
			Debug.Assert(analyzerResult.RunLogs.Single().Results.ElementAt(0).RuleId == "CS1591");
			Debug.Assert(analyzerResult.RunLogs.Single().Results.ElementAt(1).Properties.Severity == Severity.Error);
			Debug.Assert(analyzerResult.RunLogs.Single().Results.ElementAt(1).RuleId == "CS1591");
		}
	}
}