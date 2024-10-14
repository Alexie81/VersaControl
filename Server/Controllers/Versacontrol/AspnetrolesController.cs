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
    [Route("odata/versacontrol/Aspnetroles")]
    public partial class AspnetrolesController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetrolesController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetrole> GetAspnetroles()
        {
            var items = this.context.Aspnetroles.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetrole>();
            this.OnAspnetrolesRead(ref items);

            return items;
        }

        partial void OnAspnetrolesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetrole> items);

        partial void OnAspnetroleGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetrole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetroles(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetrole> GetAspnetrole(string key)
        {
            var items = this.context.Aspnetroles.Where(i => i.Id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAspnetroleGet(ref result);

            return result;
        }
        partial void OnAspnetroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        [HttpDelete("/odata/versacontrol/Aspnetroles(Id={Id})")]
        public IActionResult DeleteAspnetrole(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetroles
                    .Where(i => i.Id == Uri.UnescapeDataString(key))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetroleDeleted(item);
                this.context.Aspnetroles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetroleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        [HttpPut("/odata/versacontrol/Aspnetroles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetrole(string key, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetrole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.Id != Uri.UnescapeDataString(key)))
                {
                    return BadRequest();
                }
                this.OnAspnetroleUpdated(item);
                this.context.Aspnetroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroles.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspnetroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetroles(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetrole(string key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetrole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetroles.Where(i => i.Id == Uri.UnescapeDataString(key)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetroleUpdated(item);
                this.context.Aspnetroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroles.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspnetroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetroleCreated(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleCreated(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetrole item)
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

                this.OnAspnetroleCreated(item);
                this.context.Aspnetroles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroles.Where(i => i.Id == item.Id);

                

                this.OnAfterAspnetroleCreated(item);

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
