if(Test-Path $PSScriptRoot\Protacon.NetCore.WebApi.Util\artifacts) {
    Remove-Item $PSScriptRoot\Protacon.NetCore.WebApi.Util\artifacts -Force -Recurse
}

dotnet restore
dotnet build

dotnet test $PSScriptRoot\Protacon.NetCore.WebApi.Util.Tests\Protacon.NetCore.WebApi.Util.Tests.csproj

$version = if($env:APPVEYOR_REPO_TAG) {
    "$env:APPVEYOR_REPO_TAG_NAME"
} else {
    "0.0.1-beta$env:APPVEYOR_BUILD_NUMBER"
}

dotnet pack $PSScriptRoot\Protacon.NetCore.WebApi.Util\Protacon.NetCore.WebApi.Util.csproj -c Release -o $PSScriptRoot\Protacon.NetCore.WebApi.Util\artifacts /p:Version=$version