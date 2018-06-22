# Virtual Data Centers

## Available data centers 

Loads all available data centers: 

`GET-CLOUD4VDC`

## Loading a specific data center 

Loads a specific data center by its ID: 

`GET-CLOUD4VDC`

The following parameters can be specified: 

`-ID` **`<GUID of vDC>`**

`-NAME`  **`'<Name of the vDC's>'`**

## Creating a data center 

Creates a new virtual data center: 

`NEW-CLOUD4VDC`

The following parameters must be specified:

`-NAME` **`Name of the data center`**

`-REGIONID` **`Guid of the region`** ``

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual datacenter object as a return value. 

## Updating a data center 

Creates a new virtual data center: 

`UPDATE CLOUD4VDC`

The following parameters must be specified:

`-ID  
-NAME New name of data center   
-WAIT (SWITCH)`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual datacenter object as a return value.

## Deleting a data center 

Deletes the virtual data center. 

Attention: A deletion only takes place if all resources in the vDC have been deleted beforehand, such as SubNet, Network and VMs.

`REMOVE-CLOUD4VDC`

The following parameters must be specified: 

`-ID`



