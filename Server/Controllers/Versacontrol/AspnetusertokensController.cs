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
    [Route("odata/versacontrol/Aspnetusertokens")]
    public partial class AspnetusertokensController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetusertokensController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetusertoken> GetAspnetusertokens()
        {
            var items = this.context.Aspnetusertokens.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetusertoken>();
            this.OnAspnetusertokensRead(ref items);

            return items;
        }

        partial void OnAspnetusertokensRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetusertoken> items);

        partial void OnAspnetusertokenGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetusertoken> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetusertokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetusertoken> GetAspnetusertoken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName)
        {
            var items = this.context.Aspnetusertokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
            var result = SingleResult.Create(items);

            OnAspnetusertokenGet(ref result);

            return result;
        }
        partial void OnAspnetusertokenDeleted(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenDeleted(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        [HttpDelete("/odata/versacontrol/Aspnetusertokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        public IActionResult DeleteAspnetusertoken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetusertokens
                    .Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetusertokenDeleted(item);
                this.context.Aspnetusertokens.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetusertokenDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetusertokenUpdated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenUpdated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        [HttpPut("/odata/versacontrol/Aspnetusertokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetusertoken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetusertoken item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserId != Uri.UnescapeDataString(keyUserId) && item.LoginProvider != Uri.UnescapeDataString(keyLoginProvider) && item.Name != Uri.UnescapeDataString(keyName)))
                {
                    return BadRequest();
                }
                this.OnAspnetusertokenUpdated(item);
                this.context.Aspnetusertokens.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusertokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetusertokenUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetusertokens(UserId={keyUserId},LoginProvider={keyLoginProvider},Name={keyName})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetusertoken([FromODataUri] string keyUserId, [FromODataUri] string keyLoginProvider, [FromODataUri] string keyName, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetusertoken> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetusertokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetusertokenUpdated(item);
                this.context.Aspnetusertokens.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusertokens.Where(i => i.UserId == Uri.UnescapeDataString(keyUserId) && i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.Name == Uri.UnescapeDataString(keyName));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetusertokenUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetusertokenCreated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenCreated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetusertoken item)
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

                this.OnAspnetusertokenCreated(item);
                this.context.Aspnetusertokens.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetusertokens.Where(i => i.UserId == item.UserId && i.LoginProvider == item.LoginProvider && i.Name == item.Name);

                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");

                this.OnAfterAspnetusertokenCreated(item);

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
