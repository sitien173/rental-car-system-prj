$currentPath = (Get-Item -Path "./" -Verbose).FullName;
$src = (Get-Item -Path "../src" -Verbose).FullName;
$is4Admin = (Get-Item -Path ($src + "/Skoruba.IdentityServer4.Admin") -Verbose).FullName
$is4AdminApi = (Get-Item -Path ($src + "/Skoruba.IdentityServer4.Admin.Api") -Verbose).FullName
$is4StsIdentity = (Get-Item -Path ($src + "/Skoruba.IdentityServer4.STS.Identity") -Verbose).FullName

$paramsIs4Admin= "/c cd " + $is4Admin + " && dotnet run";
$paramsIs4AdminApi= "/c cd " + $is4AdminApi + " && dotnet run";
$paramsIs4StsIdentity= "/c cd " + $is4StsIdentity + " && dotnet run";

# Start-Process -Verb runas "cmd.exe" $paramsSSO;
Start-Process "cmd.exe" $paramsIs4Admin;
Start-Process "cmd.exe" $paramsIs4AdminApi;
Start-Process "cmd.exe" $paramsIs4StsIdentity;