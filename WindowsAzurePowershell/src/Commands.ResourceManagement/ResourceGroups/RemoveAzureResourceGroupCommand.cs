﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ResourceManagement.Properties;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.ResourceManagement
{
    /// <summary>
    /// Removes a new resource group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureResourceGroup"), OutputType(typeof(bool))]
    public class RemoveAzureResourceGroupCommand : ResourceBaseCmdlet
    {
        [Alias("ResourceGroupName")]
        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The name of the resource group.")]
        [ValidateNotNullOrEmpty]
        public string Name {get; set;}

        [Parameter(Mandatory = false, HelpMessage = "Do not ask for confirmation.")]
        public SwitchParameter Force { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }
        
        public override void ExecuteCmdlet()
        {
            ConfirmAction(
                Force.IsPresent,
                string.Format(Resources.RemovingResourceGroup, Name),
                Resources.RemoveResourceGroupMessage,
                Name,
                () => ResourceClient.DeleteResourceGroup(Name));

            if (PassThru)
            {
                WriteObject(true);
            }
        }
    }
}