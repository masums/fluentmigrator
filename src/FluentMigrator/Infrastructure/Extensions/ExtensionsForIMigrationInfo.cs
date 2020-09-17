#region License
// Copyright (c) 2007-2018, Sean Chambers <schambers80@gmail.com>
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

namespace FluentMigrator.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IMigrationInfo"/>
    /// </summary>
    public static class ExtensionsForIMigrationInfo
    {
        /// <summary>
        /// Returns <c>true</c> when the migration behind the <paramref name="migrationInfo"/> has a migration attribute
        /// </summary>
        /// <param name="migrationInfo">The migration information to test</param>
        /// <returns><c>true</c> when the migration behind the <paramref name="migrationInfo"/> has a migration attribute</returns>
        public static bool IsAttributed(this IMigrationInfo migrationInfo)
        {
            return !(migrationInfo is NonAttributedMigrationToMigrationInfoAdapter);
        }
    }
}
