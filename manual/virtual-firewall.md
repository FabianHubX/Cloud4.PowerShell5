# Virtual Firewall

## Available virtual firewalls 

Loads all available virtual firewalls: 

`GET-CLOUD4FIREWALL`

## Loading a virtual firewall 

Loads a specific virtual firewall by its ID: 

`GET-CLOUD4FIREWALL`

The following parameters must be specified: '

`-ID` **`<GUID of vFirewall>`**

`-VIRTUALSUBNETID` **`<GUID of vSubNet>`**

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

The list of Firewall is passed as an array with following object structure :

`{`

`SOURCEADDRESSPREFIX= "*";` **`(IP or Range: 192.168.0.2/32; 192.168.0.2/24 or * for any)`**

`SOURCEPORTRANGE= "*";` **`(Port is between 1 and 65535)`**

`DESTINATIONADDRESSPREFIX= "*"` **`(IP or Range: 192.168.0.2/32; 192.168.0.2/24 or * for any)`**

`DESTINATIONPORTRANGE= "*"`**`(Port is between 1 and 65535)`**

`PROTOCOL = "ALL"` **`(TCP, UDP or ALL)`**

`DIRECTION = "Inbound"` **`(Inbound or Outbound)`**

`ACTION = "Allow"` **`(Allow or Deny)`**

`PRIORITY = 200` **`(Between 101 and 65000)`**

`}`

Example: 

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

