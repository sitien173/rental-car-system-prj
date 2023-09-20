$currentPath = (Get-Item -Path "./" -Verbose).FullName;

$src = (Get-Item -Path "../src" -Verbose).FullName;

# identity provider
$is4Admin = (Get-Item -Path ($src + "/identity-server/src/Skoruba.IdentityServer4.Admin") -Verbose).FullName
$is4AdminApi = (Get-Item -Path ($src + "/identity-server/src/Skoruba.IdentityServer4.Admin.Api") -Verbose).FullName
$is4StsIdentity = (Get-Item -Path ($src + "/identity-server/src/Skoruba.IdentityServer4.STS.Identity") -Verbose).FullName

# api resource
$apiResouce = (Get-Item -Path ($src + "/ptit-rental-car-api-resource/src/WebApp") -Verbose).FullName

# UI
$customerUI = (Get-Item -Path ($src + "/ptit-rental-car-ui") -Verbose).FullName
$adminUI = (Get-Item -Path ($src + "/ptit-rental-car-ui") -Verbose).FullName

$paramsIs4Admin = "/c cd " + $is4Admin + " && dotnet run";
$paramsIs4AdminApi = "/c cd " + $is4AdminApi + " && dotnet run";
$paramsIs4StsIdentity = "/c cd " + $is4StsIdentity + " && dotnet run";

$paramsApiResouce = "/c cd " + $apiResouce + " && dotnet run";
$paramsCustomerUI = "/c cd " + $customerUI + " && npx nx serve customer-portal";
$paramsAdminUI = "/c cd " + $adminUI + " && npx nx serve admin-portal";


# Start-Process -Verb runas "cmd.exe" $paramsSSO;
Start-Process "cmd.exe" $paramsIs4Admin;
Start-Process "cmd.exe" $paramsIs4AdminApi;
Start-Process "cmd.exe" $paramsIs4StsIdentity;

Start-Process "cmd.exe" $paramsApiResouce;
Start-Process "cmd.exe" $paramsCustomerUI;
Start-Process "cmd.exe" $paramsAdminUI;