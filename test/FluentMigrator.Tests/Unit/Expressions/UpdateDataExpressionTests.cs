using System.Collections.Generic;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Tests.Helpers;
using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Expressions
{
    [TestFixture]
    public class UpdateDataExpressionTests {
        private UpdateDataExpression _expression;

        [SetUp]
        public void Initialize()
        {
            _expression =
                new UpdateDataExpression()
                {
                    TableName = "ExampleTable",
                    Set = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>("Column", "value")
                    },
                    IsAllRows = false
                };
        }

        [Test]
        public void NullUpdateTargetCausesErrorMessage()
        {
            // null is the default value, but it might not always be, so I'm codifying it here anyway
            _expression.Where = null;

            var errors = ValidationHelper.CollectErrors(_expression);
            errors.ShouldContain(ErrorMessages.UpdateDataExpressionMustSpecifyWhereClauseOrAllRows);
        }

        [Test]
        public void EmptyUpdateTargetCausesErrorMessage()
        {
            // The same should be true for an empty list
            _expression.Where = new List<KeyValuePair<string, object>>();

            var errors = ValidationHelper.CollectErrors(_expression);
            errors.ShouldContain(ErrorMessages.UpdateDataExpressionMustSpecifyWhereClauseOrAllRows);
        }

        [Test]
        public void DoesNotRequireWhereConditionWhenIsAllRowsIsSet()
        {
            _expression.IsAllRows = true;

            var errors = ValidationHelper.CollectErrors(_expression);
            errors.ShouldNotContain(ErrorMessages.UpdateDataExpressionMustSpecifyWhereClauseOrAllRows);
        }

        [Test]
        public void DoesNotAllowWhereConditionWhenIsAllRowsIsSet()
        {
            _expression.IsAllRows = true;
            _expression.Where = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("key", "value")};

            var errors = ValidationHelper.CollectErrors(_expression);
            errors.ShouldContain(ErrorMessages.UpdateDataExpressionMustNotSpecifyBothWhereClauseAndAllRows);
        }

        [Test]
        public void WhenDefaultSchemaConventionIsAppliedAndSchemaIsNotSetThenSchemaShouldBeNull()
        {
            var processed = _expression.Apply(ConventionSets.NoSchemaName);

            Assert.That(processed.SchemaName, Is.Null);
        }

        [Test]
        public void WhenDefaultSchemaConventionIsAppliedAndSchemaIsSetThenSchemaShouldNotBeChanged()
        {
            _expression.SchemaName = "testschema";

            var processed = _expression.Apply(ConventionSets.WithSchemaName);

            Assert.That(processed.SchemaName, Is.EqualTo("testschema"));
        }

        [Test]
        public void WhenDefaultSchemaConventionIsChangedAndSchemaIsNotSetThenSetSchema()
        {
            var processed = _expression.Apply(ConventionSets.WithSchemaName);

            Assert.That(processed.SchemaName, Is.EqualTo("testdefault"));
        }
    }
}
