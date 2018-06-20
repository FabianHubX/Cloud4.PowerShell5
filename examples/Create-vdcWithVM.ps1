

$vdc = New-Cloud4vDC -Name "Demo vDC" -RegionId 1e10c132-9e10-40f9-862e-037d01d21d79 -Wait

$subnets= @([pscustomobject]@{Name="Demo SubNet1";AddressPrefix="192.168.1.0/24"},
[pscustomobject]@{Name="Demo SubNet2";AddressPrefix="192.168.2.0/24"},
[pscustomobject]@{Name="Demo SubNet3";AddressPrefix="192.168.3.0/24"})

$vnet = New-Cloud4vNet -Name "Demo vNet" -AddressSpace "192.168.0.0/16" -VirtualDataCenterId $vdc.Id -SubNet $subnets -DNSServers 8.8.8.8,8.8.4.4 -Wait

$vdc = Get-Cloud4vDC -FilterByName "Demo vDC"
$vnet = Get-Cloud4vNet -VirtualDatacenterId $vdc.Id
$vsubnet = Get-Cloud4vSubNet -VirtualNetworkId $vnet.Id
$vsubnetid = $vsubnet[0].Id

$ossettings = [pscustomobject]@{adminUserPassword="<password>";adminUserName="<username>";timeZone="UTC";productKey="";registeredOrganization="itnetX AG";registeredOwner="IT";joinDomain=$false;domainJoinDomain="";domainJoinPassword="";domainJoinUsername="";domain="";domainOu="";sshKey=""}

$vm = New-Cloud4VM -Name "DEMOVM1" -VirtualDatacenterId $vdc.Id -VirtualSubNetId $vsubnetid -ImageId 1fe08740-c31b-4773-8166-5e92552990e2 -VMProfile F8v1 -OSDiskProfile S60 -NICProfile 100Mbps -OSSettings $ossettings -EnableRemoteAccess $true -EnableInternetAccess $true -EnableInOutboundVNetTraffic $true -Wait


