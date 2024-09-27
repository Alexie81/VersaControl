
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace VersaControl.Client
{
    public partial class versacontrolService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public versacontrolService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/versacontrol/");
        }


        public async System.Threading.Tasks.Task ExportBeneficiarisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/beneficiaris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/beneficiaris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportBeneficiarisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/beneficiaris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/beneficiaris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetBeneficiaris(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Beneficiari>> GetBeneficiaris(Query query)
        {
            return await GetBeneficiaris(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Beneficiari>> GetBeneficiaris(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Beneficiaris");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBeneficiaris(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Beneficiari>>(response);
        }

        partial void OnCreateBeneficiari(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> CreateBeneficiari(VersaControl.Server.Models.versacontrol.Beneficiari beneficiari = default(VersaControl.Server.Models.versacontrol.Beneficiari))
        {
            var uri = new Uri(baseUri, $"Beneficiaris");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(beneficiari), Encoding.UTF8, "application/json");

            OnCreateBeneficiari(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Beneficiari>(response);
        }

        partial void OnDeleteBeneficiari(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteBeneficiari(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Beneficiaris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteBeneficiari(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetBeneficiariById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> GetBeneficiariById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Beneficiaris({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetBeneficiariById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Beneficiari>(response);
        }

        partial void OnUpdateBeneficiari(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateBeneficiari(int id = default(int), VersaControl.Server.Models.versacontrol.Beneficiari beneficiari = default(VersaControl.Server.Models.versacontrol.Beneficiari))
        {
            var uri = new Uri(baseUri, $"Beneficiaris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(beneficiari), Encoding.UTF8, "application/json");

            OnUpdateBeneficiari(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportContractorisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractoris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractoris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContractorisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractoris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractoris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContractoris(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contractori>> GetContractoris(Query query)
        {
            return await GetContractoris(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contractori>> GetContractoris(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contractoris");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContractoris(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contractori>>(response);
        }

        partial void OnCreateContractori(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> CreateContractori(VersaControl.Server.Models.versacontrol.Contractori contractori = default(VersaControl.Server.Models.versacontrol.Contractori))
        {
            var uri = new Uri(baseUri, $"Contractoris");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contractori), Encoding.UTF8, "application/json");

            OnCreateContractori(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Contractori>(response);
        }

        partial void OnDeleteContractori(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContractori(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contractoris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContractori(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContractoriById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> GetContractoriById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contractoris({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContractoriById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Contractori>(response);
        }

        partial void OnUpdateContractori(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContractori(int id = default(int), VersaControl.Server.Models.versacontrol.Contractori contractori = default(VersaControl.Server.Models.versacontrol.Contractori))
        {
            var uri = new Uri(baseUri, $"Contractoris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contractori), Encoding.UTF8, "application/json");

            OnUpdateContractori(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRolurisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/roluris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/roluris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRolurisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/roluris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/roluris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoluris(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Roluri>> GetRoluris(Query query)
        {
            return await GetRoluris(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Roluri>> GetRoluris(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Roluris");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoluris(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Roluri>>(response);
        }

        partial void OnCreateRoluri(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> CreateRoluri(VersaControl.Server.Models.versacontrol.Roluri roluri = default(VersaControl.Server.Models.versacontrol.Roluri))
        {
            var uri = new Uri(baseUri, $"Roluris");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roluri), Encoding.UTF8, "application/json");

            OnCreateRoluri(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Roluri>(response);
        }

        partial void OnDeleteRoluri(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRoluri(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Roluris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRoluri(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoluriById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> GetRoluriById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Roluris({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoluriById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Roluri>(response);
        }

        partial void OnUpdateRoluri(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRoluri(int id = default(int), VersaControl.Server.Models.versacontrol.Roluri roluri = default(VersaControl.Server.Models.versacontrol.Roluri))
        {
            var uri = new Uri(baseUri, $"Roluris({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(roluri), Encoding.UTF8, "application/json");

            OnUpdateRoluri(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}