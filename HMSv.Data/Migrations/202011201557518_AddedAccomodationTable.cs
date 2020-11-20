namespace HMSv.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAccomodationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        AccomodationPackageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageID, cascadeDelete: true)
                .Index(t => t.AccomodationPackageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accomodations", "AccomodationPackageID", "dbo.AccomodationPackages");
            DropIndex("dbo.Accomodations", new[] { "AccomodationPackageID" });
            DropTable("dbo.Accomodations");
        }
    }
}
