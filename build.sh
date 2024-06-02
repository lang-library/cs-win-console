#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
version="${ts}"

cd $cwd
find . -name bin -exec rm -rf {} +
find . -name obj -exec rm -rf {} +

cd $cwd
#dotnet test -p:Configuration=Release -p:Platform="Any CPU" WinConsole.sln

cd $cwd/WinConsole
sed -i -e "s/<Version>.*<\/Version>/<Version>${version}<\/Version>/g" WinConsole.csproj
rm -rf *.nupkg
dotnet pack -o . -p:Configuration=Release -p:Platform="Any CPU" WinConsole.csproj

#exit 0

tag="WinConsole-v$version"
cd $cwd
git add .
git commit -m"$tag"
git tag -a "$tag" -m"$tag"
git push origin "$tag"
git push origin HEAD:main
git remote -v
