using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using VersaControl.Server.Data;

namespace VersaControl.Server
{
    public partial class versacontrolService
    {
        versacontrolContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly versacontrolContext context;
        private readonly NavigationManager navigationManager;

        public versacontrolService(versacontrolContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportBeneficiarisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/beneficiaris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/beneficiaris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBeneficiarisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/beneficiaris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/beneficiaris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBeneficiarisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Beneficiari> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Beneficiari>> GetBeneficiaris(Query query = null)
        {
            var items = Context.Beneficiaris.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBeneficiarisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBeneficiariGet(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnGetBeneficiariById(ref IQueryable<VersaControl.Server.Models.versacontrol.Beneficiari> items);


        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> GetBeneficiariById(int id)
        {
            var items = Context.Beneficiaris
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetBeneficiariById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBeneficiariGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBeneficiariCreated(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariCreated(VersaControl.Server.Models.versacontrol.Beneficiari item);

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> CreateBeneficiari(VersaControl.Server.Models.versacontrol.Beneficiari beneficiari)
        {
            OnBeneficiariCreated(beneficiari);

            var existingItem = Context.Beneficiaris
                              .Where(i => i.Id == beneficiari.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Beneficiaris.Add(beneficiari);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(beneficiari).State = EntityState.Detached;
                throw;
            }

            OnAfterBeneficiariCreated(beneficiari);

            return beneficiari;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> CancelBeneficiariChanges(VersaControl.Server.Models.versacontrol.Beneficiari item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBeneficiariUpdated(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariUpdated(VersaControl.Server.Models.versacontrol.Beneficiari item);

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> UpdateBeneficiari(int id, VersaControl.Server.Models.versacontrol.Beneficiari beneficiari)
        {
            OnBeneficiariUpdated(beneficiari);

            var itemToUpdate = Context.Beneficiaris
                              .Where(i => i.Id == beneficiari.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(beneficiari);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBeneficiariUpdated(beneficiari);

            return beneficiari;
        }

        partial void OnBeneficiariDeleted(VersaControl.Server.Models.versacontrol.Beneficiari item);
        partial void OnAfterBeneficiariDeleted(VersaControl.Server.Models.versacontrol.Beneficiari item);

        public async Task<VersaControl.Server.Models.versacontrol.Beneficiari> DeleteBeneficiari(int id)
        {
            var itemToDelete = Context.Beneficiaris
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBeneficiariDeleted(itemToDelete);


            Context.Beneficiaris.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBeneficiariDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContractorisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractoris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractoris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContractorisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractoris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractoris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContractorisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Contractori> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Contractori>> GetContractoris(Query query = null)
        {
            var items = Context.Contractoris.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContractorisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContractoriGet(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnGetContractoriById(ref IQueryable<VersaControl.Server.Models.versacontrol.Contractori> items);


        public async Task<VersaControl.Server.Models.versacontrol.Contractori> GetContractoriById(int id)
        {
            var items = Context.Contractoris
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetContractoriById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContractoriGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContractoriCreated(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriCreated(VersaControl.Server.Models.versacontrol.Contractori item);

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> CreateContractori(VersaControl.Server.Models.versacontrol.Contractori contractori)
        {
            OnContractoriCreated(contractori);

            var existingItem = Context.Contractoris
                              .Where(i => i.Id == contractori.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contractoris.Add(contractori);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contractori).State = EntityState.Detached;
                throw;
            }

            OnAfterContractoriCreated(contractori);

            return contractori;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> CancelContractoriChanges(VersaControl.Server.Models.versacontrol.Contractori item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContractoriUpdated(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriUpdated(VersaControl.Server.Models.versacontrol.Contractori item);

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> UpdateContractori(int id, VersaControl.Server.Models.versacontrol.Contractori contractori)
        {
            OnContractoriUpdated(contractori);

            var itemToUpdate = Context.Contractoris
                              .Where(i => i.Id == contractori.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contractori);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContractoriUpdated(contractori);

            return contractori;
        }

        partial void OnContractoriDeleted(VersaControl.Server.Models.versacontrol.Contractori item);
        partial void OnAfterContractoriDeleted(VersaControl.Server.Models.versacontrol.Contractori item);

        public async Task<VersaControl.Server.Models.versacontrol.Contractori> DeleteContractori(int id)
        {
            var itemToDelete = Context.Contractoris
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContractoriDeleted(itemToDelete);


            Context.Contractoris.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContractoriDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRolurisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/roluris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/roluris/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolurisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/roluris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/roluris/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolurisRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Roluri> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Roluri>> GetRoluris(Query query = null)
        {
            var items = Context.Roluris.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRolurisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoluriGet(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnGetRoluriById(ref IQueryable<VersaControl.Server.Models.versacontrol.Roluri> items);


        public async Task<VersaControl.Server.Models.versacontrol.Roluri> GetRoluriById(int id)
        {
            var items = Context.Roluris
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetRoluriById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRoluriGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoluriCreated(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriCreated(VersaControl.Server.Models.versacontrol.Roluri item);

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> CreateRoluri(VersaControl.Server.Models.versacontrol.Roluri roluri)
        {
            OnRoluriCreated(roluri);

            var existingItem = Context.Roluris
                              .Where(i => i.Id == roluri.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Roluris.Add(roluri);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(roluri).State = EntityState.Detached;
                throw;
            }

            OnAfterRoluriCreated(roluri);

            return roluri;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> CancelRoluriChanges(VersaControl.Server.Models.versacontrol.Roluri item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoluriUpdated(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriUpdated(VersaControl.Server.Models.versacontrol.Roluri item);

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> UpdateRoluri(int id, VersaControl.Server.Models.versacontrol.Roluri roluri)
        {
            OnRoluriUpdated(roluri);

            var itemToUpdate = Context.Roluris
                              .Where(i => i.Id == roluri.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(roluri);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoluriUpdated(roluri);

            return roluri;
        }

        partial void OnRoluriDeleted(VersaControl.Server.Models.versacontrol.Roluri item);
        partial void OnAfterRoluriDeleted(VersaControl.Server.Models.versacontrol.Roluri item);

        public async Task<VersaControl.Server.Models.versacontrol.Roluri> DeleteRoluri(int id)
        {
            var itemToDelete = Context.Roluris
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoluriDeleted(itemToDelete);


            Context.Roluris.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoluriDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}