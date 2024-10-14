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
    [Route("odata/versacontrol/Aspnetuserclaims")]
    public partial class AspnetuserclaimsController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetuserclaimsController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> GetAspnetuserclaims()
        {
            var items = this.context.Aspnetuserclaims.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>();
            this.OnAspnetuserclaimsRead(ref items);

            return items;
        }

        partial void OnAspnetuserclaimsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> items);

        partial void OnAspnetuserclaimGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetuserclaims(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> GetAspnetuserclaim(int key)
        {
            var items = this.context.Aspnetuserclaims.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAspnetuserclaimGet(ref result);

            return result;
        }
        partial void OnAspnetuserclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        [HttpDelete("/odata/versacontrol/Aspnetuserclaims(Id={Id})")]
        public IActionResult DeleteAspnetuserclaim(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetuserclaims
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetuserclaimDeleted(item);
                this.context.Aspnetuserclaims.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetuserclaimDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        [HttpPut("/odata/versacontrol/Aspnetuserclaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetuserclaim(int key, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetuserclaim item)
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
                this.OnAspnetuserclaimUpdated(item);
                this.context.Aspnetuserclaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserclaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetuserclaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetuserclaims(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetuserclaim(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetuserclaims.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetuserclaimUpdated(item);
                this.context.Aspnetuserclaims.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserclaims.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetuserclaimUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetuserclaim item)
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

                this.OnAspnetuserclaimCreated(item);
                this.context.Aspnetuserclaims.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserclaims.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");

                this.OnAfterAspnetuserclaimCreated(item);

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
