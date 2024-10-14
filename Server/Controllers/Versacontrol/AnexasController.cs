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
    [Route("odata/versacontrol/Anexas")]
    public partial class AnexasController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AnexasController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Anexa> GetAnexas()
        {
            var items = this.context.Anexas.AsQueryable<VersaControl.Server.Models.versacontrol.Anexa>();
            this.OnAnexasRead(ref items);

            return items;
        }

        partial void OnAnexasRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Anexa> items);

        partial void OnAnexaGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Anexa> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Anexas(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Anexa> GetAnexa(int key)
        {
            var items = this.context.Anexas.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAnexaGet(ref result);

            return result;
        }
        partial void OnAnexaDeleted(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaDeleted(VersaControl.Server.Models.versacontrol.Anexa item);

        [HttpDelete("/odata/versacontrol/Anexas(Id={Id})")]
        public IActionResult DeleteAnexa(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Anexas
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAnexaDeleted(item);
                this.context.Anexas.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAnexaDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAnexaUpdated(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaUpdated(VersaControl.Server.Models.versacontrol.Anexa item);

        [HttpPut("/odata/versacontrol/Anexas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAnexa(int key, [FromBody]VersaControl.Server.Models.versacontrol.Anexa item)
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
                this.OnAnexaUpdated(item);
                this.context.Anexas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Anexas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Contracte,Monede");
                this.OnAfterAnexaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Anexas(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAnexa(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Anexa> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Anexas.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAnexaUpdated(item);
                this.context.Anexas.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Anexas.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Contracte,Monede");
                this.OnAfterAnexaUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAnexaCreated(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaCreated(VersaControl.Server.Models.versacontrol.Anexa item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Anexa item)
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

                this.OnAnexaCreated(item);
                this.context.Anexas.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Anexas.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Contracte,Monede");

                this.OnAfterAnexaCreated(item);

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
