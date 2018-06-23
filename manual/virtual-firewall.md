# Virtual Firewall

## Available virtual firewalls 

Loads all available virtual firewalls: 

`GET-CLOUD4FIREWALL`

## Loading a virtual firewall 

Loads a specific virtual firewall by its ID: 

`GET-CLOUD4FIREWALL`

The following parameters must be specified: '

`-ID` **`<GUID of vFirewall>`**

`-VIRTUALDATACENTERID` **`<GUID of vDC> (Search by vDC)`** 

## Creating a virtual firewall 

Creates a new virtual firewall: 

`NEW-CLOUD4FIREWALL`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual firewall)`** ``

`-VIRTUALDATACENTERID` **`<GUID of vDC>`**

`-RULES` **`(list of virtual firewall rules)`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

The list of firewall rules is passed as an array: 

`$FWRULES= @([PSCUSTOMOBJECT]@{SOURCEADDRESSPREFIX="*";SOURCEPORTRANGE="*";DESTINATIONADDRESSPREFIX="*";  
DESTINATIONPORTRANGE="*";PROTOCOL="TCP";DIRECTION="INBOUND";ACTION="ALLOW";PRIORITY="1000"})`

## Updating a virtual firewall 

Update a existing virtual firewall: 

`UPDATE-CLOUD4FIREWALL`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual firewall)`** 

`-RULES` **`(list of virtual firewall rules)`**

`-WAIT`

So that the firewall rules can be adapted, they must first be loaded into a variable \(with Get-Cloud4vFirewall\). Then you can update the array in the new configuration and send it to the firewall.

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a virtual firewall 

Deletes the virtual firewall. 

`REMOVE-CLOUD4FIREWALL`

The following parameters must be specified: 

`-ID` **`<GUID of vFirewall>`**

