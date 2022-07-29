﻿using B_Riley.BankingApp.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Riley.BankingApp.Data.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(BankingAppContext context)
        {
            this.Context = context;
        }

        protected BankingAppContext Context { get; set; }

        public IQueryable<T> GetQueryable() => Context.Set<T>().AsQueryable();

        public void SetAsUpdate(T entity) => SetEntityState(entity, EntityState.Modified);

        public void SetValues(T source, T target) => Context.Entry<T>(source).CurrentValues.SetValues(target);

        public void SetEntityState(T entity, EntityState state) => Context.Entry<T>(entity).State = state;


        public void SaveChanges() => Context.SaveChanges();

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();


        public virtual async Task<T?> FindAsync(int id) => await Context.Set<T>().FindAsync(id);

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            SaveChanges();
        }
    }
}