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
    [Route("odata/versacontrol/Aspnetuserroles")]
    public partial class AspnetuserrolesController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetuserrolesController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetuserrole> GetAspnetuserroles()
        {
            var items = this.context.Aspnetuserroles.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserrole>();
            this.OnAspnetuserrolesRead(ref items);

            return items;
        }

        partial void OnAspnetuserrolesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserrole> items);

        partial void OnAspnetuserroleGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserrole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetuserroles(UserId={keyUserId},RoleId={keyRoleId})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserrole> GetAspnetuserrole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId)
        {
            var items = this.context.Aspnetuserroles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
            var result = SingleResult.Create(items);

            OnAspnetuserroleGet(ref result);

            return result;
        }
        partial void OnAspnetuserroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        [HttpDelete("/odata/versacontrol/Aspnetuserroles(UserId={keyUserId},RoleId={keyRoleId})")]
        public IActionResult DeleteAspnetuserrole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetuserroles
                    .Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetuserroleDeleted(item);
                this.context.Aspnetuserroles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetuserroleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        [HttpPut("/odata/versacontrol/Aspnetuserroles(UserId={keyUserId},RoleId={keyRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetuserrole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetuserrole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserId != Uri.UnescapeDataString(keyUserId) && item.RoleId != Uri.UnescapeDataString(keyRoleId)))
                {
                    return BadRequest();
                }
                this.OnAspnetuserroleUpdated(item);
                this.context.Aspnetuserroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserroles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole,Aspnetuser");
                this.OnAfterAspnetuserroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetuserroles(UserId={keyUserId},RoleId={keyRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetuserrole([FromODataUri] string keyUserId, [FromODataUri] string keyRoleId, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetuserrole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetuserroles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetuserroleUpdated(item);
                this.context.Aspnetuserroles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserroles.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.RoleId == Uri.UnescapeDataString(keyRoleId));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole,Aspnetuser");
                this.OnAfterAspnetuserroleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserroleCreated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleCreated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetuserrole item)
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

                this.OnAspnetuserroleCreated(item);
                this.context.Aspnetuserroles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserroles.Where(i => i.UserId == item.UserId && i.RoleId == item.RoleId);

                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetrole,Aspnetuser");

                this.OnAfterAspnetuserroleCreated(item);

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
