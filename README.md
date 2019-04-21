# TypedTree-Generator-Unity

Unity package tool for generating treescheme files for use in the [**TypedTree-editor**](https://github.com/bastianblokland/typedtree-editor)

## Description
To avoid having to handwrite treescheme files you can generated them based on the c# class
structure of your tree (for example a behaviour tree structure).

The scheme files produced be be used to edit tree's in a visual way using the [**website**](https://bastian.tech/tree).

## Installation
1. Add a reference to this repository to your package dependencies (`Packages/manifest.json`)

    ```
    "dependencies": {
        "com.typedtree.generator": "https://github.com/BastianBlokland/typedtree-generator-unity.git",
        ...
    }
    ```
2. Add the NuGet dependency to your project.

    If your project uses a NuGet package manager you can simple add a dependency to [**TypedTree.Generator.Core**](https://www.nuget.org/packages/TypedTree.Generator.Core/).

    If you are not using a NuGet package manager you can simple copy the dll from the [`.lib`](https://github.com/BastianBlokland/typedtree-generator-unity/tree/master/.lib) directory to your project.

## Usage
1. Create a `Generator` scriptable-object through the project menu-item. (Right mouse the project window -> `Create/TypedTree/Generator`)
2. In the `Root Alias Type` field of the generator configure what type (`interface`, `class`, or `struct`) to use as the base of your tree.
3. In the `Output Path` field of the generator configure where to output the scheme json.

## Example
An example of how to integrate this package with unity project can be found in the [`.example`](https://github.com/BastianBlokland/typedtree-generator-unity/tree/master/.example) directory.

## Help
More information about the generator library: [**typedtree-generator-dotnet**](https://github.com/BastianBlokland/typedtree-generator-dotnet)
More information about the editor: [**typedtree-editor**](https://github.com/BastianBlokland/typedtree-editor)
