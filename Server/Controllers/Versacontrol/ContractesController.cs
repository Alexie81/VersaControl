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
    [Route("odata/versacontrol/Contractes")]
    public partial class ContractesController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public ContractesController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Contracte> GetContractes()
        {
            var items = this.context.Contractes.AsQueryable<VersaControl.Server.Models.versacontrol.Contracte>();
            this.OnContractesRead(ref items);

            return items;
        }

        partial void OnContractesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Contracte> items);

        partial void OnContracteGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Contracte> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Contractes(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Contracte> GetContracte(int key)
        {
            var items = this.context.Contractes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnContracteGet(ref result);

            return result;
        }
        partial void OnContracteDeleted(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteDeleted(VersaControl.Server.Models.versacontrol.Contracte item);

        [HttpDelete("/odata/versacontrol/Contractes(Id={Id})")]
        public IActionResult DeleteContracte(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Contractes
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnContracteDeleted(item);
                this.context.Contractes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContracteDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContracteUpdated(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteUpdated(VersaControl.Server.Models.versacontrol.Contracte item);

        [HttpPut("/odata/versacontrol/Contractes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContracte(int key, [FromBody]VersaControl.Server.Models.versacontrol.Contracte item)
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
                this.OnContracteUpdated(item);
                this.context.Contractes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Beneficiari,Contractori,Monede,TipuriContract");
                this.OnAfterContracteUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Contractes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContracte(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Contracte> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Contractes.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnContracteUpdated(item);
                this.context.Contractes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractes.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Beneficiari,Contractori,Monede,TipuriContract");
                this.OnAfterContracteUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContracteCreated(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteCreated(VersaControl.Server.Models.versacontrol.Contracte item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Contracte item)
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

                this.OnContracteCreated(item);
                this.context.Contractes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractes.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Beneficiari,Contractori,Monede,TipuriContract");

                this.OnAfterContracteCreated(item);

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
