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

[ci]: https://github.com/craigktreasure/Treasure.Analyzers/actions/workflows/CI.yml "CI"
[codecov]: https://codecov.io/gh/craigktreasure/Treasure.Analyzers "Treasure.Analyzers codecov"
[memberorder-package]: https://www.nuget.org/packages/Treasure.Analyzers.MemberOrder/ "Treasure.Analyzers.MemberOrder"
[rules]: ./docs/Rules.md "Rules"
