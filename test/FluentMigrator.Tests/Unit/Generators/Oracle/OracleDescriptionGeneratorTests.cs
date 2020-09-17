﻿using System.Linq;
using FluentMigrator.Runner.Generators.Oracle;
using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.Oracle
{
    [TestFixture]
    public class OracleDescriptionGeneratorTests : BaseDescriptionGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            DescriptionGenerator = new OracleDescriptionGenerator();
        }

        [Test]
        public override void GenerateDescriptionStatementsForCreateTableReturnTableDescriptionStatement()
        {
            var createTableExpression = GeneratorTestHelper.GetCreateTableWithTableDescription();
            var statements = DescriptionGenerator.GenerateDescriptionStatements(createTableExpression);

            var result = statements.First();
            result.ShouldBe("COMMENT ON TABLE TestTable1 IS 'TestDescription'");
        }

        [Test]
        public override void GenerateDescriptionStatementsForCreateTableReturnTableDescriptionAndColumnDescriptionsStatements()
        {
            var createTableExpression = GeneratorTestHelper.GetCreateTableWithTableDescriptionAndColumnDescriptions();
            var statements = DescriptionGenerator.GenerateDescriptionStatements(createTableExpression).ToArray();

            var result = string.Join(";", statements);
            result.ShouldBe(
                "COMMENT ON TABLE TestTable1 IS 'TestDescription';COMMENT ON COLUMN TestTable1.TestColumn1 IS 'TestColumn1Description';COMMENT ON COLUMN TestTable1.TestColumn2 IS 'TestColumn2Description'");
        }

        [Test]
        public override void GenerateDescriptionStatementForAlterTableReturnTableDescriptionStatement()
        {
            var alterTableExpression = GeneratorTestHelper.GetAlterTableWithDescriptionExpression();
            var statement = DescriptionGenerator.GenerateDescriptionStatement(alterTableExpression);

            statement.ShouldBe("COMMENT ON TABLE TestTable1 IS 'TestDescription'");
        }

        [Test]
        public override void GenerateDescriptionStatementForCreateColumnReturnColumnDescriptionStatement()
        {
            var createColumnExpression = GeneratorTestHelper.GetCreateColumnExpressionWithDescription();
            var statement = DescriptionGenerator.GenerateDescriptionStatement(createColumnExpression);

            statement.ShouldBe("COMMENT ON COLUMN TestTable1.TestColumn1 IS 'TestColumn1Description'");
        }

        [Test]
        public override void GenerateDescriptionStatementForAlterColumnReturnColumnDescriptionStatement()
        {
            var alterColumnExpression = GeneratorTestHelper.GetAlterColumnExpressionWithDescription();
            var statement = DescriptionGenerator.GenerateDescriptionStatement(alterColumnExpression);

            statement.ShouldBe("COMMENT ON COLUMN TestTable1.TestColumn1 IS 'TestColumn1Description'");
        }

        [Test]
        public void GenerateDescriptionStatementsWithSingleQuoteForCreateTableReturnTableDescriptionStatement()
        {
            var createTableExpression = GeneratorTestHelper.GetCreateTableWithTableDescription();
            createTableExpression.TableDescription = "Test Description with single quote (') character here >> '";
            var statements = DescriptionGenerator.GenerateDescriptionStatements(createTableExpression);

            var result = statements.First();
            result.ShouldBe("COMMENT ON TABLE TestTable1 IS 'Test Description with single quote ('') character here >> '''");
        }
    }
}
