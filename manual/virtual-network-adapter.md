# Virtual Network Adapter

## Available virtual Network Adapter

Loads all available virtual Network Adapters: 

`GET-CLOUD4VNETADAPTER`

## Loading a virtual Network Adapter

Loads a specific virtual Network Adapters by its ID: 

`GET-CLOUD4VNETADAPTER`

The following parameters can be specified: 

`-ID` **`<GUID of vSubNet>`**

`-VIRTUALMACHINEID` **`<GUID of VM> (Search all by VM)`**

`-VIRTUALMACHINENAME` **`(Search for possible Name)`**

## Attaching a new virtual Network Adapter

Attaching a new virtual Network Adapter: 

`ADD-CLOUD4VNETADAPTER`

The following parameters must be specified: 

`-VIRTUALMACHINEID` **`<GUID of VM>`**

`-NAME` **`(Name of the virtual Net Adapter)`** ``

`-NICProfile` **`(Profile for the Nic)`**

`-IPADDRESS` **`(New IP Address)`**

`-DNSSERVERS` **`(Array of DNS Servers)`**

`-VIRTUALSUBNETID` **`<GUID for vSubNet>`**

`-WAIT` 

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

## Updating a virtual Network Adapter

Updates a existing virtual Network Adapter: 

`UPDATE-CLOUD4VNETADAPTER`

The following parameters must be specified: 

`-ID` **`<GUID of vNetAdpater>`**

`-VIRTUALSUBNETID` **`<GUID for vSubNet>`**

`-NICProfile` **`(Profile for the Nic)`**

`-IPADDRESS` **`(New IP Address)`**

`-DNSSERVERS` **`(Array of DNS Servers)`**

`-VIRTUALFIREWALLID` **`<GUID for vFirewall>`**

`-NAME` **`(New name of the virtual Net Adapter)`**

`-WAIT` 

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual SubNet object as a return value.

## Deleting a virtual Network Adapter

Deletes the virtual Network Adapter. 

Attention: A deletion only takes place if all resources in vSubNet have been deleted beforehand, like VMs. 

`REMOVE-CLOUD4VNETADAPTER`

The following parameters must be specified:

`-ID` **`<GUID of vNetAdapter>`**



