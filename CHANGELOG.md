# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.0.0] - Unreleased

Initial release.

### Added

- Core engine: expression-based (`Expression<Func<T, bool>>`) and descriptor-based (`FilterDescriptor`/`FilterGroup`)
  filtering, translated per-dialect into parameterized SQL.
- Type-safe CRUD, sorting, paging, and single/range upsert.
- Provider packages for SQL Server, MySQL, PostgreSQL and Oracle, each following that engine's real type and
  syntax constraints (e.g. Oracle's `UNION ALL`/`DUAL`-based multi-row insert with automatic per-column cast
  discovery, since Oracle has no native `VALUES (...), (...)` syntax).

[1.0.0]: https://github.com/davidedalcortivo/DapperForge/releases/tag/v1.0.0
