# Virtual S2S Gateway

## Available virtual S2S Gateway

Loads all available virtual S2S Gateways: 

`GET-CLOUD4VGW`

## Loading a virtual S2S Gateway

Loads a specific virtual S2S Gateway by its ID: 

`GET-CLOUD4VGW`

The following parameters must be specified: '

`-ID` **`<GUID of vGW>`**

`-VIRTUALDATACENTERID` **`<GUID of vDC> (Search by vDC)`** 

`-NAME` **`(Search for possible Name)`**  


## Creating a virtual S2S Gateway

Creates a new virtual S2S Gateway: 

`NEW-CLOUD4VGW`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual Load S2S Gateway)`** ``

`-VIRTUALDATACENTERID` **`<GUID of vDC>`**

`-VIRTUALSUBNETID` **`<GUID of Gateway Subnet>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Updating a virtual S2S Gateway

Update a existing virtual S2S Gateway: 

`UPDATE-CLOUD4VGW`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual S2S Gateway)`** 

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a virtual S2S Gateway

Deletes the virtual S2S Gateway. 

`REMOVE-CLOUD4VGW`

The following parameters must be specified: 

`-ID` **`<GUID of vGW>`**

## 

