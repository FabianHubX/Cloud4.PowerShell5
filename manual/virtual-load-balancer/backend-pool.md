# Backend Pool

## Available Load Balancer Backend Pool

Loads all available Load Balancers Backend Pool: 

`GET-CLOUD4VLBBACKENDPOOL`

The following parameters must be specified: 

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Loading a Load Balancer Backend Pool

Loads a specific Load Balancer Backend Pool by its ID: 

`GET-CLOUD4VLBBACKENDPOOL`

The following parameters must be specified: '

`-ID` **`<GUID of vLB BackEnd Pool>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Creating a Load Balancer Backend Pool

Creates a new Load Balancer BackEnd Pool: 

`NEW-CLOUD4VLBBACKENDPOOL`

The following parameters must be specified: 

`-VIRTUALMACHINEIDS`**`<List of VM GUID's>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Updating a Load Balancer Backend Pool

Update a existing Load Balancer Backend Pool: 

`UPDATE-CLOUD4VLBBACKENDPOOL`

The following parameters must be specified: 

`-ID` **`<GUID of vLB BackEnd Pool>`**

`-VIRTUALMACHINEIDS`**`<List of VM GUID's>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a Load Balancer Backend Pool

Deletes the Load Balancer Backend Pool. 

`REMOVE-CLOUD4VLBBACKENDPOOL`

The following parameters must be specified: 

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-ID` **`<GUID of vLB BackEnd Pool>`**

