

$vdc = Get-Cloud4vDC -FilterByName "Demo vDC"

$lb = New-Cloud4vLB -Name "Demo_LB" -VirtualDataCenterId $vdc.id -Wait $true
$lb = Get-Cloud4vLB -Id $lb.Id
$bePool = New-Cloud4vLBBackEndPool -VirtualMachineIds $vm.Id -VirtualLoadBalancerId $lb.id -Wait $true

$lb = Get-Cloud4vLB -Id $lb.Id
$feConfig = New-Cloud4vLBFrontEndIPConfigurations -VirtualLoadBalancerId $lb.Id -AssignPublicIP $true -VirtualSubnetId $vsubnetid -Wait $true

$lb = Get-Cloud4vLB -Id $lb.Id
$probe = New-Cloud4vLBProbe -IntervalInSeconds 5 -Protocol HTTP -Port 8080 -VirtualLoadBalancerId $lb.id -RequestPath '/iisstart.htm' -NumberOfProbes 15 -wait $true

$lb = Get-Cloud4vLB -Id $lb.Id
$lbrule = New-Cloud4vLBRule -BackendAddressPool $lb.BackendAddressPools.Guid -Protocol TCP -FrontEndPort 8080 -BackEndPort 8080 -FrontendIPConfigurations $lb.FrontEndIPConfigurations.guid -VirtualLoadBalancerId $lb.id.guid -FloatingIpEnabled $False -LoadDistribution Default -ProbeId $lb.Probes.Guid -IdleTimeoutInMinutes 30
