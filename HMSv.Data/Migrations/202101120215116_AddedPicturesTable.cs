namespace HMSv.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPicturesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pictures");
        }
    }
}
