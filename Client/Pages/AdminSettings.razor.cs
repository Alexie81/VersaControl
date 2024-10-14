using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace VersaControl.Client.Pages
{
    public partial class AdminSettings
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public versacontrolService versacontrolService { get; set; }

        protected IEnumerable<VersaControl.Server.Models.versacontrol.AdminSetting> adminSettings;

        protected RadzenDataGrid<VersaControl.Server.Models.versacontrol.AdminSetting> grid0;
        protected int count;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetAdminSettings(filter: $"{args.Filter}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                adminSettings = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AdminSettings" });
            }
        }

        protected async Task EditRow(VersaControl.Server.Models.versacontrol.AdminSetting args)
        {
            await DialogService.OpenAsync<EditAdminSetting>("Edit AdminSetting", new Dictionary<string, object> { {"Id", args.Id} });
            await grid0.Reload();
        }
    }
}