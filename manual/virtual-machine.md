# Virtual Machine

## Available virtual machines

Loads all available virtual machines:

`GET-CLOUD4VM`

## Loading a virtual machine 

Loads a specific virtual machine by its ID: 

`GET-CLOUD4VM`

The following parameters must be specified: 

`-ID` **`<GUID of VM>`**

`-Name` **`(Search by VM Name)`**

`-VIRTUALDATACENTERID` **`<GUID of vDC>  (Search by VDC)`**

## Creating a virtual machine 

Creates a new virtual machine: 

`NEW-CLOUD4VM`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual machine)`**

`-VIRTUALDATACENTERID` **`<GUID of vDC>`**

`-VMPROFILE VM Profile` **`(Physical Template)`** ``

`-OSDISKPROFILE` **`(OS Disk Profile`** ``

`-NICPROFILE` **`(Network Card Profiles`** ``

`-DATADISKPROFILE` **`(List of data disks -optional-)`** 

`-OSSETTINGS` **`(OS Settings Parameter)`** ``

`-AVAILABILITYSETID` **`<ID of an existing availability set> -optional-`** 

`-NEWAVAILABILITYSETNAME` **`(Name of a newly created Availability Sets) -optional`**

`-ENABLEREMOTEACCESS` **`(Enable Remote Access)`** ``

`-ENABLEINTERNETACCESS` **`(Enable Internet Access)`** ``

`-ENABLEOUTBOUNDVNETTRAFFIC` **`(Enable Outbound vNet Traffic)`** ``

`-VIRTUALSUBNETID` **`<GUID of the vSubNet>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

The list of data disk profiles is passed as an array:

The list of OS settings is passed as an array:   
`$OSSETTINGS = [PSCUSTOMOBJECT]@{ADMINUSERPASSWORD="P@SSW0RD";ADMINUSERNAME="HIAGDATA";  
TIMEZONE="UTC";REGISTEREDORGANIZATION="HIAG DATA AG";REGISTEREDOWNER="IT";JOINDOMAIN=$FALSE}`

## Upgrading a virtual machine 

Creates a new virtual machine: 

`UPDATE-CLOUD4VM`

The following parameters must be specified: 

`-ID` **`<GUID of VM>`**

`-VMPROFILE` **`(VM Profile (Physical Template) -Optional-`**

`-ENABLEREMOTEACCESS` **`$true or $false (optional)`** 

`-ENABLEINTERNETACCESS` **`$true or $false (optional)`** ``

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

## Deleting a virtual machine 

Deletes the virtual machine. 

`REMOVE-CLOUD4VM`

The following parameters must be specified: 

`-ID` **`<GUID of VM>`**

## Starting a virtual machine 

Deletes the virtual machine. 

`START-CLOUD4VM`

The following parameters must be specified: 

`-ID` **`<GUID of VM>`**

## Stopping a virtual machine 

Deletes the virtual machine. 

`STOP-CLOUD4VM`

The following parameters must be specified: 

`-ID` **`<GUID of VM>`**

