using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbContext {
    public partial class AppDbContext : IdentityDbContext {
        public AppDbContext(DbContextOptions options):base(options){}
        /***
         * DB SETs here
         */
    }
}