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

namespace Microsoft.WindowsAzure.Commands.Utilities.Common.Authentication
{
    /// <summary>
    /// This interface represents objects that can be used
    /// to obtain and manage access tokens.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Get a token for the given userID and login type.
        /// </summary>
        /// <param name="environment">The environment to request a token for.</param>
        /// <param name="userId">ID of user to retrieve token for. If null,
        /// requests a whole new token, prompting user for credentials.</param>
        /// <param name="loginType">What kind of login user is doing.</param>
        /// <returns>An access token or null if authentication is canceled.</returns>
        IAccessToken GetToken(WindowsAzureEnvironment environment, string userId, LoginType loginType);

        /// <summary>
        /// Get a new login token, prompting user for credentials.
        /// </summary>
        /// <param name="environment">Environment to request a token for.</param>
        /// <param name="loginType">What kind of login the user is doing.</param>
        /// <returns>An access token or null if authentication is canceled.</returns>
        IAccessToken GetNewToken(WindowsAzureEnvironment environment, LoginType loginType);
    }
}
