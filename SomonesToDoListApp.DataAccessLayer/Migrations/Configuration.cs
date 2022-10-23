﻿using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SomeonesToDoListApp.DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SomeonesToDoListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SomeonesToDoListContext someonesToDoListContext)
        {
            if (someonesToDoListContext.ToDos.Any())
                return;

            var toDo = new ToDo(Guid.NewGuid(), "Feed my dog", string.Empty, DateTime.UtcNow, Guid.NewGuid());
            someonesToDoListContext.ToDos.AddOrUpdate(toDo);
        }
    }
}