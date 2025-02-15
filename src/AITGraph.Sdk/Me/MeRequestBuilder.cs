// <auto-generated/>
#pragma warning disable CS0618
using AITGraph.Sdk.Me.Profile;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace AITGraph.Sdk.Me
{
    /// <summary>
    /// Builds and executes requests for operations under \me
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MeRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Provides operations to manage the profile property of the microsoft.graph.user entity.</summary>
        public global::AITGraph.Sdk.Me.Profile.ProfileRequestBuilder Profile
        {
            get => new global::AITGraph.Sdk.Me.Profile.ProfileRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Me.MeRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MeRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/me", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Me.MeRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MeRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/me", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
