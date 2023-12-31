# Treasure.Analyzers.MemberOrder

[![Nuget](https://img.shields.io/nuget/v/Treasure.Analyzers.MemberOrder?label=Treasure.Analyzers.MemberOrder)][memberorder-package]

This package contains analyzers and cleanup rules to keep members in a
particular order.

**Documentation**: See the list of supported [rules][rules].

**Bugs and issues**: please visit the [Treasure.Analyzers project issue tracker][issues].

## How to install

Add a `PackageReference` to the [`Treasure.Analyzers.MemberOrder` NuGet package][memberorder-package].

```xml
<PackageReference Include="Treasure.Analyzers.MemberOrder" Version="<version>">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

If using [Central Package Management][cpm], you can add the package globally by
adding the following to your `Directory.Packages.props`:

```xml
<ItemGroup>
  <GlobalPackageReference Include="Treasure.Analyzers.MemberOrder" Version="<version>" />
</ItemGroup>
```

[cpm]: https://learn.microsoft.com/nuget/consume-packages/central-package-management "Central Package Management"
[issues]: https://github.com/craigktreasure/Treasure.Analyzers/issues "Issues"
[memberorder-package]: https://www.nuget.org/packages/Treasure.Analyzers.MemberOrder/ "Treasure.Analyzers.MemberOrder"
[rules]: https://github.com/craigktreasure/Treasure.Analyzers/blob/main/docs/Rules.md "Rules"
