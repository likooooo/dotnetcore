dotnet new sln -n JsonHelper
dotnet new classlib -o JsonHelper
dotnet new console -o Sample

dotnet sln JsonHelper.sln add JsonHelper\JsonHelper.csproj --solution-folder ShareMemoryHelper
dotnet sln JsonHelper.sln add Sample\Sample.csproj

cd Sample
dotnet add reference ..\JsonHelper\JsonHelper.csproj
cd ../