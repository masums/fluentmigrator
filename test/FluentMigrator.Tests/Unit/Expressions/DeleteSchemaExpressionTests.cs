#region License
//
// Copyright (c) 2018, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Tests.Helpers;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Expressions
{
    [TestFixture]
    public class DeleteSchemaExpressionTests
    {
        [Test]
        public void ErrorIsReturnedWhenSchemaNameIsEmptyString()
        {
            var expression = new DeleteSchemaExpression { SchemaName = string.Empty };

            var errors = ValidationHelper.CollectErrors(expression);
            errors.ShouldContain(ErrorMessages.SchemaNameCannotBeNullOrEmpty);
        }

        [Test]
        public void ErrorIsNotReturnedWhenSchemaNameIsSet()
        {
            var expression = new DeleteSchemaExpression { SchemaName = "schema1" };

            var errors = ValidationHelper.CollectErrors(expression);
            Assert.That(errors.Count, Is.EqualTo(0));
        }
    }
}
