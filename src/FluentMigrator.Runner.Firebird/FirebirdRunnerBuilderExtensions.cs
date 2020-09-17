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

using FluentMigrator.Runner.Generators.Firebird;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.Firebird;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentMigrator.Runner
{
    /// <summary>
    /// Extension methods for <see cref="IMigrationRunnerBuilder"/>
    /// </summary>
    public static class FirebirdRunnerBuilderExtensions
    {
        /// <summary>
        /// Adds Firebird support
        /// </summary>
        /// <param name="builder">The builder to add the Firebird-specific services to</param>
        /// <returns>The migration runner builder</returns>
        public static IMigrationRunnerBuilder AddFirebird(this IMigrationRunnerBuilder builder)
        {
            return builder.AddFirebird(FirebirdOptions.AutoCommitBehaviour());
        }

        /// <summary>
        /// Adds Firebird support
        /// </summary>
        /// <param name="builder">The builder to add the Firebird-specific services to</param>
        /// <param name="fbOptions">Firebird options</param>
        /// <returns>The migration runner builder</returns>
        public static IMigrationRunnerBuilder AddFirebird(this IMigrationRunnerBuilder builder, FirebirdOptions fbOptions)
        {
            builder.Services
                .AddScoped(
                    sp =>
                    {
                        var processorOptions = sp.GetRequiredService<IOptionsSnapshot<ProcessorOptions>>();
                        return ((FirebirdOptions)fbOptions.Clone()).ApplyProviderSwitches(processorOptions.Value.ProviderSwitches);
                    })
                .AddScoped<FirebirdDbFactory>()
                .AddScoped<FirebirdProcessor>()
                .AddScoped<IMigrationProcessor>(sp => sp.GetRequiredService<FirebirdProcessor>())
                .AddScoped<FirebirdQuoter>()
                .AddScoped<FirebirdGenerator>()
                .AddScoped<IMigrationGenerator>(sp => sp.GetRequiredService<FirebirdGenerator>());

            return builder;
        }
    }
}
