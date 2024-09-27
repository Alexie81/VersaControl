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
    [Route("odata/versacontrol/Roluris")]
    public partial class RolurisController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public RolurisController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Roluri> GetRoluris()
        {
            var items = this.context.Roluris.AsQueryable<VersaControl.Server.Models.versacontrol.Roluri>();
            this.OnRolurisRead(ref items);

            return items;
        }

        partial void OnRolurisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Roluri> items);

        partial void OnRoluriGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Roluri> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Roluris(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Roluri> GetRoluri(int key)
        {
            var items = this.context.Roluris.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnRoluriGet(ref result);

            return result;
        }
        partial void OnRoluriDeleted(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriDeleted(VersaControl.Server.Models.versacontrol.Roluri item);

        [HttpDelete("/odata/versacontrol/Roluris(Id={Id})")]
        public IActionResult DeleteRoluri(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Roluris
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnRoluriDeleted(item);
                this.context.Roluris.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoluriDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoluriUpdated(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriUpdated(VersaControl.Server.Models.versacontrol.Roluri item);

        [HttpPut("/odata/versacontrol/Roluris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoluri(int key, [FromBody]VersaControl.Server.Models.versacontrol.Roluri item)
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
                this.OnRoluriUpdated(item);
                this.context.Roluris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roluris.Where(i => i.Id == key);
                
                this.OnAfterRoluriUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Roluris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoluri(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Roluri> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Roluris.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnRoluriUpdated(item);
                this.context.Roluris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roluris.Where(i => i.Id == key);
                
                this.OnAfterRoluriUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoluriCreated(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriCreated(VersaControl.Server.Models.versacontrol.Roluri item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Roluri item)
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

                this.OnRoluriCreated(item);
                this.context.Roluris.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roluris.Where(i => i.Id == item.Id);

                

                this.OnAfterRoluriCreated(item);

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
