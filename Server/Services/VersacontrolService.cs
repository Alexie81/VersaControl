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


        public async Task ExportEfmigrationshistoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/efmigrationshistories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/efmigrationshistories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEfmigrationshistoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/efmigrationshistories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/efmigrationshistories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEfmigrationshistoriesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Efmigrationshistory> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Efmigrationshistory>> GetEfmigrationshistories(Query query = null)
        {
            var items = Context.Efmigrationshistories.AsQueryable();


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

            OnEfmigrationshistoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEfmigrationshistoryGet(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnGetEfmigrationshistoryByMigrationId(ref IQueryable<VersaControl.Server.Models.versacontrol.Efmigrationshistory> items);


        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> GetEfmigrationshistoryByMigrationId(string migrationid)
        {
            var items = Context.Efmigrationshistories
                              .AsNoTracking()
                              .Where(i => i.MigrationId == migrationid);

 
            OnGetEfmigrationshistoryByMigrationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEfmigrationshistoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEfmigrationshistoryCreated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryCreated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> CreateEfmigrationshistory(VersaControl.Server.Models.versacontrol.Efmigrationshistory efmigrationshistory)
        {
            OnEfmigrationshistoryCreated(efmigrationshistory);

            var existingItem = Context.Efmigrationshistories
                              .Where(i => i.MigrationId == efmigrationshistory.MigrationId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Efmigrationshistories.Add(efmigrationshistory);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(efmigrationshistory).State = EntityState.Detached;
                throw;
            }

            OnAfterEfmigrationshistoryCreated(efmigrationshistory);

            return efmigrationshistory;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> CancelEfmigrationshistoryChanges(VersaControl.Server.Models.versacontrol.Efmigrationshistory item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEfmigrationshistoryUpdated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryUpdated(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> UpdateEfmigrationshistory(string migrationid, VersaControl.Server.Models.versacontrol.Efmigrationshistory efmigrationshistory)
        {
            OnEfmigrationshistoryUpdated(efmigrationshistory);

            var itemToUpdate = Context.Efmigrationshistories
                              .Where(i => i.MigrationId == efmigrationshistory.MigrationId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(efmigrationshistory);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEfmigrationshistoryUpdated(efmigrationshistory);

            return efmigrationshistory;
        }

        partial void OnEfmigrationshistoryDeleted(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);
        partial void OnAfterEfmigrationshistoryDeleted(VersaControl.Server.Models.versacontrol.Efmigrationshistory item);

        public async Task<VersaControl.Server.Models.versacontrol.Efmigrationshistory> DeleteEfmigrationshistory(string migrationid)
        {
            var itemToDelete = Context.Efmigrationshistories
                              .Where(i => i.MigrationId == migrationid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEfmigrationshistoryDeleted(itemToDelete);


            Context.Efmigrationshistories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEfmigrationshistoryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAdminSettingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/adminsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/adminsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAdminSettingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/adminsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/adminsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAdminSettingsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.AdminSetting> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.AdminSetting>> GetAdminSettings(Query query = null)
        {
            var items = Context.AdminSettings.AsQueryable();


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

            OnAdminSettingsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAdminSettingGet(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnGetAdminSettingById(ref IQueryable<VersaControl.Server.Models.versacontrol.AdminSetting> items);


        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> GetAdminSettingById(int id)
        {
            var items = Context.AdminSettings
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAdminSettingById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAdminSettingGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAdminSettingCreated(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingCreated(VersaControl.Server.Models.versacontrol.AdminSetting item);

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> CreateAdminSetting(VersaControl.Server.Models.versacontrol.AdminSetting adminsetting)
        {
            OnAdminSettingCreated(adminsetting);

            var existingItem = Context.AdminSettings
                              .Where(i => i.Id == adminsetting.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AdminSettings.Add(adminsetting);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(adminsetting).State = EntityState.Detached;
                throw;
            }

            OnAfterAdminSettingCreated(adminsetting);

            return adminsetting;
        }

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> CancelAdminSettingChanges(VersaControl.Server.Models.versacontrol.AdminSetting item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAdminSettingUpdated(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingUpdated(VersaControl.Server.Models.versacontrol.AdminSetting item);

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> UpdateAdminSetting(int id, VersaControl.Server.Models.versacontrol.AdminSetting adminsetting)
        {
            OnAdminSettingUpdated(adminsetting);

            var itemToUpdate = Context.AdminSettings
                              .Where(i => i.Id == adminsetting.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(adminsetting);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAdminSettingUpdated(adminsetting);

            return adminsetting;
        }

        partial void OnAdminSettingDeleted(VersaControl.Server.Models.versacontrol.AdminSetting item);
        partial void OnAfterAdminSettingDeleted(VersaControl.Server.Models.versacontrol.AdminSetting item);

        public async Task<VersaControl.Server.Models.versacontrol.AdminSetting> DeleteAdminSetting(int id)
        {
            var itemToDelete = Context.AdminSettings
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAdminSettingDeleted(itemToDelete);


            Context.AdminSettings.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAdminSettingDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAnexasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/anexas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/anexas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAnexasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/anexas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/anexas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAnexasRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Anexa> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Anexa>> GetAnexas(Query query = null)
        {
            var items = Context.Anexas.AsQueryable();

            items = items.Include(i => i.Contracte);
            items = items.Include(i => i.Monede);

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

            OnAnexasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAnexaGet(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnGetAnexaById(ref IQueryable<VersaControl.Server.Models.versacontrol.Anexa> items);


        public async Task<VersaControl.Server.Models.versacontrol.Anexa> GetAnexaById(int id)
        {
            var items = Context.Anexas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Contracte);
            items = items.Include(i => i.Monede);
 
            OnGetAnexaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAnexaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAnexaCreated(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaCreated(VersaControl.Server.Models.versacontrol.Anexa item);

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> CreateAnexa(VersaControl.Server.Models.versacontrol.Anexa anexa)
        {
            OnAnexaCreated(anexa);

            var existingItem = Context.Anexas
                              .Where(i => i.Id == anexa.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Anexas.Add(anexa);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(anexa).State = EntityState.Detached;
                throw;
            }

            OnAfterAnexaCreated(anexa);

            return anexa;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> CancelAnexaChanges(VersaControl.Server.Models.versacontrol.Anexa item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAnexaUpdated(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaUpdated(VersaControl.Server.Models.versacontrol.Anexa item);

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> UpdateAnexa(int id, VersaControl.Server.Models.versacontrol.Anexa anexa)
        {
            OnAnexaUpdated(anexa);

            var itemToUpdate = Context.Anexas
                              .Where(i => i.Id == anexa.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(anexa);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAnexaUpdated(anexa);

            return anexa;
        }

        partial void OnAnexaDeleted(VersaControl.Server.Models.versacontrol.Anexa item);
        partial void OnAfterAnexaDeleted(VersaControl.Server.Models.versacontrol.Anexa item);

        public async Task<VersaControl.Server.Models.versacontrol.Anexa> DeleteAnexa(int id)
        {
            var itemToDelete = Context.Anexas
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAnexaDeleted(itemToDelete);


            Context.Anexas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAnexaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetroleclaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroleclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetroleclaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroleclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetroleclaimsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim>> GetAspnetroleclaims(Query query = null)
        {
            var items = Context.Aspnetroleclaims.AsQueryable();

            items = items.Include(i => i.Aspnetrole);

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

            OnAspnetroleclaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetroleclaimGet(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnGetAspnetroleclaimById(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> GetAspnetroleclaimById(int id)
        {
            var items = Context.Aspnetroleclaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Aspnetrole);
 
            OnGetAspnetroleclaimById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetroleclaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetroleclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> CreateAspnetroleclaim(VersaControl.Server.Models.versacontrol.Aspnetroleclaim aspnetroleclaim)
        {
            OnAspnetroleclaimCreated(aspnetroleclaim);

            var existingItem = Context.Aspnetroleclaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetroleclaims.Add(aspnetroleclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetroleclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetroleclaimCreated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> CancelAspnetroleclaimChanges(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetroleclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> UpdateAspnetroleclaim(int id, VersaControl.Server.Models.versacontrol.Aspnetroleclaim aspnetroleclaim)
        {
            OnAspnetroleclaimUpdated(aspnetroleclaim);

            var itemToUpdate = Context.Aspnetroleclaims
                              .Where(i => i.Id == aspnetroleclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetroleclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetroleclaimUpdated(aspnetroleclaim);

            return aspnetroleclaim;
        }

        partial void OnAspnetroleclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);
        partial void OnAfterAspnetroleclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetroleclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetroleclaim> DeleteAspnetroleclaim(int id)
        {
            var itemToDelete = Context.Aspnetroleclaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetroleclaimDeleted(itemToDelete);


            Context.Aspnetroleclaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetroleclaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetrolesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetrole> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetrole>> GetAspnetroles(Query query = null)
        {
            var items = Context.Aspnetroles.AsQueryable();


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

            OnAspnetrolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetroleGet(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnGetAspnetroleById(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetrole> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> GetAspnetroleById(string id)
        {
            var items = Context.Aspnetroles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAspnetroleById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetroleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetroleCreated(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleCreated(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> CreateAspnetrole(VersaControl.Server.Models.versacontrol.Aspnetrole aspnetrole)
        {
            OnAspnetroleCreated(aspnetrole);

            var existingItem = Context.Aspnetroles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetroles.Add(aspnetrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetroleCreated(aspnetrole);

            return aspnetrole;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> CancelAspnetroleChanges(VersaControl.Server.Models.versacontrol.Aspnetrole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> UpdateAspnetrole(string id, VersaControl.Server.Models.versacontrol.Aspnetrole aspnetrole)
        {
            OnAspnetroleUpdated(aspnetrole);

            var itemToUpdate = Context.Aspnetroles
                              .Where(i => i.Id == aspnetrole.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetroleUpdated(aspnetrole);

            return aspnetrole;
        }

        partial void OnAspnetroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetrole item);
        partial void OnAfterAspnetroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetrole> DeleteAspnetrole(string id)
        {
            var itemToDelete = Context.Aspnetroles
                              .Where(i => i.Id == id)
                              .Include(i => i.Aspnetroleclaims)
                              .Include(i => i.Aspnetuserroles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetroleDeleted(itemToDelete);


            Context.Aspnetroles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetroleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetuserclaimsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserclaims/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetuserclaimsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserclaims/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetuserclaimsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim>> GetAspnetuserclaims(Query query = null)
        {
            var items = Context.Aspnetuserclaims.AsQueryable();

            items = items.Include(i => i.Aspnetuser);

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

            OnAspnetuserclaimsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetuserclaimGet(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnGetAspnetuserclaimById(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> GetAspnetuserclaimById(int id)
        {
            var items = Context.Aspnetuserclaims
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Aspnetuser);
 
            OnGetAspnetuserclaimById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetuserclaimGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetuserclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimCreated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> CreateAspnetuserclaim(VersaControl.Server.Models.versacontrol.Aspnetuserclaim aspnetuserclaim)
        {
            OnAspnetuserclaimCreated(aspnetuserclaim);

            var existingItem = Context.Aspnetuserclaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetuserclaims.Add(aspnetuserclaim);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserclaim).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetuserclaimCreated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> CancelAspnetuserclaimChanges(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetuserclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> UpdateAspnetuserclaim(int id, VersaControl.Server.Models.versacontrol.Aspnetuserclaim aspnetuserclaim)
        {
            OnAspnetuserclaimUpdated(aspnetuserclaim);

            var itemToUpdate = Context.Aspnetuserclaims
                              .Where(i => i.Id == aspnetuserclaim.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserclaim);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetuserclaimUpdated(aspnetuserclaim);

            return aspnetuserclaim;
        }

        partial void OnAspnetuserclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);
        partial void OnAfterAspnetuserclaimDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserclaim item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserclaim> DeleteAspnetuserclaim(int id)
        {
            var itemToDelete = Context.Aspnetuserclaims
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetuserclaimDeleted(itemToDelete);


            Context.Aspnetuserclaims.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetuserclaimDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetuserloginsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserlogins/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetuserloginsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserlogins/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetuserloginsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin>> GetAspnetuserlogins(Query query = null)
        {
            var items = Context.Aspnetuserlogins.AsQueryable();

            items = items.Include(i => i.Aspnetuser);

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

            OnAspnetuserloginsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetuserloginGet(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnGetAspnetuserloginByLoginProviderAndProviderKey(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> GetAspnetuserloginByLoginProviderAndProviderKey(string loginprovider, string providerkey)
        {
            var items = Context.Aspnetuserlogins
                              .AsNoTracking()
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey);

            items = items.Include(i => i.Aspnetuser);
 
            OnGetAspnetuserloginByLoginProviderAndProviderKey(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetuserloginGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetuserloginCreated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginCreated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> CreateAspnetuserlogin(VersaControl.Server.Models.versacontrol.Aspnetuserlogin aspnetuserlogin)
        {
            OnAspnetuserloginCreated(aspnetuserlogin);

            var existingItem = Context.Aspnetuserlogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetuserlogins.Add(aspnetuserlogin);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserlogin).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetuserloginCreated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> CancelAspnetuserloginChanges(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetuserloginUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> UpdateAspnetuserlogin(string loginprovider, string providerkey, VersaControl.Server.Models.versacontrol.Aspnetuserlogin aspnetuserlogin)
        {
            OnAspnetuserloginUpdated(aspnetuserlogin);

            var itemToUpdate = Context.Aspnetuserlogins
                              .Where(i => i.LoginProvider == aspnetuserlogin.LoginProvider && i.ProviderKey == aspnetuserlogin.ProviderKey)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserlogin);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetuserloginUpdated(aspnetuserlogin);

            return aspnetuserlogin;
        }

        partial void OnAspnetuserloginDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);
        partial void OnAfterAspnetuserloginDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserlogin item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserlogin> DeleteAspnetuserlogin(string loginprovider, string providerkey)
        {
            var itemToDelete = Context.Aspnetuserlogins
                              .Where(i => i.LoginProvider == loginprovider && i.ProviderKey == providerkey)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetuserloginDeleted(itemToDelete);


            Context.Aspnetuserlogins.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetuserloginDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetuserrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetuserrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetuserroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetuserrolesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserrole> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserrole>> GetAspnetuserroles(Query query = null)
        {
            var items = Context.Aspnetuserroles.AsQueryable();

            items = items.Include(i => i.Aspnetrole);
            items = items.Include(i => i.Aspnetuser);

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

            OnAspnetuserrolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetuserroleGet(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnGetAspnetuserroleByUserIdAndRoleId(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuserrole> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> GetAspnetuserroleByUserIdAndRoleId(string userid, string roleid)
        {
            var items = Context.Aspnetuserroles
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.RoleId == roleid);

            items = items.Include(i => i.Aspnetrole);
            items = items.Include(i => i.Aspnetuser);
 
            OnGetAspnetuserroleByUserIdAndRoleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetuserroleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetuserroleCreated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleCreated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> CreateAspnetuserrole(VersaControl.Server.Models.versacontrol.Aspnetuserrole aspnetuserrole)
        {
            OnAspnetuserroleCreated(aspnetuserrole);

            var existingItem = Context.Aspnetuserroles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetuserroles.Add(aspnetuserrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuserrole).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetuserroleCreated(aspnetuserrole);

            return aspnetuserrole;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> CancelAspnetuserroleChanges(VersaControl.Server.Models.versacontrol.Aspnetuserrole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetuserroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleUpdated(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> UpdateAspnetuserrole(string userid, string roleid, VersaControl.Server.Models.versacontrol.Aspnetuserrole aspnetuserrole)
        {
            OnAspnetuserroleUpdated(aspnetuserrole);

            var itemToUpdate = Context.Aspnetuserroles
                              .Where(i => i.UserId == aspnetuserrole.UserId && i.RoleId == aspnetuserrole.RoleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuserrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetuserroleUpdated(aspnetuserrole);

            return aspnetuserrole;
        }

        partial void OnAspnetuserroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);
        partial void OnAfterAspnetuserroleDeleted(VersaControl.Server.Models.versacontrol.Aspnetuserrole item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuserrole> DeleteAspnetuserrole(string userid, string roleid)
        {
            var itemToDelete = Context.Aspnetuserroles
                              .Where(i => i.UserId == userid && i.RoleId == roleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetuserroleDeleted(itemToDelete);


            Context.Aspnetuserroles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetuserroleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetusersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetusersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetusersRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuser> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuser>> GetAspnetusers(Query query = null)
        {
            var items = Context.Aspnetusers.AsQueryable();


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

            OnAspnetusersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetuserGet(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnGetAspnetuserById(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetuser> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> GetAspnetuserById(string id)
        {
            var items = Context.Aspnetusers
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAspnetuserById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetuserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetuserCreated(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserCreated(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> CreateAspnetuser(VersaControl.Server.Models.versacontrol.Aspnetuser aspnetuser)
        {
            OnAspnetuserCreated(aspnetuser);

            var existingItem = Context.Aspnetusers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetusers.Add(aspnetuser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetuser).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetuserCreated(aspnetuser);

            return aspnetuser;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> CancelAspnetuserChanges(VersaControl.Server.Models.versacontrol.Aspnetuser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetuserUpdated(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserUpdated(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> UpdateAspnetuser(string id, VersaControl.Server.Models.versacontrol.Aspnetuser aspnetuser)
        {
            OnAspnetuserUpdated(aspnetuser);

            var itemToUpdate = Context.Aspnetusers
                              .Where(i => i.Id == aspnetuser.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetuser);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetuserUpdated(aspnetuser);

            return aspnetuser;
        }

        partial void OnAspnetuserDeleted(VersaControl.Server.Models.versacontrol.Aspnetuser item);
        partial void OnAfterAspnetuserDeleted(VersaControl.Server.Models.versacontrol.Aspnetuser item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetuser> DeleteAspnetuser(string id)
        {
            var itemToDelete = Context.Aspnetusers
                              .Where(i => i.Id == id)
                              .Include(i => i.Aspnetuserclaims)
                              .Include(i => i.Aspnetuserlogins)
                              .Include(i => i.Aspnetuserroles)
                              .Include(i => i.Aspnetusertokens)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetuserDeleted(itemToDelete);


            Context.Aspnetusers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetuserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAspnetusertokensToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusertokens/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAspnetusertokensToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/aspnetusertokens/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAspnetusertokensRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetusertoken> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Aspnetusertoken>> GetAspnetusertokens(Query query = null)
        {
            var items = Context.Aspnetusertokens.AsQueryable();

            items = items.Include(i => i.Aspnetuser);

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

            OnAspnetusertokensRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAspnetusertokenGet(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnGetAspnetusertokenByUserIdAndLoginProviderAndName(ref IQueryable<VersaControl.Server.Models.versacontrol.Aspnetusertoken> items);


        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> GetAspnetusertokenByUserIdAndLoginProviderAndName(string userid, string loginprovider, string name)
        {
            var items = Context.Aspnetusertokens
                              .AsNoTracking()
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name);

            items = items.Include(i => i.Aspnetuser);
 
            OnGetAspnetusertokenByUserIdAndLoginProviderAndName(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAspnetusertokenGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAspnetusertokenCreated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenCreated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> CreateAspnetusertoken(VersaControl.Server.Models.versacontrol.Aspnetusertoken aspnetusertoken)
        {
            OnAspnetusertokenCreated(aspnetusertoken);

            var existingItem = Context.Aspnetusertokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Aspnetusertokens.Add(aspnetusertoken);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aspnetusertoken).State = EntityState.Detached;
                throw;
            }

            OnAfterAspnetusertokenCreated(aspnetusertoken);

            return aspnetusertoken;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> CancelAspnetusertokenChanges(VersaControl.Server.Models.versacontrol.Aspnetusertoken item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAspnetusertokenUpdated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenUpdated(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> UpdateAspnetusertoken(string userid, string loginprovider, string name, VersaControl.Server.Models.versacontrol.Aspnetusertoken aspnetusertoken)
        {
            OnAspnetusertokenUpdated(aspnetusertoken);

            var itemToUpdate = Context.Aspnetusertokens
                              .Where(i => i.UserId == aspnetusertoken.UserId && i.LoginProvider == aspnetusertoken.LoginProvider && i.Name == aspnetusertoken.Name)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aspnetusertoken);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAspnetusertokenUpdated(aspnetusertoken);

            return aspnetusertoken;
        }

        partial void OnAspnetusertokenDeleted(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);
        partial void OnAfterAspnetusertokenDeleted(VersaControl.Server.Models.versacontrol.Aspnetusertoken item);

        public async Task<VersaControl.Server.Models.versacontrol.Aspnetusertoken> DeleteAspnetusertoken(string userid, string loginprovider, string name)
        {
            var itemToDelete = Context.Aspnetusertokens
                              .Where(i => i.UserId == userid && i.LoginProvider == loginprovider && i.Name == name)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAspnetusertokenDeleted(itemToDelete);


            Context.Aspnetusertokens.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAspnetusertokenDeleted(itemToDelete);

            return itemToDelete;
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

            items = items.Include(i => i.Roluri);

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

            items = items.Include(i => i.Roluri);
 
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
                              .Include(i => i.Contractes)
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
    
        public async Task ExportContractesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContractesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/contractes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/contractes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContractesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Contracte> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Contracte>> GetContractes(Query query = null)
        {
            var items = Context.Contractes.AsQueryable();

            items = items.Include(i => i.Beneficiari);
            items = items.Include(i => i.Contractori);
            items = items.Include(i => i.Monede);
            items = items.Include(i => i.TipuriContract);

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

            OnContractesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContracteGet(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnGetContracteById(ref IQueryable<VersaControl.Server.Models.versacontrol.Contracte> items);


        public async Task<VersaControl.Server.Models.versacontrol.Contracte> GetContracteById(int id)
        {
            var items = Context.Contractes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Beneficiari);
            items = items.Include(i => i.Contractori);
            items = items.Include(i => i.Monede);
            items = items.Include(i => i.TipuriContract);
 
            OnGetContracteById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContracteGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContracteCreated(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteCreated(VersaControl.Server.Models.versacontrol.Contracte item);

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> CreateContracte(VersaControl.Server.Models.versacontrol.Contracte contracte)
        {
            OnContracteCreated(contracte);

            var existingItem = Context.Contractes
                              .Where(i => i.Id == contracte.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contractes.Add(contracte);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contracte).State = EntityState.Detached;
                throw;
            }

            OnAfterContracteCreated(contracte);

            return contracte;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> CancelContracteChanges(VersaControl.Server.Models.versacontrol.Contracte item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContracteUpdated(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteUpdated(VersaControl.Server.Models.versacontrol.Contracte item);

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> UpdateContracte(int id, VersaControl.Server.Models.versacontrol.Contracte contracte)
        {
            OnContracteUpdated(contracte);

            var itemToUpdate = Context.Contractes
                              .Where(i => i.Id == contracte.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contracte);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContracteUpdated(contracte);

            return contracte;
        }

        partial void OnContracteDeleted(VersaControl.Server.Models.versacontrol.Contracte item);
        partial void OnAfterContracteDeleted(VersaControl.Server.Models.versacontrol.Contracte item);

        public async Task<VersaControl.Server.Models.versacontrol.Contracte> DeleteContracte(int id)
        {
            var itemToDelete = Context.Contractes
                              .Where(i => i.Id == id)
                              .Include(i => i.Anexas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContracteDeleted(itemToDelete);


            Context.Contractes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContracteDeleted(itemToDelete);

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

            items = items.Include(i => i.Roluri);

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

            items = items.Include(i => i.Roluri);
 
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
                              .Include(i => i.Contractes)
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
    
        public async Task ExportMonedesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/monedes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/monedes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMonedesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/monedes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/monedes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMonedesRead(ref IQueryable<VersaControl.Server.Models.versacontrol.Monede> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.Monede>> GetMonedes(Query query = null)
        {
            var items = Context.Monedes.AsQueryable();


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

            OnMonedesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMonedeGet(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnGetMonedeById(ref IQueryable<VersaControl.Server.Models.versacontrol.Monede> items);


        public async Task<VersaControl.Server.Models.versacontrol.Monede> GetMonedeById(int id)
        {
            var items = Context.Monedes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetMonedeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMonedeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMonedeCreated(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeCreated(VersaControl.Server.Models.versacontrol.Monede item);

        public async Task<VersaControl.Server.Models.versacontrol.Monede> CreateMonede(VersaControl.Server.Models.versacontrol.Monede monede)
        {
            OnMonedeCreated(monede);

            var existingItem = Context.Monedes
                              .Where(i => i.Id == monede.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Monedes.Add(monede);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(monede).State = EntityState.Detached;
                throw;
            }

            OnAfterMonedeCreated(monede);

            return monede;
        }

        public async Task<VersaControl.Server.Models.versacontrol.Monede> CancelMonedeChanges(VersaControl.Server.Models.versacontrol.Monede item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMonedeUpdated(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeUpdated(VersaControl.Server.Models.versacontrol.Monede item);

        public async Task<VersaControl.Server.Models.versacontrol.Monede> UpdateMonede(int id, VersaControl.Server.Models.versacontrol.Monede monede)
        {
            OnMonedeUpdated(monede);

            var itemToUpdate = Context.Monedes
                              .Where(i => i.Id == monede.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(monede);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMonedeUpdated(monede);

            return monede;
        }

        partial void OnMonedeDeleted(VersaControl.Server.Models.versacontrol.Monede item);
        partial void OnAfterMonedeDeleted(VersaControl.Server.Models.versacontrol.Monede item);

        public async Task<VersaControl.Server.Models.versacontrol.Monede> DeleteMonede(int id)
        {
            var itemToDelete = Context.Monedes
                              .Where(i => i.Id == id)
                              .Include(i => i.Anexas)
                              .Include(i => i.Contractes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMonedeDeleted(itemToDelete);


            Context.Monedes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMonedeDeleted(itemToDelete);

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
                              .Include(i => i.Beneficiaris)
                              .Include(i => i.Contractoris)
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
    
        public async Task ExportTipuriContractsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/tipuricontracts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/tipuricontracts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTipuriContractsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/versacontrol/tipuricontracts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/versacontrol/tipuricontracts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTipuriContractsRead(ref IQueryable<VersaControl.Server.Models.versacontrol.TipuriContract> items);

        public async Task<IQueryable<VersaControl.Server.Models.versacontrol.TipuriContract>> GetTipuriContracts(Query query = null)
        {
            var items = Context.TipuriContracts.AsQueryable();


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

            OnTipuriContractsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTipuriContractGet(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnGetTipuriContractById(ref IQueryable<VersaControl.Server.Models.versacontrol.TipuriContract> items);


        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> GetTipuriContractById(int id)
        {
            var items = Context.TipuriContracts
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetTipuriContractById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTipuriContractGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTipuriContractCreated(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractCreated(VersaControl.Server.Models.versacontrol.TipuriContract item);

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> CreateTipuriContract(VersaControl.Server.Models.versacontrol.TipuriContract tipuricontract)
        {
            OnTipuriContractCreated(tipuricontract);

            var existingItem = Context.TipuriContracts
                              .Where(i => i.Id == tipuricontract.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TipuriContracts.Add(tipuricontract);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tipuricontract).State = EntityState.Detached;
                throw;
            }

            OnAfterTipuriContractCreated(tipuricontract);

            return tipuricontract;
        }

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> CancelTipuriContractChanges(VersaControl.Server.Models.versacontrol.TipuriContract item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTipuriContractUpdated(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractUpdated(VersaControl.Server.Models.versacontrol.TipuriContract item);

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> UpdateTipuriContract(int id, VersaControl.Server.Models.versacontrol.TipuriContract tipuricontract)
        {
            OnTipuriContractUpdated(tipuricontract);

            var itemToUpdate = Context.TipuriContracts
                              .Where(i => i.Id == tipuricontract.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tipuricontract);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTipuriContractUpdated(tipuricontract);

            return tipuricontract;
        }

        partial void OnTipuriContractDeleted(VersaControl.Server.Models.versacontrol.TipuriContract item);
        partial void OnAfterTipuriContractDeleted(VersaControl.Server.Models.versacontrol.TipuriContract item);

        public async Task<VersaControl.Server.Models.versacontrol.TipuriContract> DeleteTipuriContract(int id)
        {
            var itemToDelete = Context.TipuriContracts
                              .Where(i => i.Id == id)
                              .Include(i => i.Contractes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTipuriContractDeleted(itemToDelete);


            Context.TipuriContracts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTipuriContractDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}