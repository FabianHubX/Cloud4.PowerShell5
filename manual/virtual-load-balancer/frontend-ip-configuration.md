# FrontEnd IP Configuration

##  Available Load Balancer Frontend IP Config {#available-load-balancer-backend-pool}

Loads all available Load Balancers Frontend IP Config:

`GET-CLOUD4VLBFRONTENDIPCONFIGURATIONS`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Loading a Load Balancer Frontend IP Config {#loading-a-load-balancer-backend-pool}

Loads a specific Load Balancer Frontend IP Config by its ID:

`GET-CLOUD4VLBFRONTENDIPCONFIGURATIONS`

The following parameters must be specified: '

`-ID` **`<GUID of vLB FrontEnd IP Config>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Creating a Load Balancer Frontend IP Config {#creating-a-load-balancer-backend-pool}

Creates a new Load Balancer Frontend IP Config:

`NEW-CLOUD4VLBFRONTENDIPCONFIGURATIONS`

The following parameters must be specified:

`-INTERNALIPADDRESS`**`(Internal IP Address)`**

`-VIRTUALSUBNETID` **`<GUID of vSubNet>`**

`-ASSIGNPUBLICIP`

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Updating a Load Balancer Frontend IP Config {#updating-a-load-balancer-backend-pool}

Update a existing Load Balancer Frontend IP Config:

`UPDATE-CLOUD4VLBFRONTENDIPCONFIGURATIONS`

The following parameters must be specified:

`-ID` **`<GUID of vLB FrontEnd IP Config>`**

`-INTERNALIPADDRESS`**`(Internal IP Address)`**

`-VIRTUALSUBNETID` **`<GUID of vSubNet>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a Load Balancer Frontend IP Config {#deleting-a-load-balancer-backend-pool}

Deletes the Load Balancer Frontend IP Config.

`REMOVE-CLOUD4VLBFRONTENDIPCONFIGURATIONS`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-ID` **`<GUID of vLB FrontEnd IP Config>`**  


