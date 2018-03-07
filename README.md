# AllClean

remove _bin_ and _obj_ directory from project directory.

## Install

```sh
dotnet pack src/AllClean.csproj -c Release
mv src/bin/Release/AllClean.1.0.0.nupkg YOUR_LOCAL_NUGET_PACKAGE_DIRECTORY
dotnet install tool -g AllClean
```

## How To Use

```sh
dotnet allclean YOUR_PROJECT_DIRECTORY
```

if you want to check directory path that will be removed, add _-i_ option.

```sh
dotnet allclean YOUR_PROJECT_DIRECTORY -i
```