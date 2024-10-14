using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VersaControl.Server.Controllers.versacontrol
{
    [Route("odata/versacontrol/TipuriContracts")]
    public partial class TipuriContractsController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public TipuriContractsController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.TipuriContract> GetTipuriContracts()
        {
            var items = this.context.TipuriContracts.AsQueryable<VersaControl.Server.Models.versacontrol.TipuriContract>();
            this.OnTipuriContractsRead(ref items);

            return items;
        }

        partial void OnTipuriContractsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.TipuriContract> items);

        partial void OnTipuriContractGet(ref SingleResult<VersaControl.Server.Models.versacontrol.TipuriContract> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/TipuriContracts(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.TipuriContract> GetTipuriContract(int key)
        {
            var items = this.context.TipuriContracts.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnTipuriContractGet(ref result);

            return result;
        }
        partial void OnTipuriContractDeleted(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractDeleted(VersaControl.Server.Models.versacontrol.TipuriContract item);

        [HttpDelete("/odata/versacontrol/TipuriContracts(Id={Id})")]
        public IActionResult DeleteTipuriContract(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.TipuriContracts
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTipuriContractDeleted(item);
                this.context.TipuriContracts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTipuriContractDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTipuriContractUpdated(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractUpdated(VersaControl.Server.Models.versacontrol.TipuriContract item);

        [HttpPut("/odata/versacontrol/TipuriContracts(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTipuriContract(int key, [FromBody]VersaControl.Server.Models.versacontrol.TipuriContract item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.Id != key))
                {
                    return BadRequest();
                }
                this.OnTipuriContractUpdated(item);
                this.context.TipuriContracts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TipuriContracts.Where(i => i.Id == key);
                
                this.OnAfterTipuriContractUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/TipuriContracts(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTipuriContract(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.TipuriContract> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.TipuriContracts.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTipuriContractUpdated(item);
                this.context.TipuriContracts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TipuriContracts.Where(i => i.Id == key);
                
                this.OnAfterTipuriContractUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTipuriContractCreated(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractCreated(VersaControl.Server.Models.versacontrol.TipuriContract item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.TipuriContract item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnTipuriContractCreated(item);
                this.context.TipuriContracts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TipuriContracts.Where(i => i.Id == item.Id);

                

                this.OnAfterTipuriContractCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
