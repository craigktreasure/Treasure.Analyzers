# Treasure.Analyzers

[![CI Build](https://github.com/craigktreasure/Treasure.Analyzers/actions/workflows/CI.yml/badge.svg?branch=main)][ci]
[![codecov](https://codecov.io/gh/craigktreasure/Treasure.Analyzers/branch/main/graph/badge.svg?token=28F4PZLPN8)][codecov]

This repository contains my common analyzers. See the supported [rules][rules].

## Treasure.Analyzers.MemberOrder

[![Nuget](https://img.shields.io/nuget/v/Treasure.Analyzers.MemberOrder?label=Treasure.Analyzers.MemberOrder)][memberorder-package]
[![NuGet](https://img.shields.io/nuget/dt/Treasure.Analyzers.MemberOrder)][memberorder-package]

An analyzer that ensures that class members are ordered by type, keyword,
accessibility level, and then by name. It also includes a code fix to reorder
members appropriately.

### CodeMaid comparison

This analyzer was heavily inspired by the fantastic [CodeMaid]. There are some
important differences.

Improvements over CodeMaid:

- Uses modern Roslyn analyzer tooling.
- As a Roslyn analyzer, the rules can be integrated into the build to enforce
  member ordering at build time.
- The code fixups associated with the analyzer work anywhere Roslyn code fixups
  are supported: Visual Studio, Visual Studio Code, etc.. No extension required.
- In addition to `interface`, `class`, and `struct`, the analyzer supports the
  newer `record` and `record struct` declaration types as well.
- Running code fixups will not remove newer C# keywords like `required`, which
  happens today when reordering using CodeMaid.

Limitations compared to CodeMaid:

- The analyzer is not yet configurable.
- The analyzer does not support code regions: `#region MyRegion`.
- Likely others.

## Contributing

For information about the release process, see the [Release Process documentation](./docs/Release-Process.md).

[ci]: https://github.com/craigktreasure/Treasure.Analyzers/actions/workflows/CI.yml "CI"
[codecov]: https://codecov.io/gh/craigktreasure/Treasure.Analyzers "Treasure.Analyzers codecov"
[codemaid]: https://www.codemaid.net/ "CodeMaid"
[memberorder-package]: https://www.nuget.org/packages/Treasure.Analyzers.MemberOrder/ "Treasure.Analyzers.MemberOrder"
[rules]: ./docs/Rules.md "Rules"
