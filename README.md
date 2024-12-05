# Advent Of Code 2024

Code for 2024's Advent of Code challenge.

https://adventofcode.com/2024

# Project template

In the directory, `AocTemplate` you'll find a `dotnet` template structure so that new days can be added easily. The base usage for this is:

`> dotnet new aoc --name Day01`

This will create a directory structure that looks like the following:

```
Day01\
 --src\
   --Day01.csproj
   --input.txt
   --Program.cs
   --Solver.cs
 --tst\
   --Day01Tests.csproj
   --Day01Tests.cs
```

## Running tests

```
> cd Day01
> dotnet build && dotnet test tst/
```

## Running that Day's solution

```
> cd Day01
> dotnet build src/ && dotnet run src/
```

## Un/Installing the Template

To install a template, once you've setup the template directory/structure as per the documentation here
(https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates), then you can simply run the command

`> dotnet new install AocTemplate/`

To make changes to a template, you have to uninstall it and reinstall it. The uninstall command can be
found by running

`> dotnet new uninstall`.
