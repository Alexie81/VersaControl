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
    [Route("odata/versacontrol/Aspnetuserlogins")]
    public partial class AspnetuserloginsController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AspnetuserloginsController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> GetAspnetuserlogins()
        {
            var items = this.context.Aspnetuserlogins.AsQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>();
            this.OnAspnetuserloginsRead(ref items);

            return items;
        }

        partial void OnAspnetuserloginsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> items);

        partial void OnAspnetuserloginGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Aspnetuserlogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> GetAspnetuserlogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey)
        {
            var items = this.context.Aspnetuserlogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
            var result = SingleResult.Create(items);

            OnAspnetuserloginGet(ref result);

            return result;
        }
        partial void OnAspnetuserloginDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        [HttpDelete("/odata/versacontrol/Aspnetuserlogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        public IActionResult DeleteAspnetuserlogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Aspnetuserlogins
                    .Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAspnetuserloginDeleted(item);
                this.context.Aspnetuserlogins.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAspnetuserloginDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserloginUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        [HttpPut("/odata/versacontrol/Aspnetuserlogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAspnetuserlogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey, [FromBody]VersaControl.Server.Models.versacontrol.Aspnetuserlogin item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.LoginProvider != Uri.UnescapeDataString(keyLoginProvider) && item.ProviderKey != Uri.UnescapeDataString(keyProviderKey)))
                {
                    return BadRequest();
                }
                this.OnAspnetuserloginUpdated(item);
                this.context.Aspnetuserlogins.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserlogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetuserloginUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Aspnetuserlogins(LoginProvider={keyLoginProvider},ProviderKey={keyProviderKey})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAspnetuserlogin([FromODataUri] string keyLoginProvider, [FromODataUri] string keyProviderKey, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Aspnetuserlogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAspnetuserloginUpdated(item);
                this.context.Aspnetuserlogins.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserlogins.Where(i => i.LoginProvider == Uri.UnescapeDataString(keyLoginProvider) && i.ProviderKey == Uri.UnescapeDataString(keyProviderKey));
                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");
                this.OnAfterAspnetuserloginUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAspnetuserloginCreated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginCreated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Aspnetuserlogin item)
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

                this.OnAspnetuserloginCreated(item);
                this.context.Aspnetuserlogins.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Aspnetuserlogins.Where(i => i.LoginProvider == item.LoginProvider && i.ProviderKey == item.ProviderKey);

                Request.QueryString = Request.QueryString.Add("$expand", "Aspnetuser");

                this.OnAfterAspnetuserloginCreated(item);

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
