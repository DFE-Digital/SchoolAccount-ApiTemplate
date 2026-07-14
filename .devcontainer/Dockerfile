FROM mcr.microsoft.com/devcontainers/dotnet:1-10.0

# The image ships a Yarn apt repo whose signing key is no longer valid, which
# breaks every apt-get update (including devcontainer feature installs).
# Yarn isn't used in this project, so drop the repo.
RUN rm -f /etc/apt/sources.list.d/yarn.list
