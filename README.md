# FsLab.NBFormat
A pure F# library for parsing the `.ipynb` notebook file format v4.

It uses [System.Text.Json]() in conjunction with the absolutely amazing [FSharp.SystemTextJson](https://github.com/Tarmil/FSharp.SystemTextJson) library.

There currently is a missing link in the .NET ecosystem between working in .NET notebooks and exporting them to other formats. 
If you want to use .NET polyglot notebooks for creating documentation or websites, you have to use `nbconvert`, which is a python tool and therefore requires the setup of a python environment.

This library aims to close that gap by
- providing a type model for the `.ipynb` format (and the additional magic sauce added by .NET interactive)
- providing an extensible interface for converting polyglot notebooks into other formats

## Develop

### build

Check the [build project](https://github.com/kMutagene/FSharp.Data.NBFormat/blob/main/build) to take a look at the  build targets. Here are some examples:

```shell
# Windows

# Build only
./build.cmd

# build and run tests
./build.cmd runTests

# Linux/mac

# Build only
build.sh

# build and run tests
build.sh runTests

```

## Usage

The library is not on nuget currently, so you currently have to clone the repo and build it yourself.

Then you can use it to deserialize a notebook file like this:

```fsharp
open FSharp.Data.NBFormat
open System.IO

"your/path/to/a/notebook.ipynb"
|> File.ReadAllText
|> Serialiation.deserializeNotebook
```
