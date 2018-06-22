# Virtual SubNets

## Available virtual SubNets 

Loads all available virtual SubNets: 

`GET-CLOUD4VSUBNET`

## Loading a virtual SubNet 

Loads a specific virtual SubNet by its ID: 

`GET-CLOUD4VSUBNET`

The following parameters can be specified: 

`-ID  
-NAME`

## Creating a virtual SubNet 

Creates a new virtual SubNets: 

`NEW-CLOUD4VSUBNET`

The following parameters must be specified: 

`-NAME Name of the virtual subnet   
-ADDRESSSPACE Address Space e.g. (10.0.0.0/16)   
-VIRTUALNETWORKID Virtual Network Guid   
-WAIT` 

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

## Updating a virtual SubNet 

Creates a new virtual SubNets: 

`UPDATE CLOUD4VSUBNET`

The following parameters must be specified: 

`-ID  
-NAME New name of the virtual subnet   
-WAIT` 

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

## Deleting a virtual SubNet 

Deletes the virtual subnet. 

Attention: A deletion only takes place if all resources in vSubNet have been deleted beforehand, like VMs. 

`REMOVE-CLOUD4VSUBNET`

The following parameters must be specified:

`-ID '`


