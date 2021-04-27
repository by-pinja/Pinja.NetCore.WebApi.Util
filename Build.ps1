if(Test-Path $PSScriptRoot\Pinja.NetCore.WebApi.Util\artifacts) {
    Remove-Item $PSScriptRoot\Pinja.NetCore.WebApi.Util\artifacts -Force -Recurse
}

dotnet build

$version = if($env:APPVEYOR_REPO_TAG) {
    "$env:APPVEYOR_REPO_TAG_NAME"
} else {
    "0.0.1-beta$env:APPVEYOR_BUILD_NUMBER"
}

dotnet pack $PSScriptRoot\Pinja.NetCore.WebApi.Util\Pinja.NetCore.WebApi.Util.csproj -c Release -o $PSScriptRoot\Pinja.NetCore.WebApi.Util\artifacts /p:Version=$version