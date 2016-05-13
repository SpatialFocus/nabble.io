// <copyright file="OnBuildBadgeErrorEventHandlerArgs.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System;

	/// <summary>
	/// </summary>
	public class OnBuildBadgeErrorEventHandlerArgs : EventArgs
	{
		/// <summary>
		/// </summary>
		public Exception RaisedException { get; set; }
	}
}