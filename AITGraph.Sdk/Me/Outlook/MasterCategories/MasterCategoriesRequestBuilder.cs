// <auto-generated/>
#pragma warning disable CS0618
using AITGraph.Sdk.Models.ODataErrors;
using AITGraph.Sdk.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace AITGraph.Sdk.Me.Outlook.MasterCategories
{
    /// <summary>
    /// Provides operations to manage the masterCategories property of the microsoft.graph.outlookUser entity.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MasterCategoriesRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MasterCategoriesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/me/outlook/masterCategories{?%24count,%24filter,%24orderby,%24select,%24skip,%24top}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MasterCategoriesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/me/outlook/masterCategories{?%24count,%24filter,%24orderby,%24select,%24skip,%24top}", rawUrl)
        {
        }
        /// <summary>
        /// Get all the categories that have been defined for the user.
        /// Find more info here <see href="https://docs.microsoft.com/graph/api/outlookuser-list-mastercategories?view=graph-rest-1.0" />
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OutlookCategoryCollectionResponse"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::AITGraph.Sdk.Models.ODataErrors.ODataError">When receiving a 4XX or 5XX status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::AITGraph.Sdk.Models.OutlookCategoryCollectionResponse?> GetAsync(Action<RequestConfiguration<global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder.MasterCategoriesRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::AITGraph.Sdk.Models.OutlookCategoryCollectionResponse> GetAsync(Action<RequestConfiguration<global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder.MasterCategoriesRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "XXX", global::AITGraph.Sdk.Models.ODataErrors.ODataError.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::AITGraph.Sdk.Models.OutlookCategoryCollectionResponse>(requestInfo, global::AITGraph.Sdk.Models.OutlookCategoryCollectionResponse.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Create an outlookCategory object in the user&apos;s master list of categories.
        /// Find more info here <see href="https://docs.microsoft.com/graph/api/outlookuser-post-mastercategories?view=graph-rest-1.0" />
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Models.OutlookCategory"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::AITGraph.Sdk.Models.ODataErrors.ODataError">When receiving a 4XX or 5XX status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::AITGraph.Sdk.Models.OutlookCategory?> PostAsync(global::AITGraph.Sdk.Models.OutlookCategory body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::AITGraph.Sdk.Models.OutlookCategory> PostAsync(global::AITGraph.Sdk.Models.OutlookCategory body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "XXX", global::AITGraph.Sdk.Models.ODataErrors.ODataError.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::AITGraph.Sdk.Models.OutlookCategory>(requestInfo, global::AITGraph.Sdk.Models.OutlookCategory.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get all the categories that have been defined for the user.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder.MasterCategoriesRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder.MasterCategoriesRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Create an outlookCategory object in the user&apos;s master list of categories.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::AITGraph.Sdk.Models.OutlookCategory body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::AITGraph.Sdk.Models.OutlookCategory body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder WithUrl(string rawUrl)
        {
            return new global::AITGraph.Sdk.Me.Outlook.MasterCategories.MasterCategoriesRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get all the categories that have been defined for the user.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class MasterCategoriesRequestBuilderGetQueryParameters 
        {
            /// <summary>Include count of items</summary>
            [QueryParameter("%24count")]
            public bool? Count { get; set; }
            /// <summary>Filter items by property values</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("%24filter")]
            public string? Filter { get; set; }
#nullable restore
#else
            [QueryParameter("%24filter")]
            public string Filter { get; set; }
#endif
            /// <summary>Order items by property values</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("%24orderby")]
            public global::AITGraph.Sdk.Me.Outlook.MasterCategories.GetOrderbyQueryParameterType[]? Orderby { get; set; }
#nullable restore
#else
            [QueryParameter("%24orderby")]
            public global::AITGraph.Sdk.Me.Outlook.MasterCategories.GetOrderbyQueryParameterType[] Orderby { get; set; }
#endif
            /// <summary>Select properties to be returned</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("%24select")]
            public global::AITGraph.Sdk.Me.Outlook.MasterCategories.GetSelectQueryParameterType[]? Select { get; set; }
#nullable restore
#else
            [QueryParameter("%24select")]
            public global::AITGraph.Sdk.Me.Outlook.MasterCategories.GetSelectQueryParameterType[] Select { get; set; }
#endif
            /// <summary>Skip the first n items</summary>
            [QueryParameter("%24skip")]
            public int? Skip { get; set; }
            /// <summary>Show only the first n items</summary>
            [QueryParameter("%24top")]
            public int? Top { get; set; }
        }
    }
}
#pragma warning restore CS0618
