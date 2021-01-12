namespace HMSv.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPictureJoiningTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureID = c.Int(nullable: false),
                        AccomodationPackageID = c.Int(nullable: false),
                        AccomodationPackages_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackages_ID)
                .Index(t => t.PictureID)
                .Index(t => t.AccomodationPackages_ID);
            
            CreateTable(
                "dbo.AccomodationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureID = c.Int(nullable: false),
                        AccomodationID = c.Int(nullable: false),
                        Accomodations_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.Accomodations_ID)
                .Index(t => t.PictureID)
                .Index(t => t.Accomodations_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccomodationPictures", "Accomodations_ID", "dbo.Accomodations");
            DropForeignKey("dbo.AccomodationPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.AccomodationPackagePictures", "AccomodationPackages_ID", "dbo.AccomodationPackages");
            DropForeignKey("dbo.AccomodationPackagePictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.AccomodationPictures", new[] { "Accomodations_ID" });
            DropIndex("dbo.AccomodationPictures", new[] { "PictureID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "AccomodationPackages_ID" });
            DropIndex("dbo.AccomodationPackagePictures", new[] { "PictureID" });
            DropTable("dbo.AccomodationPictures");
            DropTable("dbo.AccomodationPackagePictures");
        }
    }
}
