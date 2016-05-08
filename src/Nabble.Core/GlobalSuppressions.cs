// <copyright file="GlobalSuppressions.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design",
	"CA1024:Use properties where appropriate",
	Justification = "Get method is async. Might return new values on consecutive calls.",
	Scope = "member",
	Target = "~M:Nabble.Core.Common.NullStatisticsService.GetTotalBadgeEntriesAsync~System.Threading.Tasks.Task{System.Int32}")]

[assembly: SuppressMessage("Design",
	"CA1024:Use properties where appropriate",
	Justification = "Get method is async. Might return new values on consecutive calls.",
	Scope = "member",
	Target = "~M:Nabble.Core.Common.NullStatisticsService.GetTotalProjectEntriesAsync~System.Threading.Tasks.Task{System.Int32}")]

[assembly: SuppressMessage("Design",
	"CA1024:Use properties where appropriate",
	Justification = "Get method is async. Might return new values on consecutive calls.",
	Scope = "member",
	Target = "~M:Nabble.Core.Common.NullStatisticsService.GetTotalRequestEntriesAsync~System.Threading.Tasks.Task{System.Int32}")]

[assembly: SuppressMessage("Performance",
	"CA1823:Avoid unused private fields",
	Justification = "Field is not unused.",
	Scope = "member",
	Target = "~F:Nabble.Core.Builder.BadgeClient.badgeStyleMapping")]

[assembly: SuppressMessage("Usage",
	"CA2234:Pass system uri objects instead of strings",
	Justification = "Parameter is not a valid URI.",
	Scope = "member",
	Target = "~M:Nabble.Core.Common.RestClient.GetHttpResponseAsync(System.Uri,System.String,System.Object[],System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Object,System.Object}})~System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}")]

[assembly: SuppressMessage("Usage",
	"CA2234:Pass system uri objects instead of strings",
	Justification = "Parameter is not a valid URI.",
	Scope = "member",
	Target = "~M:Nabble.Core.Common.RestClient.GetJsonAsStreamAsync(System.Uri,System.String,System.Object[],System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.Object,System.Object}})~System.Threading.Tasks.Task{System.IO.Stream}")]