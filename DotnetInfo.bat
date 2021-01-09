::Check dotnet version
dotnet --version 
::dotnet dev-certs https --trust
::Check dotnet sdk/runtimes list
dotnet --list-sdks
dotnet --list-runtimes

@REM Templates                                         Short Name               Language          Tags
@REM --------------------------------------------      -------------------      ------------      ----------------------
@REM Console Application                               console                  [C#], F#, VB      Common/Console        
@REM Class library                                     classlib                 [C#], F#, VB      Common/Library
@REM WPF Application                                   wpf                      [C#], VB          Common/WPF
@REM WPF Class library                                 wpflib                   [C#], VB          Common/WPF
@REM WPF Custom Control Library                        wpfcustomcontrollib      [C#], VB          Common/WPF
@REM WPF User Control Library                          wpfusercontrollib        [C#], VB          Common/WPF
@REM Windows Forms App                                 winforms                 [C#], VB          Common/WinForms       
@REM Windows Forms Control Library                     winformscontrollib       [C#], VB          Common/WinForms
@REM Windows Forms Class Library                       winformslib              [C#], VB          Common/WinForms
@REM Worker Service                                    worker                   [C#], F#          Common/Worker/Web
@REM Unit Test Project                                 mstest                   [C#], F#, VB      Test/MSTest
@REM NUnit 3 Test Project                              nunit                    [C#], F#, VB      Test/NUnit
@REM NUnit 3 Test Item                                 nunit-test               [C#], F#, VB      Test/NUnit
@REM xUnit Test Project                                xunit                    [C#], F#, VB      Test/xUnit
@REM Razor Component                                   razorcomponent           [C#]              Web/ASP.NET
@REM Razor Page                                        page                     [C#]              Web/ASP.NET
@REM MVC ViewImports                                   viewimports              [C#]              Web/ASP.NET
@REM MVC ViewStart                                     viewstart                [C#]              Web/ASP.NET
@REM Blazor Server App                                 blazorserver             [C#]              Web/Blazor
@REM Blazor WebAssembly App                            blazorwasm               [C#]              Web/Blazor/WebAssembly
@REM ASP.NET Core Empty                                web                      [C#], F#          Web/Empty
@REM ASP.NET Core Web App (Model-View-Controller)      mvc                      [C#], F#          Web/MVC
@REM ASP.NET Core Web App                              webapp                   [C#]              Web/MVC/Razor Pages
@REM ASP.NET Core with Angular                         angular                  [C#]              Web/MVC/SPA
@REM ASP.NET Core with React.js                        react                    [C#]              Web/MVC/SPA
@REM ASP.NET Core with React.js and Redux              reactredux               [C#]              Web/MVC/SPA
@REM Razor Class Library                               razorclasslib            [C#]              Web/Razor/Library
@REM ASP.NET Core Web API                              webapi                   [C#], F#          Web/WebAPI
@REM ASP.NET Core gRPC Service                         grpc                     [C#]              Web/gRPC
@REM dotnet gitignore file                             gitignore                                  Config
@REM global.json file                                  globaljson                                 Config
@REM NuGet Config                                      nugetconfig                                Config
@REM Dotnet local tool manifest file                   tool-manifest                              Config
@REM Web Config                                        webconfig                                  Config
@REM Solution File                                     sln                                        Solution
@REM Protocol Buffer File                              proto                                      Web/gRPC