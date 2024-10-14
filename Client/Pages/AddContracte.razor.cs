using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Xceed.Words.NET;

namespace VersaControl.Client.Pages
{
    public partial class AddContracte
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
            contracte = new VersaControl.Server.Models.versacontrol.Contracte();
        }
        protected bool errorVisible;
        protected VersaControl.Server.Models.versacontrol.Contracte contracte;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Beneficiari> beneficiarisForIdBeneficiar;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Contractori> contractorisForIdFurnizor;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.Monede> monedesForIdMoneda;

        protected IEnumerable<VersaControl.Server.Models.versacontrol.TipuriContract> tipuriContractsForIdTipContract;


        protected int beneficiarisForIdBeneficiarCount;
        protected VersaControl.Server.Models.versacontrol.Beneficiari beneficiarisForIdBeneficiarValue;
        protected async Task beneficiarisForIdBeneficiarLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetBeneficiaris(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(NumeCompanie, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                beneficiarisForIdBeneficiar = result.Value.AsODataEnumerable();
                beneficiarisForIdBeneficiarCount = result.Count;

                if (!object.Equals(contracte.IdBeneficiar, null))
                {
                    var valueResult = await versacontrolService.GetBeneficiaris(filter: $"Id eq {contracte.IdBeneficiar}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        beneficiarisForIdBeneficiarValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Beneficiari" });
            }
        }

        protected int contractorisForIdFurnizorCount;
        protected VersaControl.Server.Models.versacontrol.Contractori contractorisForIdFurnizorValue;
        protected async Task contractorisForIdFurnizorLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetContractoris(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(NumeCompanie, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contractorisForIdFurnizor = result.Value.AsODataEnumerable();
                contractorisForIdFurnizorCount = result.Count;

                if (!object.Equals(contracte.IdFurnizor, null))
                {
                    var valueResult = await versacontrolService.GetContractoris(filter: $"Id eq {contracte.IdFurnizor}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contractorisForIdFurnizorValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contractori" });
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

                if (!object.Equals(contracte.IdMoneda, null))
                {
                    var valueResult = await versacontrolService.GetMonedes(filter: $"Id eq {contracte.IdMoneda}");
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

        protected int tipuriContractsForIdTipContractCount;
        protected VersaControl.Server.Models.versacontrol.TipuriContract tipuriContractsForIdTipContractValue;
        protected async Task tipuriContractsForIdTipContractLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await versacontrolService.GetTipuriContracts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(Tip, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                tipuriContractsForIdTipContract = result.Value.AsODataEnumerable();
                tipuriContractsForIdTipContractCount = result.Count;

                if (!object.Equals(contracte.IdTipContract, null))
                {
                    var valueResult = await versacontrolService.GetTipuriContracts(filter: $"Id eq {contracte.IdTipContract}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        tipuriContractsForIdTipContractValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TipuriContract" });
            }
        }
        public bool isSubmited = false;
        protected async Task FormSubmit()
        {
            try
            {
                var result = await versacontrolService.CreateContracte(contracte);
                DialogService.Close(contracte);
                string filePath = @"D:\versalogic\TestDocument.docx";
                try {
                 using (var document = DocX.Create(filePath))

            {

                
                // Add a paragraph
                document.InsertParagraph("Hello, this is a test paragraph.");

                // Save the document
                document.Save();

                Console.WriteLine("Document created successfully at " + filePath);
                isSubmited = true;
            }
            } catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
        private bool isJsInitialized = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Aici poți face orice inițializări necesare
            isJsInitialized = true;
        }
    }


        protected async System.Threading.Tasks.Task Button2Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            try
            {
                var result = await versacontrolService.CreateContracte(contracte);
                DialogService.Close(contracte);
                string filePath = @"D:\versalogic\TestDocument.docx";
                try {
                 using (var document = DocX.Create(filePath))

            {

                
                // Add a paragraph
                document.InsertParagraph("Hello, this is a test paragraph.");

                // Save the document
                document.Save();

                Console.WriteLine("Document created successfully at " + filePath);
                isSubmited = true;
            }
            } catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
            }
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
// Creează conținutul fișierului în C#
        var content = "Hello, this is a test paragraph generated in C#!";

        // Apel la funcția JavaScript pentru salvare fișier, trimitem conținutul și numele fișierului
        if(isSubmited){
        Console.WriteLine(content);

        await JSRuntime.InvokeVoidAsync("saveFileFromBlazor", content, "TestDocument.txt");
        }
        
        }
    }
}