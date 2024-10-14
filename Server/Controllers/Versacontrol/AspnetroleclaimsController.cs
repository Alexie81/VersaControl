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
    [Route("odata/versacontrol/Aspnetroleclaims")]
    public partial class AspnetroleclaimsController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetroleclaimsController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> GetAspnetroleclaims()
        {
            var items = this.context.Aspnetroleclaims.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>();
            this.OnAspnetroleclaimsRead(ref items);

            return items;
        }

        partial void OnAspnetroleclaimsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> items);

        partial void OnAspnetroleclaimGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetroleclaims(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> GetAspnetroleclaim(int key)
        {
            var items = this.context.Aspnetroleclaims.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAspnetroleclaimGet(ref result);

            return result;
        }
        partial void OnAspnetroleclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        [HttpDelete("/odata/versacontrol/Aspnetroleclaims(Id={Id})")]
        public IActionResult DeleteAspnetroleclaim(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetroleclaims
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetroleclaimDeleted(item);
                this.context.Aspnetroleclaims.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetroleclaimDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetroleclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        [HttpPut("/odata/versacontrol/Aspnetroleclaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetroleclaim(int key, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetroleclaim item)
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
                this.OnAspnetroleclaimUpdated(item);
                this.context.Aspnetroleclaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroleclaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole");
                this.OnAfterAspnetroleclaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetroleclaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetroleclaim(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetroleclaims.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetroleclaimUpdated(item);
                this.context.Aspnetroleclaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroleclaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole");
                this.OnAfterAspnetroleclaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetroleclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetroleclaim item)
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

                this.OnAspnetroleclaimCreated(item);
                this.context.Aspnetroleclaims.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetroleclaims.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole");

                this.OnAfterAspnetroleclaimCreated(item);

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
