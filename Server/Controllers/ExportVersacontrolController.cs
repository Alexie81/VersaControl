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
    }
}
