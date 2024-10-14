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
    [Route("odata/versacontrol/Contractoris")]
    public partial class ContractorisController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public ContractorisController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Contractori> GetContractoris()
        {
            var items = this.context.Contractoris.AsQueryable<VersaControl.Server.Models.versacontrol.Contractori>();
            this.OnContractorisRead(ref items);

            return items;
        }

        partial void OnContractorisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Contractori> items);

        partial void OnContractoriGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Contractori> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Contractoris(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Contractori> GetContractori(int key)
        {
            var items = this.context.Contractoris.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnContractoriGet(ref result);

            return result;
        }
        partial void OnContractoriDeleted(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriDeleted(VersaControl.Server.Models.versacontrol.Contractori item);

        [HttpDelete("/odata/versacontrol/Contractoris(Id={Id})")]
        public IActionResult DeleteContractori(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Contractoris
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnContractoriDeleted(item);
                this.context.Contractoris.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContractoriDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContractoriUpdated(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriUpdated(VersaControl.Server.Models.versacontrol.Contractori item);

        [HttpPut("/odata/versacontrol/Contractoris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContractori(int key, [FromBody]VersaControl.Server.Models.versacontrol.Contractori item)
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
                this.OnContractoriUpdated(item);
                this.context.Contractoris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractoris.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Roluri");
                this.OnAfterContractoriUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Contractoris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContractori(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Contractori> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Contractoris.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnContractoriUpdated(item);
                this.context.Contractoris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractoris.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Roluri");
                this.OnAfterContractoriUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContractoriCreated(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriCreated(VersaControl.Server.Models.versacontrol.Contractori item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Contractori item)
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

                this.OnContractoriCreated(item);
                this.context.Contractoris.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contractoris.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Roluri");

                this.OnAfterContractoriCreated(item);

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
