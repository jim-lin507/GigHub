namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Id,Name) values(0,'Select an option')");
            Sql("insert into Genres (Id,Name) values(1,'Jazz')");
            Sql("insert into Genres (Id,Name) values(2,'Rock')");
            Sql("insert into Genres (Id,Name) values(3,'Country')");
        }
        
        public override void Down()
        {
            Sql("delete from Genres where id in(0,1,2,3)");
        }
    }
}
