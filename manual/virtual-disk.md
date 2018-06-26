# Virtual Disk

## Available virtual Disk

Loads all available virtual Disks: 

`GET-CLOUD4VDISK`

## Loading a virtual Disk

Loads a specific virtual Disks by its ID: 

`GET-CLOUD4VDISK`

The following parameters can be specified: 

`-ID` **`<GUID of vSubNet>`**

`-VIRTUALMACHINEID` **`<GUID of VM> (Search all by VM)`**

## Attaching a new virtual Data Disk

Attaching a new virtual Data Disk: 

`ADD-CLOUD4VDATADISK`

The following parameters must be specified: 

`-VIRTUALMACHINEID` **`<GUID of VM>`**

`-NAME` **`(Name of the virtual Data Disk)`** ``

`-DiskProfile` **`(Profile for the Data Disk)`**

`-WAIT` 

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual Data Disk object as a return value.

