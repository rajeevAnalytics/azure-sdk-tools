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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.HostedServices
{
    using System.Linq;
    using System.Management.Automation;
    using AutoMapper;
    using Commands.Utilities.Common;
    using Management.Compute;
    using Management.Compute.Models;
    using Model;
    using WindowsAzure.ServiceManagement;

    /// <summary>
    /// Retrieve a specified hosted account.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureService"), OutputType(typeof(HostedServiceDetailedContext))]
    public class GetAzureServiceCommand : ServiceManagementBaseCmdlet
    {
        public GetAzureServiceCommand()
        {
        }

        public GetAzureServiceCommand(IServiceManagement channel)
        {
            Channel = channel;
        }

        [Parameter(Position = 0, Mandatory = false, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string ServiceName
        {
            get;
            set;
        }

        protected override void OnProcessRecord()
        {
            Mapper.Initialize(m => m.AddProfile<ServiceManagementPofile>());

            if (this.ServiceName != null)
            {
                ExecuteClientActionNewSM(
                    null,
                    CommandRuntime.ToString(),
                    () => this.ComputeClient.HostedServices.Get(this.ServiceName),
                    (operation, service) => new int[1].Select(i =>
                                                                  {
                                                                      var context = ContextFactory<HostedServiceGetResponse, HostedServiceDetailedContext>(service, operation);
                                                                      Mapper.Map(service.Properties, context);
                                                                      return context;
                                                                  }));
            }
            else
            {
                ExecuteClientActionNewSM(
                    null,
                    CommandRuntime.ToString(),
                    () => this.ComputeClient.HostedServices.List(),
                    (operation, services) => services.HostedServices.Select(service =>
                                                                                {
                                                                                    var context = ContextFactory<HostedServiceListResponse.HostedService, HostedServiceDetailedContext>(service, operation);
                                                                                    Mapper.Map(service.Properties, context);
                                                                                    return context;
                                                                                }));
            }
        }
    }
}
