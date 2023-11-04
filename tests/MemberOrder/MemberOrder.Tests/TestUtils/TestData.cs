namespace Treasure.Analyzers.MemberOrder.Tests.TestUtils;

internal static class TestData
{
    public static IEnumerable<object[]> ClassAccessModifiersInOrder => new[]
    {
        new object[] { "public", "internal" },
        new object[] { "internal", "protected internal" },
        new object[] { "protected internal", "private protected" },
        new object[] { "private protected", "protected" },
        new object[] { "protected", "private" },
    };

    public static IEnumerable<object[]> ClassAccessModifiersNotInOrder => new[]
    {
        new object[] { "internal", "public" },
        new object[] { "protected internal", "internal" },
        new object[] { "private protected", "protected internal" },
        new object[] { "protected", "private protected" },
        new object[] { "private", "protected" },
    };

    public static IEnumerable<object[]> InterfaceAccessModifiersInOrder => new[]
    {
        new object[] { "public", "internal" },
        new object[] { "internal", "protected internal" },
        new object[] { "protected internal", "private protected" },
    };

    public static IEnumerable<object[]> InterfaceAccessModifiersNotInOrder => new[]
    {
        new object[] { "internal", "public" },
        new object[] { "protected internal", "internal" },
        new object[] { "private protected", "protected internal" },
    };

    public static IEnumerable<object[]> StructAccessModifiersInOrder => new[]
    {
        new object[] { "public", "internal" },
        new object[] { "internal", "private" },
    };

    public static IEnumerable<object[]> StructAccessModifiersNotInOrder => new[]
    {
        new object[] { "internal", "public" },
        new object[] { "private", "internal" },
    };
}
