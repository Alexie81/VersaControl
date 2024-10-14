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
    public partial class AddAnexa
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

        protected override async Task OnInitializedAsync()
        {
            anexa = new VersaControl.Server.Models.versacontrol.Anexa();
        }
        protected bool errorVisible;
        protected VersaControl.Server.Models.versacontrol.Anexa anexa;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Contracte> contractesForIdContract;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Monede> monedesForIdMoneda;


        protected int contractesForIdContractCount;
        protected VersaControl.Server.Models.versacontrol.Contracte contractesForIdContractValue;
        protected async Task contractesForIdContractLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetContractes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(ScopContract, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contractesForIdContract = result.Value.AsODataEnumerable();
                contractesForIdContractCount = result.Count;

                if (!object.Equals(anexa.IdContract, null))
                {
                    var valueResult = await versacontrolService.GetContractes(filter: $"Id eq {anexa.IdContract}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contractesForIdContractValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contracte" });
            }
        }

        protected int monedesForIdMonedaCount;
        protected VersaControl.Server.Models.versacontrol.Monede monedesForIdMonedaValue;
        protected async Task monedesForIdMonedaLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetMonedes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(PrescurtareMoneda, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                monedesForIdMoneda = result.Value.AsODataEnumerable();
                monedesForIdMonedaCount = result.Count;

                if (!object.Equals(anexa.IdMoneda, null))
                {
                    var valueResult = await versacontrolService.GetMonedes(filter: $"Id eq {anexa.IdMoneda}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        monedesForIdMonedaValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Monede" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await versacontrolService.CreateAnexa(anexa);
                DialogService.Close(anexa);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }
    }
}