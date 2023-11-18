namespace Treasure.Analyzers.MemberOrder.Tests.TestUtils;

internal static class TestData
{
    public static IEnumerable<object[]> ClassAccessModifiersInOrder =>
    [
        ["public", "internal"],
        ["internal", "protected internal"],
        ["protected internal", "private protected"],
        ["private protected", "protected"],
        ["protected", "private"],
    ];

    public static IEnumerable<object[]> ClassAccessModifiersNotInOrder =>
    [
        ["internal", "public"],
        ["protected internal", "internal"],
        ["private protected", "protected internal"],
        ["protected", "private protected"],
        ["private", "protected"],
    ];

    public static IEnumerable<object[]> InterfaceAccessModifiersInOrder =>
    [
        ["public", "internal"],
        ["internal", "protected internal"],
        ["protected internal", "private protected"],
    ];

    public static IEnumerable<object[]> InterfaceAccessModifiersNotInOrder =>
    [
        ["internal", "public"],
        ["protected internal", "internal"],
        ["private protected", "protected internal"],
    ];

    public static IEnumerable<object[]> StructAccessModifiersInOrder =>
    [
        ["public", "internal"],
        ["internal", "private"],
    ];

    public static IEnumerable<object[]> StructAccessModifiersNotInOrder =>
    [
        ["internal", "public"],
        ["private", "internal"],
    ];
}
