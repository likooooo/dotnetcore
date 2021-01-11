::https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/language-specification/unsafe-code
dotnet new classlib -o HeapHelper
dotnet new console -o Sample
cd Sample
dotnet add reference ..\HeapHelper\HeapHelper.csproj
cd ../
::<AllowUnsafeBlocks>true</AllowUnsafeBlocks>
