using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using VersaControl.Server.Data;

namespace VersaControl.Server.Controllers
{
    public partial class ExportversacontrolController : ExportController
    {
        private readonly versacontrolContext context;
        private readonly versacontrolService service;

        public ExportversacontrolController(versacontrolContext context, versacontrolService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/versacontrol/efmigrationshistories/csv")]
        [HttpGet("/export/versacontrol/efmigrationshistories/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEfmigrationshistoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEfmigrationshistories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/efmigrationshistories/excel")]
        [HttpGet("/export/versacontrol/efmigrationshistories/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEfmigrationshistoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEfmigrationshistories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/adminsettings/csv")]
        [HttpGet("/export/versacontrol/adminsettings/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdminSettingsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAdminSettings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/adminsettings/excel")]
        [HttpGet("/export/versacontrol/adminsettings/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAdminSettingsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAdminSettings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/anexas/csv")]
        [HttpGet("/export/versacontrol/anexas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAnexasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAnexas(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/anexas/excel")]
        [HttpGet("/export/versacontrol/anexas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAnexasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAnexas(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetroleclaims/csv")]
        [HttpGet("/export/versacontrol/aspnetroleclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetroleclaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetroleclaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetroleclaims/excel")]
        [HttpGet("/export/versacontrol/aspnetroleclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetroleclaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetroleclaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetroles/csv")]
        [HttpGet("/export/versacontrol/aspnetroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetrolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetroles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetroles/excel")]
        [HttpGet("/export/versacontrol/aspnetroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetrolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetroles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserclaims/csv")]
        [HttpGet("/export/versacontrol/aspnetuserclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserclaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetuserclaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserclaims/excel")]
        [HttpGet("/export/versacontrol/aspnetuserclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserclaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetuserclaims(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserlogins/csv")]
        [HttpGet("/export/versacontrol/aspnetuserlogins/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserloginsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetuserlogins(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserlogins/excel")]
        [HttpGet("/export/versacontrol/aspnetuserlogins/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserloginsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetuserlogins(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserroles/csv")]
        [HttpGet("/export/versacontrol/aspnetuserroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserrolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetuserroles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetuserroles/excel")]
        [HttpGet("/export/versacontrol/aspnetuserroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetuserrolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetuserroles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetusers/csv")]
        [HttpGet("/export/versacontrol/aspnetusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetusersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetusers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetusers/excel")]
        [HttpGet("/export/versacontrol/aspnetusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetusersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetusers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetusertokens/csv")]
        [HttpGet("/export/versacontrol/aspnetusertokens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetusertokensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspnetusertokens(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/aspnetusertokens/excel")]
        [HttpGet("/export/versacontrol/aspnetusertokens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspnetusertokensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspnetusertokens(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/beneficiaris/csv")]
        [HttpGet("/export/versacontrol/beneficiaris/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBeneficiarisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBeneficiaris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/beneficiaris/excel")]
        [HttpGet("/export/versacontrol/beneficiaris/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBeneficiarisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBeneficiaris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/contractes/csv")]
        [HttpGet("/export/versacontrol/contractes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContractesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContractes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/contractes/excel")]
        [HttpGet("/export/versacontrol/contractes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContractesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContractes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/contractoris/csv")]
        [HttpGet("/export/versacontrol/contractoris/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContractorisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContractoris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/contractoris/excel")]
        [HttpGet("/export/versacontrol/contractoris/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContractorisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContractoris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/monedes/csv")]
        [HttpGet("/export/versacontrol/monedes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMonedesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMonedes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/monedes/excel")]
        [HttpGet("/export/versacontrol/monedes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMonedesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMonedes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/roluris/csv")]
        [HttpGet("/export/versacontrol/roluris/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolurisToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoluris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/roluris/excel")]
        [HttpGet("/export/versacontrol/roluris/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolurisToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoluris(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/tipuricontracts/csv")]
        [HttpGet("/export/versacontrol/tipuricontracts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTipuriContractsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTipuriContracts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/versacontrol/tipuricontracts/excel")]
        [HttpGet("/export/versacontrol/tipuricontracts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTipuriContractsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTipuriContracts(), Request.Query, false), fileName);
        }
    }
}
