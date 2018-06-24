# Probe

##  Available Load Balancer Probe {#available-load-balancer-backend-pool}

Loads all available Load Balancers Probe:

`GET-CLOUD4VLBPROBE`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Loading a Load Balancer Probe {#loading-a-load-balancer-backend-pool}

Loads a specific Load Balancer Probe by its ID:

`GET-CLOUD4VLBPROBE`

The following parameters must be specified: '

`-ID` **`<GUID of vLB Probe>`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

## Creating a Load Balancer Probe {#creating-a-load-balancer-backend-pool}

Creates a new Load Balancer Probe:

`NEW-CLOUD4VLBPROBE`

The following parameters must be specified:

`-INTERVALINSECONDS` **`(Range between 5 and 2147483646)`**

`-PROTOCOL` **`(HTTP or TCP)`**

`-PORT` **`(Range between 1 and 65535)`**

`-REQUESTPATH` **`(URL if HTTP is used)`**

`-NUMBEROFPROBES` **`(Range between 11 and 65535)`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Updating a Load Balancer Probe {#updating-a-load-balancer-backend-pool}

Update a existing Load Balancer Probe:

`UPDATE-CLOUD4VLBPROBE`

The following parameters must be specified:

`-ID` **`<GUID of vLB Probe>`**

`-INTERVALINSECONDS` **`(Range between 5 and 2147483646)`**

`-PROTOCOL` **`(HTTP or TCP)`**

`-PORT` **`(Range between 1 and 65535)`**

`-REQUESTPATH` **`(URL if HTTP is used)`**

`-NUMBEROFPROBES` **`(Range between 11 and 65535)`**

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a Load Balancer Probe {#deleting-a-load-balancer-backend-pool}

Deletes the Load Balancer Probe.

`REMOVE-CLOUD4VLBPROBE`

The following parameters must be specified:

`-VIRTUALLOADBALANCERID` **`<GUID of vLB>`**

`-ID` **`<GUID of vLB Probe>`**  


