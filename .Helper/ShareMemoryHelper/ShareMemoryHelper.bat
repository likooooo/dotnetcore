::https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-sln
dotnet new sln -n ShareMemoryHelper
dotnet new classlib -o ShareMemoryHelper
dotnet new console -o Sample

dotnet sln ShareMemoryHelper.sln add ShareMemoryHelper\ShareMemoryHelper.csproj --solution-folder ShareMemoryHelper
dotnet sln ShareMemoryHelper.sln add Sample\Sample.csproj

::�������
::1. ������ļ�
dotnet pack ShareMemoryHelper
::2.�������
cd Sample
dotnet add reference ..\ShareMemoryHelper\ShareMemoryHelper.csproj