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
    public partial class AddContractori
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
            contractori = new VersaControl.Server.Models.versacontrol.Contractori();
        }
        protected bool errorVisible;
        protected VersaControl.Server.Models.versacontrol.Contractori contractori;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Roluri> rolurisForRol;


        protected int rolurisForRolCount;
        protected VersaControl.Server.Models.versacontrol.Roluri rolurisForRolValue;
        protected async Task rolurisForRolLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetRoluris(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Nume, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                rolurisForRol = result.Value.AsODataEnumerable();
                rolurisForRolCount = result.Count;

                if (!object.Equals(contractori.Rol, null))
                {
                    var valueResult = await versacontrolService.GetRoluris(filter: $"Id eq {contractori.Rol}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        rolurisForRolValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Roluri" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await versacontrolService.CreateContractori(contractori);
                DialogService.Close(contractori);
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