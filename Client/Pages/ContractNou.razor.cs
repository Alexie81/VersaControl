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
    public partial class ContractNou
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

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Beneficiari> beneficiaris;

        protected RadzenDataGrid<VersaControl.Server.Models.versacontrol.Beneficiari> grid0;
        protected int count;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task DropDownLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetBeneficiaris(filter: $@"(contains(NumeCompanie,""{search}"")", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                beneficiaris = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Beneficiaris" });
            }
        }
    }
    
}