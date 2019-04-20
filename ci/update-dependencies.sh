#!/bin/bash
set -e
source ./ci/utils.sh

# --------------------------------------------------------------------------------------------------
# Utility to download the latest NuGet dependencies.
# --------------------------------------------------------------------------------------------------

NUGET_PACKAGE="TypedTree.Generator.Core"
NUGET_DIR=".nuget"
LIBRARY_DIR=".lib"

# Verify that commands we depend on are present.
verifyCommand nuget
verifyCommand cp
verifyCommand mkdir
verifyCommand rm

info "Fetching nuget packages"
withRetry nuget install "$NUGET_PACKAGE" -OutputDirectory "$NUGET_DIR"

saveDll ()
{
    info "Processing dll: $1"
    ensureDir "$LIBRARY_DIR"
    cp -f "$1" "$LIBRARY_DIR"
}

processPackage ()
{
    info "Processing package: $1"
    local netstandard2Dir="$1/lib/netstandard2.0/"
    if [ ! -d "$netstandard2Dir" ]
    then
        fail "No 'netstandard2.0' library found for package: $1"
    fi

    for dllPath in "$netstandard2Dir"*.dll
    do
        saveDll "$dllPath"
    done
}

# Clear output.
rm -rf "$LIBRARY_DIR"

# Extract all netstandard2.0 dll's from the packages.
for packageDir in "$NUGET_DIR"/*
do
    processPackage $packageDir
done

info "Finished fetching dependencies"
exit 0
