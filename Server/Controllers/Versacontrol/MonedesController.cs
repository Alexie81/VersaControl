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
    [Route("odata/versacontrol/Monedes")]
    public partial class MonedesController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public MonedesController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Monede> GetMonedes()
        {
            var items = this.context.Monedes.AsQueryable<VersaControl.Server.Models.versacontrol.Monede>();
            this.OnMonedesRead(ref items);

            return items;
        }

        partial void OnMonedesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Monede> items);

        partial void OnMonedeGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Monede> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Monedes(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Monede> GetMonede(int key)
        {
            var items = this.context.Monedes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnMonedeGet(ref result);

            return result;
        }
        partial void OnMonedeDeleted(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeDeleted(VersaControl.Server.Models.versacontrol.Monede item);

        [HttpDelete("/odata/versacontrol/Monedes(Id={Id})")]
        public IActionResult DeleteMonede(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Monedes
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnMonedeDeleted(item);
                this.context.Monedes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMonedeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMonedeUpdated(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeUpdated(VersaControl.Server.Models.versacontrol.Monede item);

        [HttpPut("/odata/versacontrol/Monedes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMonede(int key, [FromBody]VersaControl.Server.Models.versacontrol.Monede item)
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
                this.OnMonedeUpdated(item);
                this.context.Monedes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Monedes.Where(i => i.Id == key);
                
                this.OnAfterMonedeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Monedes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMonede(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Monede> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Monedes.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnMonedeUpdated(item);
                this.context.Monedes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Monedes.Where(i => i.Id == key);
                
                this.OnAfterMonedeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMonedeCreated(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeCreated(VersaControl.Server.Models.versacontrol.Monede item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Monede item)
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

                this.OnMonedeCreated(item);
                this.context.Monedes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Monedes.Where(i => i.Id == item.Id);

                

                this.OnAfterMonedeCreated(item);

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
