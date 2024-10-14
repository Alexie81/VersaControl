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
    [Route("odata/versacontrol/Aspnetusers")]
    public partial class AspnetusersController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetusersController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetuser> GetAspnetusers()
        {
            var items = this.context.Aspnetusers.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetuser>();
            this.OnAspnetusersRead(ref items);

            return items;
        }

        partial void OnAspnetusersRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuser> items);

        partial void OnAspnetuserGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuser> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetusers(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuser> GetAspnetuser(string key)
        {
            var items = this.context.Aspnetusers.Where(i => i.Id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAspnetuserGet(ref result);

            return result;
        }
        partial void OnAspnetuserDeleted(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserDeleted(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        [HttpDelete("/odata/versacontrol/Aspnetusers(Id={Id})")]
        public IActionResult DeleteAspnetuser(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetusers
                    .Where(i => i.Id == Uri.UnescapeDataString(key))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetuserDeleted(item);
                this.context.Aspnetusers.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetuserDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserUpdated(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserUpdated(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        [HttpPut("/odata/versacontrol/Aspnetusers(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetuser(string key, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetuser item)
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
                this.OnAspnetuserUpdated(item);
                this.context.Aspnetusers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusers.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspnetuserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetusers(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetuser(string key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetuser> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetusers.Where(i => i.Id == Uri.UnescapeDataString(key)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetuserUpdated(item);
                this.context.Aspnetusers.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusers.Where(i => i.Id == Uri.UnescapeDataString(key));
                
                this.OnAfterAspnetuserUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserCreated(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserCreated(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetuser item)
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

                this.OnAspnetuserCreated(item);
                this.context.Aspnetusers.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusers.Where(i => i.Id == item.Id);

                

                this.OnAfterAspnetuserCreated(item);

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
