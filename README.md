# nabble.io (.NET Analyzers Badge Builder LE)

You are using analyzers based on the .NET Compiler Platform (like [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) or [FxCop](https://github.com/dotnet/roslyn-analyzers) to produce high quality code?
With the .NET Analyzers Badge Builder lightweight edition (nabble) you can show that you apply modern coding standards and best practices.
Display the Analyzer result of your last build as dynamically generated image as seen below.

For a detailed documentation see [nabble.io](https://nabble.io).

How to enable nabble:

0. Use .NET Analyzers
0. Enable .NET Analyzer logging in the proj file (see [AnalyzerLogging](https://github.com/SpatialFocus/AnalyzerLogging) for more information)

        // Sample (add reporting for debug configurations)

        <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
            <ErrorLog>$(MSBuildProjectDirectory)\report.json</ErrorLog>
        </PropertyGroup>

0. Publish the .NET Analyzer log file in your build process

        // Sample (add to appveyor.yml build process)

        artifacts:
            - path: '**\report.json'

Currently only AppVeyor is supported, but we plan to add more platforms in the future.

Feel free to provide feedback.

# Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/pk9k5ar6ykovopg2/branch/master?svg=true)](https://ci.appveyor.com/project/Dresel/nabble-io/branch/master)
[![codecov](https://codecov.io/gh/SpatialFocus/nabble.io/branch/master/graph/badge.svg)](https://codecov.io/gh/SpatialFocus/nabble.io)
[![Build status](https://nabble.io/api/v1/AppVeyor/dresel/nabble-io/master/StyleCop)](https://nabble.io) [![Build status](https://nabble.io/api/v1/AppVeyor/dresel/nabble-io/master/FxCop)](https://nabble.io)

