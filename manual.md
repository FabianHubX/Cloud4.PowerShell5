# Manual using this Powershell Module

## Introduction

The following documentation is used a manual on how to use this Powershell to access over the public API on the Cloud 4.0 platform https://portal.swiss.cloud at HIAG Data AG.

## Module Installation

To install the PowerShell Module you need first to download the newst release on following address:

[https://github.com/HiagData/Cloud4.PowerShell5/releases](https://github.com/HiagData/Cloud4.PowerShell5/releases)  


After downloading the ZIP file and extracting on a folder of your choice you just need to import the module if you want use it:

`IMPORT-MODULE "\CLOUD4.POWERSHELL5.MODULE.PSD1"`

## Connecting to the Cloud 4.0 

The connection to Cloud 4.0 is made via the command: 

`OPEN-CLOUD4CONNECTION`

The following parameters must be specified: 

`-CREDENTIAL (Logon data in the form of a PSCredential object)`

`-LOGINURL`  [`https://login.swiss.cloud`](https://login.swiss.cloud) ``

`-APIURL` [`https://api.swiss.cloud`](https://api.swiss.cloud)

`-TENANTID (TenantID as GUID)`

The TenantID represents the current ID of the tenant in Cloud 4.0. Further tenants can be created at a later date. To obtain the current list of available tenants, execute the "Open-Cloud4Connection" command without the "TenantId" parameter. This returns the current list of tenants. If no TenantId is specified and only one Tenant is available, it is set automatically.

The following PowerShell code can be used to create a PSCredential object: 

`$username = "e-mail address"   
$password = 'Password' | ConvertTo-SecureString -asPlainText -Force  
$cred = New-Object System.Management.Automation.PSCredential($username,$password)`

### Managing the tenant 

The corresponding tenant can be set with the following command: 

`SET-CLOUD4TENANT -ID` 

With the following command a list of the available tenants can be read out. Only works if a tenant has already been set. 

`GET-CLOUD4TENANT`

An existing tenant can be deleted with the following command. This only works if all resources from this tenant have been deleted beforehand. 

`REMOVE-CLOUD4TENANT -ID`

## Basic data

### Images

The following command returns the currently available images:

`GET-CLOUD4IMAGE`

### Regions 

The following command returns the currently available regions: 

`GET-CLOUD4REGION`

### Virtual Disk Profiles 

The following command returns the currently available disk types: 

`GET-CLOUD4PROFILE -TYPE VIRTUALDISK`

### Virtual Network Adapter Profiles 

The following command returns the currently available network adapter profiles: 

`GET-CLOUD4PROFILE -TYPE VIRTUAL NETWORK ADAPTER`

### Virtual Machine \(VM\) Profiles 

The following command returns the currently available VM types: 

`GET-CLOUD4PROFILE -TYPE VIRTUALMACHINE` 

## Jobs 

### Reading the jobs 

Loads all jobs: 

`GET-CLOUD4VJOBS`

### Loading a job 

Loads a specific job using its ID: 

`GET-CLOUD4VJOBS`

The following parameters can be specified: 

`-ID` 

## companies, contacts and users

### Reading the companies

Loads all current companies:

`GET-CLOUD4COMPANY`

### Creating a Company

`NEW-CLOUD4COMPANY`

### Reading all contacts 

Loads all current contacts: 

`GET-CLOUD4CONTACT`

### Read all user accounts 

Loads all current user accounts: 

`GET-CLOUD4USER`

### Sending a new invitation to a user

`START-CLOUD4USER`

## Virtual Data Center 

### Available data centers 

Loads all available data centers: 

`GET-CLOUD4VDC`

### Loading a specific data center 

Loads a specific data center by its ID: 

`GET-CLOUD4VDC`

The following parameters can be specified: 

`-ID  
-NAME  ('<Name of the vDC's>')`

### Creating a data center 

Creates a new virtual data center: 

`NEW-CLOUD4VDC`

The following parameters must be specified:

`-NAME Name of the data center   
-REGIONID Guid of the region   
-WAIT (SWITCH)`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual datacenter object as a return value. 

### Updating a data center 

Creates a new virtual data center: 

`UPDATE CLOUD4VDC`

The following parameters must be specified:

`-ID  
-NAME New name of data center   
-WAIT (SWITCH)`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual datacenter object as a return value.

### Deleting a data center 

Deletes the virtual data center. 

Attention: A deletion only takes place if all resources in the vDC have been deleted beforehand, such as SubNet, Network and VMs.

`REMOVE-CLOUD4VDC`

The following parameters must be specified: 

`-ID`



