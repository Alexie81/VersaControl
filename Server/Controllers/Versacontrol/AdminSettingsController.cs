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
    [Route("odata/versacontrol/AdminSettings")]
    public partial class AdminSettingsController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public AdminSettingsController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.AdminSetting> GetAdminSettings()
        {
            var items = this.context.AdminSettings.AsQueryable<VersaControl.Server.Models.versacontrol.AdminSetting>();
            this.OnAdminSettingsRead(ref items);

            return items;
        }

        partial void OnAdminSettingsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.AdminSetting> items);

        partial void OnAdminSettingGet(ref SingleResult<VersaControl.Server.Models.versacontrol.AdminSetting> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/AdminSettings(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.AdminSetting> GetAdminSetting(int key)
        {
            var items = this.context.AdminSettings.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnAdminSettingGet(ref result);

            return result;
        }
        partial void OnAdminSettingDeleted(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingDeleted(VersaControl.Server.Models.versacontrol.AdminSetting item);

        [HttpDelete("/odata/versacontrol/AdminSettings(Id={Id})")]
        public IActionResult DeleteAdminSetting(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AdminSettings
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAdminSettingDeleted(item);
                this.context.AdminSettings.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAdminSettingDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdminSettingUpdated(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingUpdated(VersaControl.Server.Models.versacontrol.AdminSetting item);

        [HttpPut("/odata/versacontrol/AdminSettings(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAdminSetting(int key, [FromBody]VersaControl.Server.Models.versacontrol.AdminSetting item)
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
                this.OnAdminSettingUpdated(item);
                this.context.AdminSettings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdminSettings.Where(i => i.Id == key);
                
                this.OnAfterAdminSettingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/AdminSettings(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAdminSetting(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.AdminSetting> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AdminSettings.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAdminSettingUpdated(item);
                this.context.AdminSettings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdminSettings.Where(i => i.Id == key);
                
                this.OnAfterAdminSettingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAdminSettingCreated(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingCreated(VersaControl.Server.Models.versacontrol.AdminSetting item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.AdminSetting item)
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

                this.OnAdminSettingCreated(item);
                this.context.AdminSettings.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AdminSettings.Where(i => i.Id == item.Id);

                

                this.OnAfterAdminSettingCreated(item);

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
