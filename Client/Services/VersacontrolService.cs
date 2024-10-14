
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


        public async System.Threading.Tasks.Task ExportEfmigrationshistoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/efmigrationshistories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/efmigrationshistories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportEfmigrationshistoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/efmigrationshistories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/efmigrationshistories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetEfmigrationshistories(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Efmigrationshistory>> GetEfmigrationshistories(Query query)
        {
            return await GetEfmigrationshistories(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Efmigrationshistory>> GetEfmigrationshistories(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Efmigrationshistories");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEfmigrationshistories(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Efmigrationshistory>>(response);
        }

        partial void OnCreateEfmigrationshistory(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> CreateEfmigrationshistory(VersaControl.Server.Models.versacontrol.Efmigrationshistory efmigrationshistory = default(VersaControl.Server.Models.versacontrol.Efmigrationshistory))
        {
            var uri = new Uri(baseUri, $"Efmigrationshistories");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(efmigrationshistory), Encoding.UTF8, "application/json");

            OnCreateEfmigrationshistory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Efmigrationshistory>(response);
        }

        partial void OnDeleteEfmigrationshistory(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteEfmigrationshistory(string migrationId = default(string))
        {
            var uri = new Uri(baseUri, $"Efmigrationshistories('{Uri.EscapeDataString(migrationId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteEfmigrationshistory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetEfmigrationshistoryByMigrationId(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> GetEfmigrationshistoryByMigrationId(string expand = default(string), string migrationId = default(string))
        {
            var uri = new Uri(baseUri, $"Efmigrationshistories('{Uri.EscapeDataString(migrationId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetEfmigrationshistoryByMigrationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Efmigrationshistory>(response);
        }

        partial void OnUpdateEfmigrationshistory(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateEfmigrationshistory(string migrationId = default(string), VersaControl.Server.Models.versacontrol.Efmigrationshistory efmigrationshistory = default(VersaControl.Server.Models.versacontrol.Efmigrationshistory))
        {
            var uri = new Uri(baseUri, $"Efmigrationshistories('{Uri.EscapeDataString(migrationId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(efmigrationshistory), Encoding.UTF8, "application/json");

            OnUpdateEfmigrationshistory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAdminSettingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/adminsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/adminsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAdminSettingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/adminsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/adminsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAdminSettings(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.AdminSetting>> GetAdminSettings(Query query)
        {
            return await GetAdminSettings(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.AdminSetting>> GetAdminSettings(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AdminSettings");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdminSettings(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.AdminSetting>>(response);
        }

        partial void OnCreateAdminSetting(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> CreateAdminSetting(VersaControl.Server.Models.versacontrol.AdminSetting adminSetting = default(VersaControl.Server.Models.versacontrol.AdminSetting))
        {
            var uri = new Uri(baseUri, $"AdminSettings");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adminSetting), Encoding.UTF8, "application/json");

            OnCreateAdminSetting(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.AdminSetting>(response);
        }

        partial void OnDeleteAdminSetting(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAdminSetting(int id = default(int))
        {
            var uri = new Uri(baseUri, $"AdminSettings({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAdminSetting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAdminSettingById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> GetAdminSettingById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"AdminSettings({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAdminSettingById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.AdminSetting>(response);
        }

        partial void OnUpdateAdminSetting(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAdminSetting(int id = default(int), VersaControl.Server.Models.versacontrol.AdminSetting adminSetting = default(VersaControl.Server.Models.versacontrol.AdminSetting))
        {
            var uri = new Uri(baseUri, $"AdminSettings({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(adminSetting), Encoding.UTF8, "application/json");

            OnUpdateAdminSetting(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAnexasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/anexas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/anexas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAnexasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/anexas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/anexas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAnexas(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Anexa>> GetAnexas(Query query)
        {
            return await GetAnexas(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Anexa>> GetAnexas(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Anexas");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAnexas(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Anexa>>(response);
        }

        partial void OnCreateAnexa(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> CreateAnexa(VersaControl.Server.Models.versacontrol.Anexa anexa = default(VersaControl.Server.Models.versacontrol.Anexa))
        {
            var uri = new Uri(baseUri, $"Anexas");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(anexa), Encoding.UTF8, "application/json");

            OnCreateAnexa(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Anexa>(response);
        }

        partial void OnDeleteAnexa(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAnexa(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Anexas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAnexa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAnexaById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> GetAnexaById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Anexas({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAnexaById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Anexa>(response);
        }

        partial void OnUpdateAnexa(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAnexa(int id = default(int), VersaControl.Server.Models.versacontrol.Anexa anexa = default(VersaControl.Server.Models.versacontrol.Anexa))
        {
            var uri = new Uri(baseUri, $"Anexas({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(anexa), Encoding.UTF8, "application/json");

            OnUpdateAnexa(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetroleclaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetroleclaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetroleclaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>> GetAspnetroleclaims(Query query)
        {
            return await GetAspnetroleclaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>> GetAspnetroleclaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetroleclaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetroleclaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>>(response);
        }

        partial void OnCreateAspnetroleclaim(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> CreateAspnetroleclaim(VersaControl.Server.Models.versacontrol.Aspnetroleclaim aspnetroleclaim = default(VersaControl.Server.Models.versacontrol.Aspnetroleclaim))
        {
            var uri = new Uri(baseUri, $"Aspnetroleclaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetroleclaim), Encoding.UTF8, "application/json");

            OnCreateAspnetroleclaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>(response);
        }

        partial void OnDeleteAspnetroleclaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetroleclaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Aspnetroleclaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetroleclaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetroleclaimById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> GetAspnetroleclaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Aspnetroleclaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetroleclaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>(response);
        }

        partial void OnUpdateAspnetroleclaim(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetroleclaim(int id = default(int), VersaControl.Server.Models.versacontrol.Aspnetroleclaim aspnetroleclaim = default(VersaControl.Server.Models.versacontrol.Aspnetroleclaim))
        {
            var uri = new Uri(baseUri, $"Aspnetroleclaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetroleclaim), Encoding.UTF8, "application/json");

            OnUpdateAspnetroleclaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetroles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetrole>> GetAspnetroles(Query query)
        {
            return await GetAspnetroles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetrole>> GetAspnetroles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetroles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetroles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetrole>>(response);
        }

        partial void OnCreateAspnetrole(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> CreateAspnetrole(VersaControl.Server.Models.versacontrol.Aspnetrole aspnetrole = default(VersaControl.Server.Models.versacontrol.Aspnetrole))
        {
            var uri = new Uri(baseUri, $"Aspnetroles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetrole), Encoding.UTF8, "application/json");

            OnCreateAspnetrole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetrole>(response);
        }

        partial void OnDeleteAspnetrole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetrole(string id = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetroles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetroleById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> GetAspnetroleById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetroles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetroleById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetrole>(response);
        }

        partial void OnUpdateAspnetrole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetrole(string id = default(string), VersaControl.Server.Models.versacontrol.Aspnetrole aspnetrole = default(VersaControl.Server.Models.versacontrol.Aspnetrole))
        {
            var uri = new Uri(baseUri, $"Aspnetroles('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetrole), Encoding.UTF8, "application/json");

            OnUpdateAspnetrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserclaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserclaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetuserclaims(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>> GetAspnetuserclaims(Query query)
        {
            return await GetAspnetuserclaims(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>> GetAspnetuserclaims(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserclaims");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserclaims(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>>(response);
        }

        partial void OnCreateAspnetuserclaim(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> CreateAspnetuserclaim(VersaControl.Server.Models.versacontrol.Aspnetuserclaim aspnetuserclaim = default(VersaControl.Server.Models.versacontrol.Aspnetuserclaim))
        {
            var uri = new Uri(baseUri, $"Aspnetuserclaims");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserclaim), Encoding.UTF8, "application/json");

            OnCreateAspnetuserclaim(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>(response);
        }

        partial void OnDeleteAspnetuserclaim(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetuserclaim(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Aspnetuserclaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetuserclaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetuserclaimById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> GetAspnetuserclaimById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Aspnetuserclaims({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserclaimById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>(response);
        }

        partial void OnUpdateAspnetuserclaim(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetuserclaim(int id = default(int), VersaControl.Server.Models.versacontrol.Aspnetuserclaim aspnetuserclaim = default(VersaControl.Server.Models.versacontrol.Aspnetuserclaim))
        {
            var uri = new Uri(baseUri, $"Aspnetuserclaims({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserclaim), Encoding.UTF8, "application/json");

            OnUpdateAspnetuserclaim(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserloginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserloginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetuserlogins(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>> GetAspnetuserlogins(Query query)
        {
            return await GetAspnetuserlogins(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>> GetAspnetuserlogins(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserlogins");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserlogins(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>>(response);
        }

        partial void OnCreateAspnetuserlogin(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> CreateAspnetuserlogin(VersaControl.Server.Models.versacontrol.Aspnetuserlogin aspnetuserlogin = default(VersaControl.Server.Models.versacontrol.Aspnetuserlogin))
        {
            var uri = new Uri(baseUri, $"Aspnetuserlogins");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserlogin), Encoding.UTF8, "application/json");

            OnCreateAspnetuserlogin(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>(response);
        }

        partial void OnDeleteAspnetuserlogin(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetuserlogin(string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserlogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetuserlogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetuserloginByLoginProviderAndProviderKey(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> GetAspnetuserloginByLoginProviderAndProviderKey(string expand = default(string), string loginProvider = default(string), string providerKey = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserlogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserloginByLoginProviderAndProviderKey(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>(response);
        }

        partial void OnUpdateAspnetuserlogin(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetuserlogin(string loginProvider = default(string), string providerKey = default(string), VersaControl.Server.Models.versacontrol.Aspnetuserlogin aspnetuserlogin = default(VersaControl.Server.Models.versacontrol.Aspnetuserlogin))
        {
            var uri = new Uri(baseUri, $"Aspnetuserlogins(LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',ProviderKey='{Uri.EscapeDataString(providerKey.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserlogin), Encoding.UTF8, "application/json");

            OnUpdateAspnetuserlogin(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetuserrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetuserroles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserrole>> GetAspnetuserroles(Query query)
        {
            return await GetAspnetuserroles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserrole>> GetAspnetuserroles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserroles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserroles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuserrole>>(response);
        }

        partial void OnCreateAspnetuserrole(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> CreateAspnetuserrole(VersaControl.Server.Models.versacontrol.Aspnetuserrole aspnetuserrole = default(VersaControl.Server.Models.versacontrol.Aspnetuserrole))
        {
            var uri = new Uri(baseUri, $"Aspnetuserroles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserrole), Encoding.UTF8, "application/json");

            OnCreateAspnetuserrole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserrole>(response);
        }

        partial void OnDeleteAspnetuserrole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetuserrole(string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserroles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetuserrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetuserroleByUserIdAndRoleId(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> GetAspnetuserroleByUserIdAndRoleId(string expand = default(string), string userId = default(string), string roleId = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetuserroles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserroleByUserIdAndRoleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuserrole>(response);
        }

        partial void OnUpdateAspnetuserrole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetuserrole(string userId = default(string), string roleId = default(string), VersaControl.Server.Models.versacontrol.Aspnetuserrole aspnetuserrole = default(VersaControl.Server.Models.versacontrol.Aspnetuserrole))
        {
            var uri = new Uri(baseUri, $"Aspnetuserroles(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',RoleId='{Uri.EscapeDataString(roleId.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuserrole), Encoding.UTF8, "application/json");

            OnUpdateAspnetuserrole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetusersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetusersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetusers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuser>> GetAspnetusers(Query query)
        {
            return await GetAspnetusers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuser>> GetAspnetusers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusers");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetusers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetuser>>(response);
        }

        partial void OnCreateAspnetuser(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> CreateAspnetuser(VersaControl.Server.Models.versacontrol.Aspnetuser aspnetuser = default(VersaControl.Server.Models.versacontrol.Aspnetuser))
        {
            var uri = new Uri(baseUri, $"Aspnetusers");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuser), Encoding.UTF8, "application/json");

            OnCreateAspnetuser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuser>(response);
        }

        partial void OnDeleteAspnetuser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetuser(string id = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetuser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetuserById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> GetAspnetuserById(string expand = default(string), string id = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetuserById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetuser>(response);
        }

        partial void OnUpdateAspnetuser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetuser(string id = default(string), VersaControl.Server.Models.versacontrol.Aspnetuser aspnetuser = default(VersaControl.Server.Models.versacontrol.Aspnetuser))
        {
            var uri = new Uri(baseUri, $"Aspnetusers('{Uri.EscapeDataString(id.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetuser), Encoding.UTF8, "application/json");

            OnUpdateAspnetuser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAspnetusertokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAspnetusertokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAspnetusertokens(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetusertoken>> GetAspnetusertokens(Query query)
        {
            return await GetAspnetusertokens(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetusertoken>> GetAspnetusertokens(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusertokens");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetusertokens(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Aspnetusertoken>>(response);
        }

        partial void OnCreateAspnetusertoken(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> CreateAspnetusertoken(VersaControl.Server.Models.versacontrol.Aspnetusertoken aspnetusertoken = default(VersaControl.Server.Models.versacontrol.Aspnetusertoken))
        {
            var uri = new Uri(baseUri, $"Aspnetusertokens");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetusertoken), Encoding.UTF8, "application/json");

            OnCreateAspnetusertoken(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetusertoken>(response);
        }

        partial void OnDeleteAspnetusertoken(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAspnetusertoken(string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusertokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAspnetusertoken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAspnetusertokenByUserIdAndLoginProviderAndName(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> GetAspnetusertokenByUserIdAndLoginProviderAndName(string expand = default(string), string userId = default(string), string loginProvider = default(string), string name = default(string))
        {
            var uri = new Uri(baseUri, $"Aspnetusertokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAspnetusertokenByUserIdAndLoginProviderAndName(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Aspnetusertoken>(response);
        }

        partial void OnUpdateAspnetusertoken(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAspnetusertoken(string userId = default(string), string loginProvider = default(string), string name = default(string), VersaControl.Server.Models.versacontrol.Aspnetusertoken aspnetusertoken = default(VersaControl.Server.Models.versacontrol.Aspnetusertoken))
        {
            var uri = new Uri(baseUri, $"Aspnetusertokens(UserId='{Uri.EscapeDataString(userId.Trim().Replace("'", "''"))}',LoginProvider='{Uri.EscapeDataString(loginProvider.Trim().Replace("'", "''"))}',Name='{Uri.EscapeDataString(name.Trim().Replace("'", "''"))}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(aspnetusertoken), Encoding.UTF8, "application/json");

            OnUpdateAspnetusertoken(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
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

        public async System.Threading.Tasks.Task ExportContractesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContractesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetContractes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contracte>> GetContractes(Query query)
        {
            return await GetContractes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contracte>> GetContractes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contractes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContractes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Contracte>>(response);
        }

        partial void OnCreateContracte(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> CreateContracte(VersaControl.Server.Models.versacontrol.Contracte contracte = default(VersaControl.Server.Models.versacontrol.Contracte))
        {
            var uri = new Uri(baseUri, $"Contractes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contracte), Encoding.UTF8, "application/json");

            OnCreateContracte(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Contracte>(response);
        }

        partial void OnDeleteContracte(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteContracte(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contractes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContracte(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetContracteById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> GetContracteById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Contractes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContracteById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Contracte>(response);
        }

        partial void OnUpdateContracte(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateContracte(int id = default(int), VersaControl.Server.Models.versacontrol.Contracte contracte = default(VersaControl.Server.Models.versacontrol.Contracte))
        {
            var uri = new Uri(baseUri, $"Contractes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contracte), Encoding.UTF8, "application/json");

            OnUpdateContracte(httpRequestMessage);

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

        public async System.Threading.Tasks.Task ExportMonedesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/monedes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/monedes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMonedesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/monedes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/monedes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMonedes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Monede>> GetMonedes(Query query)
        {
            return await GetMonedes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Monede>> GetMonedes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Monedes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMonedes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.Monede>>(response);
        }

        partial void OnCreateMonede(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Monede> CreateMonede(VersaControl.Server.Models.versacontrol.Monede monede = default(VersaControl.Server.Models.versacontrol.Monede))
        {
            var uri = new Uri(baseUri, $"Monedes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(monede), Encoding.UTF8, "application/json");

            OnCreateMonede(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Monede>(response);
        }

        partial void OnDeleteMonede(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMonede(int id = default(int))
        {
            var uri = new Uri(baseUri, $"Monedes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMonede(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMonedeById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.Monede> GetMonedeById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"Monedes({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMonedeById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.Monede>(response);
        }

        partial void OnUpdateMonede(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMonede(int id = default(int), VersaControl.Server.Models.versacontrol.Monede monede = default(VersaControl.Server.Models.versacontrol.Monede))
        {
            var uri = new Uri(baseUri, $"Monedes({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(monede), Encoding.UTF8, "application/json");

            OnUpdateMonede(httpRequestMessage);

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

        public async System.Threading.Tasks.Task ExportTipuriContractsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/tipuricontracts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/tipuricontracts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTipuriContractsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/tipuricontracts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/tipuricontracts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetTipuriContracts(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.TipuriContract>> GetTipuriContracts(Query query)
        {
            return await GetTipuriContracts(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.TipuriContract>> GetTipuriContracts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TipuriContracts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTipuriContracts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<VersaControl.Server.Models.versacontrol.TipuriContract>>(response);
        }

        partial void OnCreateTipuriContract(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> CreateTipuriContract(VersaControl.Server.Models.versacontrol.TipuriContract tipuriContract = default(VersaControl.Server.Models.versacontrol.TipuriContract))
        {
            var uri = new Uri(baseUri, $"TipuriContracts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tipuriContract), Encoding.UTF8, "application/json");

            OnCreateTipuriContract(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.TipuriContract>(response);
        }

        partial void OnDeleteTipuriContract(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteTipuriContract(int id = default(int))
        {
            var uri = new Uri(baseUri, $"TipuriContracts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTipuriContract(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetTipuriContractById(HttpRequestMessage requestMessage);

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> GetTipuriContractById(string expand = default(string), int id = default(int))
        {
            var uri = new Uri(baseUri, $"TipuriContracts({id})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTipuriContractById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<VersaControl.Server.Models.versacontrol.TipuriContract>(response);
        }

        partial void OnUpdateTipuriContract(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateTipuriContract(int id = default(int), VersaControl.Server.Models.versacontrol.TipuriContract tipuriContract = default(VersaControl.Server.Models.versacontrol.TipuriContract))
        {
            var uri = new Uri(baseUri, $"TipuriContracts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tipuriContract), Encoding.UTF8, "application/json");

            OnUpdateTipuriContract(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}