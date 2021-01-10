dotnet new sln -n IniHelper
dotnet new classlib -o IniHelper
dotnet new console -o Sample

dotnet sln IniHelper.sln add IniHelper\IniHelper.csproj --solution-folder ShareMemoryHelper
dotnet sln IniHelper.sln add Sample\Sample.csproj

cd Sample
dotnet add reference ..\IniHelper\IniHelper.csproj
cd ../