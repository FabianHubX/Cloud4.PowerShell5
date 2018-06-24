# Rule

##  Available Load Balancer Rule {#available-load-balancer-backend-pool}

Loads all available Load Balancers Backend Pool:

`GET-CLOUD4VLBRULE`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Loading a Load Balancer Rule {#loading-a-load-balancer-backend-pool}

Loads a specific Load Balancer Rule by its ID:

`GET-CLOUD4VLBRULE`

The following parameters must be specified: '

`-ID` **`<GUID of vLB Rule>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Creating a Load Balancer Rule {#creating-a-load-balancer-backend-pool}

Creates a new Load Balancer Rule:

`NEW-CLOUD4VLBRULE`

The following parameters must be specified:

`-BACKENDADDRESSPOOL` **`<GUID of vLB Backend Pool>`**

`-PROTOCOL` **`(UDP, TCP, GRE, ESP or ALL)`**

`-FRONTENDPORT` **`(Range between 1 and 65535)`**

`-BACKENDPORT` **`(Range between 1 and 65535)`**

`-FRONTENDIPCONFIGURATIONS` **`(List of Frontend IP Config GUID's)`**

`-FLOATINGIPENABLED` **`(floating IP to backend Server)`**

`-LOADDISTRIBUTION` **`(Default, SourceIP or SourceIPProtocol)`**

`-PROBEID` **`<GUID of vLB Probe>`**

`-IDLETIMEOUTINMINUTES` **`(Range between 4 and 30)`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.



## Updating a Load Balancer Rule {#updating-a-load-balancer-backend-pool}

Update a existing Load Balancer Rule:

`UPDATE-CLOUD4VLBRULE`

The following parameters must be specified:

`-ID` **`<GUID of vLB Rule>`**

`-BACKENDADDRESSPOOL` **`<GUID of vLB Backend Pool>`**

`-PROTOCOL` **`(UDP, TCP, GRE, ESP or ALL)`**

`-FRONTENDPORT` **`(Range between 1 and 65535)`**

`-BACKENDPORT` **`(Range between 1 and 65535)`**

`-FRONTENDIPCONFIGURATIONS` **`(List of Frontend IP Config GUID's)`**

`-FLOATINGIPENABLED` **`(floating IP to backend Server)`**

`-LOADDISTRIBUTION` **`(Default, SourceIP or SourceIPProtocol)`**

`-PROBEID` **`<GUID of vLB Probe>`**

`-IDLETIMEOUTINMINUTES` **`(Range between 4 and 30)`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a Load Balancer Rule {#deleting-a-load-balancer-backend-pool}

Deletes the Load Balancer Rule.

`REMOVE-CLOUD4VLBRULE`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-ID` **`<GUID of vLB Rule>`**  


