namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ImagePath", c => c.String(maxLength: 500));
            AddColumn("dbo.Games", "ImageMimeType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "ImageMimeType");
            DropColumn("dbo.Games", "ImagePath");
        }
    }
}
