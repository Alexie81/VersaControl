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
    [Route("odata/versacontrol/Efmigrationshistories")]
    public partial class EfmigrationshistoriesController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public EfmigrationshistoriesController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Efmigrationshistory> GetEfmigrationshistories()
        {
            var items = this.context.Efmigrationshistories.AsQueryable<VersaControl.Server.Models.versacontrol.Efmigrationshistory>();
            this.OnEfmigrationshistoriesRead(ref items);

            return items;
        }

        partial void OnEfmigrationshistoriesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Efmigrationshistory> items);

        partial void OnEfmigrationshistoryGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Efmigrationshistory> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Efmigrationshistories(MigrationId={MigrationId})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Efmigrationshistory> GetEfmigrationshistory(string key)
        {
            var items = this.context.Efmigrationshistories.Where(i => i.MigrationId == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnEfmigrationshistoryGet(ref result);

            return result;
        }
        partial void OnEfmigrationshistoryDeleted(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryDeleted(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        [HttpDelete("/odata/versacontrol/Efmigrationshistories(MigrationId={MigrationId})")]
        public IActionResult DeleteEfmigrationshistory(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Efmigrationshistories
                    .Where(i => i.MigrationId == Uri.UnescapeDataString(key))
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnEfmigrationshistoryDeleted(item);
                this.context.Efmigrationshistories.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEfmigrationshistoryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEfmigrationshistoryUpdated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryUpdated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        [HttpPut("/odata/versacontrol/Efmigrationshistories(MigrationId={MigrationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEfmigrationshistory(string key, [FromBody]VersaControl.Server.Models.versacontrol.Efmigrationshistory item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.MigrationId != Uri.UnescapeDataString(key)))
                {
                    return BadRequest();
                }
                this.OnEfmigrationshistoryUpdated(item);
                this.context.Efmigrationshistories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Efmigrationshistories.Where(i => i.MigrationId == Uri.UnescapeDataString(key));
                
                this.OnAfterEfmigrationshistoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Efmigrationshistories(MigrationId={MigrationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEfmigrationshistory(string key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Efmigrationshistory> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Efmigrationshistories.Where(i => i.MigrationId == Uri.UnescapeDataString(key)).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnEfmigrationshistoryUpdated(item);
                this.context.Efmigrationshistories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Efmigrationshistories.Where(i => i.MigrationId == Uri.UnescapeDataString(key));
                
                this.OnAfterEfmigrationshistoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEfmigrationshistoryCreated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryCreated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Efmigrationshistory item)
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

                this.OnEfmigrationshistoryCreated(item);
                this.context.Efmigrationshistories.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Efmigrationshistories.Where(i => i.MigrationId == item.MigrationId);

                

                this.OnAfterEfmigrationshistoryCreated(item);

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
