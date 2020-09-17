#region License
// Copyright (c) 2018, FluentMigrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using JetBrains.Annotations;

namespace FluentMigrator.Runner.Initialization
{
    /// <summary>
    /// Interface to access the connection string
    /// </summary>
    public interface IConnectionStringReader
    {
        /// <summary>
        /// Gets the priority
        /// </summary>
        /// <remarks>
        /// Higher value means that it gets processed first
        /// </remarks>
        int Priority { get; }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        /// <param name="connectionStringOrName">The name of the connection string</param>
        [CanBeNull]
        string GetConnectionString([CanBeNull] string connectionStringOrName);
    }
}
