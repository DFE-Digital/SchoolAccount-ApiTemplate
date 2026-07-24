#!/usr/bin/env bash
# One-time developer setup: restores dotnet tools and enables the git hooks.
# Usage: ./init.sh
set -euo pipefail
cd "$(dirname "$0")"

dotnet tool restore
git config core.hooksPath .githooks
chmod +x .githooks/*

echo "Done: dotnet tools restored and git hooks enabled from .githooks/"
