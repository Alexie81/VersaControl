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
    [Route("odata/versacontrol/Beneficiaris")]
    public partial class BeneficiarisController : ODataController
    {
        private VersaControl.Server.Data.versacontrolContext context;

        public BeneficiarisController(VersaControl.Server.Data.versacontrolContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<VersaControl.Server.Models.versacontrol.Beneficiari> GetBeneficiaris()
        {
            var items = this.context.Beneficiaris.AsQueryable<VersaControl.Server.Models.versacontrol.Beneficiari>();
            this.OnBeneficiarisRead(ref items);

            return items;
        }

        partial void OnBeneficiarisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Beneficiari> items);

        partial void OnBeneficiariGet(ref SingleResult<VersaControl.Server.Models.versacontrol.Beneficiari> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/versacontrol/Beneficiaris(Id={Id})")]
        public SingleResult<VersaControl.Server.Models.versacontrol.Beneficiari> GetBeneficiari(int key)
        {
            var items = this.context.Beneficiaris.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnBeneficiariGet(ref result);

            return result;
        }
        partial void OnBeneficiariDeleted(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariDeleted(VersaControl.Server.Models.versacontrol.Beneficiari item);

        [HttpDelete("/odata/versacontrol/Beneficiaris(Id={Id})")]
        public IActionResult DeleteBeneficiari(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Beneficiaris
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnBeneficiariDeleted(item);
                this.context.Beneficiaris.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBeneficiariDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBeneficiariUpdated(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariUpdated(VersaControl.Server.Models.versacontrol.Beneficiari item);

        [HttpPut("/odata/versacontrol/Beneficiaris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBeneficiari(int key, [FromBody]VersaControl.Server.Models.versacontrol.Beneficiari item)
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
                this.OnBeneficiariUpdated(item);
                this.context.Beneficiaris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Beneficiaris.Where(i => i.Id == key);
                
                this.OnAfterBeneficiariUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/versacontrol/Beneficiaris(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBeneficiari(int key, [FromBody]Delta<VersaControl.Server.Models.versacontrol.Beneficiari> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Beneficiaris.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnBeneficiariUpdated(item);
                this.context.Beneficiaris.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Beneficiaris.Where(i => i.Id == key);
                
                this.OnAfterBeneficiariUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBeneficiariCreated(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariCreated(VersaControl.Server.Models.versacontrol.Beneficiari item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] VersaControl.Server.Models.versacontrol.Beneficiari item)
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

                this.OnBeneficiariCreated(item);
                this.context.Beneficiaris.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Beneficiaris.Where(i => i.Id == item.Id);

                

                this.OnAfterBeneficiariCreated(item);

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
