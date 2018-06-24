# Virtual Load Balancer

Additional to the virtual Load Balancer you need to configure addition topics:

* [Backend Pool](https://hiagdata.gitbook.io/cloud4-powershell5/~/edit/drafts/-LFnTWLh2fJJgXLDv_LW/manual/virtual-load-balancer)
* [FrontEnd IP Configuration](https://hiagdata.gitbook.io/cloud4-powershell5/~/edit/drafts/-LFnTWLh2fJJgXLDv_LW/manual/virtual-load-balancer)
* [Probe](https://hiagdata.gitbook.io/cloud4-powershell5/~/edit/drafts/-LFnTWLh2fJJgXLDv_LW/manual/virtual-load-balancer)
* [Rule](https://hiagdata.gitbook.io/cloud4-powershell5/~/edit/drafts/-LFnTWLh2fJJgXLDv_LW/manual/virtual-load-balancer)

## Available virtual Load Balancers

Loads all available virtual Load Balancers: 

`GET-CLOUD4VLB`

## Loading a virtual Load Balancer

Loads a specific virtual Load Balancer by its ID: 

`GET-CLOUD4VLB`

The following parameters must be specified: '

`-ID` **`<GUID of vLB>`**

`-VIRTUALDATACENTERID` **`<GUID of vDC> (Search by vDC)`** 

## Creating a virtual Load Balancer

Creates a new virtual Load Balancer: 

`NEW-CLOUD4VLB`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual Load Balancer)`** ``

`-VIRTUALDATACENTERID` **`<GUID of vDC>`**

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Updating a virtual Load Balancer

Update a existing virtual Load Balancer: 

`UPDATE-CLOUD4VLB`

The following parameters must be specified: 

`-NAME` **`(Name of the virtual Load Balancer)`** 

`-WAIT`

The Wait parameter forces you to wait for the process to be completed \(otherwise this command is created as a job\) and returns the virtual firewall object as a return value.

## Deleting a virtual Load Balancer

Deletes the virtual Load Balancer. 

`REMOVE-CLOUD4VLB`

The following parameters must be specified: 

`-ID` **`<GUID of vLB>`**





