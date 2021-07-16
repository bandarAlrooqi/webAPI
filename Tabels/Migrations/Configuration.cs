
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Tabels.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Tabels.EmplyeePageEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}