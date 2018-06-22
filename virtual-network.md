# Virtual Network

## Available virtual networks

Loads all available virtual networks:

`GET-CLOUD4VNET`

## Loading a specific virtual network 

Loads a specific data center using its Id: 

`GET-CLOUD4VNET`

The following parameters can be specified: 

`-ID  
-NAME`

## Creating a virtual network 

Creates a new virtual data center: 

`NEW-CLOUD4VNET`

The following parameters must be specified:

`-NAME Name of the virtual network   
-ADDRESSSPACE Address Space e.g. (10.0.0.0/16)  
-VIRTUALDATACENTERID virtual data center GUID   
-SUBNET List of SubNet   
-DNSSERVERS List of DNS server IP's separated by coma   
-WAIT (switch)`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual network object as a return value.

The list of SubNet is passed as an array:   
`$SUBNETS= @([PSCUSTOMOBJECT]@{NAME="SUBNET1";ADDRESS PREFIX="192.168.1.0/24"}, [PSCUSTOMOBJECT]@{NAME="SUBNET2";ADDRESS PREFIX="192.168.2.0/24"}, [PSCUSTOMOBJECT]@{NAME="SUBNET3";ADDRESS PREFIX="192.168.3.0/24"})`

## Updating a virtual network 

Creates a new virtual data center: 

`UPDATE-CLOUD4VNET`

The following parameters must be specified:

`-ID '<GUID of the virtual network's>'   
-NAME New name of the virtual network   
-DNSSERVERS List of DNS server IP's separated by coma   
-WAIT (switch)`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual network object as a return value.

## Deleting a virtual network 

Deletes the virtual network. 

Attention: Deletion only takes place if all resources in the vNet have been deleted beforehand, such as SubNet and VMs. 

`REMOVE-CLOUD4VNET`

The following parameters must be specified: 

`-ID '<GUID of the virtual network's>'`



