﻿# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
.SYNOPSIS
Create a linked service and then do a Get to compare the result are identical.
Delete the created linked service after test finishes.
#>
function Test-LinkedService
{
    $dfname = Get-DataFactoryName
    $rgname = Get-ResourceGroupName
    $rglocation = Get-ProviderLocation ResourceManagement
    $dflocation = Get-ProviderLocation DataFactoryManagement
        
    New-AzureResourceGroup -Name $rgname -Location $rglocation -Force

    try
    {
        New-AzureDataFactory -ResourceGroupName $rgname -Name $dfname -Location $dflocation -Force
     
        $lsname = "foo"
   
        $actual = New-AzureDataFactoryLinkedService -ResourceGroupName $rgname -DataFactoryName $dfname -Name $lsname -File .\Resources\linkedService.json -Force
        $expected = Get-AzureDataFactoryLinkedService -ResourceGroupName $rgname -DataFactoryName $dfname -Name $lsname

        Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName
        Assert-AreEqual $expected.DataFactoryName $actual.DataFactoryName
        Assert-AreEqual $expected.LinkedServiceName $actual.LinkedServiceName

        Remove-AzureDataFactoryLinkedService -ResourceGroupName $rgname -DataFactoryName $dfname -Name $lsname -Force
    }
    finally
    {
        Clean-DataFactory $rgname $dfname
    }
}

<#
.SYNOPSIS
Nagative test. Get resources with an empty linked service name.
#>
function Test-GetLinkedServiceWithEmptyName
{	
    $lsname = ""
	$dfname = Get-DataFactoryName
    $rgname = Get-ResourceGroupName
    $rglocation = Get-ProviderLocation ResourceManagement
    $dflocation = Get-ProviderLocation DataFactoryManagement
    
    New-AzureResourceGroup -Name $rgname -Location $rglocation -Force
    New-AzureDataFactory -ResourceGroupName $rgname -Name $dfname -Location $dflocation -Force

    # Test
    Assert-ThrowsContains { Get-AzureDataFactoryLinkedService -ResourceGroupName $rgname -DataFactoryName $dfname -Name $lsname } "null"    
}

<#
.SYNOPSIS
Nagative test. Get resources with a linked service name which only contains white space.
#>
function Test-GetLinkedServiceWithWhiteSpaceName
{	
    $lsname = "   "
	$dfname = Get-DataFactoryName
    $rgname = Get-ResourceGroupName
    $rglocation = Get-ProviderLocation ResourceManagement
    $dflocation = Get-ProviderLocation DataFactoryManagement
    
    New-AzureResourceGroup -Name $rgname -Location $rglocation -Force
    New-AzureDataFactory -ResourceGroupName $rgname -Name $dfname -Location $dflocation -Force

    # Test
    Assert-ThrowsContains { Get-AzureDataFactoryLinkedService -ResourceGroupName $rgname -DataFactoryName $dfname -Name $lsname } "null"    
}