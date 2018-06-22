# Connecting to the Cloud 4.0

The connection to Cloud 4.0 is made via the command: 

`OPEN-CLOUD4CONNECTION`

The following parameters must be specified: 

`-CREDENTIAL (Logon data in the form of a PSCredential object)`

`-LOGINURL`  [`https://login.swiss.cloud`](https://login.swiss.cloud)

`-APIURL` [`https://api.swiss.cloud`](https://api.swiss.cloud)

`-TENANTID (TenantID as GUID)`

The TenantID represents the current ID of the tenant in Cloud 4.0. Further tenants can be created at a later date. To obtain the current list of available tenants, execute the "Open-Cloud4Connection" command without the "TenantId" parameter. This returns the current list of tenants. If no TenantId is specified and only one Tenant is available, it is set automatically.

The following PowerShell code can be used to create a PSCredential object: 

`$username = "e-mail address"   
$password = 'Password' | ConvertTo-SecureString -asPlainText -Force  
$cred = New-Object System.Management.Automation.PSCredential($username,$password)`

## Managing the tenant 

The corresponding tenant can be set with the following command: 

`SET-CLOUD4TENANT -ID` **`<GUID of Tenant>`**

With the following command a list of the available tenants can be read out. Only works if a tenant has already been set. 

`GET-CLOUD4TENANT`

An existing tenant can be deleted with the following command. This only works if all resources from this tenant have been deleted beforehand. 

`REMOVE-CLOUD4TENANT -ID` **`<GUID of Tenant>`**

