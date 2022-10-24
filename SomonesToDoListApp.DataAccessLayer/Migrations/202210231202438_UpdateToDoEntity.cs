namespace SomeonesToDoListApp.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToDoEntity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ToDo");
            AddColumn("dbo.ToDo", "Title_Value", c => c.String());
            AddColumn("dbo.ToDo", "Description", c => c.String());
            AddColumn("dbo.ToDo", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDo", "CreatedBy", c => c.Guid(nullable: false));
            DropColumn("dbo.ToDo", "Id");
            AddColumn("dbo.ToDo", "Id", c => c.Guid(nullable: false, defaultValue: Guid.NewGuid()));
            AddPrimaryKey("dbo.ToDo", "Id");
            DropColumn("dbo.ToDo", "ToDoItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDo", "ToDoItem", c => c.String(nullable: false));
            DropPrimaryKey("dbo.ToDo");
            AlterColumn("dbo.ToDo", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ToDo", "CreatedBy");
            DropColumn("dbo.ToDo", "CreatedAt");
            DropColumn("dbo.ToDo", "Description");
            DropColumn("dbo.ToDo", "Title_Value");
            AddPrimaryKey("dbo.ToDo", "Id");
        }
    }
}
